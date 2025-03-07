using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BillUpDTO : BillDTO
{
    public double PromotionPrice { get; set; }

    public double CustomerPrice { get; set; }

    public double OutPrice { get; set; }

    public double Revenue { get; set; }

    public string TableName { get; set; }

    public BillUpDTO(DataRow row) : base(row)
    {
        PromotionPrice = (double)row["giauudai"];
        CustomerPrice = (double)row["giakhachhang"];
        OutPrice = (double)row["giangoai"];
        Revenue = (double)row["doanhthu"];
        TableName = row["maban"] == DBNull.Value ? "" : row["tenban"].ToString();
    }
}
public class BillDTO
{
    public BillDTO(int id, DateTime createDay, double total, int idtable, int employ, int status)
    {
        this.ID = id;
        CreateDay = createDay;
        Total = total;
        Idtable = idtable;
        Employ = employ;
        Status = status;
    }
    public BillDTO(DataRow row)
    {
        ID = (int)row["ma"];
        CreateDay = Convert.ToDateTime(row["ngaytao"]);
        Total = (double)row["tongtien"];
        Idtable = (int)row["maban"];
        Employ = (int)row["manhanvien"];
        Status = (int)row["trangthai"];
    }

    public int Idtable { get; set; }

    public double Total { get; set; }

    public DateTime CreateDay { get; set; }

    public int ID { get; set; }

    public int Employ { get; set; }

    public int Status { get; set; }
}
