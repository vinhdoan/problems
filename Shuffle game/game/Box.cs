using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace ConsoleApplication1
{
    class Box
    {
        private int x_i = 0, y_i = 0;
        private int val_i = 0;
        public int x
        {
            get {return x_i;}
            set {x_i = value;}
        }
        public int y
        {
            get {return y_i;}
            set {y_i = value;}
        }
        public int val
        {
            get {return val_i;}
            set {val_i = value;}
        }
        public Box()
        {
            x_i = 0;
            y_i = 0;
            val_i = 0;
        }
    }
}
