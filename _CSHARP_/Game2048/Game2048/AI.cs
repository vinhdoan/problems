using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game2048
{
    class AI
    {
        private struct node
        {
            public TextBox[,] box;
            public int box_actual_score;
            public int box_score;
        }
        const int DEPTH = 2;
        const int ATTEMPTS = 3;
        const int NO_MOVE = -1;
        const int MOVE_UP = 0;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = 2;
        const int MOVE_RIGHT = 3;

        public static void DFSAlgo(TextBox[,] box, Game2048.Form1 form, TextBox txtScore)
        {
            while (!Resources.stop)
            {
                node new_node;
                new_node.box = Gameplay.NewBox(box);
                new_node.box_actual_score = Resources.score;
                new_node.box_score = HeuristicVal(new_node.box, new_node.box_actual_score);
                switch (DFSBestMove(new_node))
                {
                    case MOVE_UP:
                        Gameplay.CopyBox(Gameplay.Move(box, "UP"), box);
                        break;
                    case MOVE_DOWN:
                        Gameplay.CopyBox(Gameplay.Move(box, "DOWN"), box);
                        break;
                    case MOVE_LEFT:
                        Gameplay.CopyBox(Gameplay.Move(box, "LEFT"), box);
                        break;
                    case MOVE_RIGHT:
                        Gameplay.CopyBox(Gameplay.Move(box, "RIGHT"), box);
                        break;
                }
                form.Update();
                System.Threading.Thread.Sleep(100);
                txtScore.Text = Resources.score.ToString();
            }
        }

        private static int DFSBestMove(node Node)
        {
            int[] move_stats = new int[] { 0, 0, 0, 0 };
            for (int attempt = 0; attempt < ATTEMPTS; attempt++)
            {
                int move = NO_MOVE;
                DFSSearch(Node, DEPTH, ref move);
                move_stats[move] += 1;
            }

            int best_move = NO_MOVE;
            int max = 0;
            for (int i = 0; i < 4; i++)
            {
                if (move_stats[i] > max)
                {
                    max = move_stats[i];
                    best_move = i;
                }
            }
            return best_move;
        }

        private static int DFSSearch(node current_node, int depth, ref int best_move)
        {
            if (depth == 0)
                return current_node.box_score;
            Random random = new Random();
            int temp_best_move = random.Next(0, 4);
            //int temp_best_move = MOVE_UP;
            int temp_best_score = current_node.box_score;
            for (int i = MOVE_UP; i <= MOVE_RIGHT; i++)
            {
                node new_node = AIMove(current_node, i);
                if (new_node.box_score == current_node.box_score)
                    continue;
                int nothing = 0;
                int score = DFSSearch(new_node, depth - 1, ref nothing);
                if (score > temp_best_score)
                {
                    temp_best_score = score;
                    temp_best_move = i;
                }
            }
            if (depth == DEPTH)
                best_move = temp_best_move;
            return temp_best_score;
        }

        public static void BFSAlgo(TextBox[,] box, Game2048.Form1 form, TextBox txtScore)
        {
            while (!Resources.stop)
            {
                node new_node;
                new_node.box = Gameplay.NewBox(box);
                new_node.box_actual_score = Resources.score;
                new_node.box_score = HeuristicVal(new_node.box, new_node.box_actual_score);
                switch (BFSBestMove(new_node))
                {
                    case MOVE_UP:
                        Gameplay.CopyBox(Gameplay.Move(box, "UP"), box);
                        break;
                    case MOVE_DOWN:
                        Gameplay.CopyBox(Gameplay.Move(box, "DOWN"), box);
                        break;
                    case MOVE_LEFT:
                        Gameplay.CopyBox(Gameplay.Move(box, "LEFT"), box);
                        break;
                    case MOVE_RIGHT:
                        Gameplay.CopyBox(Gameplay.Move(box, "RIGHT"), box);
                        break;
                }
                form.Update();
                System.Threading.Thread.Sleep(100);
                txtScore.Text = Resources.score.ToString();
            }
        }

        private static int BFSBestMove(node Node)
        {
            int[] move_stats = new int[] { 0, 0, 0, 0 };
            for (int attempt = 0; attempt < ATTEMPTS; attempt++)
            {
                int move = NO_MOVE;
                BFSSearch(Node, DEPTH, ref move);
                move_stats[move] += 1;
            }

            int best_move = NO_MOVE;
            int max = 0;
            for (int i = 0; i < 4; i++)
            {
                if (move_stats[i] > max)
                {
                    max = move_stats[i];
                    best_move = i;
                }
            }
            return best_move;
        }

        private static int BFSSearch(node start_node, int depth, ref int best_move)
        {
            Queue<node> queue = new Queue<node>();
            queue.Enqueue(start_node);
            int d = depth;
            while (d > 0)
            {
                int no_of_depth_nodes = IntPow(4, depth - d);
                for (int j = 0; j < no_of_depth_nodes; j++)
                {
                    node current_node = queue.Dequeue();
                    for (int i = MOVE_UP; i <= MOVE_RIGHT; i++)
                    {
                        node new_node = AIMove(current_node, i);
                        queue.Enqueue(new_node);
                    }
                }
                d--;
            }

            double max_score = double.NegativeInfinity;
            int max_pos = -1;
            int pos = -1;
            while (queue.Count > 0)
            {
                node check_node = queue.Dequeue();
                pos++;
                if (check_node.box_score > max_score)
                {
                    max_score = check_node.box_score;
                    max_pos = pos;
                }
            }
            best_move = max_pos / IntPow(4, DEPTH - 1);
            return (int)max_score;
        }

        public static void HCAlgo(TextBox[,] box, Game2048.Form1 form, TextBox txtScore)
        {
            while (!Resources.stop)
            {
                node new_node;
                new_node.box = Gameplay.NewBox(box);
                new_node.box_actual_score = Resources.score;
                new_node.box_score = HeuristicVal(new_node.box, new_node.box_actual_score);
                switch (HCBestMove(new_node))
                {
                    case MOVE_UP:
                        Gameplay.CopyBox(Gameplay.Move(box, "UP"), box);
                        break;
                    case MOVE_DOWN:
                        Gameplay.CopyBox(Gameplay.Move(box, "DOWN"), box);
                        break;
                    case MOVE_LEFT:
                        Gameplay.CopyBox(Gameplay.Move(box, "LEFT"), box);
                        break;
                    case MOVE_RIGHT:
                        Gameplay.CopyBox(Gameplay.Move(box, "RIGHT"), box);
                        break;
                }
                form.Update();
                System.Threading.Thread.Sleep(100);
                txtScore.Text = Resources.score.ToString();
            }
        }

        private static int HCBestMove(node Node)
        {
            int[] move_stats = new int[] { 0, 0, 0, 0 };
            for (int attempt = 0; attempt < ATTEMPTS; attempt++)
            {
                int move = NO_MOVE;
                DFSSearch(Node, DEPTH, ref move);
                move_stats[move] += 1;
            }

            int best_move = NO_MOVE;
            int max = 0;
            for (int i = 0; i < 4; i++)
            {
                if (move_stats[i] > max)
                {
                    max = move_stats[i];
                    best_move = i;
                }
            }
            return best_move;
        }

        private static int HCSearch(node current_node, int depth, ref int best_move)
        {
            double max_score = double.NegativeInfinity;
            int max_pos = -1;
            for (int i = MOVE_UP; i <= MOVE_RIGHT; i++)
            {
                node check_node = AIMove(current_node, i);
                if (check_node.box_score > max_score)
                {
                    max_score = check_node.box_score;
                    max_pos = i;
                }
            }
            best_move = max_pos;
            return (int)max_score;
        }

        private static node AIMove(node current_node, int move)
        {
            node new_node;
            new_node.box = new TextBox[4, 4];
            int point = 0;
            switch (move)
            {
                case MOVE_UP:
                    new_node.box = Gameplay.MoveUp(current_node.box, ref point);
                    break;
                case MOVE_DOWN:
                    new_node.box = Gameplay.MoveDown(current_node.box, ref point);
                    break;
                case MOVE_LEFT:
                    new_node.box = Gameplay.MoveLeft(current_node.box, ref point);
                    break;
                case MOVE_RIGHT:
                    new_node.box = Gameplay.MoveRight(current_node.box, ref point);
                    break;
            }
            new_node.box_actual_score = current_node.box_actual_score + point;
            new_node.box_score = HeuristicVal(new_node.box, new_node.box_actual_score);
            return new_node;
        }

        private static int HeuristicVal(TextBox[,] box, int actual_score)
        {
            // Number of empty cells:
            int no_of_empty_cell = 0;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (box[i, j].Text == "")
                        no_of_empty_cell++;
            int score = (int) (actual_score + Math.Log(actual_score) * no_of_empty_cell + no_of_empty_cell);
            return Math.Max(score, Math.Min(actual_score, 1));
        }

        private static int IntPow(int x, int pow)
        {
            int ret = 1;
            while (pow != 0)
            {
                if ((pow & 1) == 1)
                    ret *= x;
                x *= x;
                pow >>= 1;
            }
            return ret;
        }
    }
}
