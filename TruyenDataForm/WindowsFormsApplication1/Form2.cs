using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        Form1 form_dangnhap = null;
        public Form2(string chuoi1, string chuoi2,Form1 form_truyen)
        {
            InitializeComponent();
            textBox1.Text = chuoi1;
            textBox2.Text = chuoi2;
            form_dangnhap = form_truyen;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            form_dangnhap.Enabled = true;
        }
    }
}
