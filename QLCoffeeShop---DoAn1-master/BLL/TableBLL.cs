using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
namespace BLL
{
    public class TableBLL
    {
        public static List<TableDTO> GetAllListTable()
        {
            return TableDAL.GetAllListTable();
        }
      
          
            public static int TabWidth = 110;
            public static int TabHeight = 90;
        
        public static void UpdateStatusTable(int status, int id)
        {
            TableDAL.UpdateStatusTable(status, id);
        }
        public static int GetStatusByIDTable(int id)
        {
            return TableDAL.GetStatusByIDTable(id);
        }
        public static List<TableDTO> GetListTableHaveStatusOne()
        {
            return TableDAL.GetListTableHaveStatusOne();
        }
        public static List<TableDTO> GetListTableDifferentID(int id)
        {
            return TableDAL.GetListTableDifferentID(id);
        }
        public static bool InsertTable(TableDTO tb)
        {
            return TableDAL.InsertTable(tb);
        }
        public static bool DeleteTable(TableDTO tb)
        {
            return TableDAL.DeleteTable(tb);
        }
    }
}
