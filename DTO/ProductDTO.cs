using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ProductDTO
    {
        public ProductDTO()
        {

        }
        public ProductDTO(int ID, string NameProducts, double PriceBasic, double SalePrice, int Status, int IDTypeProduct)
        {
            this.ID = ID;
            this.NameProducts = NameProducts;
            this.PriceBasic = PriceBasic;
            this.SalePrice = SalePrice;
            this.Status = Status;
            this.IDTypeProduct = IDTypeProduct;
        }

        public ProductDTO(DataRow row)
        {
            ID = Convert.ToInt32(row["ma"]);
            NameProducts = row["tensanpham"].ToString();
            PriceBasic = Convert.ToDouble(row["giacoban"]);

            // Kiểm tra NULL trước khi ép kiểu
            SalePrice = row["giakhuyenmai"] != DBNull.Value ? Convert.ToDouble(row["giakhuyenmai"]) : 0;

            Status = Convert.ToInt32(row["trangthai"]);
            IDTypeProduct = Convert.ToInt32(row["maloaisanpham"]);
        }
        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        private string nameProducts;

        public string NameProducts
        {
            get { return nameProducts; }
            set { nameProducts = value; }
        }
        private double priceBasic;

        public double PriceBasic
        {
            get { return priceBasic; }
            set { priceBasic = value; }
        }
        private double salePrice;

        public double SalePrice
        {
            get { return salePrice; }
            set { salePrice = value; }
        }
        private int status;

        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        private int iDTypeProduct;

        public int IDTypeProduct
        {
            get { return iDTypeProduct; }
            set { iDTypeProduct = value; }
        }
    }
}
