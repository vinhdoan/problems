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
        const int max_size = 16;
        PictureBox[,] pic = new PictureBox[max_size,max_size];
        int x, y, n = 8;
        int r, c;
        int[,] state = new int[max_size, max_size];
        int[] solution = new int[max_size];
        bool end = false, add_Queen, go_on=false;
        const int time = 500;
        bool instant_solve = false;
        bool exited = false;
                        
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
//            this.Text += " (Phiên bản " + this.ProductVersion + ")";
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
                //Trong ImageList, các ảnh tương ứng như sau, giá trị của biến state[i,j] chỉ ra tính chất của ô:
                //* Số 0: ô trắng ko Hậu
                //* Số 1: ô đen ko Hậu
                //* Số 2: ô trắng có Hậu
                //* Số 3: ô đen ko Hậu
                //* Số 4: ô trắng cấm
                //* Số 5: ô đen cấm
                for (int j = 0; j < n; j++)
                {
                    pic[i, j] = new PictureBox();
                    state[i, j] = (i + j) % 2;      //ô đầu tiên là ô trắng ko Hậu
                    pic[i, j].Image = imageList1.Images[state[i, j]];
                    pic[i, j].Size = new System.Drawing.Size(size, size);
                    pic[i, j].Location = new System.Drawing.Point(10 + size * j, 30 + size * i);
                    pic[i, j].Name = (n*i+j).ToString();    //đánh số theo hàng i cột j, trái->phải, trên->dưới
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
            x = int.Parse(picture.Name) / n;            //Lấy tọa độ ảnh đc click
            y = int.Parse(picture.Name) % n;            //x dọc, y ngang
            int k;
            label1.Text = "...";
            if (state[x, y] <= 1)   //Nếu là ô ko Hậu...
            {
                state[x, y] += 2;   //...đặt Hậu vào
                pic[x, y].Image = imageList1.Images[state[x, y]];
                solution[x] = y;        //Đánh dấu vị trí đặt Hậu ở hàng x là cột y
                for (int i = 0; i < n; i++)         //Duyệt từng ô:
                    for (int j = 0; j < n; j++)
                        if (state[i, j] <= 1 && !safe(i, j))    //Nếu là ô ko Hậu và ko an toàn (bị Hậu khác ăn)...
                        {
                            state[i, j] = (i + j) % 2 + 4;  //...chuyển ô đó sang ô cấm
                            pic[i, j].Image = imageList1.Images[state[i, j]];
                        }
                for (k = 0; k < n; k++)     //Duyệt từng hàng:
                {
                    if (solution[k] == -1)  //Thoát ngay nếu hàng k nào đó ko có Hậu
                        break;
                }
                if (k == n)     //Nếu duyệt hết mà ko break, tức là tất cả các hàng đều có Hậu
                {
                    DialogResult check = MessageBox.Show("Chúc mừng! Bạn đã thắng.");
                    if (check == DialogResult.OK)
                        newToolStripMenuItem_Click(newToolStripMenuItem, System.EventArgs.Empty);
                }

            }
            else if (state[x, y] <= 3)  //Nếu là ô có Hậu...
            {
                state[x, y] -= 2;   //...bỏ Hậu đi
                pic[x, y].Image = imageList1.Images[state[x, y]];
                solution[x] = -1;   //Bỏ đánh dấu Hậu ở hàng đó
                for (int i = 0; i < n; i++)     //Duyệt từng ô:
                    for (int j = 0; j < n; j++)
                        if (state[i, j] >= 4 && safe(i, j)) //Nếu là ô cấm và vừa đc an toàn (sau khi xóa Hậu)...
                        {
                            state[i, j] = (i + j) % 2;      //Đặt lại ô trống ko Hậu
                            pic[i, j].Image = imageList1.Images[state[i, j]];
                        }
            }
            else label1.Text = "Bạn không thể đặt Hậu vào vị trí này"; //Nếu là ô cấm thì thông báo...
        }

        //Nếu solution[x] == -1 thì có nghĩa là hàng x ko có Hậu
        //Nếu solution[x] != -1 thi có nghĩa là hàng x có Hậu

        //Kiểm tra xem ô (i,j) có phải là ô an toàn hay ko:
        bool safe(int i, int j)
        {
            bool safe = true;
            for (int x = 0; x < n; x++) //Duyệt từng hàng x,
            {
                int y = solution[x];    //với y là vị trí đặt Hậu ở hàng x
                if ((y != -1)&&(i == x || j == y || i + j == x + y || i - j == x - y))  //Nếu ô (i,j) ko Hậu và (cùng hàng hoặc cùng cột hoặc cùng đg chéo với ô (x,y))
                {
                    safe = false;   //Đánh dấu ko an toàn và break
                    break;
                }
            }
            return safe;    //Trả về kquả
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

                    if (!instant_solve)
                    {
                        //state[i, j] = (i + j) % 2 + 2;
                        //pic[i, j].Image = imageList1.Images[state[i, j]];
                        r = i;
                        c = j;
                        add_Queen = true;
                        go_on = false;
                        timer1.Enabled = true;
                        wait();
                    }

                    if (i < n - 1)          //nếu chưa đến hàng chót
                    {
                        try_row(i + 1);         //check hàng tiếp theo
                        if (solution[i] == -1) AllCellsUnsafe = true;
                    }   // nếu hậu bị loại ra thì gán lại all cells unsafe
                    else
                    {
                        if (instant_solve) output_all();
                        end = true; //cho phép kết thúc ko giải nữa
                    }
                }
                if ((j == n - 1) && AllCellsUnsafe)
                {
                    if (!instant_solve)
                    {
                        //state[i - 1, solution[i - 1]] = ((i - 1) + solution[i - 1]) % 2;
                        //pic[i - 1, solution[i - 1]].Image = imageList1.Images[state[i - 1, solution[i - 1]]];
                        r = i - 1;
                        c = solution[i - 1];
                        add_Queen = false;
                        go_on = false;
                        timer1.Enabled = true;
                        wait();
                    }
                    solution[i - 1] = -1;
                }
                    
            }   //nếu đến cột cuối mà all cells unsafe thì loại con hậu ở hàng trên đi
        }

        private void btn_Solve_Click(object sender, EventArgs e)
        {
            DialogResult result;
            if (!radioInstant.Checked)
                result = MessageBox.Show("Bạn không chọn chế độ giải LẬP TỨC nên thời gian giải kéo dài và bạn sẽ không thể ngưng quá trình giải giữa chừng. Bạn có chắc chắn muốn tiếp tục?", "Cảnh báo!", MessageBoxButtons.OKCancel);
            else
                result = DialogResult.OK;
            if (result == DialogResult.OK)
            {
                this.ControlBox = false;
                btn_Help.Enabled = false;
                btn_Load.Enabled = false;
                btn_New.Enabled = false;
                btn_Save.Enabled = false;
                btn_Solve.Enabled = false;
                fileToolStripMenuItem.Enabled = false;
                helpToolStripMenuItem.Enabled = false;
                groupBox1.Enabled = false;
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

                if (!instant_solve)
                {
                    //state[0, solution[0]] = (0 + solution[0]) % 2 + 2;
                    //pic[0, solution[0]].Image = imageList1.Images[state[0, solution[0]]];
                    r = 0;
                    c = solution[0];
                    add_Queen = true;
                    go_on = false;
                    timer1.Enabled = true;
                    wait();
                }

                for (int i = 1; i < n; i++)
                    solution[i] = -1;
                end = false;
                try_row(1);
                btn_Solve.Enabled = true;
                groupBox1.Enabled = true;
                btn_Help.Enabled = true;
                btn_Load.Enabled = true;
                btn_New.Enabled = true;
                fileToolStripMenuItem.Enabled = true;
                helpToolStripMenuItem.Enabled = true;
                this.ControlBox = true;
            }
        }
        void output()
        {
            state[r, c] = (r + c) % 2;
            if (add_Queen) state[r, c] += 2;
            pic[r, c].Image = imageList1.Images[state[r, c]];
            go_on = true;
        }

        void output_all()
        {
            for (int i = 0; i < n; i++)
            {
                state[i, solution[i]] = (i + solution[i]) % 2 + 2;
                pic[i, solution[i]].Image = imageList1.Images[state[i, solution[i]]];
            }
        }

        void wait()
        {
            while (!go_on)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(50);
            }            
        }


        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int m;
            bool cancel_pressed = false;
            saveToolStripMenuItem.Enabled = true;
            btn_Save.Enabled = true;
/*<C1>*/    //try
            //{
            //    do
            //    {
            //        string check = Interaction.InputBox("Nhập vào kích thước bàn cờ", "Trò chơi mới", n.ToString(), 50, 50);
            //        if (check == "")
            //        {
            //            cancel_pressed = true;
            //            m = n;
            //        }
            //        else m = int.Parse(check);
            //    }
            //    while (m < 4 || m > 16);
            //}
            //catch
            //{
            //    cancel_pressed = true;
            //    m = n;
/*</C1>*/   //}

/*<C2>*/    do
            {
                string check = Interaction.InputBox("Nhập vào kích thước bàn cờ", "Trò chơi mới", n.ToString(), 50, 50);
                if (check == "")
                {
                    cancel_pressed = true;
                    m = n;
                }
                else
                    int.TryParse(check, out m);
            }
/*</C2>*/   while (m < 4 || m > 16);


            if (!cancel_pressed)
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
            saveToolStripMenuItem.Enabled = true;
            btn_Save.Enabled = true;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Tập tin trò chơi|*.qvu";
            saveFileDialog1.Title = "Lưu trò chơi";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
                Save(saveFileDialog1.FileName);

        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Tập tin trò chơi|*.qvu";
            openFileDialog1.Title = "Mở trò chơi";
            openFileDialog1.FileName = "";
            openFileDialog1.InitialDirectory = appPath;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Open(openFileDialog1.FileName);
            }
        }

        //Xử lý khi click tắt chương trình:
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Form1_FormClosing(this, e);
            if (saveToolStripMenuItem.Enabled)
            {
                DialogResult s = MessageBox.Show("Bạn muốn lưu trò chơi không?", "Trò chơi Bát Hậu", MessageBoxButtons.YesNoCancel);
                if (s == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(saveToolStripMenuItem, System.EventArgs.Empty);
                    exited = true;
                    Application.Exit();
                }
                else if (s == DialogResult.No)
                {
                    exited = true;
                    Application.Exit();
                }
            }
            else
            {
                exited = true;
                Application.Exit();
            }
        }

        private void btn_New_Click(object sender, EventArgs e)
        {
            newToolStripMenuItem_Click(newToolStripMenuItem, System.EventArgs.Empty);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem_Click(saveToolStripMenuItem, System.EventArgs.Empty);
        }

        private void btn_Load_Click(object sender, EventArgs e)
        {
            loadToolStripMenuItem_Click(loadToolStripMenuItem, System.EventArgs.Empty);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            output();
        }

        //Nhằm xóa các thông báo sau khi rời chuột khỏi các component
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = "...";
        }

        //Thay đổi tốc độ giải bàn cờ khi lựa chọn trên các radio button
        private void radioSlow_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSlow.Checked)
            {
                timer1.Interval = 300;
                instant_solve = false;
            }
        }

        private void radioMedium_CheckedChanged(object sender, EventArgs e)
        {
            if (radioMedium.Checked)
            {
                timer1.Interval = 150;
                instant_solve = false;
            }
        }

        private void radioFast_CheckedChanged(object sender, EventArgs e)
        {
            if (radioFast.Checked)
            {
                timer1.Interval = 50;
                instant_solve = false;
            }
        }

        private void radioInstant_CheckedChanged(object sender, EventArgs e)
        {
            if (radioInstant.Checked) instant_solve = true;
        }

        //Hiển thị thông báo hướng dẫn ở phía dưới form khi con trỏ đc di chuyển đến các component
        private void btn_Help_MouseHover(object sender, EventArgs e)
        {
            label1.Text = "Bấm vào đây để xem hướng dẫn cách chơi";
        }

        private void btn_New_MouseHover(object sender, EventArgs e)
        {
            label1.Text = "Tạo bàn cờ trống mới với kích thước tự chọn";
        }

        private void btn_Save_MouseHover(object sender, EventArgs e)
        {
            label1.Text = "Lưu lại bàn cờ hiện tại";
        }

        private void btn_Load_MouseHover(object sender, EventArgs e)
        {
            label1.Text = "Mở lại bàn cờ đã lưu trước đây";
        }

        private void btn_Solve_MouseHover(object sender, EventArgs e)
        {
            label1.Text = "Xem 1 kết quả ngẫu nhiên của bàn cờ " + n.ToString() + " x " + n.ToString();
        }

        private void radioSlow_MouseHover(object sender, EventArgs e)
        {
            label1.Text = "Giải bàn cờ với tốc độ chậm";
        }

        private void radioMedium_MouseHover(object sender, EventArgs e)
        {
            label1.Text = "Giải bàn cờ với tốc độ vừa";
        }

        private void radioFast_MouseHover(object sender, EventArgs e)
        {
            label1.Text = "Giải bàn cờ với tốc độ nhanh";
        }

        private void radioInstant_MouseHover(object sender, EventArgs e)
        {
            label1.Text = "Hiển thị ngay kết quả bàn cờ";
        }
        
        //Click vào nút Hướng dẫn, hiện Form2
        private void btn_Help_Click(object sender, EventArgs e)
        {
            Form form2 = new Form2(this);
            form2.Show();
            form2.Activate();
            this.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (saveToolStripMenuItem.Enabled && !exited)
            {
                DialogResult s = MessageBox.Show("Bạn muốn lưu trò chơi không?", "Trò chơi Bát Hậu", MessageBoxButtons.YesNoCancel);
                if (s == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(saveToolStripMenuItem, System.EventArgs.Empty);
                    //Application.Exit();
                }
                else if (s == DialogResult.Cancel)
                    e.Cancel = true;
                    //Application.Exit();
            }
        }
}   
}
