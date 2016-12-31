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
    public partial class Form1 : Form
    {
        string s = "Loading";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            timer1.Interval = 500;
            button1.Text = "Load";
            label1.Text = s;
            label1.Visible = false;
            label2.Text = progressBar1.Value.ToString() + @"%";
            label2.Visible = false;
            progressBar1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            label2.Visible = true;
            progressBar1.Visible = true;
            timer1.Enabled = true;
            button1.Enabled=false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (s == "Loading...")
                s = "Loading";
            else
                s += @".";
            label1.Text = s;
            if (progressBar1.Value != 100)
            {
                progressBar1.Value += 10;
                label2.Text = progressBar1.Value.ToString() + @"%";
            }
            else
            {
                
                timer1.Enabled = false;
                button1.Enabled=true;
                label1.Visible = false;
                label2.Visible = false;
                progressBar1.Visible = false;
                progressBar1.Value = progressBar1.Minimum;
            }

                
        }
    }
}
