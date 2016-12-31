using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    struct successor
    {
        public int player;
        public int[,] board_status;
        public double eval;
        public bool evaluated;
    }

    class AI
    {
        public static void MinimaxAlgo()
        {
            successor first_node = new successor();
            first_node.board_status = SaveStatus();
            first_node.player = Resource.player.ai;
            first_node.eval = 0;
            first_node.evaluated = false;
            successor result;
            if (!Resource.ab_pruning)
                result = Player(first_node, 0);
            else
                result = Player(first_node, 0, double.NegativeInfinity, double.PositiveInfinity);
            LoadToStatus(result.board_status);
            Resource.current_player = Resource.player.ai;
        }

        private static successor Player(successor node, int depth)
        {
            successor best_path = new successor();
            List<successor> list = NewSuccessors(node);
            if (depth == Constant.DEPTH || list.Count == 0)
            {
                node.eval = Evaluate(node);
                node.evaluated = true;
                best_path = node;
            }
            else
            {
                double max_score = double.NegativeInfinity;
                foreach (successor s in list)
                {

                    successor result = Opponent(s, depth + 1);

                    if (!node.evaluated)
                    {
                        max_score = result.eval;
                        best_path = s;
                        node.evaluated = true;
                    }
                    else
                    {
                        double new_value = result.eval;
                        if (new_value > max_score)
                        {
                            max_score = new_value;
                            best_path = s;
                        }
                    }
                }
                best_path.eval = max_score;
                best_path.evaluated = true;
            }
            return best_path;
        }

        private static successor Opponent(successor node, int depth)
        {
            successor best_path = new successor();
            List<successor> list = NewSuccessors(node);
            if (depth == Constant.DEPTH || list.Count == 0)
            {
                node.eval = Evaluate(node);
                node.evaluated = true;
                best_path = node;
            }
            else
            {
                double min_score = double.PositiveInfinity;
                foreach (successor s in list)
                {

                    successor result = Player(s, depth + 1);

                    if (!node.evaluated)
                    {
                        min_score = result.eval;
                        best_path = s;
                        node.evaluated = true;
                    }
                    else
                    {
                        double new_value = result.eval;
                        if (new_value < min_score)
                        {
                            min_score = new_value;
                            best_path = s;
                        }
                    }
                }
                best_path.eval = min_score;
                best_path.evaluated = true;
            }
            return best_path;
        }

        private static successor Player(successor node, int depth, double alpha, double beta)
        {
            successor best_path = new successor();
            List<successor> list = NewSuccessors(node);
            if (depth == Constant.DEPTH || list.Count == 0)
            {
                node.eval = Evaluate(node);
                node.evaluated = true;
                best_path = node;
            }
            else
            {
                foreach (successor s in list)
                {

                    successor result = Opponent(s, depth + 1, alpha, beta);

                    if (!node.evaluated)
                    {
                        alpha = result.eval;
                        best_path = s;
                        node.evaluated = true;
                    }
                    else
                    {
                        double new_value = result.eval;
                        if (new_value > alpha)
                        {
                            alpha = new_value;
                            best_path = s;
                        }
                    }
                    if (alpha >= beta)
                        break;
                }
                best_path.eval = alpha;
                best_path.evaluated = true;
            }
            return best_path;
        }

        private static successor Opponent(successor node, int depth, double alpha, double beta)
        {
            successor best_path = new successor();
            List<successor> list = NewSuccessors(node);
            if (depth == Constant.DEPTH || list.Count == 0)
            {
                node.eval = Evaluate(node);
                node.evaluated = true;
                best_path = node;
            }
            else
            {
                foreach (successor s in list)
                {

                    successor result = Player(s, depth + 1, alpha, beta);

                    if (!node.evaluated)
                    {
                        beta = result.eval;
                        best_path = s;
                        node.evaluated = true;
                    }
                    else
                    {
                        double new_value = result.eval;
                        if (new_value < beta)
                        {
                            beta = new_value;
                            best_path = s;
                        }
                    }
                    if (beta <= alpha)
                        break;
                }
                best_path.eval = beta;
                best_path.evaluated = true;
            }
            return best_path;
        }

        private static List<successor> NewSuccessors(successor s)
        {
            List<successor> list = new List<successor>();
            LoadToStatus(s.board_status);
            Resource.current_player = s.player;
            GamePlay.UpdateAvailableBoxes();
            bool not_available = true;
            for (int i = 0; i < Constant.SIZE; i++)
                for (int j = 0; j < Constant.SIZE; j++)
                    if (Resource.available[i, j])
                    {
                        not_available = false;
                        successor new_successsor = new successor();
                        int[,] backup_board = SaveStatus();
                        // put disk
                        Resource.status[i, j] = Resource.current_player;
                        // convert disk
                        GamePlay.Reverse(i, j);
                        // assign new successor
                        new_successsor.player = GamePlay.EnemyOf(s.player);
                        new_successsor.board_status = SaveStatus();
                        new_successsor.eval = 0;
                        new_successsor.evaluated = false;
                        list.Add(new_successsor);
                        // turn back status
                        LoadToStatus(backup_board);
                    }
            if (not_available)
            {
                successor new_successsor = new successor();
                new_successsor.player = GamePlay.EnemyOf(s.player);
                new_successsor.board_status = SaveStatus();
                new_successsor.eval = 0;
                new_successsor.evaluated = false;
                list.Add(new_successsor);
            }
            return list;
        }

        private static double Evaluate(successor s)
        {
            
            // rel_i & rel_j are used to check boxes neighboring to a specific box
            int[] rel_i = { -1, -1, 0, 1, 1, 1, 0, -1 };
            int[] rel_j = { 0, 1, 1, 1, 0, -1, -1, -1 };
            // FACTOR 1: different of the number of disks
            int player_cnt = 0;
            int enemy_cnt = 0;
            double p;
            // FACTOR 2: adv_point is a matrix of point which show the advantage of the position
            int[,] adv_point = {{20, -3, 11, 8, 8, 11, -3, 20},
                               {-3, -7, -4, 1, 1, -4, -7, -3},
                               {11, -4, 2, 2, 2, 2, -4, 11},
                               {8, 1, 2, -3, -3, 2, 1, 8},
                               {8, 1, 2, -3, -3, 2, 1, 8},
                               {11, -4, 2, 2, 2, 2, -4, 11},
                               {-3, -7, -4, 1, 1, -4, -7, -3},
                               {20, -3, 11, 8, 8, 11, -3, 20}};
            double d = 0;
            // FACTOR 3: number of empty neighboring box is included in evaluation
            int player_empty_boxes = 0;
            int enemy_empty_boxes = 0;
            double f = 0;

            for (int i = 0; i < Constant.SIZE; i++)
                for (int j = 0; j < Constant.SIZE; j++)
                {
                    if (s.board_status[i, j] == s.player)
                    {
                        d += adv_point[i, j];
                        player_cnt++;
                    }
                    else if (s.board_status[i, j] == GamePlay.EnemyOf(s.player))
                    {
                        d -= adv_point[i, j];
                        enemy_cnt++;
                    }
                    if (s.board_status[i, j] != (int)Constant.STATUS.BLANK)
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            int x = i + rel_i[k];
                            int y = j + rel_j[k];
                            if (x >= 0 && x < Constant.SIZE && 
                                y >= 0 && y < Constant.SIZE && 
                                s.board_status[x, y] == (int)Constant.STATUS.BLANK)
                            {
                                if (s.board_status[i, j] == s.player) player_empty_boxes++;
                                else enemy_empty_boxes++;
                                break;
                            }
                        }
                    }
                }
            if (player_cnt > enemy_cnt)
                p = (100.0 * player_cnt) / (player_cnt + enemy_cnt);
            else if (player_cnt < enemy_cnt)
                p = -(100.0 * enemy_cnt) / (player_cnt + enemy_cnt);
            else p = 0;

            if (player_empty_boxes > enemy_empty_boxes)
                f = -(100.0 * player_empty_boxes) / (player_empty_boxes + enemy_empty_boxes);
            else if (player_empty_boxes < enemy_empty_boxes)
                f = (100.0 * enemy_empty_boxes) / (player_empty_boxes + enemy_empty_boxes);
            else f = 0;

            // FACTOR 4: Corner occupancy
            player_cnt = 0;
            enemy_cnt = 0;
            double c;
            
            if (s.board_status[0, 0] == s.player) player_cnt++;
            else if (s.board_status[0, 0] == GamePlay.EnemyOf(s.player)) enemy_cnt++;
            if (s.board_status[0, 7] == s.player) player_cnt++;
            else if (s.board_status[0, 7] == GamePlay.EnemyOf(s.player)) enemy_cnt++;
            if (s.board_status[7, 0] == s.player) player_cnt++;
            else if (s.board_status[7, 0] == GamePlay.EnemyOf(s.player)) enemy_cnt++;
            if (s.board_status[7, 7] == s.player) player_cnt++;
            else if (s.board_status[7, 7] == GamePlay.EnemyOf(s.player)) enemy_cnt++;
            c = 25 * (player_cnt - enemy_cnt);

            // FACTOR 5: Corner closeness
            player_cnt = 0;
            enemy_cnt = 0;
            double l;

            if (s.board_status[0, 0] == (int)Constant.STATUS.BLANK)
            {
                if (s.board_status[0, 1] == s.player) player_cnt++;
                else if (s.board_status[0, 1] == GamePlay.EnemyOf(s.player)) enemy_cnt++;
                if (s.board_status[1, 1] == s.player) player_cnt++;
                else if (s.board_status[1, 1] == GamePlay.EnemyOf(s.player)) enemy_cnt++;
                if (s.board_status[1, 0] == s.player) player_cnt++;
                else if (s.board_status[1, 0] == GamePlay.EnemyOf(s.player)) enemy_cnt++;
            }
            if (s.board_status[0, 7] == (int)Constant.STATUS.BLANK)
            {
                if (s.board_status[0, 6] == s.player) player_cnt++;
                else if (s.board_status[0, 6] == GamePlay.EnemyOf(s.player)) enemy_cnt++;
                if (s.board_status[1, 6] == s.player) player_cnt++;
                else if (s.board_status[1, 6] == GamePlay.EnemyOf(s.player)) enemy_cnt++;
                if (s.board_status[1, 7] == s.player) player_cnt++;
                else if (s.board_status[1, 7] == GamePlay.EnemyOf(s.player)) enemy_cnt++;
            }
            if (s.board_status[7, 0] == (int)Constant.STATUS.BLANK)
            {
                if (s.board_status[7, 1] == s.player) player_cnt++;
                else if (s.board_status[7, 1] == GamePlay.EnemyOf(s.player)) enemy_cnt++;
                if (s.board_status[6, 1] == s.player) player_cnt++;
                else if (s.board_status[6, 1] == GamePlay.EnemyOf(s.player)) enemy_cnt++;
                if (s.board_status[6, 0] == s.player) player_cnt++;
                else if (s.board_status[6, 0] == GamePlay.EnemyOf(s.player)) enemy_cnt++;
            }
            if (s.board_status[7, 7] == (int)Constant.STATUS.BLANK)
            {
                if (s.board_status[6, 7] == s.player) player_cnt++;
                else if (s.board_status[6, 7] == GamePlay.EnemyOf(s.player)) enemy_cnt++;
                if (s.board_status[6, 6] == s.player) player_cnt++;
                else if (s.board_status[6, 6] == GamePlay.EnemyOf(s.player)) enemy_cnt++;
                if (s.board_status[7, 6] == s.player) player_cnt++;
                else if (s.board_status[7, 6] == GamePlay.EnemyOf(s.player)) enemy_cnt++;
            }
            l = -12.5 * (player_cnt - enemy_cnt);

            // FACTOR 6: Mobility, number of available boxes
            player_cnt = 0;
            enemy_cnt = 0;
            double m = 0;
            LoadToStatus(s.board_status);
            Resource.current_player = s.player;
            GamePlay.UpdateAvailableBoxes();
            player_cnt = GamePlay.CountAvailableBoxes();
            Resource.current_player = GamePlay.EnemyOf(s.player);
            GamePlay.UpdateAvailableBoxes();
            enemy_cnt = GamePlay.CountAvailableBoxes();
            if (player_cnt > enemy_cnt)
                m = (100.0 * player_cnt) / (player_cnt + enemy_cnt);
            else if (player_cnt < enemy_cnt)
                m = -(100.0 * enemy_cnt) / (player_cnt + enemy_cnt);
            else m = 0;

            // Final weighted score
            double score = (10 * p) + (801.724 * c) + (382.026 * l) + (78.922 * m) + (74.396 * f) + (10 * d);
            return score;
        }

        private static void LoadToStatus(int[,] board_status)
        {
            for (int i = 0; i < Constant.SIZE; i++)
                for (int j = 0; j < Constant.SIZE; j++)
                    Resource.status[i, j] = board_status[i, j];
        }

        private static int[,] SaveStatus()
        {
            int[,] board = new int[8, 8];
            for (int i = 0; i < Constant.SIZE; i++)
                for (int j = 0; j < Constant.SIZE; j++)
                    board[i, j] = Resource.status[i, j];
            return board;
        }
    }
}
