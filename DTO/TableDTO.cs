using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DTO
{
    public class TableDTO
    {
        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        private string nameTable;

        public string NameTable
        {
            get { return nameTable; }
            set { nameTable = value; }
        }
        private int status;

        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        public TableDTO()
        {

        }
        public TableDTO(int id, string nametable, int status)
        {
            ID = id;
            NameTable = nametable;
            this.Status = status;
        }
        public TableDTO(DataRow row)
        {
            ID = (int)row["ma"];
            NameTable = row["tenban"].ToString();
            Status = (int)row["trangthai"];
        }
    }
}
