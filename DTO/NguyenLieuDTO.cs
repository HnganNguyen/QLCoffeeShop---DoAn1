using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NguyenLieuDTO
    {
        public int Ma { get; set; }
        public string Ten { get; set; }
        public DateTime NgayTao { get; set; }
        public double GiaGoc { get; set; }
        public string GhiChu { get; set; }
        public int MaTaiKhoan { get; set; }

        public NguyenLieuDTO(int ma, string ten, DateTime ngayTao, double giaGoc, string ghiChu, int maTaiKhoan)
        {
            Ma = ma;
            Ten = ten;
            NgayTao = ngayTao;
            GiaGoc = giaGoc;
            GhiChu = ghiChu;
            MaTaiKhoan = maTaiKhoan;
        }

        public NguyenLieuDTO() { }
    }
}
