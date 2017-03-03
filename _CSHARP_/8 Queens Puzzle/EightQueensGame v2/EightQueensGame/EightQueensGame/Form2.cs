using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EightQueensGame
{
    public partial class Form2 : Form
    {
        Form1 myParent = null;
        public Form2(Form1 myParent)
        {
            InitializeComponent();
            this.myParent = myParent;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            myParent.Enabled = true;
        }

    }
}
