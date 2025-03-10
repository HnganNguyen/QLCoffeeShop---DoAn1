using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
namespace BLL
{
    public class TypeProductBLL
    {
        public static List<TypeProductDTO> GetAllListTypeProduct()
        {
            return TypeProductDAL.GetAllListTypeProduct();
        }

        public static List<TypeProductDTO> GetListTypeProductWithStatusOne(int status)
        {
            return TypeProductDAL.GetListTypeProductWithStatusOne(status);
        }

        public static bool InsertTypeProduct(TypeProductDTO typeProduct)
        {
            return TypeProductDAL.InsertTypeProduct(typeProduct);
        }

        public static bool UpdateTypeProduct(TypeProductDTO tydr)
        {
            return TypeProductDAL.UpdateTypeProduct(tydr);
        }

        public static bool DeleteTypeProduct(TypeProductDTO tydr)
        {
            return TypeProductDAL.DeleteTypeProduct(tydr);
        }

        public static string GetTypeNameByID(int id)
        {
            return TypeProductDAL.GetTypeNameByID(id);
        }

        public static int GetIDByTypeName(string name)
        {
            return TypeProductDAL.GetIDByTypeName(name);
        }

    }
}
