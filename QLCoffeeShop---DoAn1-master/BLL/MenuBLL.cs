using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
namespace BLL
{
    public class MenuBLL
    {
        public static List<MenuDTO> GetListMenuByIDTable(int id)
        {
            return MenuDAL.GetListMenuByIDTable(id);
        }
        public static List<MenuDTO> GetListMenuByIDBill(int idBill)
        {
            return MenuDAL.GetListMenuByIDBill(idBill);
        }
        public static List<MenuDTO> GetReviewBill(int idBill)
        {
            return MenuDAL.GetReviewBill(idBill);
        }
    }
}