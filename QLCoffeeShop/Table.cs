using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCoffeeShop
{
    public partial class Table : UserControl
    {
        public Table()
        {
            InitializeComponent();
        }
        public Label lbl_Name1 { get => lbl_Name; set => lbl_Name = value; }
        public Label lblStatus1 { get => lblStatus; set => lblStatus = value; }
        public PictureBox image { get => pictureBox1; }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }

        private void lblStatus_Click_1(object sender, EventArgs e)
        {

        }

        private void lbl_Name_Click(object sender, EventArgs e)
        {

        }
    }
}
