using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;

namespace Graficzne2.Objects
{
    internal struct Point3d
    {
        public double X;
        public double Y;
        public double Z;

        public Point3d(double x, double y, double z)
        {
            X = x; Y = y; Z = z;
        }

        public static bool operator <(Point3d p1, Point3d p2)
        {
            if (p1.Y == p2.Y)
            {
                if (p1.X == p2.X)
                {
                    return p1.Z < p2.Z;
                }
                return p1.X < p2.X;
            }
            return p1.Y < p2.Y;
        }

        public static bool operator >(Point3d p1, Point3d p2)
        {
            if (p1.Y == p2.Y)
            {
                if (p1.X == p2.X)
                {
                    return p1.Z > p2.Z;
                }
                return p1.X > p2.X;
            }
            return p1.Y > p2.Y;
        }

        public static Point3d operator -(Point3d p1, Point3d p2)
        {
            return new Point3d(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        }

        public Point TwoD()
        {
            return new Point((int)X, (int)Y);
        }

        public Point TwoDInverseY(int height)
        {
            return new Point((int)X, (int)(height - Y));
        }

        public Vector3d GetVersorToPoint(Point3d p)
        {
            Vector3d v = new Vector3d(p - this);
            v.Normalize();

            return v;
        }

        public static double GetDistance(Point3d p1, Point3d p2)
        {
            return Math.Sqrt(p1.X * p2.X + p1.Y * p2.Y + p1.Z * p2.Z);
        }
    }
}
