using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
namespace BLL
{
    public class ChiTietBillBLL
    {
        public static void InsertChiTietBill(int mabill, int idProduct, int soLuong)
        {
             ChiTietBillDAL.InsertChiTietBill(mabill, idProduct, soLuong);
        }
        public static bool CheckSPcoTrongBill(int id)
        {
           return  ChiTietBillDAL.CheckSPcoTrongBill(id);
        }
        public static int GetSoLuongSanPham(int idbill, int idProduct)
        {
            return ChiTietBillDAL.GetSoLuongSanPham(idbill, idProduct);
        }
        public static void DeleteChiTietBill(int idbill, int idProduct)
        {
            ChiTietBillDAL.DeleteChiTietBill(idbill, idProduct);
        }
    }
}
