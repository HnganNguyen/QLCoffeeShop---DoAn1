using System;
using System.Collections.Generic;
using System.Data;


namespace DTO
{
    public class TaiKhoanDTO
    {
        public TaiKhoanDTO()
        {
        }
        public TaiKhoanDTO(DataRow row) {
            ID = (int)row["MA"];
            Password = (string)row["PAS"];
            TenTK = (string)row["TEN"];   
            CCCD = (string)row["CCCD"];
            SDT = (string)row["SODIENTHOAI"];
            DiaChi = (string)row["DIACHI"];
            Quyen = (int)row["QUYEN"];
            TrangThai = (int)row["TRANGTHAI"];
       

        }
        public int ID { get; set; }
        public string Password { get; set; }
        public string TenTK { get; set; }
        public string CCCD { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public int Quyen { get; set; }
        public int TrangThai { get; set; }
    }
}
