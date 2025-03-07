using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAL
{
    public class BillDAL
    {
        public static List<BillDTO> GetAllListBill()
        {
            List<BillDTO> lstBill = new List<BillDTO>();
            string query = "SELECT * from HOADON WHERE TRANGTHAI = 1";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                BillDTO detail = new BillDTO(item);
                lstBill.Add(detail);
            }
            return lstBill;
        }
        public static List<BillUpDTO> GetAllListBillup()
        {
            List<BillUpDTO> lstBill = new List<BillUpDTO>();
            string query = "Exec USP_GetBillup";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                BillUpDTO detail = new BillUpDTO(item);
                lstBill.Add(detail);
            }
            return lstBill;
        }

        public static int GetIDBillNoPaymentByIDTable(int idTable)
        {
            DataTable data = DataProvider.Instance.ExcuteQuery("Select * from HOADON where MABAN = " + idTable + " and TRANGTHAI = 0");//chưa thanh toán
            if (data.Rows.Count > 0)
            {
                BillDTO bill = new BillDTO(data.Rows[0]);
                return bill.ID;
            }
            return -1;
        }
        public static int GetIDBillMax()
        {
            string re = DataProvider.Instance.ExcuteScalar("SELECT MAX(MA) FROM HOADON").ToString();
            if (re != "")
                return Convert.ToInt32(re);
            return 1;
        }
        public static void UpdatetBill(int id, double totalbill, double promotion, double cusPrice, double outPrice, double revenue, DateTime datetime, int employ)
        {
            string query = "Exec USP_UpdateBill @IDBILL , @TOTALBILL , @DATETIME , @EMPLOY , @PROMOTIONPRICE , @CUSTOMERPRICE , @OUTPRICE , @REVENUE ";

            DataProvider.Instance.ExcuteNonQuery(query, new object[] { id, totalbill, datetime, employ, promotion, cusPrice, outPrice, revenue });
        }
        public static void InsertBill(DateTime ThoiGian, double TongTien, int Employ, int idTable)
        {
            if (!TableDAL.IsTableExists(idTable))
            {
                throw new Exception("Table ID does not exist.");
            }

            string query = "Exec USP_InsertBILL @datetime , @tongtien , @manhanvien , @idtable ";
            DataProvider.Instance.ExcuteNonQuery(query, new object[] { ThoiGian, TongTien, Employ, idTable });
        }


        }
}
