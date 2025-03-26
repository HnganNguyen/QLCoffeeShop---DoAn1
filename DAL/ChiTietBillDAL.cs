using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAL
{
    public class ChiTietBillDAL
    {
        public static bool CheckSPcoTrongBill(int id) // kiểm tra sản phẩm có trong bill hay không
        {
            DataTable data = DataProvider.Instance.ExcuteQuery("SELECT * FROM CHITIETHOADON WHERE MAHOADON = " + id);
            if (data.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        public static void InsertChiTietBill(int mabill, int idProduct, int soLuong)
        {
            DataProvider.Instance.ExcuteNonQuery("EXEC USP_INSERTCHITIETBILL  @idbill , @idProduct , @soluong", new object[] { mabill, idProduct, soLuong });
        }
        public static int GetSoLuongSanPham(int idbill, int idProduct)
        {
            DataTable data = DataProvider.Instance.ExcuteQuery("SELECT [MAHOADON], [MASANPHAM], [SOLUONG]  FROM DBO.CHITIETHOADON AS DE WHERE MAHOADON = " + idbill + " and MASANPHAM = " + idProduct);
            if (data.Rows.Count > 0)
            {
                ChiTietBillDTO debill = new ChiTietBillDTO(data.Rows[0]);
                return debill.SoLuong;
            }
            return 0;
        }
        public static void DeleteChiTietBill(int idbill, int idProduct)
        {

            DataProvider.Instance.ExcuteNonQuery("DELETE FROM CHITIETHOADON WHERE MAHOADON = @idbill AND MASANPHAM = @idProduct", new object[] { idbill, idProduct });
        }
    }
}
