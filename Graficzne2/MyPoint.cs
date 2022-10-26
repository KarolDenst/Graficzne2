using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graficzne2
{
    internal class MyPoint
    {
        public int X;
        public int Y;
        public int Z;

        //public MyVector Vector;

        //public MyPoint(int x, int y, int z, MyVector vector)
        //{
        //    X = x;
        //    Y = y;
        //    Z = z;
        //    Vector = vector;
        //}

        public MyPoint(int x, int y, int z)
        {
            X = x; Y = y; Z = z; 
        }
    }
}
