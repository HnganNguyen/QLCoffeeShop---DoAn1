using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class TinhLuongBLL
    {
        private TinhLuongDAL tinhLuongDAL = new TinhLuongDAL();

        public List<TinhLuongDTO> GetAllTinhLuong()
        {
            return tinhLuongDAL.GetAllTinhLuong();
        }

        public bool ThemLuong(TinhLuongDTO tinhLuong)
        {
            return tinhLuongDAL.InsertTinhLuong(tinhLuong);
        }

        public bool ThanhToanLuong(int maTaiKhoan, int thang, int nam)
        {
            return tinhLuongDAL.ThanhToanLuong(maTaiKhoan, thang, nam);
        }

        private TinhLuongDAL TinhLuongDAL = new TinhLuongDAL();

        public string GetTenNhanVien(int maTaiKhoan)
        {
            return tinhLuongDAL.LayTenNhanVien(maTaiKhoan);
        }

        public string GetSDTNhanVien(int maTaiKhoan)
        {
            return tinhLuongDAL.LaySDTNhanVien(maTaiKhoan);
        }

        public string GetDiaChiNhanVien(int maTaiKhoan)
        {
            return tinhLuongDAL.LayDiaChiNhanVien(maTaiKhoan);
        }

        public float GetLuongCoBan(int maTaiKhoan)
        {
            return tinhLuongDAL.LayLuongCoBan(maTaiKhoan);
        }
    }
}
