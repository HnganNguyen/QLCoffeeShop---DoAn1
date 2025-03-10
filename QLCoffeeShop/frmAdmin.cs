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
using DAL;
using DTO;

namespace QLCoffeeShop
{
    public partial class frmAdmin : Form
    {
        public frmAdmin()
        {
            InitializeComponent();
            _showTable();
            _showTypeProduct();
            _showProduct();
            _loadTypeProduct();
            _filterProduct();
            _showIngredient();
        }



        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            HandleButtonClick(sender, "tabPage1");
        }

        private void btnSanpham_Click(object sender, EventArgs e)
        {
            HandleButtonClick(sender, "tabPage2");
        }
        private void btnBan_Click(object sender, EventArgs e)
        {
            HandleButtonClick(sender, "tabPage3");
        }

        private void btnDoanhthu_Click(object sender, EventArgs e)
        {
            HandleButtonClick(sender, "tabPage4");
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            HandleButtonClick(sender, "tabPage5");
        }


        private void HandleButtonClick(object sender, string tabPageName)
        {
            // Đặt lại màu nền của tất cả các nút trong panel_btnSet
            foreach (Control ct in panel_btnSet.Controls)
            {
                if (ct is Button)
                    ct.BackColor = System.Drawing.Color.White;
            }

            // Đặt màu nền cho nút được nhấn
            Button btn = sender as Button;
            if (btn != null)
            {
                btn.BackColor = System.Drawing.Color.FromArgb(9, 132, 227);

                // Chuyển đổi tab trong TabControl
                foreach (TabPage tp in radAdminAccount.TabPages)
                {
                    if (tp.Name == tabPageName)
                    {
                        radAdminAccount.SelectTab(tp);
                        break;
                    }
                }
            }
        }

        #region quản lý bàn
        private void _showTable()
        {
            lstTable.Items.Clear();
            List<TableDTO> menulist = TableBLL.GetAllListTable();
            for (int i = 0; i < menulist.Count; i++)
            {
                ListViewItem listitem = new ListViewItem
                {
                    Text = "#" + (i + 1).ToString()
                };
                listitem.SubItems.Add(menulist[i].NameTable.ToString());
                listitem.SubItems.Add("B00" + menulist[i].ID.ToString());
                if (menulist[i].Status == 0)
                    listitem.SubItems.Add("Bàn trống");
                else listitem.SubItems.Add("Bàn đang có khách");
                listitem.Tag = menulist[i];
                lstTable.Items.Add(listitem);
            }
        }
        #endregion

        private void btnThemBan_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTenBan.Text == "")
                {
                    MessageBox.Show("Bạn không thể thêm nếu như để trống một trường dữ liệu nào.", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    TableDTO sp = new TableDTO
                    {
                        NameTable = txtTenBan.Text,
                        Status = 0
                    };
                    if (TableBLL.InsertTable(sp))
                    {
                        _showTable();
                        _deleteTextTable();
                        MessageBox.Show("Bạn đã thêm mới thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    else
                        MessageBox.Show("Bạn thêm mới thất bại, thử lại.", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void btnXoaBan_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstTable.SelectedItems.Count > 0)
                {
                    TableDTO sp = lstTable.SelectedItems[0].Tag as TableDTO;

                    if (sp.Status == 0) // Chỉ xóa khi bàn trống
                    {
                        if (TableBLL.DeleteTable(sp))
                        {
                            btnXoaBan.Enabled = false;
                            MessageBox.Show("Bàn bạn chọn đã được xóa khỏi hệ thống", "Thông báo", MessageBoxButtons.OK);
                            _showTable();
                        }
                        else
                        {
                            MessageBox.Show("Xóa bàn thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bàn " + sp.ID + " hiện chưa được thanh toán, không thể xóa!", "Thông báo", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn bàn để thực hiện!", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void _deleteTextTable()
        {
            txtTenBan.Text = "";
            txtMaBan.Text = "";
            btnXoaBan.Enabled = false;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            _deleteTextTable();
        }

        private void lstTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnXoaBan.Enabled = true;
            if (lstTable.SelectedItems.Count > 0)
            {
                ListViewItem lvw = lstTable.SelectedItems[0];
                TableDTO sp = (TableDTO)lvw.Tag;
                txtTenBan.Text = sp.NameTable;
                txtMaBan.Text = "B00" + sp.ID;
            }
        }
        
        #region Quản lý loại sản phẩm
        private void _showTypeProduct()
        {
            lstTypeProduct.Items.Clear();
            List<TypeProductDTO> typeList = TypeProductBLL.GetAllListTypeProduct();
            for (int i = 0; i < typeList.Count; i++)
            {
                ListViewItem listItem = new ListViewItem
                {
                    Text = (i + 1).ToString() 
                };
                listItem.SubItems.Add(typeList[i].ID.ToString()); 
                listItem.SubItems.Add(typeList[i].Nametype); 
                listItem.SubItems.Add(typeList[i].Status == 1 ? "Đang hoạt động" : "Khóa"); 
                listItem.Tag = typeList[i]; 
                lstTypeProduct.Items.Add(listItem);
            }
        }

        private void _clearTypeProductForm()
        {
            txtIDTypeProduct.Text = "";
            txtTypeProductName.Text = "";
            radHienType.Checked = true;
            btnDeTypeProduct.Enabled = false;
            btnEditTypeProduct.Enabled = false;
        }

        private void btnNewType_Click(object sender, EventArgs e)
        {
            _clearTypeProductForm();
        }

        private void btnAddTypeProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTypeProductName.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên loại sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                TypeProductDTO newType = new TypeProductDTO
                {
                    Nametype = txtTypeProductName.Text,
                    Status = radHienType.Checked ? 1 : 0
                };

                if (TypeProductBLL.InsertTypeProduct(newType))
                {
                    _showTypeProduct();
                    _clearTypeProductForm();
                    MessageBox.Show("Thêm loại sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Thêm thất bại! Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeTypeProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstTypeProduct.SelectedItems.Count > 0)
                {
                    TypeProductDTO selectedType = lstTypeProduct.SelectedItems[0].Tag as TypeProductDTO;

                    DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa loại sản phẩm '{selectedType.Nametype}' không?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        if (TypeProductBLL.DeleteTypeProduct(selectedType))
                        {
                            _showTypeProduct();
                            _clearTypeProductForm();
                            MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại! Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn loại sản phẩm cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditTypeProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstTypeProduct.SelectedItems.Count > 0)
                {
                    TypeProductDTO selectedType = lstTypeProduct.SelectedItems[0].Tag as TypeProductDTO;

                    selectedType.Nametype = txtTypeProductName.Text;
                    selectedType.Status = radHienType.Checked ? 1 : 0;

                    if (TypeProductBLL.UpdateTypeProduct(selectedType))
                    {
                        _showTypeProduct();
                        _clearTypeProductForm();
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại! Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn loại sản phẩm cần chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstTypeProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTypeProduct.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lstTypeProduct.SelectedItems[0];
                TypeProductDTO selectedType = (TypeProductDTO)selectedItem.Tag;

                txtIDTypeProduct.Text = selectedType.ID.ToString();
                txtTypeProductName.Text = selectedType.Nametype;
                radHienType.Checked = selectedType.Status == 1;
                radAnType.Checked = selectedType.Status == 0;

                btnDeTypeProduct.Enabled = true;
                btnEditTypeProduct.Enabled = true;
            }
        }
        #endregion

        #region Quản lý nguyên liệu

        private void _showIngredient()
        {
            lstNguyenLieu.Items.Clear();
            List<NguyenLieuDTO> list = NguyenLieuBLL.GetAllNguyenLieu();
            int stt = 1; // sTT tự tăng
            foreach (NguyenLieuDTO nl in list)
            {
                ListViewItem item = new ListViewItem(stt.ToString());
                item.SubItems.Add(nl.Ma.ToString());                 
                item.SubItems.Add(nl.Ten);                           
                item.SubItems.Add(nl.NgayTao.ToString("dd/MM/yyyy"));
                item.SubItems.Add(nl.GiaGoc.ToString());             
                item.SubItems.Add(nl.GhiChu);                        
                lstNguyenLieu.Items.Add(item);
                stt++; 
            }
            _calculateTotalPrice(); // tính tổng
        }

        private void btnLamMoiNL_Click(object sender, EventArgs e)
        {
            _clearIngredientForm();
        }

        private void btnLuuNL_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenNL.Text) || string.IsNullOrWhiteSpace(txtGiaNL.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            NguyenLieuDTO nl = new NguyenLieuDTO
            {
                Ten = txtTenNL.Text,
                NgayTao = DateTime.Now,
                GiaGoc = double.Parse(txtGiaNL.Text),
                GhiChu = txtGhiChuNL.Text
            };
            NguyenLieuBLL.InsertNguyenLieu(nl);
            _showIngredient();
            _clearIngredientForm();
            _calculateTotalPrice();
        }

        private void btnXoaNL_Click(object sender, EventArgs e)
        {
            if (lstNguyenLieu.SelectedItems.Count > 0)
            {
                int ma = int.Parse(lstNguyenLieu.SelectedItems[0].SubItems[1].Text);
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nguyên liệu này?",
                                                      "Xác nhận xóa",MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    NguyenLieuBLL.DeleteNguyenLieu(ma);
                    _showIngredient();
                    _clearIngredientForm();
                    _calculateTotalPrice();
                }
            }
        }

        private void lstNguyenLieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstNguyenLieu.SelectedItems.Count > 0)
            {
                ListViewItem item = lstNguyenLieu.SelectedItems[0];
                txtMaNL.Text = item.SubItems[1].Text; 
                txtTenNL.Text = item.SubItems[2].Text; 
                txtGiaNL.Text = item.SubItems[4].Text; 
                txtGhiChuNL.Text = item.SubItems[5].Text; 
                btnXoaNL.Enabled = true;
            }
            else
            {
                btnXoaNL.Enabled = false;
            }
        }

        private void _clearIngredientForm()
        {
            txtMaNL.Text = "";
            txtTenNL.Text = "";
            txtGiaNL.Text = "";
            txtGhiChuNL.Text = "";
            btnXoaNL.Enabled = false;
        }

        private void _calculateTotalPrice() //hàm tính tổng
        {
            double total = 0;
            foreach (ListViewItem item in lstNguyenLieu.Items)
            {
                if (double.TryParse(item.SubItems[4].Text, out double price)) 
                {
                    total += price;
                }
            }
            txtTongGiaNL.Text = total.ToString("N0");
        }
        #endregion

        #region Quản lý sản phẩm
        void _loadTypeProduct()
        {
            List<TypeProductDTO> listType = TypeProductBLL.GetAllListTypeProduct();

            cbLoaiSP.DataSource = new List<TypeProductDTO>(listType);
            cbLoaiSP.DisplayMember = "NameType";
            cbLoaiSP.ValueMember = "ID";

            //phần này cho lọc
            listType.Insert(0, new TypeProductDTO { ID = 0, Nametype = "Tất cả" });
            cbLocLoaiSP.DataSource = listType;
            cbLocLoaiSP.DisplayMember = "NameType";
            cbLocLoaiSP.ValueMember = "ID";
        }

        void _showProduct()
        {
            lstProduct.Items.Clear();
            List<ProductDTO> listProduct = ProductBLL.GetAllListProduct();
            int stt = 1; 
            foreach (ProductDTO product in listProduct)
            {
                ListViewItem item = new ListViewItem(stt.ToString()); 
                item.SubItems.Add(product.ID.ToString());
                item.SubItems.Add(product.NameProducts); 
                item.SubItems.Add(product.SalePrice.ToString()); 
                item.SubItems.Add(product.Status == 1 ? "Đang hoạt động" : "Ngừng bán"); 
                item.SubItems.Add(TypeProductBLL.GetTypeNameByID(product.IDTypeProduct)); 
                lstProduct.Items.Add(item);
                stt++; 
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenSP.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm!");
                return;
            }

            double price;
            if (!double.TryParse(txtDonGia.Text.Replace(",", "").Trim(), out price))
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng số cho đơn giá!");
                return;
            }

            if (cbLoaiSP.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn loại sản phẩm!");
                return;
            }

            int typeID = (int)cbLoaiSP.SelectedValue;
            int status = radHien.Checked ? 1 : 0;

            ProductDTO newProduct = new ProductDTO(0, txtTenSP.Text, price, price, status, typeID);
            if (ProductBLL.AddProduct(newProduct))
            {
                MessageBox.Show("Thêm sản phẩm thành công!");
                _showProduct(); _clearTextProduct();
            }
            else
            {
                MessageBox.Show("Thêm sản phẩm thất bại!");
            }
        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            if (lstProduct.SelectedItems.Count == 0) 
                return;
            int id = int.Parse(lstProduct.SelectedItems[0].SubItems[1].Text); 
            string name = txtTenSP.Text;
            string priceText = txtDonGia.Text.Replace(",", "").Trim();
            if (!double.TryParse(priceText, out double price))
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng số cho đơn giá!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int typeID = (int)cbLoaiSP.SelectedValue;
            int status = radHien.Checked ? 1 : 0;

            ProductDTO updateProduct = new ProductDTO(id, name, price, price, status, typeID);

            if (ProductBLL.UpdateProduct(updateProduct))
            {
                MessageBox.Show("Cập nhật sản phẩm thành công!");
                _showProduct();
                _clearTextProduct();
            }
            else
            {
                MessageBox.Show("Cập nhật sản phẩm thất bại!");
            }
        }

        private void btnDeProduct_Click(object sender, EventArgs e)
        {
            if (lstProduct.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!");
                 return;
            }

            if (!int.TryParse(lstProduct.SelectedItems[0].SubItems[1].Text, out int id)) 
            {
                MessageBox.Show("Lỗi: ID sản phẩm không hợp lệ!");
                return;
            }

            if (ProductBLL.DeleteProduct(id))
            {
                MessageBox.Show("Xóa sản phẩm thành công!");
                _showProduct();
                _clearTextProduct();
            }
            else
            {
                MessageBox.Show("Xóa sản phẩm thất bại!");
            }
        }

        private void lstProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstProduct.SelectedItems.Count == 0) return;
            ListViewItem item = lstProduct.SelectedItems[0];
            txtMaSP.Text = item.SubItems[1].Text; 
            txtTenSP.Text = item.SubItems[2].Text; 

            if (!double.TryParse(item.SubItems[3].Text.Replace(",", "").Trim(), out double price))
                txtDonGia.Text = "0";
            else
                txtDonGia.Text = price.ToString();
            if (item.SubItems[4].Text == "Đang hoạt động")
                radHien.Checked = true;
            else
                radAn.Checked = true;
            int? typeID = TypeProductBLL.GetIDByTypeName(item.SubItems[5].Text);

            if (typeID.HasValue)
                cbLoaiSP.SelectedValue = typeID.Value;
            else
                cbLoaiSP.SelectedIndex = -1;
        }


        private void _clearTextProduct()
        {
            txtMaSP.Clear();
            txtTenSP.Clear();
            txtDonGia.Clear();
            cbLoaiSP.SelectedIndex = -1; 
            radHien.Checked = true; 
        }

        private void btnWatchProduct_Click(object sender, EventArgs e)
        {
            _clearTextProduct();
        }

        private void frmAdmin_Activated(object sender, EventArgs e) //sự kiện để load loại sp sau khi thêm
        {
            _loadTypeProduct();
        }

        //phần lọc
        void _filterProduct(int idLoaiSP = 0)
        {
            lstProduct.Items.Clear();
            List<ProductDTO> listProduct = ProductBLL.GetAllListProduct();

            // Nếu chọn loại khác "Tất cả", chỉ lấy sản phẩm của loại đã chọn
            if (idLoaiSP != 0)
            {
                listProduct = listProduct.Where(p => p.IDTypeProduct == idLoaiSP).ToList();
            }

            int stt = 1;
            foreach (ProductDTO product in listProduct)
            {
                ListViewItem item = new ListViewItem(stt.ToString());
                item.SubItems.Add(product.ID.ToString());
                item.SubItems.Add(product.NameProducts);
                item.SubItems.Add(product.SalePrice.ToString());
                item.SubItems.Add(product.Status == 1 ? "Đang hoạt động" : "Ngừng bán");
                item.SubItems.Add(TypeProductBLL.GetTypeNameByID(product.IDTypeProduct));
                lstProduct.Items.Add(item);
                stt++;
            }
        }

        private void cbLocLoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLocLoaiSP.SelectedValue != null)
            {
                int idLoaiSP;
                if (int.TryParse(cbLocLoaiSP.SelectedValue.ToString(), out idLoaiSP))
                {
                    _filterProduct(idLoaiSP);
                }
            }
        }

        //phần tìm kiếm sp
        private void _searchProduct(string keyword)
        {
            lstProduct.Items.Clear();
            List<ProductDTO> listProduct = ProductBLL.SearchProductByName(keyword);

            int stt = 1;
            foreach (ProductDTO product in listProduct)
            {
                ListViewItem item = new ListViewItem(stt.ToString());
                item.SubItems.Add(product.ID.ToString());
                item.SubItems.Add(product.NameProducts);
                item.SubItems.Add(product.SalePrice.ToString());
                item.SubItems.Add(product.Status == 1 ? "Đang hoạt động" : "Ngừng bán");
                item.SubItems.Add(TypeProductBLL.GetTypeNameByID(product.IDTypeProduct));

                lstProduct.Items.Add(item);
                stt++;
            }
        }

        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
            string keyword = txtSearchProduct.Text.Trim();
            _searchProduct(keyword);
        }
        #endregion

    }
}