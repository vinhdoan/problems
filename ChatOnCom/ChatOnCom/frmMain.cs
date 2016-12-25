using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Text;


namespace ChatOnCom
{
    public partial class frmMain : Form
    {
        private ComPort comport;
        //private string bgImage = "";
        private TextProcess text;
        private bool Connected = false;
        //variable local
        private string FontColor = "#000000";
        private int FontSize = 4;
        private string FontName = "Tahoma";
        private bool Bold = false, Underline = false, Italic = false;

        public frmMain()
        {
            InitializeComponent();
            text = new TextProcess();
            cobFontSize.SelectedIndex = 0;
        }

        private void cấuHìnhKếtNốiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSetting frm = new frmSetting();
            frm.ShowDialog();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tácGiảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout frm = new frmAbout();
            frm.ShowDialog();
        }

        private void txtMsgInput_TextChanged(object sender, EventArgs e)
        {
            //

            if (string.IsNullOrEmpty(txtMsgInput.Text) || txtMsgInput.Text == "\r\n" || !Connected)
            {
                btnSend.Enabled = false;
                txtMsgInput.Text = "";
            }
            else
            {
                btnSend.Enabled = true;
                if (txtMsgInput.Text.Contains('\n'))
                    btnSend.PerformClick();
            }

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if(txtMsgInput.Text!="" && Connected)
            {
                //AddTextToPanel(txtMsgInput.Text);
                comport.AddMsgToList(txtMsgInput.Text, false);
                string msg = txtMsgInput.Text;
                msg = comport.ProcessMessageTransmit(msg, false);
                msg = "msg@@" + msg;
                byte[] buff = new byte[msg.Length];
                buff = Encoding.UTF8.GetBytes(msg);
                try
                {
                    comport.WriteBytes(buff, 0, buff.Length);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xảy ra lỗi khi kết nối:\n" + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    NgatKetNoi();
                }
                //panelMessage.DocumentText += text.ReplaceSmileURL(msg);
                txtMsgInput.Text = "";
                txtMsgInput.Focus();
            }          
        }

        private void MoKetNoi()
        {
            //Mở kết nối
            try
            {
                comport.PortName = Properties.Settings.Default.PortName;
                comport.BaudRate = Properties.Settings.Default.BaudRate;
                comport.StopBits = Properties.Settings.Default.StopBits;
                comport.DataBits = Properties.Settings.Default.DataBits;
                comport.Parity = Properties.Settings.Default.Parity;
                comport.comPortOpen();
                Connected = true;
                lblStatus.Text = "Kết nối đã được mở...";
            }
            catch (Exception ex)
            {
                Connected = false;
                MessageBox.Show("Xảy ra lỗi sau khi kết nối:\n" + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NgatKetNoi()
        {
            //đóng kết nối
            try
            {
                comport.comPortClose();
                Connected = false;
                lblStatus.Text = "Chưa kết nối...";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Xảy ra lỗi sau khi kết nối:\n" + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NgatKetNoi();
            }
        }
        private void kếtNốiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoKetNoi();
        }

        private void đónngKếtNốiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NgatKetNoi();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            comport = new ComPort(Properties.Settings.Default.PortName, Properties.Settings.Default.BaudRate, Properties.Settings.Default.StopBits, Properties.Settings.Default.DataBits, Properties.Settings.Default.Parity, true, panelMessage, proStatus,lblSendingStatus,btnAcceptSending,btnCancelSending,FontName,FontSize,FontColor,Bold,Italic,Underline);
            //Load FontFamily For Combobox
            InstalledFontCollection fontFamily = new InstalledFontCollection();
            foreach (FontFamily c in fontFamily.Families)
            {
                try
                {
                    cobFontName.Items.Add(c.Name);
                }
                catch
                {
                    //Bỏ qua nếu ko lỗi
                }
                finally
                {
                    cobFontName.SelectedItem = "Tahoma";
                }
            }
        }

        private void btnAttachFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Tất cả|*.*|Tệp tin nén RAR|*.rar|Tệp tin nén ZIP|*.zip";
            open.FileName = "";
            open.Multiselect = false;
            if (open.ShowDialog() == DialogResult.OK)
            {
                comport.FileName = open.FileName;
                int lastIndex = comport.FileName.LastIndexOf('\\');
                string shortFileName = comport.FileName.Substring(lastIndex + 1);

                comport.ResetAll();
                //chuyển file thành mảng byte
                FileStream file = new FileStream(open.FileName,FileMode.Open,FileAccess.Read);
                byte[] buff = new byte[file.Length];
                file.Read(buff, 0, (int)(file.Length));
                if (comport.BufferByteArr != null)
                    comport.BufferByteArr = null;
                comport.BufferByteArr = new byte[file.Length];
                comport.BufferByteArr = buff;
                comport.BytesToRead = (int)(file.Length);
                
                //proStatus.Maximum = (int)(file.Length);
                //proStatus.Value = 0;
                //proStatus.Visible = true;
                //gui yeu cau
                try
                {
                    comport.Write(string.Format("req-file:{0}:{1}", shortFileName, file.Length));
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Xảy ra lỗi khi kết nối:\n" + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    NgatKetNoi();
                }
                file.Close();
                file.Dispose();
                //File.WriteAllBytes("D:\\Tung.jpg", comport.BufferByteArr);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //truyền tệp tin
            byte[] test = new byte[123456];
            for (int i = 0; i < 123456; i++)
            {
                test[i] = i < 256 ? (byte)i : (byte)(i % 256);
            }
            if (comport.BufferByteArr != null)
                comport.BufferByteArr = null;
            comport.BufferByteArr = new byte[test.Length];
            comport.BufferByteArr = test;
            comport.BytesToRead = (int)(test.Length);
            comport.Count = 0;
            //proStatus.Maximum = (int)(file.Length);
            //proStatus.Value = 0;
            //proStatus.Visible = true;
            //gui yeu cau
            comport.Write(string.Format("req-file:{0}:{1}", "Test.txt", test.Length));
        }

        public void AddSmiles(string smile)
        {
            int currentCursor = txtMsgInput.SelectionStart;
            txtMsgInput.Text = txtMsgInput.Text.Insert(currentCursor, smile);
            txtMsgInput.SelectionStart = currentCursor + smile.Length;
        }

        private void btnSmiles_Click(object sender, EventArgs e)
        {
            Point Location = btnSmiles.PointToScreen(new Point(0, 0));
            frmSmiles f = new frmSmiles();
            f.DesktopLocation = new Point(Location.X, Location.Y - f.Height);
            f.getSmile += new frmSmiles.GetSmile(this.AddSmiles);
            f.Show();
        }

        private void btnColorChooser_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
            {
                FontColor = text.GetHexCodeColor(color.Color);
                btnColorChooser.BackColor = color.Color;
                comport.FontColor = text.GetHexCodeColor(color.Color);
            }
        }

        private void btnBold_Click(object sender, EventArgs e)
        {
            //Bold,UnderLine,Italic Button Click
            Button btn = (Button)sender;
            if (btn.Tag.ToString() == "bold" )
                Bold = true;
        }

        private void btnBold_Click_1(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            //btn.BackColor = Color.Red;
            if (btn.Tag.ToString() == "bold")
            {
                if (!Bold)
                {
                    Bold = true;
                    comport.Bold = true;
                    btn.BackColor = Color.DeepSkyBlue;
                }
                else
                {
                    Bold = false;
                    comport.Bold = false;
                    btn.BackColor = Color.LightSkyBlue;
                }

            }
            if (btn.Tag.ToString() == "italic")
            {
                if (!Italic)
                {
                    Italic = true;
                    comport.Italic = true;
                    btn.BackColor = Color.DeepSkyBlue;
                }
                else
                {
                    Italic = false;
                    comport.Italic = false;
                    btn.BackColor = Color.LightSkyBlue;
                }
            }
            if (btn.Tag.ToString() == "underline")
            {
                if (!Underline)
                {
                    Underline = true;
                    comport.UnderLine = true;
                    btn.BackColor = Color.DeepSkyBlue;
                }
                else
                {
                    Underline = false;
                    comport.UnderLine = false;
                    btn.BackColor = Color.LightSkyBlue;
                }
            }
        }

        private void cobFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FontSize = int.Parse(cobFontSize.Text.ToString());
                comport.FontSize = int.Parse(cobFontSize.Text.ToString()); 
            }
            catch
            {
                FontSize = 2;
            }
        }

        private void cobFontName_SelectedIndexChanged(object sender, EventArgs e)
        {
            FontName = cobFontName.SelectedItem.ToString();
            comport.FontName = cobFontName.SelectedItem.ToString();
        }

        private void bầuTrờiTìnhYêuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                comport.ChangeBackground(((ToolStripMenuItem)sender).Tag.ToString());
                comport.Write("change-bg:" + ((ToolStripMenuItem)sender).Tag.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xảy ra lỗi khi kết nối:\n" + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NgatKetNoi();
            }
            
        }

        private void cốLênEmYêuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //add flash
            try
            {
                string msg = text.GetFlashHTML(((ToolStripMenuItem)sender).Tag.ToString());
                comport.AddMsgFlash(msg);
                comport.Write("flash@@" + msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xảy ra lỗi khi kết nối:\n" + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NgatKetNoi();
            }
        }

        private void bkavcomvnToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
