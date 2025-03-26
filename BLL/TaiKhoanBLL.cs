using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
namespace BLL
{
    public class TaiKhoanBLL
    {
        public static List<TaiKhoanDTO> GetAllTaiKhoan()
        {
            return TaiKhoanDAL.GetAllTaiKhoan();
        }
        public static bool AddTaiKhoan(TaiKhoanDTO tk)
        {
            return TaiKhoanDAL.AddTaiKhoan(tk);
        }
        public static bool UpdateTaiKhoan(TaiKhoanDTO tk)
        {
            return TaiKhoanDAL.UpdateTaiKhoan(tk);
        }
        public static bool DeleteTaiKhoan(int id)
        {
            return TaiKhoanDAL.DeleteTaiKhoan(id);
        }
        public static List<TaiKhoanDTO> GetListAccountOnStatus(int status)
        {
            return TaiKhoanDAL.GetListAccountOnStatus(status);
        }
        public static TaiKhoanDTO DangNhap(int maTK, string password)
        {
            TaiKhoanDTO account = TaiKhoanDAL.GetAccountByUsernameAndPassword(maTK, password);
            return account;
        }

        //public static TaiKhoanDTO GetName(int username, string password)
        //{
        //    // Logic để kiểm tra đăng nhập và trả về đối tượng TaiKhoanDTO nếu thành công, ngược lại trả về null
        //    TaiKhoanDTO account = TaiKhoanDAL.GetAccountByUsernameAndPassword(username, password);
        //    return account;
        //}
        public static List<TaiKhoanDTO> SearchTaiKhoanByName(string keyword)
        {
            return TaiKhoanDAL.SearchTaiKhoanByName(keyword);
        }
        public static TaiKhoanDTO GetTaiKhoanById(int id)
        {
            return TaiKhoanDAL.GetTaiKhoanById(id);
        }
        public static List<TaiKhoanDTO> LayDanhSachNhanVien()
        {
            return TaiKhoanDAL.LayDanhSachNhanVien();  // Gọi từ DAL
        }
    }
}
