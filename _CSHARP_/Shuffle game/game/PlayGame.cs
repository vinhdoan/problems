using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;


namespace ConsoleApplication1
{
    class PlayGame
    {
        public static Box[] a = new Box[9];
        public static int m = 0;
        const int pos_left = 0, pos_top = 6, dist_x = 5, dist_y = 2;
        public static int dem;
        public void playgame() 
        {
            bool again = false;
            do
            {
                Console.Clear();
                Console.CursorLeft = 0;
                Console.CursorTop = 0;
                int number = 0;
                dem = 0;
                //get folder where contains file.exe
                string appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string link = appPath + @"\data.txt";
                string linksave = appPath + @"\save.txt";
                string r_player = "", r_steps = "";
                read_record(ref r_player, ref r_steps, ref link, ref number);
                while (!initials()) ;
                display();
                while (true)
                {
                    Console.SetCursorPosition(0, 3);
                    Console.WriteLine("Press S to save game.\nPress L to load recent saved game.");
                    Console.SetCursorPosition(0, 11);
                    Console.WriteLine("Number of steps: {0}", dem);
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Escape)
                        break;
                    if (keyInfo.Key == ConsoleKey.S)
                        save(ref linksave);
                    if (keyInfo.Key == ConsoleKey.LeftArrow)
                        if (m % 3 != 2) process(m, m + 1);
                    if (keyInfo.Key == ConsoleKey.RightArrow)
                        if (m % 3 != 0) process(m, m - 1);
                    if (keyInfo.Key == ConsoleKey.UpArrow)
                        if (m < 6) process(m, m + 3);
                    if (keyInfo.Key == ConsoleKey.DownArrow)
                        if (m > 2) process(m, m - 3);
                    if (keyInfo.Key == ConsoleKey.L)
                        load(ref linksave);
                    if (check())
                    {
                        Console.WriteLine("You win after {0} steps.", dem);
                        if ((number == 0) || (dem < number)) 
                            write_record(ref r_player, ref link, ref dem);
                        break;
                    }
                }
                ending(ref link, ref again);
            }
            while (again);
        }
        public void process(int i, int j)
        {
            int x1 = a[i].val;
            int x2 = a[j].val;
            swap(ref x1, ref x2);
            a[i].val = x1;
            a[j].val = x2;
            update(i, j);
            m += j - i;
            dem++;
        }
        public void update(int i, int j)
        {
            Console.CursorTop = a[i].y;
            Console.CursorLeft = a[i].x;
            Console.WriteLine("{0}", a[i].val);
            Console.CursorTop = a[j].y;
            Console.CursorLeft = a[j].x;
            Console.WriteLine("{0}", a[j].val);
        }
        public void read_record(ref string r_player, ref string r_steps, ref string link, ref int number)
        {
            if (File.Exists(link))
            {
                FileStream fs = new FileStream(link, FileMode.Open);
                StreamReader rd = new StreamReader(fs);
                Console.WriteLine("Record Holder: {0}", r_player = rd.ReadLine());
                Console.WriteLine("Key Presses: {0}", r_steps = rd.ReadLine());
                rd.Close();
                fs.Close();
            }
            else 
                Console.WriteLine("There is no record.");
            if ((r_steps == "") || (r_steps == null)) 
                number = 0;
            else 
                number = int.Parse(r_steps);
        }
        public void write_record(ref string r_player, ref string link, ref int dem)
        {
            Console.Write("NEW RECORD!!! What's your name? --> ");
            Console.CursorVisible = true;
            r_player = Console.ReadLine();
            FileStream fs = new FileStream(link, FileMode.Create, FileAccess.Write);
            StreamWriter wr = new StreamWriter(fs);
            wr.WriteLine(r_player);
            wr.Write(dem.ToString());
            wr.Close();
            fs.Close();
        }
        public void ending(ref string link, ref bool again) 
        {
            char chk;
            Console.CursorTop = pos_top + 2 * dist_y + 3;
            Console.CursorLeft = 0;
            Console.WriteLine("Press D to clear record.\nPress A to play again.");
            do
            {
                chk = 'r';
                ConsoleKeyInfo keyInfo1 = Console.ReadKey(true);
                if (keyInfo1.Key == ConsoleKey.D)
                {
                    chk = 'd';
                    if (File.Exists(link))
                    {
                        File.Delete(link);
                        Console.WriteLine("File successfully deleted.");
                    }
                    else Console.WriteLine("Fail to delete file.");
                }
                if (keyInfo1.Key == ConsoleKey.A) 
                    again = true; 
                else 
                    again = false;
            }
            while (chk == 'd');
        }
        public bool check()
        {
            int i = 0;
            for (i = 0; i <= 8; i++)
                if ((a[i].val != i + 1))
                    break;
            if (i == 8) 
                return true;
            else 
                return false;
        }
        public void display()
        {
            int i;
            for (i = 0; i <= 8; i++)
            {
                Console.CursorTop = a[i].y;
                Console.CursorLeft = a[i].x;
                Console.WriteLine("{0}", a[i].val);
            }
        }

        //Khởi tạo và trả về kq khởi tạo thành công hay ko
        public bool initials()
        {
            Console.CursorVisible = false;  //Ko cho hiển thị con trỏ
            Random rand = new Random();
            int[] so = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            int lap, index;
            int i = 0, j = 0, k = 8;
            for (i = 0; i <= 8; i++)
            {
                index = rand.Next(0, k--);
                a[i] = new Box();
                a[i].val = so[index];
                postition(so, index, i);
                for (lap = index; lap <= k; lap++)
                    so[lap] = so[lap + 1];
            }
            // 8 - puzzle algorithm
            lap = 0;
            for (i = 0; i <= 8; i++)
                for (j = i + 1; j <= 8; j++)
                    if ((a[j].val != 0) && (a[i].val > a[j].val))
                        lap++;
            if (lap % 2 == 0)
                return true;
            else
                return false;
        }

        private static void postition(int[] so, int index, int i)
        {
            a[i].x = pos_left + (i % 3) * dist_x;
            a[i].y = pos_top + (i / 3) * dist_y;
            if (so[index] == 0) { m = i; }
        }
        public void swap(ref int a, ref int b)
        {
            int tam;
            tam = a;
            a = b;
            b = tam;
        }
        public void save(ref string linksave)
        {
            if (!File.Exists(linksave))
            {
                FileStream fs = new FileStream(linksave, FileMode.CreateNew);
                StreamWriter wr = new StreamWriter(fs);
                for (int i = 0; i <= 8; i++)
                    wr.WriteLine(a[i].val.ToString());
                wr.WriteLine(dem.ToString());
                wr.Close();
                fs.Close();
            }
            else
            {
                FileStream fs1 = new FileStream(linksave, FileMode.Create);
                StreamWriter wr = new StreamWriter(fs1);
                for (int i = 0; i <= 8; i++)
                    wr.WriteLine(a[i].val.ToString());
                wr.WriteLine(dem.ToString()); 
                wr.Close();
                fs1.Close();
            }
        }
        public void load(ref string linksave)
        {
            if (!File.Exists(linksave))
            {
                Console.WriteLine("No recent game was saved!");
            }
            else
            {
                string[] s = new string[10];
                int[] b = new int[10];
                int index = 0, j = 0;
                int dem1;
                FileStream fs = new FileStream(linksave, FileMode.Open);
                StreamReader rd = new StreamReader(fs);
                for (int i = 0; i <= 8; i++)
                {
                    s[index] = rd.ReadLine();
                    b[j] = int.Parse(s[index]);
                    a[i].val = b[j];
                    postition(b, j, i);
                    updateload(i);
                    index++;
                    j++;
                }
                s[index] = rd.ReadToEnd();
                if (index == 9)
                {
                    dem1 = int.Parse(s[index]);
                    dem = dem1;
                }
                rd.Close();
                fs.Close();
            }
        }
        public void updateload(int i)
        {
            Console.CursorTop = a[i].y;
            Console.CursorLeft = a[i].x;
            Console.WriteLine("{0}", a[i].val);
        }
    }
}
