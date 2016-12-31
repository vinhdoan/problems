using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;
using MymatrixNET;

namespace testMATLABdll
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MWArray res = null;
            MWArray size = (MWArray) 5;
            MymatrixNET.MymatrixNET test = new MymatrixNET.MymatrixNET();
            res = test.mymagic(size);
            MessageBox.Show(res.ToString());
        }
    }
}
