using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Microsoft.VisualBasic;

namespace EightQueensGame
{
    public partial class Form1 : Form
    {
        string appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        PictureBox[,] pic = new PictureBox[25,25];
        int x, y, n = 4;
        int[,] state = new int[25, 25];
        int[] solution = new int[25];
        bool end = false;
                        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initials();           
        }

        void initials()
        {
            int size = 600 / n;
            imageList1.ImageSize = new Size(size, size);
            for(int k=0;k<=5;k++)
            {
/*C1*/          //string link=appPath+@"\Queen\"+k.ToString()+@".jpg";
                //imageList1.Images.Add(Image.FromFile(link));
/*C2*/          imageList1.Images.Add(imageList2.Images[k]);
            }
            for (int k = 0; k < n; k++)
                solution[k] = -1;
            
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    pic[i, j] = new PictureBox();
                    state[i, j] = (i + j) % 2;
                    pic[i, j].Image = imageList1.Images[state[i, j]];
                    pic[i, j].Size = new System.Drawing.Size(size, size);
                    pic[i, j].Location = new System.Drawing.Point(10 + size * j, 30 + size * i);
                    pic[i, j].Name = (n*i+j).ToString();
                    this.Controls.Add(pic[i,j]);
                    pic[i, j].Enabled = true;
                    pic[i, j].Click += new EventHandler(Form1_Click);
                }
            }
            saveToolStripMenuItem.Enabled = true;
        }


        void Form1_Click(object sender, EventArgs e)
        {
            PictureBox picture = (PictureBox)sender;
            x = int.Parse(picture.Name) / n;
            y = int.Parse(picture.Name) % n;
            int k;
            if (state[x, y] <= 1)
            {
                state[x,y]+=2;
                pic[x, y].Image = imageList1.Images[state[x,y]];
                solution[x]=y;
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        if (state[i, j] <= 1 && !safe(i, j))
                        {
                            state[i, j] = (i + j) % 2 + 4;
                            pic[i, j].Image = imageList1.Images[state[i, j]];
                        }
                for (k = 0; k < n; k++)
                {
                    if (solution[k] == -1)
                        break;
                }
                if (k == n)
                {
                    DialogResult check = MessageBox.Show("Congratulation You Won!");
                    if (check == DialogResult.OK)
                        newToolStripMenuItem_Click(newToolStripMenuItem, System.EventArgs.Empty);
                }
                    
            }
            else if(state[x, y] <= 3)
            {
                state[x, y] -= 2;
                pic[x, y].Image = imageList1.Images[state[x, y]];
                solution[x] = -1;
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        if (state[i, j] >=4 && safe(i, j))
                        {
                            state[i, j] = (i + j) % 2;
                            pic[i, j].Image = imageList1.Images[state[i, j]];
                        }
            }
        }

        //neu y=-1 thi co nghia la hang x ko co hau
        //neu y!=-1 thi co nghia la o (x,y) co hau

        bool safe(int i, int j) //doi chieu voi ~ o co hau
        {                       //neu o can ktra trung hang/cot/dg cheo voi o xet thi ko safe
            bool safe = true;
            for (int x = 0; x < n; x++)
            {
                int y = solution[x];
                if ((y != -1)&&(i == x || j == y || i + j == x + y || i - j == x - y))  //(y!=-1) --> có hậu
                {
                    safe = false;
                    break;
                }
            }
            return safe;
        }

        void try_row(int i)
        {
            bool AllCellsUnsafe = true; //all cells unsafe (tạm thời)
            for (int j = 0; j < n && !end; j++) //xét từng cột nếu chưa cho phép kết thúc
            {
                if (safe(i, j)) //nếu ô (i,j) an toàn (ko bị hậu khác ăn)
                {
                    AllCellsUnsafe = false; //tồn tại ô an toàn trên hàng i
                    solution[i] = j;        //đặt hậu vào ô an toàn (i,j)
                    if (i < n - 1)          //nếu chưa đến hàng chót
                    {
                        try_row(i + 1);         //check hàng tiếp theo
                        if (solution[i] == -1) AllCellsUnsafe = true;
                    }   // nếu hậu bị loại ra thì gán lại all cells unsafe
                    else
                    {
                        output();   //xuất kết quả nếu đến hàng chót
                        end = true; //cho phép kết thúc ko giải nữa
                    }
                }
                if ((j == n - 1) && AllCellsUnsafe) solution[i - 1] = -1;
            }   //nếu đến cột cuối mà all cells unsafe thì loại con hậu ở hàng trên đi
        }

        private void btn_Solve_Click(object sender, EventArgs e)
        {
            btn_Solve.Enabled = false;
            saveToolStripMenuItem.Enabled = false;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    state[i, j] = (i + j) % 2;
                    pic[i, j].Image = imageList1.Images[state[i, j]];
                    pic[i, j].Enabled = false;
                }
            }
            Random random = new Random();
            if (n == 4)
                solution[0] = random.Next(1, 3);
            else if (n == 6)
                solution[0] = random.Next(1, 5);
            else
                solution[0] = random.Next(0, n);
            for (int i = 1; i < n; i++)
                solution[i] = -1;
            end = false;
            try_row(1);
            btn_Solve.Enabled = true;
        }

        void output()
        {
            for (int i = 0; i < n; i++)
            {
                state[i, solution[i]] = (i + solution[i]) % 2 + 2;
                pic[i, solution[i]].Image = imageList1.Images[state[i,solution[i]]];
            }
            //for (int j = 0; j < 8; j++)
            //    for (int k = 0; k < 8; k++)
            //        pic[j, k].Enabled = false;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int m;
            saveToolStripMenuItem.Enabled = true;
            do
            {
                string check = Interaction.InputBox("Input size of chessboard!", "New Game", n.ToString(), 50, 50);
                if (check == "")
                    m = 26;         //m=26 tức là ko làm gì cả
                else m = int.Parse(check);
            }
            while (m < 4 || m > 26);
            if (m != 26)
            {
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                    {
                        this.Controls.Remove(pic[i, j]);
                        pic[i, j] = null;
                    }
                n = m;
                initials();
            }
        }

        public void Save(string linksave)
        {
            FileStream fs = new FileStream(linksave, FileMode.Create);
            StreamWriter wr = new StreamWriter(fs);
            wr.WriteLine(n.ToString());
            for (int i = 0; i < n; i++)
                wr.WriteLine(solution[i].ToString());
            wr.Close();
            fs.Close();
        }

        public void Open(string linksave)
        {
            FileStream fs = new FileStream(linksave,FileMode.Open);
            StreamReader rd = new StreamReader(fs);
            int m = int.Parse(rd.ReadLine());
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    this.Controls.Remove(pic[i, j]);
                    pic[i, j] = null;
                }
            n = m;
            initials();
            for (int i = 0; i < n; i++)
            {
                solution[i] = int.Parse(rd.ReadLine());
            }
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    if (solution[i] == j)
                    {
                        //dat hau
                        state[i, j] = (i + j) % 2 + 2;
                    }
                    else if (!safe(i, j))
                    {
                        state[i, j] = (i + j) % 2 + 4;
                    }
                    pic[i, j].Image = imageList1.Images[state[i, j]];
                }
            rd.Close();
            fs.Close();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Game file|*.qvu";
            saveFileDialog1.Title = "Save File";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
                Save(saveFileDialog1.FileName);

        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Game file|*.qvu";
            openFileDialog1.Title = "Open File";
            openFileDialog1.InitialDirectory = appPath;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Open(openFileDialog1.FileName);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveToolStripMenuItem.Enabled)
            {
                DialogResult s = MessageBox.Show("Do you want to save?", "Eight Queens Puzzle", MessageBoxButtons.YesNoCancel);
                if (s == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(saveToolStripMenuItem, System.EventArgs.Empty);
                    Application.Exit();
                }
                else if (s == DialogResult.No)
                    Application.Exit();
            }
            else Application.Exit();
        }
    }
}
