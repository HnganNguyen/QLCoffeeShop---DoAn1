using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
namespace BLL
{
    public class BillBLL
    {
        public static List<BillDTO> GetAllListBill()
        {
            return BillDAL.GetAllListBill();
        }
        public static List<BillUpDTO> GetAllListBillup()
        {
            return BillDAL.GetAllListBillup();
        }

        public static int GetIDBillNoPaymentByIDTable(int id)
        {
            return BillDAL.GetIDBillNoPaymentByIDTable(id);
        }
        public static int GetIDBillMax()
        {
            return BillDAL.GetIDBillMax();
        }
        public static void InsertBill(DateTime ThoiGian, double TongTien, int Employ, int idTable)
        {
            BillDAL.InsertBill(ThoiGian, TongTien, Employ, idTable);
        }
        public static void UpdatetBill(int id, double totalbill, double promotion, double cusPrice, double outPrice, double revenue, DateTime datetime, int employ)
        {
            BillDAL.UpdatetBill(id, totalbill, promotion, cusPrice, outPrice, revenue, datetime, employ);
        }
    }
}