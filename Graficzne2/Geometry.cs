namespace Graficzne2
{
    internal static class Geometry
    {
        public static double Get2dArea(Point p1, Point p2, Point p3)
        {
            return 1.0 / 2 * Math.Abs(p1.X * p2.Y - p2.X * p1.Y + p2.X * p3.Y - p3.X * p2.Y + p3.X * p1.Y - p1.X * p3.Y);
        }
    }
}
