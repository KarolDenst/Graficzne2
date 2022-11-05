using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Graficzne2.Objects
{
    internal class Face
    {
        public Point3d P1;
        public Point3d P2;
        public Point3d P3;

        public double Area;

        public Face(Point3d p1, Point3d p2, Point3d p3)
        {
            var points = new Point3d[] { p1, p2, p3 };
            points = points.OrderBy(p => p.Y).ThenBy(p => p.X).ThenBy(p => p.Z).ToArray();
            P1 = points[0];
            P2 = points[1];
            P3 = points[2];

            Area = Get2dArea();
        }

        public void ColorSolid(DirectBitmap bitmap, Color color)
        {
            int yMin = (int)P1.Y;
            int yMax = (int)P3.Y;

            List<Edge> aet = new List<Edge>();
            aet.Add(new Edge(P1.TwoD(), P2.TwoD()));
            aet.Add(new Edge(P1.TwoD(), P3.TwoD()));

            for (int i = yMin; i < yMax; i++)
            {
                if (i == P2.Y)
                {
                    aet.Remove(new Edge(P1.TwoD(), P2.TwoD()));
                    aet.Add(new Edge(P2.TwoD(), P3.TwoD()));
                }

                if (i == yMin)
                {
                    if (P1.Y != P2.Y) continue;
                    if (P1.X > P2.X) bitmap.DrawScanLine(i, (int)P2.X, (int)P1.X, color);
                    else bitmap.DrawScanLine(i, (int)P1.X, (int)P2.X, color);
                    continue;
                }

                aet = aet.OrderBy(p => p.GetX(i)).ToList();
                int x1 = (int)aet[0].GetX(i);
                int x2 = (int)aet[1].GetX(i);
                bitmap.DrawScanLine(i, x1, x2, color);
            }
        }

        public void Color(DirectBitmap bitmap, Color objectColor, LightSource light, Point center)
        {
            Vector3d v1 = new Vector3d(P1.X - center.X, P1.Y - center.Y, P1.Z);
            v1.Normalize();
            Vector3d v2 = new Vector3d(P2.X - center.X, P2.Y - center.Y, P2.Z);
            v2.Normalize();
            Vector3d v3 = new Vector3d(P3.X - center.X, P3.Y - center.Y, P3.Z);
            v3.Normalize();

            Color colorP1 = light.GetColor(P1.GetVersorToPoint(light.LightLocation), v1, objectColor);
            Color colorP2 = light.GetColor(P2.GetVersorToPoint(light.LightLocation), v2, objectColor);
            Color colorP3 = light.GetColor(P3.GetVersorToPoint(light.LightLocation), v3, objectColor);

            int yMin = (int)P1.Y;
            int yMax = (int)P3.Y;

            List<Edge> aet = new List<Edge>();
            aet.Add(new Edge(P1.TwoD(), P2.TwoD()));
            aet.Add(new Edge(P1.TwoD(), P3.TwoD()));

            for (int i = yMin; i < yMax; i++)
            {
                if (i == P2.Y)
                {
                    aet.Remove(new Edge(P1.TwoD(), P2.TwoD()));
                    aet.Add(new Edge(P2.TwoD(), P3.TwoD()));
                }

                if (i == yMin)
                {
                    if (P1.Y != P2.Y) continue;
                    if (P1.X > P2.X) bitmap.DrawScanLine(i, (int)P2.X, (int)P1.X, this, colorP1, colorP2, colorP3);
                    else bitmap.DrawScanLine(i, (int)P1.X, (int)P2.X, this, colorP1, colorP2, colorP3);
                    continue;
                }

                aet = aet.OrderBy(p => p.GetX(i)).ToList();
                int x1 = (int)aet[0].GetX(i);
                int x2 = (int)aet[1].GetX(i);
                bitmap.DrawScanLine(i, x1, x2, this, colorP1, colorP2, colorP3);
            }
        }

        public void ColorEachPoint(DirectBitmap bitmap, Color objectColor, LightSource light)
        {
            int yMin = (int)P1.Y;
            int yMax = (int)P3.Y;

            List<Edge> aet = new List<Edge>();
            aet.Add(new Edge(P1.TwoD(), P2.TwoD()));
            aet.Add(new Edge(P1.TwoD(), P3.TwoD()));

            for (int i = yMin; i < yMax; i++)
            {
                if (i == P2.Y)
                {
                    aet.Remove(new Edge(P1.TwoD(), P2.TwoD()));
                    aet.Add(new Edge(P2.TwoD(), P3.TwoD()));
                }

                if (i == yMin)
                {
                    if (P1.Y != P2.Y) continue;
                    if (P1.X > P2.X) bitmap.DrawScanLine(i, (int)P2.X, (int)P1.X, this, light, Vector, objectColor);
                    else bitmap.DrawScanLine(i, (int)P1.X, (int)P2.X, this, light, Vector, objectColor);
                    continue;
                }

                aet = aet.OrderBy(p => p.GetX(i)).ToList();
                int x1 = (int)aet[0].GetX(i);
                int x2 = (int)aet[1].GetX(i);
                bitmap.DrawScanLine(i, x1, x2, this, light, Vector, objectColor);
            }
        }

        public double Get2dArea()
        {
            return 1.0 / 2 * Math.Abs(P1.X * P2.Y - P2.X * P1.Y + P2.X * P3.Y - P3.X * P2.Y + P3.X * P1.Y - P1.X * P3.Y);
        }

        public Point3d GetPointByXY(int x, int y)
        {
            double det = (P2.Y - P3.Y) * (P1.X - P3.X) + (P3.X - P2.X) * (P1.Y - P3.Y);

            double l1 = ((P2.Y - P3.Y) * (x - P3.X) + (P3.X - P2.X) * (y - P3.Y)) / det;
            double l2 = ((P3.Y - P1.Y) * (x - P3.X) + (P1.X - P3.X) * (y - P3.Y)) / det;
            double l3 = 1.0f - l1 - l2;

            double z = l1 * P1.Z + l2 * P2.Z + l3 * P3.Z;

            return new Point3d(x, y, z);
        }
    }
}
