using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
namespace BLL
{
    public class ProductBLL
    {
        public static List<ProductDTO> GetSanPhambyIDLoaiSP(int id, int status)
        {
            return ProductDAL.GetSanPhambyIDLoaiSP(id, status);
        }
        public static List<ProductDTO> GetListProductByID(int id)
        {
            return ProductDAL.GetListProductByID(id);
        }
        public static int GetMaLoaibySP(int id)
        {
            return ProductDAL.GetMaLoaibySP(id);
        }
      
        public static List<ProductDTO> GetAllListProduct()
        {
            return ProductDAL.GetAllListProduct();
        }

        //them vo
        public static bool AddProduct(ProductDTO product)
        {
            return ProductDAL.AddProduct(product);
        }

        public static bool UpdateProduct(ProductDTO product)
        {
            return ProductDAL.UpdateProduct(product);
        }

        public static bool DeleteProduct(int id)
        {
            return ProductDAL.DeleteProduct(id);
        }

        public static List<ProductDTO> SearchProductByName(string keyword)//tìm
        {
            return ProductDAL.SearchProductByName(keyword);
        }
    }
}
