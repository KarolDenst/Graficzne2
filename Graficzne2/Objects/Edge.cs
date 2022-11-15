namespace Graficzne2.Objects
{
    internal struct Edge
    {
        public Point P1;
        public Point P2;
        public int dx;
        public int dy;
        public double m;
        public double mInverse;

        public Edge(Point p1, Point p2)
        {
            P1 = p1;
            P2 = p2;
            dx = P1.X - P2.X;
            dy = P1.Y - P2.Y;
            if (dx != 0) m = dy / (double)dx;
            else m = 1000;
            if (dy != 0) mInverse = dx / (double)dy;
            else mInverse = 1000;
        }

        public double GetX(double y)
        {
            return P1.X + (y - P1.Y) * mInverse;
        }
    }
}
