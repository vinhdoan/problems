using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    
    public partial class Form1 : Form
    {
        const int BOX_SIZE = 40;
        const int BLOCK_D = 10;

        public static TextBox[,] box = new TextBox[9, 9];

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
           
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    int block = i / 3 * 3 + j / 3;
                    box[i, j] = new TextBox();
                    box[i, j].Font = new Font(box[i, j].Font.FontFamily, 20);
                    box[i, j].TextAlign = HorizontalAlignment.Center;
                    
                    box[i, j].MaxLength = 1;

                    box[i, j].Height = BOX_SIZE;
                    box[i, j].Width = BOX_SIZE;
                    box[i, j].Location = new System.Drawing.Point(
                        BLOCK_D + (block % 3) * (3 * BOX_SIZE + BLOCK_D) + (j % 3) * BOX_SIZE,
                        BLOCK_D + (block / 3) * (3 * BOX_SIZE + BLOCK_D) + (i % 3) * BOX_SIZE);
                    this.Controls.Add(box[i, j]);
                    box[i, j].KeyPress += Form1_KeyPress;

                }
            txtTime.Text = "";
            txtMemory.Text = "";
            txtHV.Text = "";
        }

        public void Func()
        {

        }

        void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dfs_Click(object sender, EventArgs e)
        {
            PreConfig("dfs");
            Stack<TextBox[,]> stack = new Stack<TextBox[,]>();
            TextBox[,] start_box = Algo.NewBox(box);
            stack.Push(start_box);
            string Result = Algo.DFSAlgo(stack, box, this, txtMemory);
            PostConfig();
            MessageBox.Show(Result);
        }

        private void bfs_Click(object sender, EventArgs e)
        {
            PreConfig("bfs");
            Queue<TextBox[,]> queue = new Queue<TextBox[,]>();
            TextBox[,] start_box = Algo.NewBox(box);
            queue.Enqueue(start_box);
            string Result = Algo.BFSAlgo(queue, box, this, txtMemory);
            PostConfig();
            MessageBox.Show(Result);
        }

        private void hillclimbing_Click(object sender, EventArgs e)
        {
            PreConfig("hc");
            bool[,] is_fixed = Algo.IsFixed(box);
            TextBox[,] start_box = Algo.NewBox(box);
            Algo.FillStartBox(ref start_box);
            Algo.CopyBox(start_box, box);
            this.Update();
            Algo.HillClimbingAlgo(start_box, Algo.HeuristicFunc(start_box), is_fixed, box, this, txtHV, txtMemory);
            PostConfig();
            MessageBox.Show("DONE");
        }

        private void hillclimbing2_Click(object sender, EventArgs e)
        {
            PreConfig("hc");
            bool[,] is_fixed = Algo.IsFixed(box);
            TextBox[,] start_box = Algo.NewBox(box);
            Algo.FillStartBox(ref start_box);
            Algo.CopyBox(start_box, box);
            this.Update();
            Algo.HillClimbingAlgo2(start_box, Algo.HeuristicFunc2(start_box), is_fixed, box, this, txtHV, txtMemory);
            PostConfig();
            MessageBox.Show("DONE");
        }

        private void FixNumbers()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (box[i, j].Text != "")
                        box[i, j].BackColor = Color.Gray;
        }

        private void ClearBox()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    box[i, j].Text = "";
                    box[i, j].BackColor = Color.White;
                }
            txtHV.Visible = false;
            txtMemory.Visible = false;
            txtTime.Visible = false;
            lblHV.Visible = false;
            lblMemory.Visible = false;
            lblTime.Visible = false;
        }

        private void selectInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearBox();
            Input.Select(box, selectInput.Text);
        }

        private void refreshbt_Click(object sender, EventArgs e)
        {
            ClearBox();
            selectInput.Text = "";
        }

        private void ShowStatistics(String s)
        {
            switch (s)
            {
                case "dfs":
                case "bfs":
                    txtHV.Visible = false;
                    txtMemory.Visible = true;
                    txtTime.Visible = true;
                    lblHV.Visible = false;
                    lblMemory.Visible = true;
                    lblTime.Visible = true;
                    break;
                case "hc":
                    txtHV.Visible = true;
                    txtMemory.Visible = true;
                    txtTime.Visible = true;
                    lblHV.Visible = true;
                    lblMemory.Visible = true;
                    lblTime.Visible = true;
                    break;
            }
        }

        private void PreConfig(String s)
        {
            FixNumbers();
            ShowStatistics(s);
            txtTime.Text = "";
            txtMemory.Text = "";
            txtHV.Text = "";
            Resources.sw.Restart();
        }

        private void PostConfig()
        {
            txtTime.Text = string.Format("{0:F2}", (float)Resources.sw.ElapsedMilliseconds/1000);
            Resources.sw.Stop();
            txtMemory.Text = Resources.memory.ToString();
            Resources.memory = 0;
        }

        

    }
}
