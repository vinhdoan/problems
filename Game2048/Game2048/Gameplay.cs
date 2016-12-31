using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game2048
{
    class Gameplay
    {
        // For real player only
        public static TextBox[,] Move(TextBox[,] box, String s)
        {
            TextBox[,] new_box = new TextBox[4, 4];
            int point = 0;
            switch (s)
            {
                case "UP":
                    new_box = MoveUp(box, ref point);
                    break;
                case "DOWN":
                    new_box = MoveDown(box, ref point);
                    break;
                case "LEFT":
                    new_box = MoveLeft(box, ref point);
                    break;
                case "RIGHT":
                    new_box = MoveRight(box, ref point);
                    break;
            }
            Resources.stop = AfterMoving(box, new_box);
            Resources.score += point;
            return new_box;
        }

        // For both AI & real player
        public static TextBox[,] MoveUp(TextBox[,] box, ref int point)
        {
            TextBox[,] new_box = NewBox(box);
            for (int j = 0; j < 4; j++)
            {
                List<int> list = new List<int>();
                for (int i = 0; i < 4; i++)
                    point += HandleMoving(new_box, list, i, j);
                for (int i = 0; i < list.Count; i++)
                    new_box[i, j].Text = list.ElementAt(i).ToString();
            }
            return new_box;
        }

        public static TextBox[,] MoveDown(TextBox[,] box, ref int point)
        {
            TextBox[,] new_box = NewBox(box);
            for (int j = 0; j < 4; j++)
            {
                List<int> list = new List<int>();
                for (int i = 3; i >= 0; i--)
                    point += HandleMoving(new_box, list, i, j);
                for (int i = 3; i >= 4 - list.Count; i--)
                    new_box[i, j].Text = list.ElementAt(3 - i).ToString();
            }
            return new_box;
        }

        public static TextBox[,] MoveLeft(TextBox[,] box, ref int point)
        {
            TextBox[,] new_box = NewBox(box);
            for (int i = 0; i < 4; i++)
            {
                List<int> list = new List<int>();
                for (int j = 0; j < 4; j++)
                    point += HandleMoving(new_box, list, i, j);
                for (int j = 0; j < list.Count; j++)
                    new_box[i, j].Text = list.ElementAt(j).ToString();
            }
            return new_box;
        }

        public static TextBox[,] MoveRight(TextBox[,] box, ref int point)
        {
            TextBox[,] new_box = NewBox(box);
            for (int i = 0; i < 4; i++)
            {
                List<int> list = new List<int>();
                for (int j = 3; j >= 0; j--)
                    point += HandleMoving(new_box, list, i, j);
                for (int j = 3; j >= 4 - list.Count; j--)
                    new_box[i, j].Text = list.ElementAt(3 - j).ToString();
            }
            return new_box;
        }

        private static int HandleMoving(TextBox[,] box, List<int> list, int i, int j)
        {
            int point = 0;
            if (box[i, j].Text != "")
            {
                int val = int.Parse(box[i, j].Text);
                if (list.Count == 0)
                    list.Add(val);
                else
                    if (val == list.ElementAt(list.Count - 1))
                    {
                        list.RemoveAt(list.Count - 1);
                        list.Add(val * 2);
                        point = val * 2;
                    }
                    else
                        list.Add(val);
                box[i, j].Text = "";
            }
            return point;
        }

        private static bool AfterMoving(TextBox[,] box, TextBox[,] new_box)
        {
            // Check if new_box and box are the same -> no doing anything
            bool same_box = true;
            bool breakable = false;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    if (box[i, j].Text != new_box[i, j].Text)
                    {
                        same_box = false;
                        breakable = true;
                        break;
                    }
                    if (breakable) break;
                }
            // If they are not the same
            if (!same_box)
            {
                RandomNew(new_box);
                CopyBox(new_box, box);
            }
            return EndGame(box);
        }

        private static bool EndGame(TextBox[,] box)
        {
            bool end_game = true;
            bool breakable = false;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    if (box[i, j].Text == "" || box[i, j].Text == box[i, j + 1].Text || box[i, j].Text == box[i + 1, j].Text)
                    {
                        end_game = false;
                        breakable = true;
                        break;
                    }
                    if (breakable) break;
                }
            if (end_game)
                for (int i = 0; i < 3; i++)
                    if (box[i, 3].Text == "" || box[i, 3].Text == box[i + 1, 3].Text)
                    {
                        end_game = false;
                        break;
                    }
            if (end_game)
                for (int j = 0; j < 3; j++)
                    if (box[3, j].Text == "" || box[3, j].Text == box[3, j + 1].Text)
                    {
                        end_game = false;
                        break;
                    }
            return end_game;
        }

        private static void RandomNew(TextBox[,] box)
        {
            // New random value: 2 95%;
            //                   4 5%;
            Random random = new Random();
            int new_val = 2;
            int number = random.Next(0, 20);
            if (number == 0)
                new_val = 4;

            // New random position:
            List<int[]> list = new List<int[]>();
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (box[i, j].Text == "")
                        list.Add(new int[] {i, j});
            int[] pos = list[random.Next(0, list.Count)];

            // Display:
            box[pos[0], pos[1]].Text = new_val.ToString(); 
        }

        public static TextBox[,] NewBox(TextBox[,] box)
        {
            TextBox[,] new_box = new TextBox[4, 4];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    new_box[i, j] = new TextBox();
                    new_box[i, j].Text = box[i, j].Text;
                }
            return new_box;
        }

        public static void CopyBox(TextBox[,] src, TextBox[,] dest)
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    dest[i, j].Text = src[i, j].Text;
        }
    }
}
