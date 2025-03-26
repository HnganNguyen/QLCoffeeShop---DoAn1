using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAL
{
    public class TaiKhoanDAL
    {
        public static List<TaiKhoanDTO> GetAllTaiKhoan()
        {
            List<TaiKhoanDTO> taiKhoanList = new List<TaiKhoanDTO>();
            DataTable resuft = DataProvider.Instance.ExcuteQuery("Select * from TAIKHOAN");
            foreach (DataRow item in resuft.Rows)
            {
                TaiKhoanDTO taikhoan = new TaiKhoanDTO(item);
                taiKhoanList.Add(taikhoan);
            }
            return taiKhoanList;
        }
        public static bool AddTaiKhoan(TaiKhoanDTO tk)
        {
            string query = "INSERT INTO TAIKHOAN (MATAIKHOAN, TEN, PASS, CCCD, SODIENTHOAI, DIACHI, QUYEN, TRANGTHAI, LUONG) VALUES (@MATAIKHOAN,@TEN, @PASS, @CCCD, @SODIENTHOAI, @DIACHI, @QUYEN, @TRANGTHAI, @LUONG)";
            if (DataProvider.Instance.ExcuteNonQuery(query, new object[] { tk.ID,tk.TenTK, tk.Password,tk.CCCD,tk.SDT,tk.DiaChi,tk.Quyen,tk.TrangThai,tk.LuongByCa }) == 1)
            {
                return true;
            }
            return false;
        }
        public static bool UpdateTaiKhoan(TaiKhoanDTO tk)
        {
            string query = "UPDATE TAIKHOAN SET  TEN = @TEN, PASS = @PASS, CCCD = @CCCD, SODIENTHOAI = @SODIENTHOAI, DIACHI = @DIACHI, QUYEN = @QUYEN, TRANGTHAI = @TRANGTHAI, LUONG = @LUONG WHERE MATAIKHOAN = @MATAIKHOAN";
            if (DataProvider.Instance.ExcuteNonQuery(query, new object[]{ tk.TenTK, tk.Password, tk.CCCD, tk.SDT, tk.DiaChi, tk.Quyen, tk.TrangThai, tk.LuongByCa, tk.ID }) == 1)
            {
                return true;
            }
            return false;
        }
        public static bool DeleteTaiKhoan(int id)
        {
            string query = "DELETE FROM TAIKHOAN WHERE MATAIKHOAN = @MATAIKHOAN";
            if (DataProvider.Instance.ExcuteNonQuery(query, new object[] { id }) == 1)
            {
                return true;
            }
            return false;
        }
        public static List<TaiKhoanDTO> GetListAccountOnStatus(int status)
        {
            List<TaiKhoanDTO> listaccount = new List<TaiKhoanDTO>();
            string query = "SELECT * FROM TAIKHOAN WHERE QUYEN IN (0,1) AND TRANGTHAI = " + status;
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                TaiKhoanDTO account = new TaiKhoanDTO(item);
                listaccount.Add(account);
            }
            return listaccount;
        }
        public static bool DangNhap(int maTK, string password)
        {
            string query = "SELECT * FROM TAIKHOAN WHERE MATAIKHOAN = @user and PASS = @pass ";
            DataTable resuft = DataProvider.Instance.ExcuteQuery(query, new object[] { maTK, password });
            return resuft.Rows.Count > 0;
        }
        public static TaiKhoanDTO GetAccountByUsernameAndPassword(int username, string password)
        {
            string query = "SELECT * FROM TAIKHOAN WHERE MATAIKHOAN = @user and Pass = @pass ";
            DataTable resuft = DataProvider.Instance.ExcuteQuery(query, new object[] { username, password });
            if (resuft.Rows.Count > 0)
            {
                TaiKhoanDTO account = new TaiKhoanDTO(resuft.Rows[0]);
                return account;
            }
            return null;
        }
        public static List<TaiKhoanDTO> SearchTaiKhoanByName(string keyword)
        {
            List<TaiKhoanDTO> list = new List<TaiKhoanDTO>();
            string query = "SELECT * FROM TAIKHOAN WHERE TEN LIKE @keyword";

            DataTable data = DataProvider.Instance.ExcuteQuery(query, new object[] { "%" + keyword + "%" });

            foreach (DataRow row in data.Rows)
            {
                TaiKhoanDTO taikhoan = new TaiKhoanDTO(
                    Convert.ToInt32(row["MATAIKHOAN"]),
                    row["TEN"].ToString(),
                    row["PASS"].ToString(),
                    row["CCCD"].ToString(),
                    row["SODIENTHOAI"].ToString(),
                    row["DIACHI"].ToString(),
                    Convert.ToInt32(row["QUYEN"]),
                    Convert.ToInt32(row["TRANGTHAI"]),
                    Convert.ToDouble(row["LUONG"])
                );
                list.Add(taikhoan);
            }

            return list;
        }
        public static TaiKhoanDTO GetTaiKhoanById(int id)
        {
            string query = "SELECT * FROM TAIKHOAN WHERE MATAIKHOAN = @id";
            DataTable resuft = DataProvider.Instance.ExcuteQuery(query, new object[] { id });
            if (resuft.Rows.Count > 0)
            {
                TaiKhoanDTO account = new TaiKhoanDTO(resuft.Rows[0]);
                return account;
            }
            return null;
        }

        public static List<TaiKhoanDTO> LayDanhSachNhanVien()
        {
            List<TaiKhoanDTO> dsNhanVien = new List<TaiKhoanDTO>();
            string query = "SELECT MATAIKHOAN, TEN FROM TAIKHOAN";  // Kiểm tra tên cột trong CSDL

            DataTable dt = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                dsNhanVien.Add(new TaiKhoanDTO
                {
                    ID = Convert.ToInt32(row["MATAIKHOAN"]), // Đảm bảo đúng cột
                    TenTK = row["TEN"].ToString()
                });
            }
            return dsNhanVien;
        }
    }

}