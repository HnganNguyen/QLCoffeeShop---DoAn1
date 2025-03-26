using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class BaoCaoBLL
    {
        private static BaoCaoBLL _instance;
        public static BaoCaoBLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BaoCaoBLL();
                }
                return _instance;
            }
        }
        private BaoCaoBLL() { }

        public List<BaoCaoDTO> LoadBaoCao(int thang, int nam)
        {
            return new BaoCaoDAL().GetBaoCao(thang, nam);
        }

        public float GetTongDoanhThuNam(int nam)
        {
            BaoCaoDAL dal = new BaoCaoDAL(); // ✅ Tạo đối tượng mới
            return dal.GetTongDoanhThuNam(nam);
        }
    }



}
