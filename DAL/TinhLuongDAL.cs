using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class TinhLuongDAL
    {
        public List<TinhLuongDTO> GetAllTinhLuong()
        {
            List<TinhLuongDTO> danhSach = new List<TinhLuongDTO>();
            string query = "SELECT * FROM TINHLUONGNHANVIEN";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow row in data.Rows)
            {
                TinhLuongDTO tl = new TinhLuongDTO(
                    Convert.ToInt32(row["MALUONG"]),
                    Convert.ToInt32(row["THANG"]),
                    Convert.ToInt32(row["NAM"]),
                    Convert.ToDateTime(row["NGAYTAO"]),
                    Convert.ToInt32(row["CA"]),
                    Convert.ToSingle(row["TONG"]),
                    row["GHICHU"].ToString(),
                    Convert.ToInt32(row["TINHTRANG"]),
                    Convert.ToInt32(row["MATAIKHOAN"])
                );
                danhSach.Add(tl);
            }
            return danhSach;
        }

        public bool InsertTinhLuong(TinhLuongDTO tinhLuong)
        {
            string query = "INSERT INTO TINHLUONGNHANVIEN (THANG, NAM, NGAYTAO, CA, TONG, GHICHU, TINHTRANG, MATAIKHOAN) " +
                           "VALUES (@Thang, @Nam, @NgayTao, @Ca, @Tong, @GhiChu, @TinhTrang, @MaTaiKhoan)";
            return DataProvider.Instance.ExcuteNonQuery(query, new object[]
            {tinhLuong.Thang, tinhLuong.Nam, tinhLuong.NgayTao, tinhLuong.Ca,
            tinhLuong.Tong, tinhLuong.GhiChu, tinhLuong.TinhTrang, tinhLuong.MaTaiKhoan
            }) > 0;
        }

        public string LayTenNhanVien(int maTaiKhoan)
        {
            string query = "SELECT TEN FROM TAIKHOAN WHERE MATAIKHOAN = @MaTaiKhoan";
            DataTable dt = DataProvider.Instance.ExcuteQuery(query, new object[] { maTaiKhoan });
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["TEN"].ToString();

            return "";
        }

        public string LaySDTNhanVien(int maTaiKhoan)
        {
            string query = "SELECT SODIENTHOAI FROM TAIKHOAN WHERE MATAIKHOAN = @MaTaiKhoan";
            DataTable dt = DataProvider.Instance.ExcuteQuery(query, new object[] { maTaiKhoan });
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["SODIENTHOAI"].ToString();

            return "";
        }

        public string LayDiaChiNhanVien(int maTaiKhoan)
        {
            string query = "SELECT DIACHI FROM TAIKHOAN WHERE MATAIKHOAN = @MaTaiKhoan";
            DataTable dt = DataProvider.Instance.ExcuteQuery(query, new object[] { maTaiKhoan });
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["DIACHI"].ToString();

            return "";
        }

        public float LayLuongCoBan(int maTaiKhoan)
        {
            string query = "SELECT LUONG FROM TAIKHOAN WHERE MATAIKHOAN = @MaTaiKhoan";
            DataTable dt = DataProvider.Instance.ExcuteQuery(query, new object[] { maTaiKhoan });

            if (dt.Rows.Count > 0)
                return float.Parse(dt.Rows[0]["LUONG"].ToString());
            return 0;
        }

        public bool ThanhToanLuong(int maTaiKhoan, int thang, int nam)
        {
            string query = "UPDATE TINHLUONGNHANVIEN SET TINHTRANG = 1 WHERE MATAIKHOAN = @maTaiKhoan AND THANG = @thang AND NAM = @nam";
            object[] parameters = new object[] { maTaiKhoan, thang, nam };
            int result = DataProvider.Instance.ExcuteNonQuery(query, parameters);
            return result > 0;
        }
    }
}
