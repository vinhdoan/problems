using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game2048
{
    class Input
    {
        public static void Select(TextBox[,] box, String s)
        {
            switch (s)
            {
                case "Input0":
                    box[2, 1].Text = "2";
                    box[3, 3].Text = "2";
                    break;
                case "Input1":
                    box[1, 0].Text = "2";
                    box[3, 1].Text = "2";
                    break;
                case "Input2":
                    box[2, 0].Text = "2";
                    box[2, 2].Text = "4";
                    break;
                case "Input3":
                    box[2, 2].Text = "2";
                    box[3, 3].Text = "2";
                    break;
                case "Input4":
                    box[0, 0].Text = "2";
                    box[1, 2].Text = "2";
                    break;
                case "Input5":
                    box[2, 1].Text = "2";
                    box[3, 3].Text = "2";
                    break;
                case "Input6":
                    box[0, 0].Text = "4";
                    box[2, 3].Text = "2";
                    break;
                case "Input7":
                    box[2, 1].Text = "2";
                    box[3, 3].Text = "2";
                    break;
                case "Input8":
                    box[2, 0].Text = "2";
                    box[2, 1].Text = "2";
                    break;
                case "Input9":
                    box[0, 2].Text = "2";
                    box[2, 0].Text = "2";
                    break;
            }
        }
    }
}
