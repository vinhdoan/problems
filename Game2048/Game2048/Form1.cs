using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game2048
{
    public partial class Form1 : Form
    {
        const int BOX_SIZE = 100;
        const int BLOCK_D = 20;

        public static TextBox[,] box = new TextBox[4, 4];
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Initialize();
        }

        private void Initialize()
        {

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    this.Height = 4 * BLOCK_D + 4 * BOX_SIZE;
                    this.Width = 2 * BLOCK_D + 4 * BOX_SIZE + 100;
                    box[i, j] = new TextBox();
                    box[i, j].Font = new Font(box[i, j].Font.FontFamily, 30);
                    box[i, j].Font = new Font(box[i, j].Font, FontStyle.Bold);
                    box[i, j].Multiline = true;
                    box[i, j].TextAlign = HorizontalAlignment.Center;
                    box[i, j].BackColor = Color.FromArgb(204, 192, 180);
                    box[i, j].Enabled = false;
                    box[i, j].TextAlign = HorizontalAlignment.Center;
                    box[i, j].Height = BOX_SIZE;
                    box[i, j].Width = BOX_SIZE;
                    box[i, j].Location = new System.Drawing.Point(
                        BLOCK_D + j * BOX_SIZE,
                        BLOCK_D + i * BOX_SIZE);
                    this.Controls.Add(box[i, j]);
                    box[i, j].TextChanged += Form1_TextChanged;
                }
            selectInput.Text = selectInput.Items[0].ToString();
            selectInput.TabStop = false;
        }

        void Form1_TextChanged(object sender, EventArgs e)
        {
            //Set background color according to the number
            TextBox textbox = (TextBox)sender;
            switch (textbox.Text)
            {
                case "":
                    textbox.BackColor = Color.FromArgb(204, 192, 180);
                    break;
                case "2":
                    textbox.BackColor = Color.FromArgb(238, 228, 218);
                    break;
                case "4":
                    textbox.BackColor = Color.FromArgb(237, 224, 195);
                    break;
                case "8":
                    textbox.BackColor = Color.FromArgb(254, 183, 113);
                    break;
                case "16":
                    textbox.BackColor = Color.FromArgb(255, 162, 89);
                    break;
                case "32":
                    textbox.BackColor = Color.FromArgb(255, 143, 87);
                    break;
                case "64":
                    textbox.BackColor = Color.FromArgb(255, 117, 49);
                    break;
                case "128":
                    textbox.BackColor = Color.FromArgb(241, 203, 102);
                    break;
                case "256":
                    textbox.BackColor = Color.FromArgb(245, 198, 86);
                    break;
                case "512":
                    textbox.BackColor = Color.FromArgb(245, 195, 65);
                    break;
                case "1024":
                    textbox.BackColor = Color.FromArgb(247, 190, 48);
                    break;
                case "2048":
                    textbox.BackColor = Color.FromArgb(247, 187, 30);
                    break;
                case "4096":
                    textbox.BackColor = Color.FromArgb(60, 58, 49);
                    break;
                case "8192":
                    textbox.BackColor = Color.FromArgb(66, 61, 59);
                    break;
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Resources.ai_mode && !Resources.stop)
            {
                //Resources.stop = true;
                switch (e.KeyChar)
                {
                    case 'w':
                    case 'W':
                        Gameplay.Move(box, "UP");
                        break;
                    case 's':
                    case 'S':
                        Gameplay.Move(box, "DOWN");
                        break;
                    case 'a':
                    case 'A':
                        Gameplay.Move(box, "LEFT");
                        break;
                    case 'd':
                    case 'D':
                        Gameplay.Move(box, "RIGHT");
                        break;
                }
                txtScore.Text = Resources.score.ToString();
                if(Resources.stop)
                    MessageBox.Show("YOU LOSE");
            }
        }

       

        private void ClearBox()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    box[i, j].Text = "";
                }
        }

        private void selectInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearBox();
            Input.Select(box, selectInput.Text);
            Resources.score = 0;
            txtScore.Text = Resources.score.ToString();
        }

        private void dfs_Click(object sender, EventArgs e)
        {
            Resources.ai_mode = true;
            AI.DFSAlgo(box, this, txtScore);
        }

        private void bfs_Click(object sender, EventArgs e)
        {
            Resources.ai_mode = true;
            AI.BFSAlgo(box, this, txtScore);
        }

        private void hc_Click(object sender, EventArgs e)
        {
            Resources.ai_mode = true;
            AI.HCAlgo(box, this, txtScore);
        }
    }
}
