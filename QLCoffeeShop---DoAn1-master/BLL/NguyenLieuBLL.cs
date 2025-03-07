using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL;
using DTO;

namespace BLL
{
    public class NguyenLieuBLL
    {
        public static List<NguyenLieuDTO> GetAllNguyenLieu()
        {
            return NguyenLieuDAL.GetAllNguyenLieu();
        }

        public static List<NguyenLieuDTO> InsertNguyenLieu(NguyenLieuDTO nl)
        {
            NguyenLieuDAL.InsertNguyenLieu(nl);
            return GetAllNguyenLieu(); // Trả về danh sách mới sau khi thêm
        }

        public static List<NguyenLieuDTO> UpdateNguyenLieu(NguyenLieuDTO nl)
        {
            NguyenLieuDAL.UpdateNguyenLieu(nl);
            return GetAllNguyenLieu(); // Trả về danh sách mới sau khi cập nhật
        }

        public static List<NguyenLieuDTO> DeleteNguyenLieu(int ma)
        {
            NguyenLieuDAL.DeleteNguyenLieu(ma);
            return GetAllNguyenLieu(); // Trả về danh sách mới sau khi xóa
        }
    }
}
