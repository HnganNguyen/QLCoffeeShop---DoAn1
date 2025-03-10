using System;
using System.Collections.Generic;
using System.Data;

using DTO;
namespace DAL
{
    public class MenuDAL
    {
        public static List<MenuDTO> GetListMenuByIDTable(int id)
        {
            List<MenuDTO> listmenu = new List<MenuDTO>();
            string query = "SELECT DE.MASANPHAM, D.MA, D.TENSANPHAM, DE.SOLUONG, D.GIACOBAN, DE.SOLUONG*D.GIACOBAN AS TONGTIEN, TRANGTHAI = 0 FROM HOADON AS BI, CHITIETHOADON AS DE, SANPHAM AS D WHERE DE.MAHOADON = BI.MA AND DE.MASANPHAM = D.MA AND BI.TRANGTHAI = 0 AND BI.MABAN = " + id;//0 chưa thanh toán / 1 đã thanh toán rồi.
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                MenuDTO menu = new MenuDTO(item);
                listmenu.Add(menu);
            }
            return listmenu;
        }
        public static List<MenuDTO> GetListMenuByIDBill(int idBill)
        {
            List<MenuDTO> listmenu = new List<MenuDTO>();
            string query = "SELECT DE.MASANPHAM, D.MA, D.TENSANPHAM, DE.SOLUONG, D.GIACOBAN, DE.SOLUONG*D.GIACOBAN AS TONGTIEN, TRANGTHAI = 0 FROM HOADON AS BI, CHITIETHOADON AS DE, SANPHAM AS D WHERE DE.MAHOADON = BI.MA AND DE.MASANPHAM = D.MA AND BI.TRANGTHAI = 0 AND DE.MAHOADON = " + idBill;//0 chưa thanh toán / 1 đã thanh toán rồi.
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                MenuDTO menu = new MenuDTO(item);
                listmenu.Add(menu);
            }
            return listmenu;
        }
        public static List<MenuDTO> GetReviewBill(int idBill)
        {
            List<MenuDTO> listmenu = new List<MenuDTO>();
            string query = "SELECT DE.MASANPHAM, D.MA, D.TENSANPHAM, DE.SOLUONG, D.GIACOBAN, DE.SOLUONG*D.GIACOBAN AS TONGTIEN, TRANGTHAI = 0 FROM HOADON AS BI, CHITIETHOADON AS DE, SANPHAM AS D WHERE DE.MAHOADON = BI.MA AND DE.MASANPHAM = D.MA AND BI.TRANGTHAI = 1 AND DE.MAHOADON =  " + idBill;//0 chưa thanh toán / 1 đã thanh toán rồi.
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                MenuDTO menu = new MenuDTO(item);
                listmenu.Add(menu);
            }
            return listmenu;
        }
    }
}
