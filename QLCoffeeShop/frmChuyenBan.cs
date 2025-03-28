using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;

namespace QLCoffeeShop
{
    public partial class frmChuyenBan : Form
    {
        private frmOrder _parentForm;
        public frmChuyenBan(frmOrder parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;
            _loadTableOfTable(TableBLL.GetListTableHaveStatusOne(), cbxTableFrom, "NameTable", "ID");
            _loadTableOfTable(TableBLL.GetListTableHaveStatusZero(), cbxTableTo, "NameTable", "ID");
            _loadBillInTextBox(txtBillFrom, (int)cbxTableFrom.SelectedValue);
            _loadBillInTextBox(txtBillTo, (int)cbxTableTo.SelectedValue);
        }
        private void _loadBillInTextBox(TextBox txtHD, int idTable)
        {
            int idBill = BLL.BillBLL.GetIDBillNoPaymentByIDTable(idTable);
            if (idBill != -1)
            {
                txtHD.Text = "HD00" + idBill.ToString();
            }
            else txtHD.Text = "";
        }
        private void _loadTableOfTable(List<TableDTO> list, ComboBox cbx, string display, string value)
        {
            cbx.DataSource = list;
            cbx.DisplayMember = display;
            cbx.ValueMember = value;

        }


        private void frmChuyenBan_Load(object sender, EventArgs e)
        {
                
        }

        private void btnNextOne_Click(object sender, EventArgs e)
        {
            int idTableFrom = (int)cbxTableFrom.SelectedValue;
            int idTableTo = (int)cbxTableTo.SelectedValue;

            if (idTableFrom == idTableTo)
            {
                MessageBox.Show("Cannot transfer to the same table.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int idBillFrom = BLL.BillBLL.GetIDBillNoPaymentByIDTable(idTableFrom);
            int idBillTo = BLL.BillBLL.GetIDBillNoPaymentByIDTable(idTableTo);

            if (idBillFrom == -1)
            {
                MessageBox.Show("No bill found for the source table.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (idBillTo == -1)
            {
                // Create a new bill for the destination table
                BLL.BillBLL.InsertBill(DateTime.Now, 0.0, Program.sTaiKhoan.ID, idTableTo);
                idBillTo = BLL.BillBLL.GetIDBillNoPaymentByIDTable(idTableTo);
            }

            // Transfer bill details from source table to destination table
            List<ChiTietBillDTO> billDetails = BLL.ChiTietBillBLL.GetListProductByIDBill(idBillFrom);
            foreach (var detail in billDetails)
            {
                BLL.ChiTietBillBLL.InsertChiTietBill(idBillTo, detail.IDProduct, detail.SoLuong);
            }

            // Delete the old bill details
            BLL.ChiTietBillBLL.DeleteChiTietBillByBillID(idBillFrom);

            // Delete the old bill
            BLL.BillBLL.DeleteBill(idBillFrom);

            // Update table statuses
            BLL.TableBLL.UpdateStatusTable(0, idTableFrom);
            BLL.TableBLL.UpdateStatusTable(1, idTableTo);

            MessageBox.Show("Bàn đã được chuyển.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            _loadBillInTextBox(txtBillFrom, idTableFrom);
            _loadBillInTextBox(txtBillTo, idTableTo);
            // Close the current form


            // Reload the tables in the parent form
            _parentForm.LoadTable();
            this.Close();
        }

        private void btnThoatoder_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
