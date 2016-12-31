using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace MemorizeGame
{
    public partial class Form1 : Form
    {
        PictureBox[] pic = new PictureBox[16];
        int[] img = new int[16];
        public int x = 16, y = 0;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int j = 0; j < 16; j++)
            {
                pic[j] = new PictureBox();
                pic[j].Image = imageList1.Images[8];
                pic[j].Location = new System.Drawing.Point(10 + (j%4)*60, 10 + (j/4)*80);
                pic[j].Size = new System.Drawing.Size(50, 70);
                pic[j].Name = j.ToString();
                this.Controls.Add(pic[j]);
                pic[j].Enabled = false;
                pic[j].Click += new EventHandler(Form1_Click);  
            }
        }
        void Form1_Click(object sender, EventArgs e)
        {
            PictureBox picture = (PictureBox)sender;
            if (x == 16)
            {
                x = int.Parse(picture.Name);
                pic[x].Image = imageList1.Images[img[x]];
                pic[x].Enabled = false;
            }
            else
            {
                y = int.Parse(picture.Name);
                for (int j = 0; j <= 15; j++)
                    pic[j].Enabled = false;
                pic[y].Image = imageList1.Images[img[y]];
                timer2.Enabled = true;
            }
        }
        public void Randomize()
        {
            Random rand = new Random();
            byte[] so = { 0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7 };
            int lap, index;
            int k = 15, i;
            for (i = 0; i <= 15; i++)
            {
                index = rand.Next(0, k--);
                img[i] = so[index];
                for (lap = index; lap <= k; lap++)
                    so[lap] = so[lap + 1];
            }
       }
        private void timer2_Tick(object sender, EventArgs e)
        {
            int j;
            timer2.Enabled = false;
            if (img[x] == img[y])
            {
                pic[x].Visible = pic[y].Visible = false;
                x = 16;
                for (j = 0; j <= 15; j++)
                {
                    if (pic[j].Visible == true)
                        break;     
                }
                if (j == 16)
                {
                    timer1.Enabled = false;
                    MessageBox.Show("You Win!");
                }
            }
            else
            {
                pic[x].Image = pic[y].Image = imageList1.Images[8];
                x = 16;
            }
            for (j = 0; j <= 15; j++)
            {
                pic[j].Enabled = true;
            }
        }
        private void btn_Start_Click(object sender, EventArgs e)
        {
            Randomize();
            for (int j = 0; j <= 15; j++)
            {
                pic[j].Enabled = true;
                pic[j].Visible = true;
                pic[j].Image = imageList1.Images[8];
                x = 16;
                y = 0;
                timer2.Enabled = false;
                timer1.Enabled = true;
                progressBar1.Value = progressBar1.Maximum;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value != progressBar1.Minimum)
                progressBar1.Value--;
            else
            {
                timer1.Enabled = false;
                timer2.Enabled = false;
                for (int j = 0; j <= 15; j++)
                    pic[j].Enabled = false;
                MessageBox.Show("Game over!");
            }
        }        
    }
}
