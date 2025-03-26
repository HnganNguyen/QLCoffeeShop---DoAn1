using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;

namespace QLCoffeeShop
{
    internal static class Program
    {
        public static DTO.TaiKhoanDTO sTaiKhoan = new TaiKhoanDTO();
        public static int CurrentUserID { get; set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Frm_DangNhap());
        }

    }
}
