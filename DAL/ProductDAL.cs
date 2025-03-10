using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAL
{
    public class ProductDAL
    {

        public static List<ProductDTO> GetSanPhambyIDLoaiSP(int id, int status) //get sản phẩm theo trạng thái mà mình đã cài đặt trong admin
        {
            List<ProductDTO> listProduct = new List<ProductDTO>();
            string query;
            if (id == 0)
                query = "SELECT TOP 10 * FROM SANPHAM WHERE TRANGTHAI = " + status + " AND SANPHAM.MALOAISANPHAM NOT IN (SELECT MA FROM LOAISANPHAM WHERE TRANGTHAI = 0)";
            else
            {
                if (status == -1)
                    query = "select * from SANPHAM where MALOAISANPHAM = " + id;
                else
                    query = "select * from SANPHAM where MALOAISANPHAM = " + id + " and TRANGTHAI = " + status;
            }
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                ProductDTO Product = new ProductDTO(item);
                listProduct.Add(Product);
            }
            return listProduct;
        }
        public static int GetMaLoaibySP(int id)//lấy mã loại sản phẩm từ dbo.sản phẩm
        {
            DataTable data = DataProvider.Instance.ExcuteQuery("Select d.MALOAISANPHAM from dbo.SANPHAM as d where MALOAISANPHAM = " + id);//chưa thanh toán
            if (data.Rows.Count > 0)
            {
                return Convert.ToInt32(data.Rows[0]["MALOAISANPHAM"].ToString());
            }
            return -1;
        }
        public static List<ProductDTO> GetAllListProduct() //lấy tất cả sản phẩm
        {
            List<ProductDTO> listProduct = new List<ProductDTO>();
            string query = "select * from SANPHAM";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                ProductDTO Product = new ProductDTO(item);
                listProduct.Add(Product);
            }
            return listProduct;
        }
        public static List<ProductDTO> GetListProductByID(int id) //lấy mã sản phẩm theo sản phẩm để Tìm kiếm
        {
            List<ProductDTO> listProduct = new List<ProductDTO>();
            string query = "select * from SANPHAM where MA = " + id;
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                ProductDTO Product = new ProductDTO(item);
                listProduct.Add(Product);
            }
            return listProduct;
        }

        //them vo
        public static bool AddProduct(ProductDTO product)
        {
            string query = "INSERT INTO SANPHAM (TENSANPHAM, GIACOBAN, GIAKHUYENMAI, MALOAISANPHAM, TRANGTHAI) " +
                           "VALUES (@name, @priceBasic, @salePrice, @typeID, @status)";
            object[] parameters = { product.NameProducts, product.PriceBasic, product.SalePrice, product.IDTypeProduct, product.Status };
            return DataProvider.Instance.ExcuteNonQuery(query, parameters) > 0;
        }

        public static bool UpdateProduct(ProductDTO product)
        {
            string query = "UPDATE SANPHAM SET TENSANPHAM = @Ten, GIACOBAN = @GiaCoBan, " +
                   "GIAKHUYENMAI = @GiaKhuyenMai, TRANGTHAI = @TrangThai, MALOAISANPHAM = @LoaiSP " +"WHERE MA = @Ma";
            object[] parameters = new object[]
            {
                product.NameProducts,
                product.SalePrice,  
                product.SalePrice,
                product.Status,
                product.IDTypeProduct,
                product.ID
            };
            return DataProvider.Instance.ExcuteNonQuery(query, parameters) > 0;
        }

        public static bool DeleteProduct(int id)
        {
            string query = "DELETE FROM SANPHAM WHERE MA = @id";
            object[] parameters = { id };
            return DataProvider.Instance.ExcuteNonQuery(query, parameters) > 0;
        }

        //của tìm kiếm sp
        public static List<ProductDTO> SearchProductByName(string keyword)
        {
            List<ProductDTO> list = new List<ProductDTO>();
            string query = "SELECT * FROM SANPHAM WHERE TENSANPHAM LIKE @keyword";

            DataTable data = DataProvider.Instance.ExcuteQuery(query, new object[] { "%" + keyword + "%" });

            foreach (DataRow row in data.Rows)
            {
                ProductDTO product = new ProductDTO(
                    Convert.ToInt32(row["MA"]),
                    row["TENSANPHAM"].ToString(),
                    Convert.ToDouble(row["GIACOBAN"]),
                    Convert.ToDouble(row["GIAKHUYENMAI"]),
                    Convert.ToInt32(row["TRANGTHAI"]),
                    Convert.ToInt32(row["MALOAISANPHAM"])
                );
                list.Add(product);
            }

            return list;
        }

    }
}