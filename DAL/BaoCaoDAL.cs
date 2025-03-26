using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class BaoCaoDAL
    {
        public BaoCaoDAL() { }
        public List<BaoCaoDTO> GetBaoCao(int thang, int nam)
        {
            List<BaoCaoDTO> list = new List<BaoCaoDTO>();

            string query = @"SELECT @Thang AS Thang, @Nam AS Nam,
                     (SELECT COALESCE(SUM(TONGTIEN), 0) FROM HOADON 
                      WHERE MONTH(NGAYTAO) = @Thang AND YEAR(NGAYTAO) = @Nam) AS TongTienBan,
                     (SELECT COALESCE(SUM(GIAGOC), 0) FROM HANGTONKHO 
                      WHERE MONTH(NGAYTAO) = @Thang AND YEAR(NGAYTAO) = @Nam) AS TongNguyenVatLieu,
                     (SELECT COALESCE(SUM(TONG), 0) FROM TINHLUONGNHANVIEN 
                      WHERE TINHTRANG = 1 AND MONTH(NGAYTAO) = @Thang AND YEAR(NGAYTAO) = @Nam) AS TongLuongNhanVien";

            DataTable data = DataProvider.Instance.ExcuteQuery(query, new object[] { thang, nam });

            foreach (DataRow row in data.Rows)
            {
                BaoCaoDTO report = new BaoCaoDTO()
                {
                    Thang = Convert.ToInt32(row["Thang"]),
                    Nam = Convert.ToInt32(row["Nam"]),
                    TongTienBan = row["TongTienBan"] != DBNull.Value ? Convert.ToSingle(row["TongTienBan"]) : 0,
                    TongNguyenVatLieu = row["TongNguyenVatLieu"] != DBNull.Value ? Convert.ToSingle(row["TongNguyenVatLieu"]) : 0,
                    TongLuongNhanVien = row["TongLuongNhanVien"] != DBNull.Value ? Convert.ToSingle(row["TongLuongNhanVien"]) : 0,
                };
                report.TongDoanhThuThang = report.TongTienBan - (report.TongNguyenVatLieu + report.TongLuongNhanVien);
                list.Add(report);
            }

            return list;
        }

        public float GetTongDoanhThuNam(int nam)
        {
            string query = @"
        SELECT 
            COALESCE(SUM(TONGTIEN), 0) 
            - (COALESCE((SELECT SUM(GIAGOC) FROM HANGTONKHO WHERE YEAR(NGAYTAO) = @Nam), 0) 
            + COALESCE((SELECT SUM(TONG) FROM TINHLUONGNHANVIEN WHERE TINHTRANG = 1 AND YEAR(NGAYTAO) = @Nam), 0))
        AS TongDoanhThuNam
        FROM HOADON 
        WHERE YEAR(NGAYTAO) = @Nam";

            // 🟢 Cách truyền tham số đúng
            DataTable data = DataProvider.Instance.ExcuteQuery(query, new object[] { nam });

            if (data.Rows.Count > 0 && data.Rows[0][0] != DBNull.Value)
            {
                return Convert.ToSingle(data.Rows[0][0]);
            }
            return 0;
        }


    }
}