using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace ComProcess
{
    public class ComPort
    {
        SerialPort comPort;

        private string _baudRate = string.Empty;
        private string _stopBits = string.Empty;
        private string _dataBits = string.Empty;
        private string _parity = string.Empty;
        private string _portName = string.Empty;
        private bool _isText = true;

        public ComPort()
        {

        }
        public ComPort(string portName,string baudRate,string stopBits,string dataBits,string parity,bool isText)
        {
            //khởi tạo
            _baudRate = baudRate;
            _stopBits = stopBits;
            _dataBits = dataBits;
            _parity = parity;
            _portName = portName;
            _isText = isText;
            comPort.DataReceived+=new SerialDataReceivedEventHandler(comPort_DataReceived);
        }

        public void comPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //sự kiện nhận được dữ liệu
            if (_isText)
            {
            }
            else
            {
            }
        }

        public void Write(string msg)
        {
            if (comPort.IsOpen)
            {
                try
                {
                    comPort.Write(msg);
                }
                catch (Exception)
                {
                    throw;
                }
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
                catch (Exception)
                {      
                    throw;
                }
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

        #region Properties

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
        #endregion

        #region Method

        public void SendText(string Msg)
        {
            comPort.Write(Msg);
        }


        public List<string> getListPortName()
        {
            //lấy danh sách cổng COM có trên máy
            List<string> portNames=new List<string>();
            foreach(string c in SerialPort.GetPortNames())
            {
                portNames.Add(c);
            }
            return portNames;
        }

        public List<string> getListStopBit()
        {
            //lấy danh sách Stopbits
            List<string> stopBits = new List<string>();
            foreach(string c in Enum.GetNames(typeof(StopBits)))
            {
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
            string[] lstBaudRate = { "2400","4800","9600","19200","38400","57600","115200"};
            List<string> baudRates = new List<string>();
            foreach (string c in lstBaudRate)
            {
                baudRates.Add(c);
            }
            return baudRates;
        }

        public List<string> getListDataBits()
        {
            string[] lstDataBits = { "7","8","9"};
            List<string> dataBits=new List<string>();
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
        public void getDefaultSettings(ref string _BaudRate,ref string _StopBit,ref string _DataBit,ref string _Parity)
        {
            _baudRate = "9600";
            _StopBit = "None";
            _DataBit = "8";
            _parity = "None";
        }

        #endregion
    }
}
