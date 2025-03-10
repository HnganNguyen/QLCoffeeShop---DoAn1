using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAL
{
    public class TypeProductDAL
    {
        public static List<TypeProductDTO> GetListTypeProductWithStatusOne(int status)
        {
            List<TypeProductDTO> listtype = new List<TypeProductDTO>();// 0 ẩn , 1 hiện
            string query = "select * from LOAISANPHAM where TRANGTHAI = " + status;
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                TypeProductDTO type = new TypeProductDTO(item);
                listtype.Add(type);
            }
            return listtype;
        }
        public static List<TypeProductDTO> GetAllListTypeProduct()
        {
            List<TypeProductDTO> listtypeProduct = new List<TypeProductDTO>();
            string query = "select * from LOAISANPHAM";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                TypeProductDTO typeProduct = new TypeProductDTO(item);
                listtypeProduct.Add(typeProduct);
            }
            return listtypeProduct;
        }
        public static bool InsertTypeProduct(TypeProductDTO typeProduct)
        {
            string query = "Exec USP_INSERTLOAISANPHAM @nametype , @status ";
            if (DataProvider.Instance.ExcuteNonQuery(query, new object[] { typeProduct.Nametype, typeProduct.Status }) == 1)
            {
                return true;
            }
            return false;
        }

        public static bool UpdateTypeProduct(TypeProductDTO tydr)
        {
            string query = "Exec USP_UPDATELOAISANPHAM @id , @nametype , @status ";
            if (DataProvider.Instance.ExcuteNonQuery(query, new object[] { tydr.ID, tydr.Nametype, tydr.Status }) == 1)
            {
                return true;
            }
            else
                return false;
        }
        public static bool DeleteTypeProduct(TypeProductDTO tydr)
        {
            string query = "Exec USP_DELETELOAISANPHAM @id ";
            if (DataProvider.Instance.ExcuteNonQuery(query, new object[] { tydr.ID }) == 1)
            {
                return true;
            }
            else
                return false;
        }

        public static string GetTypeNameByID(int id)
        {
            string query = $"SELECT TENLOAI FROM LOAISANPHAM WHERE MA = {id}";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            if (data.Rows.Count > 0)
                return data.Rows[0]["TENLOAI"].ToString();
            return "";
        }

        public static int GetIDByTypeName(string name)
        {
            string query = $"SELECT MA FROM LOAISANPHAM WHERE TENLOAI = N'{name}'";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            if (data.Rows.Count > 0)
                return int.Parse(data.Rows[0]["MA"].ToString());
            return -1;
        }
    }

}