using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ChiTietBillDTO
    {
        public ChiTietBillDTO(DataRow row)
        {
            Mabill = (int)row["mahoadon"];
            IDProduct = (int)row["masanpham"];
            SoLuong = (int)row["soluong"];
        }

        public int Mabill { get; set; }

        public int IDProduct { get; set; }

        public int SoLuong { get; set; }

    }
}