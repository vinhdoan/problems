using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace hoadon
{
    public partial class Form1 : Form
    {
        static string connectionString = @"Data Source =.\SQLEXPRESS; Initial Catalog = SieuThiMini; User ID=sa; Password=nhattuan";
        SQLServer sqlserver = new SQLServer(connectionString);
        //Tham số gọi hàm để gửi query khi query ko có tham số
        const string sqlKoBien = "khong co bien";
        //Các biến lưu dữ liệu tính toán và xuất nhập
        static int varSoHoaDon;
        static DateTime varNgayTao;
        static float varTongTriGia;
        static int varVAT;
        static float varTongTienThue;
        static float varTongCong;
        //Các biến lưu giá trị có trong grid
        static string[,] varGridHH = new string[2, 100];     //2 cột đầu của Grid Hàng hóa
        static string[] varGridQuyCach = new string[100];
        static float[] varGridDonGia = new float[100];
        static int[] varGridSoLuong = new int[100];
        static float[] varGridTongCong = new float[100];
        //Các hằng số lưu thuộc tính của ComboBox (các ComboBox đc tạo run-time)
        const int cbWidth = 123;
        const int cbHeight = 21;
        const int cbX = 98;
        const int cbY = 33;
        const int cbKhoangCach = 130;
        static int cbSoLuong = 0;           //Biến xđ số lượng ComboBox hiện tại
        const int cbMax = 4;                //Chỉ số ComboBox tối đa
        ComboBox[] cbKhachHang = new ComboBox[cbMax + 1];       //Các ComboBox
        static string[] varKhachHang = new string[cbMax + 1];   //Các biến lưu gtrị (tên KH) mỗi ComboBox tương ứng
        static string[] varMaKH = new string[cbMax + 1];        //Các biến lưu mã KH của gtrị mỗi ComboBox tương ứng

        DataTable cbData = new DataTable();
        static bool ChoPhepSave = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NgayTao.Text = DateTime.Now.ToString("dd/MM/yyyy - hh:mm:ss tt");

            //Danh sách khách hàng:
            DataTable data = sqlserver.ExecuteCommandText("Select KH_TenKH from tbKhachHang", sqlKoBien);
            cbData = data;  //Lấy danh sách khách hàng để nhập liệu cho các ComboBox
            VoHieuHoa();

            //Danh muc Mã hàng hóa:
            data = sqlserver.ExecuteCommandText("Select HH_MaHH from HangHoa", sqlKoBien);
            foreach (DataRow row in data.AsEnumerable())
                dgcolMaHH.Items.Add(row[0]);

            //Danh mục Tên hàng hóa:
            data = sqlserver.ExecuteCommandText("Select HH_TenHH from HangHoa", sqlKoBien);
            foreach (DataRow row in data.AsEnumerable())
                dgcolTenHH.Items.Add(row[0]);

            //Cho chạy hiển thị ngày giờ hệ thống:
            timer1.Enabled = true;

            //Các giá trị mặc định khác:
            txtTongTriGia.Text = txtVAT.Text = varVAT.ToString("N0");
        }

        //Xử lý sự kiện Timer chu kỳ 1s:
        private void timer1_Tick(object sender, EventArgs e)
        {
            NgayTao.Text = DateTime.Now.ToString("dd/MM/yyyy - hh:mm:ss tt"); //Hiển thị ngày giờ hiện tại
        }

        //Xử lý sự kiện Grid Hàng hóa đc cập nhật:
        private void gridHangHoa_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int Dong = e.RowIndex;      //Lưu dòng và cột thay đổi (xác định đc ô)
            int Cot = e.ColumnIndex;
            if (gridHangHoa.Rows[Dong].Cells[Cot].Value != null)        //Nếu ô khác rỗng
            {
                if (Cot < 2)    //Nếu thuộc cột 0 (Mã HH) hoặc cột 1 (Tên HH)
                {
                    varGridHH[Cot, Dong] = gridHangHoa.Rows[Dong].Cells[Cot].Value.ToString();  //Ghi lại giá trị ô

                    //Lấy danh sách tương ứng với gtrị Mã HH hoặc Tên HH:
                    DataTable data;
                    if (Cot == 0)
                        data = sqlserver.ExecuteCommandText("Select HH_TenHH,HH_DonViTinh,HH_DonGia from HangHoa where HH_MaHH = @bien", varGridHH[Cot, Dong]);
                    else
                        data = sqlserver.ExecuteCommandText("Select HH_MaHH,HH_DonViTinh,HH_DonGia from HangHoa where HH_TenHH = @bien", varGridHH[Cot, Dong]);
                    DataRow a = data.Rows[0];
                    gridHangHoa.Rows[Dong].Cells[1 - Cot].Value = a[0];     //Nhập vào Grid tùy đang là cột nào (Mã hoặc Tên)
                    varGridHH[1 - Cot, Dong] = a[0].ToString();             //Lưu lại

                    a = data.Rows[0];           //Quy cách
                    gridHangHoa.Rows[Dong].Cells[2].Value = a[1];
                    varGridQuyCach[Dong] = a[1].ToString();

                    a = data.Rows[0];           //Đơn giá
                    gridHangHoa.Rows[Dong].Cells[3].Value = Convert.ToInt32(a[2]).ToString("N0");
                    varGridDonGia[Dong] = float.Parse(a[2].ToString());

                    if (gridHangHoa.Rows[Dong].Cells[4].Value != null && gridHangHoa.Rows[Dong].Cells[4].Value != "")
                    {//Nếu ô số lượng khác rỗng...
                        varGridSoLuong[Dong] = (int)gridHangHoa.Rows[Dong].Cells[4].Value;   //...ghi lại gtrị ô Số lượng
                        varGridTongCong[Dong] = varGridSoLuong[Dong] * varGridDonGia[Dong]; //...tính toán, ghi lại gtrị ô Tổng
                        gridHangHoa.Rows[Dong].Cells[5].Value = varGridTongCong[Dong].ToString("N0"); //...xuất ra ô Tổng
                        TinhTien(); //Cập nhật cho các TextBox bên trên
                    }
                }
                else if (Cot == 4)      //Nếu ô thay đổi thuộc cột Số lượng:
                    if (gridHangHoa.Rows[Dong].Cells[0].Value == null) //Nếu hàng hóa chưa đc chọn...
                    {
                        gridHangHoa.Rows[Dong].Cells[4].Value = ""; //...xóa ô đó đi
                        gridHangHoa.Rows.RemoveAt(Dong);            //...remove dòng mới tạo trong Grid HH
                    }
                    else         //Nếu hàng hóa đã đc chọn...
                    {
                        string DuLieuNhap = gridHangHoa.Rows[Dong].Cells[4].Value.ToString();
                        if (KiemTraCoPhaiLaSo(DuLieuNhap))      //...thì ktra ô đó có phải là số hay không, nếu phải...
                        {
                            varGridSoLuong[Dong] = int.Parse(DuLieuNhap);   //...ghi lại gtrị ô Số lượng
                            varGridTongCong[Dong] = varGridSoLuong[Dong] * varGridDonGia[Dong]; //tính toán, ghi alị gtrị ô Tổng
                            gridHangHoa.Rows[Dong].Cells[5].Value = varGridTongCong[Dong].ToString(); //...xuất ra ô Tổng
                            TinhTien(); //Cập nhật cho các TextBox ở trên
                        }
                        else   //Nếu dữ liệu nhập không phải số
                        {
                            gridHangHoa.Rows[Dong].Cells[4].Value = "";  //...xóa đi
                            gridHangHoa.Rows.RemoveAt(Dong);               //remove dòng mới tạo trong Grid HH
                        }
                    }

                ChoPhepSave = true;                                     
                for (int i = 0; i <= gridHangHoa.RowCount - 2; i++) //Nếu có thiếu ô Số Lượng...
                    if (varGridSoLuong[i] == 0)
                        ChoPhepSave = false;                        //...không cho phép Save
            }
        }

        //Xử lý khi ô VAT thay đổi gtrị:
        private void txtVAT_TextChanged(object sender, EventArgs e)
        {
            if (txtVAT.Text != "")   //Nếu có ký tự...
            {
                bool so = KiemTraCoPhaiLaSo(txtVAT.Text);   //...ktra xem có phải số không
                if (so)             //Nếu là số...
                {
                    varVAT = int.Parse(txtVAT.Text);
                    TinhTien();     //...tính toán lại các gtrị TextBox
                }
                else txtVAT.Text = "0";     //Nếu không phải là số thì đặt lại bằng 0
            }
            else
            {
                varVAT = 0;
                TinhTien();
            }
        }

        //Xử lý khi click vào nút Tạo Hóa Đơn:
        private void btTaoHoaDon_Click(object sender, EventArgs e)
        {
            VoHieuHoa();    //Xóa dữ liệu cũ
            KichHoat();     //Cho phép nhập dữ liệu mới

            int SoHD_HienTai = 0;
            DataTable data = sqlserver.ExecuteCommandText("Select MAX(CAST(HD_SoHD AS INT)) from HoaDon", sqlKoBien);
            foreach (DataRow row in data.AsEnumerable())
                SoHD_HienTai = int.Parse(row[0].ToString());
            varSoHoaDon = SoHD_HienTai + 1;
            SoHoaDon.Text = varSoHoaDon.ToString();     //Ghi nhận số hóa đơn hiện tại (đánh số tiếp theo số HĐ lớn nhất nhận dc từ database
        }

        //Xử lý khi click vào nút Lưu Hóa Đơn (lưu xong ko cho phép tiếp tục thao tác trên dữ liệu):
        private void btLuuHoaDon_Click(object sender, EventArgs e)
        {
            bool KhachHangSelected = false;     //Biến cho biết đã có khách hàng nào đc chọn hay chưa
            for (int i = 0; i <= cbMax; i++)    //Dò hết các ComboBox...
                if (cbKhachHang[i] != null && cbKhachHang[i].Text != "")    //...ComboBox nào khác rỗng...
                {
                    varKhachHang[i] = cbKhachHang[i].Text;  //...ghi nhận tên khách hàng tại đó...
                    KhachHangSelected = true;               //...và đánh dấu đã có khác hàng đc chọn
                }
                else
                    varKhachHang[i] = "";

            if (KhachHangSelected && gridHangHoa.RowCount > 1 && ChoPhepSave)  //Nếu đã có khách hàng, và đã có hàng hóa đc chọn, cho phép Lưu
            {
                this.Enabled = false;
                for (int i = 0; i <= cbMax; i++)
                    if (cbKhachHang[i] != null) cbKhachHang[i].Enabled = false; //Vô hiệu hóa các ComboBox
                txtVAT.Enabled = false;         //Vô hiệu hóa các ô nhập liệu
                btLuuHoaDon.Enabled = false;
                gridHangHoa.Enabled = false;
                varNgayTao = DateTime.Now;      //Lấy thời gian hiện tại
                int j = 0;
                while (j < 5 && varKhachHang[j] != "")
                {
                    DataTable data = sqlserver.ExecuteCommandText("Select KH_MaKH from tbKhachHang where KH_TenKH = @bien", varKhachHang[j]);
                    foreach (DataRow row in data.AsEnumerable())
                        varMaKH[j] = row[0].ToString();     //Lấy Mã KH tương ứng với Tên KH

                    //Lưu vào bảng Hóa đơn:
                    int kq = sqlserver.ExecuteStoredProcedure("dbo.psql_TaoHoaDon", varSoHoaDon.ToString(), varNgayTao, varMaKH[j], varTongTriGia, varVAT, varTongTienThue, varTongCong, "Programmer");

                    //Lưu vào bảng Chi tiết hóa đơn:
                    for (int i = 0; i < gridHangHoa.RowCount - 1; i++)
                        kq = sqlserver.ExecuteStoredProcedure("dbo.psql_TaoChiTietHD", varGridHH[0, i], varSoHoaDon.ToString(), varGridSoLuong[i], varGridDonGia[i], varGridTongCong[i]);

                    varSoHoaDon++; //Tăng số hóa đơn lên 1 cho khách hàng tiếp theo
                    j++;    //Duyệt đến khách hàng tiếp theo
                }
                this.Enabled = true;
                MessageBox.Show("Đã lưu xong"); //Thông báo đã lưu xong
                btInHoaDon.Enabled = true;      //Cho phép in hóa đơn
            }
            else
                MessageBox.Show("Yêu cầu chọn khách hàng, mặt hàng và số lượng"); //Thông báo nếu chưa có khách hàng hay chưa có hàng hóa chọn
        }

        //Xử lý khi click vào nút In Hóa Đơn (sử dụng RichTextBox là richPrint để in)
        private void btInHoaDon_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK) //Khi xác nhận in...
            {
                this.Enabled = false;
                varSoHoaDon = int.Parse(SoHoaDon.Text); //Lấy lại số hóa đơn của khách hàng đầu tiên
                int j = 0;
                while (j < 5 && varKhachHang[j] != "")  //Duyệt từng khách hàng
                {
                    richPrint.Clear();  //Nhập các gtrị cần thiết vào RichTextBox
                    richPrint.Text = string.Format("{0,-12}{1,6}{2,82}\n\n", "Số hóa đơn: ", varSoHoaDon.ToString(),"Ngày tạo: "+ varNgayTao.ToString("dd/MM/yyyy - hh:mm:ss tt"));
                    richPrint.AppendText(string.Format("{0,50}\n\n","HÓA ĐƠN"));
                    richPrint.AppendText("Khách hàng:  " + varKhachHang[j].ToUpper() + "\n");
                    richPrint.AppendText("----------------------------------------------------------------------------------------------------\n");
                    richPrint.AppendText(string.Format("{0,-55}{1,20}{2,10}{3,15}\n","MẶT HÀNG","GIÁ","SỐ LƯỢNG", "TỔNG"));
                    for (int i = 0; i < gridHangHoa.RowCount - 1; i++)
                    {
                        string TenHang = varGridHH[1, i];
                        if (TenHang.Length > 55)
                            TenHang = TenHang.Substring(0, 55);
                        richPrint.AppendText(string.Format("{0,-55}{1,20}{2,10}{3,15}\n", TenHang, varGridDonGia[i].ToString("N0") + "đ/" + varGridQuyCach[i], varGridSoLuong[i].ToString(), varGridTongCong[i].ToString("N0") + "đ"));
                    }
                    richPrint.AppendText("----------------------------------------------------------------------------------------------------\n");
                    richPrint.AppendText(string.Format("{0,-75}{1,25}\n","TỔNG CỘNG",varTongTriGia.ToString("N0") + "đ"));
                    richPrint.AppendText(string.Format("{0,-75}{1,25}\n", "TIỀN THUẾ (" + varVAT.ToString() + "%)", varTongTienThue.ToString("N0") + "đ"));
                    richPrint.AppendText(string.Format("{0,-75}{1,25}\n","THÀNH TIỀN",varTongCong.ToString("N0") + "đ"));

                    StringReader reader = new StringReader(richPrint.Text);
                    printDocument1.Print(); //gọi in

                    varSoHoaDon++;  //tăng mã số hóa đơn lên
                    j++;            //duyệt khách hàng tiếp theo nếu có
                }
                this.Enabled = true;
            }
        }

        //Quá trình in trang
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //StringReader reader = new StringReader(richPrint.Text);
            //float LinesPerPage = 0;
            //float YPosition = 0;
            //int Count = 0;
            //float LeftMargin = e.MarginBounds.Left;
            //float TopMargin = e.MarginBounds.Top;
            //string Line = null;
            //Font PrintFont = this.richPrint.Font;
            //SolidBrush PrintBrush = new SolidBrush(Color.Black);

            //LinesPerPage = e.MarginBounds.Height / PrintFont.GetHeight(e.Graphics);

            //while (Count < LinesPerPage && ((Line = reader.ReadLine()) != null))
            //{
            //    YPosition = TopMargin + (Count * PrintFont.GetHeight(e.Graphics));
            //    e.Graphics.DrawString(Line, PrintFont, PrintBrush, LeftMargin, YPosition, new StringFormat());
            //    Count++;
            //}

            //if (Line != null)
            //{
            //    e.HasMorePages = true;
            //}
            //else
            //{
            //    e.HasMorePages = false;
            //}
            //PrintBrush.Dispose();
            //------------------------------------------------------------------------
            StringReader myReader = new StringReader(richPrint.Text);
            float linesPerPage = 0;
            float yPosition = 0;
            int count = 0;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            string line = null;
            Font printFont = this.richPrint.Font;
            SolidBrush myBrush = new SolidBrush(Color.Black);
            // Work out the number of lines per page, using the MarginBounds.
            linesPerPage = e.MarginBounds.Height / printFont.GetHeight(e.Graphics);
            // Iterate over the string using the StringReader, printing each line.
            while (count < linesPerPage && ((line = myReader.ReadLine()) != null))
            {
                // calculate the next line position based on the height of the font according to the printing device
                yPosition = topMargin + (count * printFont.GetHeight(e.Graphics));
                // draw the next line in the rich edit control
                e.Graphics.DrawString(line, printFont, myBrush, leftMargin, yPosition, new StringFormat());
                count++;
            }
            // If there are more lines, print another page.
            if (line != null)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
            myBrush.Dispose();
        }

        //Tính toán dữ liệu các ô TextBox
        private void TinhTien()
        {
            varTongTriGia = 0;
            for (int i = 0; i < 100; i++)
                varTongTriGia = varTongTriGia + varGridTongCong[i];     //Cộng dồn các ô Tổng trong Grid
            varTongTienThue = varVAT * varTongTriGia / 100;             //Thuế
            varTongCong = varTongTriGia + varTongTienThue;              //Tổng thành tiền

            txtTongTriGia.Text = varTongTriGia.ToString("N0");          //Xuất dữ liệu ra TextBox
            txtTongTienThue.Text = varTongTienThue.ToString("N0");
            txtTongCong.Text = varTongCong.ToString("N0");
        }

        //Kiểm tra chuỗi dữ liệu nhập có phải là con số hay không
        private bool KiemTraCoPhaiLaSo(string text)
        {
            bool kqua = true;
            foreach (char c in text)        //Với mỗi ký tự trong chuỗi...
                if (!char.IsNumber(c))      //...nếu ký tự đó không phải là số...
                {
                    kqua = false;           //...xác nhận kết quả là false...
                    break;                  //...và thoát
                }
            return kqua;
        }

        //Tạo 1 ComboBox (kèm chỉ số) mới khi cần thiết
        private void Tao_ComboBox(int index)
        {
            cbKhachHang[index] = new ComboBox();
            cbKhachHang[index].Size = new System.Drawing.Size(cbWidth, cbHeight);       //Kích cỡ
            cbKhachHang[index].Location = new System.Drawing.Point(cbX + index * cbKhoangCach, cbY); //Vị trí
            cbKhachHang[index].Name = index.ToString();     //Tên ComboBox -> dùng để xác định khi xảy ra sự kiện
            cbKhachHang[index].DropDownStyle = ComboBoxStyle.DropDownList;      //Chỉ cho phép user chọn trong list, ko cho nhập text
            this.Controls.Add(cbKhachHang[index]);
            cbSoLuong = index;                          //Ghi nhận chỉ số ComboBox hiện tại
            foreach (DataRow row in cbData.AsEnumerable())
                cbKhachHang[index].Items.Add(row[0]);
            //Xử lý sự kiện
            cbKhachHang[index].SelectionChangeCommitted += new EventHandler(Form1_SelectionChangeCommitted);
            cbKhachHang[index].KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        //Xử lý sự kiện nhấn Delete ở ComboBox:
        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            ComboBox combobox = (ComboBox)sender;
            int index = int.Parse(combobox.Name);       //Xác định ComboBox gây ra sự kiện
            if (e.KeyCode == Keys.Delete)
                XoaDuLieuComboBox(index);
        }

        //Xử lý sự kiện ComboBox thay đổi giá trị (xem thủ tục Tao_ComboBox):
        void Form1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox combobox = (ComboBox)sender;
            int index = int.Parse(combobox.Name);       //Xác định ComboBox gây ra sự kiện
            if (combobox.Text != "")                    //Nếu khác rỗng
            {
                int i;
                for (i = 0; i <= cbSoLuong; i++)        //Lặp, nếu dữ liệu trùng với 1 trong các ComboBox khác...
                    if (i != index && combobox.Text == cbKhachHang[i].Text) break;  //...thoát khỏi vòng lặp
                if (i <= cbSoLuong)     //Nếu tên khách hàng đc chọn trùng với các khách hàng khác
                    XoaDuLieuComboBox(index);
                else if (cbSoLuong == index && cbSoLuong < cbMax)   //Nếu ko trùng: nếu đang là ComboBox sau cùng, và nếu chưa đến giới hạn số ComboBox...
                    Tao_ComboBox(index + 1);    //...tạo ComboBox mới liền kề
            }
        }

        //Xóa những ComboBox ko cần thiết
        private void XoaDuLieuComboBox(int index)
        {
            int Tam = cbSoLuong;            //Remove và tạo lại ComboBox đó
            this.Controls.Remove(cbKhachHang[index]);
            Tao_ComboBox(index);
            cbSoLuong = Tam;

            int mark;
            for (mark = cbSoLuong; mark >= 0; mark--)
                if (cbKhachHang[mark].Text != "") break;
            if (mark + 2 <= cbSoLuong)
            {
                for (int j = mark + 2; j <= cbSoLuong; j++)
                    this.Controls.Remove(cbKhachHang[j]);
                cbSoLuong = mark + 1;
            }
        }

        //Vô hiệu hóa các controls và xóa dữ liệu
        private void VoHieuHoa()
        {
            for (int i = 0; i < cbMax; i++)                 //Loại bỏ tất cả các ComboBox
                if (cbKhachHang[i] != null)
                    this.Controls.Remove(cbKhachHang[i]);
            Tao_ComboBox(0);                                //Tạo lại ComboBox đầu tiên
            cbKhachHang[0].Enabled = false;                 
            richPrint.Clear();                              //Xóa dữ liệu trên RichTextBox (dùng để in hóa đơn)
            txtVAT.Text = "";
            txtVAT.Enabled = false;                         //Vô hiệu hóa các controls
            gridHangHoa.Enabled = false;
            btInHoaDon.Enabled = false;
            btLuuHoaDon.Enabled = false;
            btTaoHoaDon.Enabled = true;                     //Cho phép user tương tác với nút Tạo hóa đơn

            varSoHoaDon = 0;                                //Xóa biến lưa mã số hóa đơn
            for (int i = 0; i < 5; i++)                     //Xóa các biến lưu danh sách khách hàng đã chọn
                varKhachHang[i] = "";
            varTongTriGia = 0;                              //Xóa dữ liệu các thành phần khác
            txtTongTriGia.Text = varTongTriGia.ToString();
            varVAT = 0;
            varTongTienThue = 0;
            txtTongTienThue.Text = varTongTienThue.ToString();
            varTongCong = 0;
            txtTongCong.Text = varTongCong.ToString();
            for (int i = 0; i < 2; i++)                     //Xóa các biến lưu gtrị trên Grid Hàng hóa
                for (int j = 0; j < 100; j++)
                    varGridHH[i, j] = "";
            for (int i = 0; i < 100; i++)
            {
                varGridQuyCach[i] = "";
                varGridDonGia[i] = 0;
                varGridSoLuong[i] = 0;
                varGridTongCong[i] = 0;
            }
        }

        //Kích hoạt controls cho phép user tương tác
        private void KichHoat()
        {
            cbKhachHang[0].Enabled = true;  //Cho phép chọn khách hàng đầu tiên
            txtVAT.Enabled = true;          //Cho phép nhập TextBox VAT

            //Tao moi Grid hang hoa
            gridHangHoa.Enabled = false;       //Cho phép user tương tác với Grid hàng hóa
            gridHangHoa.Rows.Clear();
            gridHangHoa.Enabled = true;

            btInHoaDon.Enabled = false;         //Cho phép/vô hiệu các nút nhấn
            btLuuHoaDon.Enabled = true;
            btTaoHoaDon.Enabled = true;
        }
    }
}
