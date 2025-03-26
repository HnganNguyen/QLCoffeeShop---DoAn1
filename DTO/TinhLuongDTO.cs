using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TinhLuongDTO
    {
        public int MaLuong { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public DateTime NgayTao { get; set; }
        public int Ca { get; set; }
        public float Tong { get; set; }
        public string GhiChu { get; set; }
        public int TinhTrang { get; set; } 
        public int MaTaiKhoan { get; set; }

        public TinhLuongDTO() { }

        public TinhLuongDTO(int maLuong, int thang, int nam, DateTime ngayTao, int ca, float tong, string ghiChu, int tinhTrang, int maTaiKhoan)
        {
            MaLuong = maLuong;
            Thang = thang;
            Nam = nam;
            NgayTao = ngayTao;
            Ca = ca;
            Tong = tong;
            GhiChu = ghiChu;
            TinhTrang = tinhTrang;
            MaTaiKhoan = maTaiKhoan;
        }
    }

}
