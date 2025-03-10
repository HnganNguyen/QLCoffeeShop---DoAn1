using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
namespace DTO
{
    public class MenuDTO
    {
        public MenuDTO(int ID, string NameProduct, int Quantity, double PriceBasic, int IDProduct, double TotalPrice = 0)
        {
            this.ID = ID;
            this.TenSP = NameProduct;
            this.SoLuong = Quantity;
            this.Gia = PriceBasic;
            this.TongTien = TotalPrice;
            this.MaSP = IDProduct;

        }

        public MenuDTO(DataRow row)
        {
            ID = (int)row["ma"];
            NameProduct = row["tensanpham"].ToString();
            Quantity = (int)row["soluong"];
            PriceBasic = (double)row["giacoban"];
            TotalPrice = (double)row["TONGTIEN"];
            IdProduct = (int)row["masanpham"];
        }

        private int MaSP;

        public int IdProduct
        {
            get { return MaSP; }
            set { MaSP = value; }
        }
        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private string TenSP;

        public string NameProduct
        {
            get { return TenSP; }
            set { TenSP = value; }
        }
        private int SoLuong;

        public int Quantity
        {
            get { return SoLuong; }
            set { SoLuong = value; }
        }
        private double Gia;

        public double PriceBasic
        {
            get { return Gia; }
            set { Gia = value; }
        }
        private double TongTien;

        public double TotalPrice
        {
            get { return TongTien; }
            set { TongTien = value; }
        }
    }
}

   