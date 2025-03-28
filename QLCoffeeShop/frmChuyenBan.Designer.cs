namespace QLCoffeeShop
{
    partial class frmChuyenBan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbxTableTo = new System.Windows.Forms.ComboBox();
            this.txtBillTo = new System.Windows.Forms.TextBox();
            this.cbxTableFrom = new System.Windows.Forms.ComboBox();
            this.txtBillFrom = new System.Windows.Forms.TextBox();
            this.btnNextOne = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.btnThoatoder = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã hóa đơn:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(300, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mã hóa đơn:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Bàn:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(300, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Bàn:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbxTableTo);
            this.panel1.Controls.Add(this.txtBillTo);
            this.panel1.Controls.Add(this.cbxTableFrom);
            this.panel1.Controls.Add(this.txtBillFrom);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.panel1.Location = new System.Drawing.Point(51, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(553, 152);
            this.panel1.TabIndex = 4;
            // 
            // cbxTableTo
            // 
            this.cbxTableTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.cbxTableTo.FormattingEnabled = true;
            this.cbxTableTo.Location = new System.Drawing.Point(407, 78);
            this.cbxTableTo.Name = "cbxTableTo";
            this.cbxTableTo.Size = new System.Drawing.Size(143, 28);
            this.cbxTableTo.TabIndex = 14;
            // 
            // txtBillTo
            // 
            this.txtBillTo.Enabled = false;
            this.txtBillTo.Location = new System.Drawing.Point(398, 30);
            this.txtBillTo.Multiline = true;
            this.txtBillTo.Name = "txtBillTo";
            this.txtBillTo.Size = new System.Drawing.Size(143, 27);
            this.txtBillTo.TabIndex = 11;
            // 
            // cbxTableFrom
            // 
            this.cbxTableFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.cbxTableFrom.FormattingEnabled = true;
            this.cbxTableFrom.Location = new System.Drawing.Point(110, 83);
            this.cbxTableFrom.Name = "cbxTableFrom";
            this.cbxTableFrom.Size = new System.Drawing.Size(143, 28);
            this.cbxTableFrom.TabIndex = 10;
            // 
            // txtBillFrom
            // 
            this.txtBillFrom.Enabled = false;
            this.txtBillFrom.Location = new System.Drawing.Point(110, 30);
            this.txtBillFrom.Multiline = true;
            this.txtBillFrom.Name = "txtBillFrom";
            this.txtBillFrom.Size = new System.Drawing.Size(143, 27);
            this.txtBillFrom.TabIndex = 7;
            // 
            // btnNextOne
            // 
            this.btnNextOne.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnNextOne.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextOne.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNextOne.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnNextOne.Location = new System.Drawing.Point(218, 216);
            this.btnNextOne.Name = "btnNextOne";
            this.btnNextOne.Size = new System.Drawing.Size(86, 42);
            this.btnNextOne.TabIndex = 15;
            this.btnNextOne.Text = "Chuyển";
            this.btnNextOne.UseVisualStyleBackColor = false;
            this.btnNextOne.Click += new System.EventHandler(this.btnNextOne_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(333, 216);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 42);
            this.button1.TabIndex = 16;
            this.button1.Text = "Hủy";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Top;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label23.ForeColor = System.Drawing.Color.White;
            this.label23.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label23.Location = new System.Drawing.Point(0, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(628, 44);
            this.label23.TabIndex = 61;
            this.label23.Text = "Chuyển Bàn";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnThoatoder
            // 
            this.btnThoatoder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThoatoder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnThoatoder.FlatAppearance.BorderSize = 0;
            this.btnThoatoder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThoatoder.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoatoder.ForeColor = System.Drawing.Color.White;
            this.btnThoatoder.Location = new System.Drawing.Point(571, 0);
            this.btnThoatoder.Margin = new System.Windows.Forms.Padding(2);
            this.btnThoatoder.Name = "btnThoatoder";
            this.btnThoatoder.Size = new System.Drawing.Size(46, 40);
            this.btnThoatoder.TabIndex = 64;
            this.btnThoatoder.Text = "x";
            this.btnThoatoder.UseVisualStyleBackColor = false;
            this.btnThoatoder.Click += new System.EventHandler(this.btnThoatoder_Click);
            // 
            // frmChuyenBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 276);
            this.Controls.Add(this.btnThoatoder);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnNextOne);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmChuyenBan";
            this.Text = "Chuyển bàn";
            this.Load += new System.EventHandler(this.frmChuyenBan_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbxTableTo;
        private System.Windows.Forms.TextBox txtBillTo;
        private System.Windows.Forms.ComboBox cbxTableFrom;
        private System.Windows.Forms.TextBox txtBillFrom;
        private System.Windows.Forms.Button btnNextOne;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btnThoatoder;
    }
}