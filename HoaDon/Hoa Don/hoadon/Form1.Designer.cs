namespace hoadon
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblSoHoaDon = new System.Windows.Forms.Label();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.lblNgayTao = new System.Windows.Forms.Label();
            this.lblTongTriGia = new System.Windows.Forms.Label();
            this.lblVAT = new System.Windows.Forms.Label();
            this.lblTongTienThue = new System.Windows.Forms.Label();
            this.lblTongCong = new System.Windows.Forms.Label();
            this.btTaoHoaDon = new System.Windows.Forms.Button();
            this.btLuuHoaDon = new System.Windows.Forms.Button();
            this.btInHoaDon = new System.Windows.Forms.Button();
            this.txtTongTriGia = new System.Windows.Forms.TextBox();
            this.txtVAT = new System.Windows.Forms.TextBox();
            this.txtTongTienThue = new System.Windows.Forms.TextBox();
            this.txtTongCong = new System.Windows.Forms.TextBox();
            this.NgayTao = new System.Windows.Forms.Label();
            this.SoHoaDon = new System.Windows.Forms.Label();
            this.gridHangHoa = new System.Windows.Forms.DataGridView();
            this.dgcolMaHH = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgcolTenHH = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgcolQuyCach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcolDonGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcolSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcolTongCong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblHangHoa = new System.Windows.Forms.Label();
            this.lblPercent = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.richPrint = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridHangHoa)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSoHoaDon
            // 
            this.lblSoHoaDon.AutoSize = true;
            this.lblSoHoaDon.Location = new System.Drawing.Point(27, 13);
            this.lblSoHoaDon.Name = "lblSoHoaDon";
            this.lblSoHoaDon.Size = new System.Drawing.Size(63, 13);
            this.lblSoHoaDon.TabIndex = 0;
            this.lblSoHoaDon.Text = "Số hóa đơn";
            // 
            // lblKhachHang
            // 
            this.lblKhachHang.AutoSize = true;
            this.lblKhachHang.Location = new System.Drawing.Point(27, 36);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Size = new System.Drawing.Size(65, 13);
            this.lblKhachHang.TabIndex = 1;
            this.lblKhachHang.Text = "Khách hàng";
            // 
            // lblNgayTao
            // 
            this.lblNgayTao.AutoSize = true;
            this.lblNgayTao.Location = new System.Drawing.Point(272, 13);
            this.lblNgayTao.Name = "lblNgayTao";
            this.lblNgayTao.Size = new System.Drawing.Size(50, 13);
            this.lblNgayTao.TabIndex = 2;
            this.lblNgayTao.Text = "Ngày tạo";
            // 
            // lblTongTriGia
            // 
            this.lblTongTriGia.AutoSize = true;
            this.lblTongTriGia.Location = new System.Drawing.Point(27, 59);
            this.lblTongTriGia.Name = "lblTongTriGia";
            this.lblTongTriGia.Size = new System.Drawing.Size(114, 13);
            this.lblTongTriGia.TabIndex = 3;
            this.lblTongTriGia.Text = "Tổng trị giá (đv: Đồng)";
            // 
            // lblVAT
            // 
            this.lblVAT.AutoSize = true;
            this.lblVAT.Location = new System.Drawing.Point(274, 59);
            this.lblVAT.Name = "lblVAT";
            this.lblVAT.Size = new System.Drawing.Size(28, 13);
            this.lblVAT.TabIndex = 4;
            this.lblVAT.Text = "VAT";
            // 
            // lblTongTienThue
            // 
            this.lblTongTienThue.AutoSize = true;
            this.lblTongTienThue.Location = new System.Drawing.Point(521, 60);
            this.lblTongTienThue.Name = "lblTongTienThue";
            this.lblTongTienThue.Size = new System.Drawing.Size(130, 13);
            this.lblTongTienThue.TabIndex = 5;
            this.lblTongTienThue.Text = "Tổng tiền thuế (đv: Đồng)";
            // 
            // lblTongCong
            // 
            this.lblTongCong.AutoSize = true;
            this.lblTongCong.Location = new System.Drawing.Point(27, 99);
            this.lblTongCong.Name = "lblTongCong";
            this.lblTongCong.Size = new System.Drawing.Size(113, 13);
            this.lblTongCong.TabIndex = 6;
            this.lblTongCong.Text = "Tổng cộng (đv: Đồng)";
            // 
            // btTaoHoaDon
            // 
            this.btTaoHoaDon.Location = new System.Drawing.Point(30, 338);
            this.btTaoHoaDon.Name = "btTaoHoaDon";
            this.btTaoHoaDon.Size = new System.Drawing.Size(80, 23);
            this.btTaoHoaDon.TabIndex = 8;
            this.btTaoHoaDon.Text = "Tạo hóa đơn";
            this.btTaoHoaDon.UseVisualStyleBackColor = true;
            this.btTaoHoaDon.Click += new System.EventHandler(this.btTaoHoaDon_Click);
            // 
            // btLuuHoaDon
            // 
            this.btLuuHoaDon.Location = new System.Drawing.Point(116, 338);
            this.btLuuHoaDon.Name = "btLuuHoaDon";
            this.btLuuHoaDon.Size = new System.Drawing.Size(80, 23);
            this.btLuuHoaDon.TabIndex = 9;
            this.btLuuHoaDon.Text = "Lưu hóa đơn";
            this.btLuuHoaDon.UseVisualStyleBackColor = true;
            this.btLuuHoaDon.Click += new System.EventHandler(this.btLuuHoaDon_Click);
            // 
            // btInHoaDon
            // 
            this.btInHoaDon.Location = new System.Drawing.Point(202, 338);
            this.btInHoaDon.Name = "btInHoaDon";
            this.btInHoaDon.Size = new System.Drawing.Size(80, 23);
            this.btInHoaDon.TabIndex = 10;
            this.btInHoaDon.Text = "In hóa đơn";
            this.btInHoaDon.UseVisualStyleBackColor = true;
            this.btInHoaDon.Click += new System.EventHandler(this.btInHoaDon_Click);
            // 
            // txtTongTriGia
            // 
            this.txtTongTriGia.ForeColor = System.Drawing.Color.Blue;
            this.txtTongTriGia.Location = new System.Drawing.Point(30, 76);
            this.txtTongTriGia.Name = "txtTongTriGia";
            this.txtTongTriGia.ReadOnly = true;
            this.txtTongTriGia.Size = new System.Drawing.Size(100, 20);
            this.txtTongTriGia.TabIndex = 11;
            this.txtTongTriGia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtVAT
            // 
            this.txtVAT.Location = new System.Drawing.Point(277, 75);
            this.txtVAT.MaxLength = 2;
            this.txtVAT.Name = "txtVAT";
            this.txtVAT.Size = new System.Drawing.Size(25, 20);
            this.txtVAT.TabIndex = 12;
            this.txtVAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtVAT.TextChanged += new System.EventHandler(this.txtVAT_TextChanged);
            // 
            // txtTongTienThue
            // 
            this.txtTongTienThue.ForeColor = System.Drawing.Color.Red;
            this.txtTongTienThue.Location = new System.Drawing.Point(524, 76);
            this.txtTongTienThue.Name = "txtTongTienThue";
            this.txtTongTienThue.ReadOnly = true;
            this.txtTongTienThue.Size = new System.Drawing.Size(100, 20);
            this.txtTongTienThue.TabIndex = 13;
            this.txtTongTienThue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTongCong
            // 
            this.txtTongCong.ForeColor = System.Drawing.Color.Blue;
            this.txtTongCong.Location = new System.Drawing.Point(30, 115);
            this.txtTongCong.Name = "txtTongCong";
            this.txtTongCong.ReadOnly = true;
            this.txtTongCong.Size = new System.Drawing.Size(100, 20);
            this.txtTongCong.TabIndex = 14;
            this.txtTongCong.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // NgayTao
            // 
            this.NgayTao.AutoSize = true;
            this.NgayTao.Location = new System.Drawing.Point(338, 13);
            this.NgayTao.Name = "NgayTao";
            this.NgayTao.Size = new System.Drawing.Size(13, 13);
            this.NgayTao.TabIndex = 15;
            this.NgayTao.Text = "0";
            // 
            // SoHoaDon
            // 
            this.SoHoaDon.AutoSize = true;
            this.SoHoaDon.Location = new System.Drawing.Point(98, 12);
            this.SoHoaDon.Name = "SoHoaDon";
            this.SoHoaDon.Size = new System.Drawing.Size(13, 13);
            this.SoHoaDon.TabIndex = 17;
            this.SoHoaDon.Text = "0";
            // 
            // gridHangHoa
            // 
            this.gridHangHoa.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.gridHangHoa.AllowUserToOrderColumns = true;
            this.gridHangHoa.AllowUserToResizeColumns = false;
            this.gridHangHoa.AllowUserToResizeRows = false;
            this.gridHangHoa.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridHangHoa.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.gridHangHoa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridHangHoa.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcolMaHH,
            this.dgcolTenHH,
            this.dgcolQuyCach,
            this.dgcolDonGia,
            this.dgcolSoLuong,
            this.dgcolTongCong});
            this.gridHangHoa.Location = new System.Drawing.Point(30, 161);
            this.gridHangHoa.MultiSelect = false;
            this.gridHangHoa.Name = "gridHangHoa";
            this.gridHangHoa.RowHeadersVisible = false;
            this.gridHangHoa.Size = new System.Drawing.Size(789, 171);
            this.gridHangHoa.TabIndex = 18;
            this.gridHangHoa.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridHangHoa_CellEndEdit);
            // 
            // dgcolMaHH
            // 
            this.dgcolMaHH.FillWeight = 70F;
            this.dgcolMaHH.HeaderText = "Mã hàng hóa";
            this.dgcolMaHH.Name = "dgcolMaHH";
            this.dgcolMaHH.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dgcolTenHH
            // 
            this.dgcolTenHH.FillWeight = 170F;
            this.dgcolTenHH.HeaderText = "Tên hàng hóa";
            this.dgcolTenHH.Name = "dgcolTenHH";
            // 
            // dgcolQuyCach
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcolQuyCach.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgcolQuyCach.FillWeight = 52F;
            this.dgcolQuyCach.HeaderText = "Quy cách";
            this.dgcolQuyCach.Name = "dgcolQuyCach";
            this.dgcolQuyCach.ReadOnly = true;
            this.dgcolQuyCach.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dgcolDonGia
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcolDonGia.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgcolDonGia.FillWeight = 70F;
            this.dgcolDonGia.HeaderText = "Đơn giá";
            this.dgcolDonGia.Name = "dgcolDonGia";
            this.dgcolDonGia.ReadOnly = true;
            this.dgcolDonGia.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dgcolSoLuong
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcolSoLuong.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgcolSoLuong.FillWeight = 47F;
            this.dgcolSoLuong.HeaderText = "Số lượng";
            this.dgcolSoLuong.Name = "dgcolSoLuong";
            this.dgcolSoLuong.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dgcolTongCong
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcolTongCong.DefaultCellStyle = dataGridViewCellStyle15;
            this.dgcolTongCong.HeaderText = "Tổng cộng (đv: Đồng)";
            this.dgcolTongCong.Name = "dgcolTongCong";
            this.dgcolTongCong.ReadOnly = true;
            // 
            // lblHangHoa
            // 
            this.lblHangHoa.AutoSize = true;
            this.lblHangHoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHangHoa.Location = new System.Drawing.Point(368, 138);
            this.lblHangHoa.Name = "lblHangHoa";
            this.lblHangHoa.Size = new System.Drawing.Size(95, 20);
            this.lblHangHoa.TabIndex = 19;
            this.lblHangHoa.Text = "HÀNG HÓA";
            // 
            // lblPercent
            // 
            this.lblPercent.AutoSize = true;
            this.lblPercent.Location = new System.Drawing.Point(308, 79);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(15, 13);
            this.lblPercent.TabIndex = 20;
            this.lblPercent.Text = "%";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // printDialog1
            // 
            this.printDialog1.Document = this.printDocument1;
            this.printDialog1.UseEXDialog = true;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // richPrint
            // 
            this.richPrint.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.richPrint.Location = new System.Drawing.Point(706, 33);
            this.richPrint.Name = "richPrint";
            this.richPrint.Size = new System.Drawing.Size(100, 96);
            this.richPrint.TabIndex = 21;
            this.richPrint.Text = "";
            this.richPrint.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 382);
            this.Controls.Add(this.richPrint);
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.lblHangHoa);
            this.Controls.Add(this.gridHangHoa);
            this.Controls.Add(this.SoHoaDon);
            this.Controls.Add(this.NgayTao);
            this.Controls.Add(this.txtTongCong);
            this.Controls.Add(this.txtTongTienThue);
            this.Controls.Add(this.txtVAT);
            this.Controls.Add(this.txtTongTriGia);
            this.Controls.Add(this.btInHoaDon);
            this.Controls.Add(this.btLuuHoaDon);
            this.Controls.Add(this.btTaoHoaDon);
            this.Controls.Add(this.lblTongCong);
            this.Controls.Add(this.lblTongTienThue);
            this.Controls.Add(this.lblVAT);
            this.Controls.Add(this.lblTongTriGia);
            this.Controls.Add(this.lblNgayTao);
            this.Controls.Add(this.lblKhachHang);
            this.Controls.Add(this.lblSoHoaDon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "TẠO HÓA ĐƠN";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridHangHoa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSoHoaDon;
        private System.Windows.Forms.Label lblKhachHang;
        private System.Windows.Forms.Label lblNgayTao;
        private System.Windows.Forms.Label lblTongTriGia;
        private System.Windows.Forms.Label lblVAT;
        private System.Windows.Forms.Label lblTongTienThue;
        private System.Windows.Forms.Label lblTongCong;
        private System.Windows.Forms.Button btTaoHoaDon;
        private System.Windows.Forms.Button btLuuHoaDon;
        private System.Windows.Forms.Button btInHoaDon;
        private System.Windows.Forms.TextBox txtTongTriGia;
        private System.Windows.Forms.TextBox txtVAT;
        private System.Windows.Forms.TextBox txtTongTienThue;
        private System.Windows.Forms.TextBox txtTongCong;
        private System.Windows.Forms.Label NgayTao;
        private System.Windows.Forms.Label SoHoaDon;
        private System.Windows.Forms.DataGridView gridHangHoa;
        private System.Windows.Forms.Label lblHangHoa;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgcolMaHH;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgcolTenHH;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcolQuyCach;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcolDonGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcolSoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcolTongCong;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.RichTextBox richPrint;
    }
}

