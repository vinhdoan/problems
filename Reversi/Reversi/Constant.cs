using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    public static class Constant
    {
        public const int SIZE = 8;
        public const int PIC_SIZE = 48;
        public enum STATUS
        {
            BLANK = 0,
            PLY1 = 1,
            PLY2 = 2
        }
        public const int HOVER = 3;
        public enum DIRECTION
        {
            UP = 0,
            UPRIGHT = 1,
            RIGHT = 2,
            DOWNRIGHT = 3,
            DOWN = 4,
            DOWNLEFT = 5,
            LEFT = 6,
            UPLEFT = 7
        }
        public enum MODE
        {
            PLAYERS = 0,
            MINIMAX = 1,
            ABPRUNING = 2
        }
        public const int DEPTH = 2;
    }
}
