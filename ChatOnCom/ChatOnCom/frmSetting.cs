using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChatOnCom
{
    public partial class frmSetting : Form
    {
        ComPort Comport;

        public frmSetting()
        {
            InitializeComponent();
        }
        private void frmSetting_Load(object sender, EventArgs e)
        {
            try
            {
                //load constructor
                Comport = new ComPort();
                // Load danh sach ten
                List<string> portNames = Comport.getListPortName();
                if (portNames.Count > 0)
                {
                    foreach (string c in portNames)
                    {
                        cobPortName.Items.Add(c);
                    }
                    cobPortName.SelectedItem= Properties.Settings.Default.PortName;
                }
                else
                {
                    if (MessageBox.Show("Không tìm thấy cổng COM trên máy tính hiện tại. Chương trình không thể hoạt động!\nBạn có muốn tắt chương trình không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        Application.Exit();
                    }
                }

                //Load stopbit
                foreach (string c in Comport.getListStopBit())
                {
                    cobStopbits.Items.Add(c);
                }
                cobStopbits.SelectedItem = Properties.Settings.Default.StopBits;

                //Load Parity
                foreach (string c in Comport.getListParity())
                {
                    cobParity.Items.Add(c);
                }
                cobParity.SelectedItem = Properties.Settings.Default.Parity;

                //Load BaudRate
                foreach (string c in Comport.getListBaudrate())
                {
                    cobBaudRate.Items.Add(c);
                }
                cobBaudRate.SelectedItem = Properties.Settings.Default.BaudRate;

                //Load Databits
                foreach (string c in Comport.getListDataBits())
                {
                    cobDataBits.Items.Add(c);
                }
                cobDataBits.SelectedItem = Properties.Settings.Default.DataBits;
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi khi lấy thông tin thiết lập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.PortName = cobPortName.SelectedItem.ToString();
                Properties.Settings.Default.BaudRate = cobBaudRate.SelectedItem.ToString();
                Properties.Settings.Default.Parity = cobParity.SelectedItem.ToString();
                Properties.Settings.Default.DataBits = cobDataBits.SelectedItem.ToString();
                Properties.Settings.Default.StopBits = cobStopbits.SelectedItem.ToString();
                Properties.Settings.Default.Save();
                if (MessageBox.Show("Thông tin thiết lập đã được lưu lại!\n Bạn có muốn đóng cửa sổ thiết lập?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu thông tin thiết lập!\n" + ex.Message,"Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDefaultSetting_Click(object sender, EventArgs e)
        {
            try
            {
                cobBaudRate.SelectedItem = ((int)ComPort.DefaultSettings.BaudRate).ToString();
                cobStopbits.SelectedItem = ComPort.DefaultSettings.None.ToString();
                cobParity.SelectedItem = ComPort.DefaultSettings.None.ToString();
                cobDataBits.SelectedItem = ((int)ComPort.DefaultSettings.DataBit).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy thông tin thiết lập mặc định!\n" + ex.Message,"Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
