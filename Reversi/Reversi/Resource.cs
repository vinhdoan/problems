using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reversi
{
    class Resource
    {
        public static int current_player = (int)Constant.STATUS.PLY1;
        public static int[,] status = new int[Constant.SIZE, Constant.SIZE];
        public static bool[,] available = new bool[Constant.SIZE, Constant.SIZE];
        public static List<int[]>[,] direction = new List<int[]>[Constant.SIZE, Constant.SIZE];
        public static bool stop_game = false;
        public static PictureBox[,] board = new PictureBox[Constant.SIZE, Constant.SIZE];
        public static PictureBox picCurrentPlayer = new PictureBox();
        public static int count_ply1 = 0, count_ply2 = 0;
        public static int mode = 0;
        public struct player
        {
            public static int human = 0;
            public static int ai = 0;
        }
        public static bool ab_pruning;
    }
}
