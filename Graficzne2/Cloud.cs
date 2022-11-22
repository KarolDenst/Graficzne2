using Graficzne2.Enums;
using Graficzne2.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Graficzne2
{
    internal class Cloud
    {
        public double Height;
        public Point[] Points;
        public Color Color;
        public CloudMoveDirection Direction;

        public Cloud(double height)
        {
            Height = height;
            Color = Color.SkyBlue;
            Direction = CloudMoveDirection.Right;

            Point p1 = new Point(200, 200);
            Point p2 = new Point(401, 201);
            Point p3 = new Point(400, 400);
            Point p5 = new Point(201, 401);
            Point p4 = new Point(300, 300);

            Points = new Point[] { p1, p2, p3, p4, p5};
        }

        public void Draw(DirectBitmap bitmap, Point[] points, Color color)
        {
            int yMin = points.Min(cloud => cloud.Y);
            int yMax = points.Max(cloud => cloud.Y);

            List<Edge> aet = new List<Edge>();
            // I assume that points[0] always is the point with the smallest Y coordinate
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
                    bitmap.DrawCloudScanLine(i, x1, x2, color);                    
                }
            }
        }

        public void DrawCloud(DirectBitmap bitmap)
        {
            Draw(bitmap, Points, Color);
        }

        public void DrawShadow(DirectBitmap bitmap, LightSource lightSource)
        {
            int colorIntensity = (int)(lightSource.Ka * 255);
            Color color = Color.FromArgb(255, colorIntensity, colorIntensity, colorIntensity);

            double distLighToCloud = lightSource.LightLocation.Z - Height;
            double ratio = lightSource.LightLocation.Z / distLighToCloud;
            var points = new List<Point>();

            for(int i = 0; i < Points.Length; i++)
            {
                double dx = Points[i].X - lightSource.LightLocation.X;
                double dy = Points[i].Y - lightSource.LightLocation.Y;
                dx *= ratio;
                dy *= ratio;
                int x = (int)(lightSource.LightLocation.X + dx);
                int y = (int)(lightSource.LightLocation.Y + dy);

                points.Add(new Point(x, y));
            }

            Draw(bitmap, points.ToArray(), color);
        }

        public void Move(int width, int increment)
        {
            if (Points[0].X > width - 250) Direction = CloudMoveDirection.Left;
            if (Points[0].X < 100) Direction = CloudMoveDirection.Right;

            for (int i = 0; i < Points.Length; i++)
            {
                if (Direction == CloudMoveDirection.Right) Points[i].X += increment;
                else Points[i].X -= increment;
            }
        }
    }
}
