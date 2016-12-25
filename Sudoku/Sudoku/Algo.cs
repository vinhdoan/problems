using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    public class Algo
    {
        public static void HillClimbingAlgo(TextBox[,] current_box, int current_val, bool[,] is_fixed, TextBox[,] box, Sudoku.Form1 form, TextBox display, TextBox memory)
        {
            memory.Text = (++Resources.memory).ToString();
            while (current_val != 0)
            {
                TextBox[,] new_successor = NewSuccessor(current_box, is_fixed, current_val);
                CopyBox(new_successor, box);
                form.Update();
                display.Text = current_val.ToString();
                int new_val = HeuristicFunc(new_successor);
                if (new_val < current_val)
                    HillClimbingAlgo(new_successor, new_val, is_fixed, box, form, display, memory);
            }
        }

        public static void HillClimbingAlgo2(TextBox[,] current_box, int current_val, bool[,] is_fixed, TextBox[,] box, Sudoku.Form1 form, TextBox display, TextBox memory)
        {
            memory.Text = (++Resources.memory).ToString();
            while (current_val != 0)
            {
                TextBox[,] new_successor = NewSuccessor(current_box, is_fixed, current_val);
                CopyBox(new_successor, box);
                form.Update();
                display.Text = current_val.ToString();
                int new_val = HeuristicFunc2(new_successor);
                if (new_val < current_val)
                    HillClimbingAlgo2(new_successor, new_val, is_fixed, box, form, display, memory);
            }
        }

        public static string DFSAlgo(Stack<TextBox[,]> stack, TextBox[,] box, Sudoku.Form1 form, TextBox memory)
        {
            while (stack.Count != 0)
            {
                if (stack.Count > Resources.memory)
                    Resources.memory = stack.Count;
                memory.Text = Resources.memory.ToString();
                TextBox[,] node_box = stack.Pop();
                CopyBox(node_box, box);
                form.Update();
                bool full_box = true;
                List<TextBox[,]> list = NextPossibleBoxes(node_box, ref full_box);
                if (full_box)
                    break;
                else
                    foreach (TextBox[,] b in list)
                        stack.Push(b);
            }
            if (stack.Count == 0)
                return "Cannot solve";
            else
                return "DONE";
        }

        public static string BFSAlgo(Queue<TextBox[,]> queue, TextBox[,] box, Sudoku.Form1 form, TextBox memory)
        {
            bool done_flag = false;
            while (queue.Count != 0)
            {
                if (queue.Count > Resources.memory)
                    Resources.memory = queue.Count;
                memory.Text = Resources.memory.ToString();
                TextBox[,] node_box = queue.Dequeue();
                CopyBox(node_box, box);
                form.Update();
                bool full_box = true;
                List<TextBox[,]> list = NextPossibleBoxes(node_box, ref full_box);
                if (full_box)
                {
                    done_flag = true;
                    break;
                }
                else
                    foreach (TextBox[,] b in list)
                        queue.Enqueue(b);
            }
            if (done_flag)
                return "DONE";
            else
                return "Cannot solve";
        }

        // Total conflicts in all of cols & blocks
        public static int HeuristicFunc(TextBox[,] box)
        {
            int count = 0;
            //Check all cols
            for (int j = 0; j < 9; j++)
            {
                List<String> list = new List<String>();
                for (int i = 0; i < 9; i++)
                    list.Add(box[i, j].Text);
                count += CountNoOfConflicts(list);
            }
            //Check all blocks
            for (int b = 0; b < 9; b++)
            {
                List<String> list = new List<String>();
                int row_start = b / 3 * 3;
                int row_stop = row_start + 2;
                int col_start = b % 3 * 3;
                int col_stop = col_start + 2;
                for (int x = row_start; x <= row_stop; x++)
                    for (int y = col_start; y <= col_stop; y++)
                        list.Add(box[x, y].Text);
                count += CountNoOfConflicts(list);
            }
            return count;
        }

        public static int HeuristicFunc2(TextBox[,] box)
        {
            int count = 0;
            //Check all cols
            for (int j = 0; j < 9; j++)
            {
                List<String> list = new List<String>();
                for (int i = 0; i < 9; i++)
                    list.Add(box[i, j].Text);
                count += ValueDifference(list);
            }
            //Check all blocks
            for (int b = 0; b < 9; b++)
            {
                List<String> list = new List<String>();
                int row_start = b / 3 * 3;
                int row_stop = row_start + 2;
                int col_start = b % 3 * 3;
                int col_stop = col_start + 2;
                for (int x = row_start; x <= row_stop; x++)
                    for (int y = col_start; y <= col_stop; y++)
                        list.Add(box[x, y].Text);
                count += ValueDifference(list);
            }
            return count;
        }

        private static List<TextBox[,]> NextPossibleBoxes(TextBox[,] box, ref bool full_box)
        {
            List<TextBox[,]> list_of_boxes = new List<TextBox[,]>();
            bool breakable = false;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (box[i, j].Text != "")
                        continue;
                    else
                    {
                        full_box = false;
                        for (int num = 1; num <= 9; num++)
                            if (Available(num, box, i, j))
                            {
                                TextBox[,] newbox = NewBox(box);
                                newbox[i, j].Text = num.ToString();
                                list_of_boxes.Add(newbox);
                            }
                        breakable = true;
                        break;
                    }
                }
                if (breakable) break;
            }
            return list_of_boxes;
        }

        private static bool Available(int num, TextBox[,] box, int i, int j)
        {
            return (CheckRow(num, box, i, j) && CheckCol(num, box, i, j) && CheckBlock(num, box, i, j));
        }

        private static bool CheckRow(int num, TextBox[,] box, int i, int j)
        {
            bool result = true;
            for (int k = 0; k < 9; k++)
            {
                if (box[i, k].Text == num.ToString())
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        private static bool CheckCol(int num, TextBox[,] box, int i, int j)
        {
            bool result = true;
            for (int k = 0; k < 9; k++)
            {
                if (box[k, j].Text == num.ToString())
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        private static bool CheckBlock(int num, TextBox[,] box, int i, int j)
        {
            bool result = true;
            bool breakable = false;
            int block = i / 3 * 3 + j / 3;
            int row_start = block / 3 * 3;
            int row_stop = row_start + 2;
            int col_start = block % 3 * 3;
            int col_stop = col_start + 2;
            for (int x = row_start; x <= row_stop; x++)
            {
                for (int y = col_start; y <= col_stop; y++)
                    if (box[x, y].Text == num.ToString())
                    {
                        result = false;
                        breakable = true;
                        break;
                    }
                if (breakable)
                    break;
            }
            return result;
        }

        public static TextBox[,] NewBox(TextBox[,] box)
        {
            TextBox[,] newbox = new TextBox[9, 9];
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    newbox[i, j] = new TextBox();
                    newbox[i, j].Text = box[i, j].Text;
                }
            return newbox;
        }

        public static void CopyBox(TextBox[,] src, TextBox[,] dest)
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    dest[i, j].Text = src[i, j].Text;
        }

        public static void FillStartBox(ref TextBox[,] start_box)
        {
            for (int i = 0; i < 9; i++)
            {
                List<String> numlist = new List<String> { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                for (int j = 0; j < 9; j++)
                    if (start_box[i, j].Text != "")
                        numlist.Remove(start_box[i, j].Text);
                foreach (String s in numlist)
                {

                    for (int j = 0; j < 9; j++)
                        if (start_box[i, j].Text == "")
                        {
                            start_box[i, j].Text = s;
                            break;
                        }
                }
            }
        }

        private static int CountNoOfConflicts(List<String> list)
        {
            int count = 0;
            for (int x = 0; x < 8; x++)
                for (int y = x + 1; y < 9; y++)
                    if (list.ElementAt(x) == list.ElementAt(y))
                    {
                        count++;
                        break;
                    }
            return count;
        }

        private static int ValueDifference(List<String> list)
        {
            const int SUM = 45;
            int sum = 0;
            for (int x = 0; x < 8; x++)
                sum += int.Parse(list.ElementAt(x));
            if (sum > SUM)
                return sum - SUM;
            else
                return SUM - sum;
        }

        private static TextBox[,] NewSuccessor(TextBox[,] current_box, bool[,] is_fixed, int chaos)
        {
            TextBox[,] new_successor = NewBox(current_box);
            for (int i = 0; i < 9; i++)
            {
                int no_of_blank = 0;
                for (int j = 0; j < 9; j++)
                    if (!is_fixed[i, j])
                        no_of_blank++;
                if (no_of_blank >= 2)
                {
                    for (int c = 0; c < chaos / (5 + no_of_blank) + 1; c++)
                        Swap(new_successor, is_fixed, i, no_of_blank);


                }
            }
            return new_successor;
        }

        private static void Swap(TextBox[,] new_successor, bool[,] is_fixed, int row, int no_of_blank)
        {
            Random random = new Random();
            int j1 = -1;
            int j2 = -2;
            //random 1
            int rand1 = random.Next(0, no_of_blank);
            for (int j = 0; j < 9; j++)
                if (is_fixed[row, j])
                    continue;
                else
                    if (rand1 == 0)
                    {
                        j1 = j;
                        break;
                    }
                    else
                        rand1--;
            //random 2
            int rand2 = random.Next(0, no_of_blank);
            for (int j = 0; j < 9; j++)
                if (is_fixed[row, j])
                    continue;
                else
                    if (rand2 == 0)
                    {
                        j2 = j;
                        break;
                    }
                    else
                        rand2--;
            //swap
            String temp = new_successor[row, j1].Text;
            new_successor[row, j1].Text = new_successor[row, j2].Text;
            new_successor[row, j2].Text = temp;
        }

        public static bool[,] IsFixed(TextBox[,] box)
        {
            bool[,] is_fixed = new bool[9, 9];
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    is_fixed[i, j] = new bool();
                    if (box[i, j].Text != "")
                        is_fixed[i, j] = true;
                    else
                        is_fixed[i, j] = false;
                }
            return is_fixed;
        }
    }
}
