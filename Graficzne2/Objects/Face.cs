namespace Graficzne2.Objects
{
    internal class Face
    {
        public Point3d P1;
        public Point3d P2;
        public Point3d P3;

        public Vector3d V1;
        public Vector3d V2;
        public Vector3d V3;

        public double Area;

        public Face((Point3d, Vector3d) p1, (Point3d, Vector3d) p2, (Point3d, Vector3d) p3)
        {
            var points = new (Point3d, Vector3d)[] { p1, p2, p3};
            points = points.OrderBy(p => p.Item1.Y).ThenBy(p => p.Item1.X).ThenBy(p => p.Item1.Z).ToArray();
            
            P1 = points[0].Item1;
            P2 = points[1].Item1;
            P3 = points[2].Item1;

            V1 = points[0].Item2;
            V2 = points[1].Item2;
            V3 = points[2].Item2;

            Area = Get2dArea();
        }

        public void Color(DirectBitmap bitmap, Color objectColor, LightSource light, bool useNormals, bool eachPoint, bool useTexture)
        {
            // I'm ussing the fact that points are sorted in the constructor so no need to sort them here
            int yMin = (int)P1.Y;
            int yMax = (int)P3.Y;

            Point[] points = { P1.To2d(), P2.To2d(), P3.To2d() };
            List<Edge> aet = new List<Edge>();
            aet.Add(new Edge(points[0], points[1]));
            aet.Add(new Edge(points[0], points[^1]));

            for (int i = yMin; i <= yMax; i++)
            {
                for (int j = 1; j < points.Length - 1; j++)
                {
                    if (i - 1 == points[j].Y)
                    {
                        if (points[j - 1].Y >= points[j].Y) aet.Add(new Edge(points[j - 1], points[j]));
                        else aet.Remove(new Edge(points[j - 1], points[j]));

                        if (points[j + 1].Y >= points[j].Y) aet.Add(new Edge(points[j], points[j + 1]));
                        else aet.Remove(new Edge(points[j], points[j + 1]));
                    }
                }
                
                aet = aet.OrderBy(p => p.GetX(i)).ToList();
                for (int k = 0; k < aet.Count; k += 2)
                {
                    int x1 = (int)aet[k].GetX(i);
                    int x2 = (int)aet[k + 1].GetX(i);
                    if (eachPoint) bitmap.DrawScanLineFromCorners(i, x1, x2, this, light, objectColor, useNormals, useTexture);
                    else bitmap.DrawScanLine(i, x1, x2, this, light, objectColor, useNormals, useTexture);
                }
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
