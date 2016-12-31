using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
using System.Collections;

namespace ChatOnCom
{
    public class ComPort
    {
        SerialPort comPort;
        List<string> ListMessages;
        //TextProcess textProcess = new TextProcess();
        private string _baudRate = string.Empty;
        private string _stopBits = string.Empty;
        private string _dataBits = string.Empty;
        private string _parity = string.Empty;
        private string _portName = string.Empty;

        private bool _isText = true;
        private System.Windows.Forms.WebBrowser msgPanel;
        private ToolStripLabel lblSendingStatus;
        private ToolStripSplitButton btnYes;
        private ToolStripSplitButton btnCancel;
        private ToolStripProgressBar proStatus;
        private Timer timer,timer2;
        private int CountReceived=0;
        //variable for sending file
        private string filename;
        private byte[] buffer;
        private int _BytesToRead;
        private int count=0;
        private string CurrentCheckSumNumber = "0";
        private TextProcess text;
        private int CoutnDebug = 0;
        private int CountWait = 0;

        //tùy chỉnh
        private string _FontName;
        private int _FontSize;
        private string _FontColor;
        private bool _Bold;
        private bool _Underline;
        private bool _Italic;

        #region Constructor

        public ComPort()
        {

        }
        public ComPort(string portName, string baudRate, string stopBits, string dataBits, string parity, bool isText,System.Windows.Forms.WebBrowser MessagePanel,ToolStripProgressBar ProStatus,ToolStripLabel labelSendingStatus,ToolStripSplitButton buttonyes,ToolStripSplitButton buttonCancel,string fontName,int fontSize,string fontColor,bool bold,bool italic,bool underline)
        {
            //
            //khởi tạo
            _baudRate = baudRate;
            _stopBits = stopBits;
            _dataBits = dataBits;
            _parity = parity;
            _portName = portName;
            _isText = isText;

            //khởi tạo Document Text tự cuộn màn hình
            msgPanel = MessagePanel;
            ListMessages = new List<string>();
            ListMessages.Add(GetScriptScrollPage());
            ListMessages.Add(GetPageStyle());
            ListMessages.Add("<body onLoad=\"pageScroll()\">");


            //Khở tạo các control
            lblSendingStatus = labelSendingStatus;
            proStatus = ProStatus;
            btnYes = buttonyes;
            btnCancel = buttonCancel;

            comPort = new SerialPort();
            //comPort = new SerialPort(_portName, int.Parse(_baudRate), (Parity)Enum.Parse(typeof(Parity), _parity), int.Parse(_dataBits), (StopBits)Enum.Parse(typeof(StopBits), _stopBits));
            //comPort.RtsEnable = true;
            comPort.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);
            comPort.ErrorReceived+=new SerialErrorReceivedEventHandler(comPort_ErrorReceived);
            text = new TextProcess();
            timer = new Timer();
            timer.Interval = 100;
            timer.Enabled = false;
            //timer.Tick+=new EventHandler(timer_Tick);

            //Timer dieu khien nha
            timer2 = new Timer();
            timer2.Interval = 100;
            timer2.Enabled = false;
            //timer2.Tick+=new EventHandler(timer2_Tick);

            //tùy chỉnh hiển thị
            _FontName = fontName;
            _FontSize = fontSize;
            _FontColor = fontColor;
            _Bold = bold;
            _Italic = italic;
            _Underline = underline;

        }
        #endregion

        #region Properties
        public string FileName
        {
            set { filename = value; }
            get { return filename; }
        }

        public int Count
        {
            set { count = value; }
            get { return count; }
        }

        public byte[] BufferByteArr
        {
            set { buffer = value; }
            get { return buffer; }
        }

        public int BytesToRead
        {
            set { _BytesToRead = value; }
            get { return _BytesToRead; }
        }

        public bool IsText
        {
            set { _isText = value; }
            get { return _isText; }
        }
        public string BaudRate
        {
            set { _baudRate = value; }
            get { return _baudRate; }
        }
        public string StopBits
        {
            set { _stopBits = value; }
            get { return _stopBits; }
        }
        public string DataBits
        {
            set { _dataBits = value; }
            get { return _dataBits; }
        }
        public string Parity
        {
            set { _parity = value; }
            get { return _parity; }
        }
        public string PortName
        {
            set { _portName = value; }
        }

        public string FontName
        {
            set { _FontName = value; }
        }

        public int FontSize
        {
            set{_FontSize=value;}
        }

        public string FontColor
        {
            set{_FontColor=value;}
        }

        public bool Bold
        {
            set{_Bold=value;}
        }

        public bool Italic
        {
            set{_Italic=value;}
        }

        public bool UnderLine
        {
            set{_Underline=value;}
        }

        #endregion

        #region Events
        //private void timer_Tick(object sender, EventArgs e)
        //{
        //    //timer kiểm soát quá trình gửi
        //    CountWait += 1;
        //    if (CountWait > 20)
        //    {
        //        if (count < _BytesToRead && buffer != null)
        //        {
        //            WriteACK("req-send:" + count.ToString() + ":"); //thông báo cho bên nhận biết nhận 4K tiếp theo
        //        }
        //    }
        //}

        //private void timer2_Tick(object sender, EventArgs e)
        //{
        //    //Timer kiểm soát quá trình nhận
        //    //sau 2 giây ko gửi thì báo lại là dã nhận để bên gửi tiếp tục gửi
        //    CountWait += 1;
        //    if (CountWait > 20)
        //    {
        //        WriteACK("ack-received:" + CheckSum(buffer, CountReceived, CountReceived + 4096) + ":");
        //    }
        //}
        public void AddSendMessage(string msg)
        {
            Write(msg);
            ListMessages.Add(msg);
        }

        public void comPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //tmpMsg1 = Encoding.UTF8.GetString(buff);
            //sự kiện nhận được dữ liệu
            //MessageBox.Show(tmpMsg1);
            if (_isText)
            {
                int length = comPort.BytesToRead;
                byte[] buff = new byte[length];
                comPort.Read(buff, 0, length);
                string tmpMsg;
                tmpMsg = Encoding.UTF8.GetString(buff);
                //msgPanel.DocumentText = tmpMsg;
                if (tmpMsg.StartsWith("msg@@") && _isText)
                {
                    //nhận và hiển thị nội dung chat
                    int startIndex = tmpMsg.IndexOf(':');
                    string Msg = tmpMsg.Substring(startIndex + 1);
                    //ListMessages.Add(Msg); //thêm vào danh sách tin
                    AddMsgToList(Msg,true);
                    
                }
                if (tmpMsg.StartsWith("flash@@"))
                {
                    //nhận và hiển thị nội dung flash
                    int startIndex = tmpMsg.IndexOf("@@");
                    string Msg = tmpMsg.Substring(startIndex + 2);
                    //ListMessages.Add(Msg); //thêm vào danh sách tin
                    AddMsgFlash(Msg);
                }
                if (tmpMsg.StartsWith("change-bg"))
                {
                    //Thay đổi hình nền
                    int startIndex = tmpMsg.IndexOf(':');
                    string bgURL = tmpMsg.Substring(startIndex + 1);
                    ChangeBackground(bgURL);
                }
                if (tmpMsg.StartsWith("req-file") && _isText)
                {
                    //Yều cầu gửi tệp tin
                    int startIndex = tmpMsg.IndexOf(':');
                    int stopIndex = tmpMsg.LastIndexOf(':');
                    //lấy tên tệp tin yêu cầu gửi
                    string FileName = tmpMsg.Substring(startIndex + 1,stopIndex-startIndex-1).Replace("<br>","");
                    int RqByteToRead = int.Parse(tmpMsg.Substring(stopIndex + 1));
                    if (MessageBox.Show(string.Format("Bạn nhận được lời mời nhận tệp tin: {0}.\n Bạn có muốn nhận tệp tin này không?",FileName), "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //SaveFileDialog save = new SaveFileDialog();
                        //save.Filter = FileName.Substring(FileName.LastIndexOf('.')).ToUpper() + "|*." + FileName.Substring(FileName.LastIndexOf('.')).ToLower();
                        //if (save.ShowDialog() == DialogResult.OK)
                        //{
                            //gửi trả lời đòngu ý 
                            //Hiển thị trạng thái gửi tệp tin
                            //proStatus.Visible = true;
                            //proStatus.Maximum = RqByteToRead;
                            //proStatus.Value = 0;
                            //xác định định dạng tệp tin
                            int lastIndex = FileName.LastIndexOf('.');
                            string fileType = FileName.Substring(lastIndex + 1);

                            //SaveFileDialog save = new SaveFileDialog();
                            //save.Filter = string.Format("Tệp tin {0}|*.{1}|Tất cả|*.*", fileType.ToUpper(), fileType);
                            ////save.Filter = "Tất cả|*.*";
                            //save.FileName = FileName;
                            //if (save.ShowDialog() == DialogResult.OK)
                            //{
                            //filename = save.FileName;
                            filename = @"D:\" + FileName;
                            //filename = save.FileName;
                            _BytesToRead = RqByteToRead; //lưu lại số byte cần nhận
                            if (buffer != null)
                                buffer = null;
                            buffer = new byte[_BytesToRead];
                            _isText = false;
                            CountReceived = 0;
                            //string ackMsg = "ack-file";
                            //Write(ackMsg);
                            WriteACK("ack-file");
                            //byte[] buffSend = new byte[ackMsg.Length];
                            //buffSend = Encoding.UTF8.GetBytes(ackMsg);
                            //WriteBytes(buffSend, 0, ackMsg.Length);
                            //}
                        //}
                    }

                }
                if (tmpMsg.StartsWith("req-send"))
                {
                    //CountWait = 0;
                    //timer2.Enabled = false;

                    int startIndex = tmpMsg.IndexOf(':');
                    int stopIndex = tmpMsg.LastIndexOf(':');
                    int RequestSend = int.Parse(tmpMsg.Substring(startIndex + 1, stopIndex - startIndex - 1));
                    CountReceived = RequestSend;
                    WriteACK("ack-ready");
                    _isText = false;

                    CountWait = 0;
                    timer2.Enabled = true;
                }
                if (tmpMsg.StartsWith("ack-file"))
                {
                    //Nếu nhận được tín hiệu đồng ý cho truyền thì gửi tín hiệu bắt đầu gửi
                    WriteACK("req-send:0:");
                    //comPort.Write(buffer, 0, 4096);
                }
                if(tmpMsg.StartsWith("ack-ready"))
                {
                    CountWait = 0;
                    timer.Enabled = false; //ngừng bộ đếm sau khi nhận được tín hiệu phản hồi đã nhân được file
                    //tín hiệu sẵn sàng nhận tin
                    if ((_BytesToRead - count) > 4096)
                    {
                        comPort.Write(buffer, count, 4096);
                        CurrentCheckSumNumber = CheckSum(buffer, count, count + 4096);
                    }
                    else
                    {
                        WriteBytes(buffer, count, _BytesToRead - count);
                        CurrentCheckSumNumber = CheckSum(buffer, count, _BytesToRead);
                    }
                    timer.Enabled = true;

                }
                if (tmpMsg.StartsWith("ack-received"))
                {
                    timer.Enabled = false;
                    CountWait = 0;


                    //Nhận tín hiệu trả lời ACK-FILE
                    int Length = 4096;
                    //lấy thông tin checksum
                    //mẫu dạng: ack-received:123456:
                    int startIndex = tmpMsg.IndexOf(':');
                    int stopIndex = tmpMsg.LastIndexOf(':');
                    string checkSumNumber = tmpMsg.Substring(startIndex + 1, stopIndex - startIndex - 1);

                    CoutnDebug++;
                    if (checkSumNumber == CurrentCheckSumNumber)
                    {
                        count += Length;
                        if (count > _BytesToRead)
                        {
                            WriteACK("ack-finish");
                            count = 0;
                            CountReceived = 0;
                            //File.WriteAllBytes("D:\\ABCD.JPG", buffer);
                            buffer = null; //Hủy nội dung đệm
                            //log.Close();
                            MessageBox.Show("Tệp tin đã được truyền thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    if (count < _BytesToRead && buffer!=null)
                    {
                        WriteACK("req-send:" + count.ToString() + ":"); //thông báo cho bên nhận biết nhận 4K tiếp theo
                    }

                    timer.Enabled = true;
                }
                if (tmpMsg.StartsWith("ack-finish"))
                {
                    CountWait = 0;
                    timer.Enabled = false;
                    //Write("ack-file-Received\r\n");
                    //MessageBox.Show("CountReceived: " + (CountReceived + (_BytesToRead - CountReceived)).ToString());
                    //MessageBox.Show(string.Format("Buffer[{0}]={1}", _BytesToRead-1, buffer[_BytesToRead-1]));
                    File.WriteAllBytes(filename, buffer);
                    //log.Close();
                    _isText = true;
                    CountReceived = 0;
                    count = 0;
                    buffer = null;
                    if (MessageBox.Show("Tệp tin đã được nhận thành công!\nBạn có muốn mở tệp tin vừa nhận không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(filename);
                    }
                }
            }
            else
            {
                //nhận tệp tin
                //proStatus.Maximum = _BytesToRead;
                //proStatus.Visible = true;
                int Length = 4096;//comPort.ReadBufferSize;
                lblSendingStatus.Text = CountReceived.ToString();
                //proStatus.Value = CountReceived;

                //if (CountReceived - Length < _BytesToRead)
                //{
                CoutnDebug++;
                if ((_BytesToRead - CountReceived) > Length)
                {
                    //đọc 4K
                    comPort.Read(buffer, CountReceived, Length);
                    //Write("ack-file:" + CountReceived.ToString());
                    //Write("ack-file");
                    WriteACK("ack-received:" + CheckSum(buffer, CountReceived, CountReceived + 4096) + ":");

                    timer2.Enabled = true;

                    //comPort.DiscardInBuffer();
                    //Write("NoThing");
                    _isText = true;
                    //Write("ack-file:" + CountReceived.ToString());
                    //Write("NoThing");
                }
                else
                {
                    CountWait = 0;
                    timer2.Enabled = false;

                    comPort.Read(buffer, CountReceived, _BytesToRead - CountReceived);
                    WriteACK("ack-received:" + CheckSum(buffer, CountReceived, _BytesToRead) + ":");
                    _isText = true;
                }
            }
        }


        public void comPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            //nếu truyền lỗi
            MessageBox.Show("Có lỗi truyền tin");
            comPort.Close();
            CountReceived = 0;
            buffer = null;
            lblSendingStatus.Text = "Đã có lỗi truyền tin!";
        }

        public string CheckSum(byte[] ArrToCheck,int start,int stop)
        {

            long Sum=0;
            for (int i = start; i < stop; i++)
            {
                Sum += ArrToCheck[i];
            }
            return Sum.ToString();
        }

        public void WriteACK(int StartSend, int StopSend)
        {
            //gửi tin ACK với CheckSum

        }

        public byte[] GetFullBufferMessage(string str)
        {
            byte[] arr = new byte[4096];
            byte[] strByteArr = new byte[str.Length];
            strByteArr = Encoding.UTF8.GetBytes(str);
            for (int i = 0; i < str.Length; i++)
            {
                arr[i] = strByteArr[i];
            }
            return arr;
        }

        public void WriteACK(string msg)
        {
            if (comPort.IsOpen)
            {
                comPort.Write(GetFullBufferMessage(msg), 0, 4096);
            }
            else
            {
                MessageBox.Show("Kết nối chưa được mở!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void Write(string msg)
        {
            if (comPort.IsOpen)
            {
                try
                {
                    byte[] buff = new byte[msg.Length];
                    buff = Encoding.UTF8.GetBytes(msg);
                    comPort.Write(buff, 0, msg.Length);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                MessageBox.Show("Kết nối chưa được mở!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void WriteBytes(byte[] buff, int offset, int length)
        {
            if (comPort.IsOpen)
            {
                try
                {
                    comPort.Write(buff, offset, length);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("WriteBytes" + ex.Message);
                    //throw;
                }
            }
            else
            {
                MessageBox.Show("Kết nối chưa được mở!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public string ReadString()
        {
            if (comPort.IsOpen)
            {
                return comPort.ReadExisting();
            }
            return null;
        }

        public string ReadStringFromBytes()
        {
            if (comPort.IsOpen)
            {
                int length = comPort.BytesToRead;
                byte[] buff = new byte[length];
                comPort.Read(buff, 0, length);
                string tmp = Encoding.UTF8.GetString(buff);
                return tmp;
            }
            return null;
        }

        public byte[] ReadBytes()
        {
            if (comPort.IsOpen)
            {
                try
                {
                    int length = comPort.BytesToRead;
                    byte[] buff = new byte[length];
                    comPort.Read(buff, 0, length);
                    return buff;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return null;
        }

        public void ResetAll()
        {
            CountWait = 0;
            count = 0;
            _BytesToRead = 0;
        }

        #endregion


        #region Method

        #region Comport Methods
        public void comPortOpen()
        {
            try
            {
                if (!comPort.IsOpen)
                {
                    comPort.PortName = _portName;
                    comPort.BaudRate = int.Parse(_baudRate);
                    comPort.Parity = (Parity)Enum.Parse(typeof(Parity), _parity);
                    comPort.DataBits = int.Parse(_dataBits);
                    comPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), _stopBits);
                    comPort.Open();
                }
                else
                {
                    if (MessageBox.Show("Port đã sẵn sàng mở!\nBạn cần đóng nó trước khi mở trở lại!\nBạn có muốn thực hiện đóng kết nối hiện tại và mở lại kết nối này hay không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        //Ngắt và mở lại kết nối
                        comPort.Close();
                        comPortOpen();
                    }
                }
            }
            catch (Exception ex)
            {
                if (MessageBox.Show("Lỗi trong khi mở kết nối!\n" + ex.Message + "\nBạn có muốn mở cửa sổ cấu hình kết nối?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    frmSetting f = new frmSetting();
                    f.ShowDialog();
                }
            }
        }

        public void comPortClose()
        {
            try
            {
                //Close the Port
                comPort.Close();
                buffer = null;
                count = 0;
                CountReceived = 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SendText(string Msg)
        {
            comPort.Write(Msg);
        }


        public List<string> getListPortName()
        {
            //lấy danh sách cổng COM có trên máy
            List<string> portNames = new List<string>();
            foreach (string c in SerialPort.GetPortNames())
            {
                portNames.Add(c);
            }
            return portNames;
        }

        public List<string> getListStopBit()
        {
            //lấy danh sách Stopbits
            List<string> stopBits = new List<string>();
            foreach (string c in Enum.GetNames(typeof(StopBits)))
            {
                if (c != "None")
                    stopBits.Add(c);
            }
            return stopBits;
        }

        public List<string> getListParity()
        {
            //lấy danh sách Parity
            List<string> parity = new List<string>();
            foreach (string c in Enum.GetNames(typeof(Parity)))
            {
                parity.Add(c);
            }
            return parity;
        }

        public List<string> getListBaudrate()
        {
            string[] lstBaudRate = { "2400", "4800", "9600", "19200", "38400", "57600", "115200" };
            List<string> baudRates = new List<string>();
            foreach (string c in lstBaudRate)
            {
                baudRates.Add(c);
            }
            return baudRates;
        }

        public List<string> getListDataBits()
        {
            string[] lstDataBits = { "7", "8", "9" };
            List<string> dataBits = new List<string>();
            foreach (string c in lstDataBits)
            {
                dataBits.Add(c);
            }
            return dataBits;
        }


        public enum DefaultSettings
        {
            COM1,
            BaudRate = 9600,
            None,
            DataBit = 8
        }
        public void getDefaultSettings(ref string _BaudRate, ref string _StopBit, ref string _DataBit, ref string _Parity)
        {
            _baudRate = "9600";
            _StopBit = "None";
            _DataBit = "8";
            _parity = "None";
        }
        #endregion

        #region Display Methods
        public void AddMsgToList(string msg,bool receive)
        {
            ListMessages.Add(ProcessMessageTransmit(msg, receive));
            DisplayListMessage();
        }

        public void AddMsgFlash(string msg)
        {
            ListMessages.Add(msg);
            DisplayListMessage();
        }

        public void DisplayListMessage()
        {
            string currentAuthor = "...";
            string document = "";
            foreach (string s in ListMessages)
            {
                string msg=text.ReplaceSmileURL(s);
                if (msg.IndexOf(':') != -1)
                {
                    if (msg.StartsWith(currentAuthor))
                    {
                        int startIndex = msg.IndexOf("<font color");
                        int stopIndex= msg.IndexOf("</font>");
                        if (startIndex != -1 && stopIndex != -1)
                            msg = msg.Remove(startIndex, stopIndex - startIndex + 7);
                        msg = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + msg;
                    }
                    else
                    {
                        currentAuthor = s.Substring(0, s.IndexOf(':'));
                    }
                }
                document += msg;
            }
            msgPanel.DocumentText = document;
        }

        public string ProcessMessageReceived(string msg)
        {
            return "";
        }

        public string ProcessMessageTransmit(string Msg, bool receive)
        {
            string tmp = Msg;
            //tmp = text.ReplaceSmileURL(tmp);
            //Chèn định dạng thẻ Font
            if (!receive)
            {
                //nếu xử lý tin gửi đi
                tmp = tmp.Insert(0, text.GetFontHTMLFormat(_FontName, _FontColor, _FontSize));
                tmp = tmp.Insert(tmp.Length, "</font>");

                if (_Bold)
                {
                    tmp = tmp.Insert(0, "<b>");
                    tmp = tmp.Insert(tmp.Length, "</b>");
                }
                if (_Underline)
                {
                    tmp = tmp.Insert(0, "<u>");
                    tmp = tmp.Insert(tmp.Length, "</u>");
                }
                if (_Italic)
                {
                    tmp = tmp.Insert(0, "<i>");
                    tmp = tmp.Insert(tmp.Length, "</i>");
                }
                tmp = "<font color = #5F04B4 face = \"Tahoma\" size=2><b>Tôi nói: </b></font>" + tmp + "<br>";
            }
            else
            {
                tmp = tmp.Insert(0, "<font color = #FF0080 face = \"Tahoma\" size=2><b>Bên kia nói: </b></font>");
            }     
            return tmp;
        }

        private string GetPageStyle()
        {
            return "<style> body { background-image: url(image.gif); background-repeat: repeat-x,repeat-y; background-attachment: fixed;} </style></head> ";
        }

        private string GetScriptScrollPage()
        {
            //hàm cuộn màn hình
            return "<head><script>" +
            "function pageScroll() { " +
            "window.scrollBy(0,1000000);}" +
            "</script>";
        }

        public void ChangeBackground(string image)
        {
            string BgURL = text.GetImageURL("Background\\" + image);
            int startIndex = ListMessages[1].IndexOf("url");
            int count = ListMessages[1].IndexOf(");") - startIndex -4;
            ListMessages[1] = ListMessages[1].Remove(startIndex + 4, count);
            ListMessages[1] = ListMessages[1].Insert(startIndex+4, BgURL);
            DisplayListMessage();
        }

        #endregion

        #endregion

    }
}
