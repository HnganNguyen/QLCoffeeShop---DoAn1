using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class NguyenLieuDAL
    {
        public static List<NguyenLieuDTO> GetAllNguyenLieu()
        {
            List<NguyenLieuDTO> list = new List<NguyenLieuDTO>();
            string query = "SELECT * FROM HANGTONKHO";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                NguyenLieuDTO nl = new NguyenLieuDTO(
                    Convert.ToInt32(row["MA"]),
                    row["TEN"].ToString(),
                    Convert.ToDateTime(row["NGAYTAO"]),
                    Convert.ToDouble(row["GIAGOC"]),
                    row["GHICHU"]?.ToString() ?? "",
                    Convert.ToInt32(row["MATAIKHOAN"]) // Thêm lấy MATAIKHOAN
                );
                list.Add(nl);
            }
            return list;
        }

        public static bool InsertNguyenLieu(NguyenLieuDTO nl)
        {
            string query = "INSERT INTO HANGTONKHO (TEN, NGAYTAO, GIAGOC, GHICHU, MATAIKHOAN) " +
                   "VALUES (@Ten, @NgayTao, @GiaGoc, @GhiChu, @MaTaiKhoan)";

            object[] parameters = { nl.Ten, nl.NgayTao, nl.GiaGoc, nl.GhiChu ?? (object)DBNull.Value, nl.MaTaiKhoan };

            return DataProvider.Instance.ExcuteNonQuery(query, parameters) > 0;
        }

        public static void UpdateNguyenLieu(NguyenLieuDTO nl)
        {
            string query = "UPDATE HANGTONKHO SET TEN = @TEN, GIAGOC = @GIAGOC, GHICHU = @GHICHU WHERE MA = @MA";

            // Đảm bảo đúng thứ tự tham số
            object[] parameters = { nl.Ten, (float)nl.GiaGoc, nl.GhiChu, nl.Ma };

            DataProvider.Instance.ExcuteNonQuery(query, parameters);
        }


        public static void DeleteNguyenLieu(int ma)
        {
            string query = "DELETE FROM HANGTONKHO WHERE MA = @MA";

            object[] parameters = { ma };

            DataProvider.Instance.ExcuteNonQuery(query, parameters);
        }

    }
}
