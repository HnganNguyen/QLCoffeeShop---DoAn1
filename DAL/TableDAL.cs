using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DTO;
namespace DAL
{
    public class TableDAL
    {
        public static List<TableDTO> GetAllListTable()
        {
            List<TableDTO> tableList = new List<TableDTO>();
            DataTable resuft = DataProvider.Instance.ExcuteQuery("Select * from BAN");
            foreach (DataRow item in resuft.Rows)
            {
                TableDTO table = new TableDTO(item);
                tableList.Add(table);
            }
            return tableList;
        }
        public static void UpdateStatusTable(int status, int id)
        {
            string query = "USP_UPDATETRANGTHAITABLE  @status , @idtable ";
            DataProvider.Instance.ExcuteNonQuery(query, new object[] { id, status });

        }
        public static int GetStatusByIDTable(int id)
        {
            DataTable data = DataProvider.Instance.ExcuteQuery("Select * from BAN as t where ma = " + id);
            if (data.Rows.Count > 0)
            {
                TableDTO table = new TableDTO(data.Rows[0]);
                return table.Status;
            }
            return -1;
        }
        public static List<TableDTO> GetListTableHaveStatusOne()
        {
            List<TableDTO> tablelist = new List<TableDTO>();
            DataTable data = DataProvider.Instance.ExcuteQuery("Select * from ban where trangthai = 1");
            foreach (DataRow item in data.Rows)
            {
                TableDTO table = new TableDTO(item);
                tablelist.Add(table);
            }
            return tablelist;
        }

        public static List<TableDTO> GetListTableDifferentID(int id)
        {
            List<TableDTO> tableList = new List<TableDTO>();
            DataTable resuft = DataProvider.Instance.ExcuteQuery("Select * from ban where ma != @id ", new object[] { id });
            foreach (DataRow item in resuft.Rows)
            {
                TableDTO table = new TableDTO(item);
                tableList.Add(table);
            }
            return tableList;
        }

        public static bool InsertTable(TableDTO tb)
        {
            string query = "Exec USP_INSERTTABLE @nametable , @status ";
            if (DataProvider.Instance.ExcuteNonQuery(query, new object[] { tb.NameTable, tb.Status }) == 1)
            {
                return true;
            }
            return false;
        }
        public static bool DeleteTable(TableDTO tb)
        {
            string query = "Exec USP_DELETETABLE @idtable";
            if (DataProvider.Instance.ExcuteNonQuery(query, new object[] { tb.ID }) == 1)
            {
                return true;
            }
            return false;
        }
        public static bool IsTableExists(int idTable)
        {
            string query = "SELECT COUNT(*) FROM BAN WHERE MA = @idTable";
            object result = DataProvider.Instance.ExcuteScalar(query, new object[] { idTable });
            return Convert.ToInt32(result) > 0;
        }

    }
}
