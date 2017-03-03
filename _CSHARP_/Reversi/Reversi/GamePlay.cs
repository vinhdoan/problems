using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    public class GamePlay
    {
        public static void UpdateAvailableBoxes()
        {   // Reset Available & Direction
            for (int i = 0; i < Constant.SIZE; i++)
                for (int j = 0; j < Constant.SIZE; j++)
                {
                    Resource.available[i, j] = false;
                    Resource.direction[i, j] = new List<int[]>();
                }
            // Check each box
            for (int i = 0; i < Constant.SIZE; i++)
                for (int j = 0; j < Constant.SIZE; j++)
                {
                    if (IsSelf(i, j))
                        CheckAll(Resource.current_player, 0, i, j);
                    else if (IsBlank(i, j))
                        CheckAll(0, Resource.current_player, i, j);
                    else
                        continue;
                }
        }

        public static int CountAvailableBoxes()
        {
            int count = 0;
            for (int i = 0; i < Constant.SIZE; i++)
                for (int j = 0; j < Constant.SIZE; j++)
                    if (Resource.available[i, j])
                        count++;
            return count;
        }

        public static bool CheckAvailableBoxes()
        {
            bool no_more_disk = true;
            for (int i = 0; i < Constant.SIZE; i++)
            {
                bool breakable = false;
                for (int j = 0; j < Constant.SIZE; j++)
                    if (Resource.available[i, j] == true)
                    {
                        no_more_disk = false;
                        breakable = true;
                        break;
                    }
                if (breakable)
                    break;
            }
            return no_more_disk;
        }

        public static void Reverse(int row, int col)
        {
            foreach (int[] d in Resource.direction[row, col])
            {
                for(int k = 1; k < d[1]; k++)
                switch (d[0])
                {
                    case (int)Constant.DIRECTION.UP:
                        Resource.status[row - k, col] = EnemyOf(Resource.status[row - k, col]);
                        break;
                    case (int)Constant.DIRECTION.DOWN:
                        Resource.status[row + k, col] = EnemyOf(Resource.status[row + k, col]);
                        break;
                    case (int)Constant.DIRECTION.LEFT:
                        Resource.status[row, col - k] = EnemyOf(Resource.status[row, col - k]);
                        break;
                    case (int)Constant.DIRECTION.RIGHT:
                        Resource.status[row, col + k] = EnemyOf(Resource.status[row, col + k]);
                        break;
                    case (int)Constant.DIRECTION.UPLEFT:
                        Resource.status[row - k, col - k] = EnemyOf(Resource.status[row - k, col - k]);
                        break;
                    case (int)Constant.DIRECTION.UPRIGHT:
                        Resource.status[row - k, col + k] = EnemyOf(Resource.status[row - k, col + k]);
                        break;
                    case (int)Constant.DIRECTION.DOWNLEFT:
                        Resource.status[row + k, col - k] = EnemyOf(Resource.status[row + k, col - k]);
                        break;
                    case (int)Constant.DIRECTION.DOWNRIGHT:
                        Resource.status[row + k, col + k] = EnemyOf(Resource.status[row + k, col + k]);
                        break;
                }
            }
        }

        public static int EnemyOf(int player)
        {
            return 3 - player;
        }

        public static void PutFirstDisks()
        {
            Resource.status[Constant.SIZE / 2 - 1, Constant.SIZE / 2 - 1] =
                (int)Constant.STATUS.PLY1;
            Resource.status[Constant.SIZE / 2, Constant.SIZE / 2] =
                (int)Constant.STATUS.PLY1;
            Resource.status[Constant.SIZE / 2 - 1, Constant.SIZE / 2] =
                (int)Constant.STATUS.PLY2;
            Resource.status[Constant.SIZE / 2, Constant.SIZE / 2 - 1] =
                (int)Constant.STATUS.PLY2;
        }

        private static void CheckAll(int From, int To, int row, int col)
        {
            CheckRow(From, To, row, col);
            CheckCol(From, To, row, col);
            CheckDia(From, To, row, col);
            CheckSec(From, To, row, col);
        }
        private static void CheckRow(int From, int To, int row, int col)
        {
            // Column 0-5
            if (col < Constant.SIZE - 2 && IsEnemy(row, col + 1))
                for (int k = col + 2; k < Constant.SIZE; k++)
                
                    if (IsEnemy(row, k))
                        continue;
                    else if (Resource.status[row, k] == From)
                        break;
                    else
                    {
                        if (From == 0)
                        {
                            Resource.available[row, col] = true;
                            Resource.direction[row, col].Add(new int[]{(int)Constant.DIRECTION.RIGHT, k - col});
                        }
                        else
                        {
                            Resource.available[row, k] = true;
                            Resource.direction[row, k].Add(new int[]{(int)Constant.DIRECTION.LEFT, k - col});
                        }
                        break;
                    }
        }
        private static void CheckCol(int From, int To, int row, int col)
        {
            // Row 0-5
            if (row < Constant.SIZE - 2 && IsEnemy(row + 1, col))
                for (int k = row + 2; k < Constant.SIZE; k++)
                
                    if (IsEnemy(k, col))
                        continue;
                    else if (Resource.status[k, col] == From)
                        break;
                    else
                    {
                        if (From == 0)
                        {
                            Resource.available[row, col] = true;
                            Resource.direction[row, col].Add(new int[]{(int)Constant.DIRECTION.DOWN, k - row});
                        }
                        else
                        {
                            Resource.available[k, col] = true;
                            Resource.direction[k, col].Add(new int[]{(int)Constant.DIRECTION.UP, k - row});
                        }
                        break;
                    }
                
        }
        private static void CheckDia(int From, int To, int row, int col)
        {
            // Column & Row 0-5
            if (row < Constant.SIZE - 2 && col < Constant.SIZE - 2 && IsEnemy(row + 1, col + 1))
                for (int k = 2; Math.Max(row + k, col + k) < Constant.SIZE; k++)
                
                    if (IsEnemy(row + k, col + k))
                        continue;
                    else if (Resource.status[row + k, col + k] == From)
                        break;
                    else
                    {
                        if (From == 0)
                        {
                            Resource.available[row, col] = true;
                            Resource.direction[row, col].Add(new int[] { (int)Constant.DIRECTION.DOWNRIGHT, k });
                        }
                        else
                        {
                            Resource.available[row + k, col + k] = true;
                            Resource.direction[row + k, col + k].Add(new int[] { (int)Constant.DIRECTION.UPLEFT, k });
                        }
                        break;
                    }
                
        }
        private static void CheckSec(int From, int To, int row, int col)
        {
            // Column 2-7, Row 0-5
            if (col > 1 && row < Constant.SIZE - 2 && IsEnemy(row + 1, col - 1))
                for (int k = 2; Math.Max(row + k, Constant.SIZE - 1 + k - col) < Constant.SIZE; k++)

                    if (IsEnemy(row + k, col - k))
                        continue;
                    else if (Resource.status[row + k, col - k] == From)
                        break;
                    else
                    {
                        if (From == 0)
                        {
                            Resource.available[row, col] = true;
                            Resource.direction[row, col].Add(new int[] { (int)Constant.DIRECTION.DOWNLEFT, k });
                        }
                        else
                        {
                            Resource.available[row + k, col - k] = true;
                            Resource.direction[row + k, col - k].Add(new int[] { (int)Constant.DIRECTION.UPRIGHT, k });
                        }
                        break;
                    }
        }
       
        private static bool IsBlank(int row, int col)
        {
            return (Resource.status[row, col] == (int)Constant.STATUS.BLANK);
        }
        private static bool IsSelf(int row, int col)
        {
            return (Resource.status[row, col] == Resource.current_player);
        }
        private static bool IsEnemy(int row, int col)
        {
            return (Resource.status[row, col] == 3 - Resource.current_player);
        }
    }

  
}
