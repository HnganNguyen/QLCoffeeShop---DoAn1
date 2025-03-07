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
                    Convert.ToSingle(row["GIAGOC"]),
                    row["GHICHU"]?.ToString() ?? ""
                );
                list.Add(nl);
            }
            return list;
        }

        public static void InsertNguyenLieu(NguyenLieuDTO nl)
        {
            string query = "INSERT INTO HANGTONKHO (TEN, GIAGOC, GHICHU) VALUES (@TEN, @GIAGOC, @GHICHU)";

            // Đảm bảo đúng thứ tự tham số truyền vào
            object[] parameters = { nl.Ten, (float)nl.GiaGoc, nl.GhiChu };

            DataProvider.Instance.ExcuteNonQuery(query, parameters);
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
