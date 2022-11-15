namespace Graficzne2.Objects
{
    internal struct Vector3d
    {
        public double X;
        public double Y;
        public double Z;

        public Vector3d(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3d(Point3d p)
        {
            X = p.X;
            Y = p.Y;
            Z = p.Z;
        }

        // https://math.stackexchange.com/questions/2100229/calculate-the-area-of-the-face-and-determine-a-unit-normal-vector-for-the-face
        public static Vector3d GetUnitVector3d(Point3d p1, Point3d p2, Point3d p3)
        {
            Vector3d a = new Vector3d(p2 - p1);
            Vector3d b = new Vector3d(p3 - p1);

            Vector3d c = a * b;
            c.Normalize();

            return c;
        }

        public static Vector3d operator -(Vector3d v1, Vector3d v2)
        {
            return new Vector3d(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static Vector3d operator *(Vector3d v1, Vector3d v2)
        {
            double x = v1.Y * v2.Z - v1.Z * v2.Y;
            double y = v1.Z * v2.X - v1.X * v2.Z;
            double z = v1.X * v2.Y - v1.Y * v2.X;

            return new Vector3d(x, y, z);
        }

        public static Vector3d operator *(double k, Vector3d v1)
        {
            double x = v1.X * k;
            double y = v1.Y * k;
            double z = v1.Z * k;

            return new Vector3d(x, y, z);
        }

        public double GetLength()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public void Normalize()
        {
            double length = GetLength();
            X = X / length;
            Y = Y / length;
            Z = Z / length;
        }

        public static double GetCosBetweenNormal(Vector3d v1, Vector3d v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public void RotateRadians(double radians)
        {
            var ca = Math.Cos(radians);
            var sa = Math.Sin(radians);

            X = ca * X - sa * Y;
            Y = sa * X + ca * Y;
        }

        public static Vector3d GetFromColor(Color color)
        {
            double x = (color.R - 127.0) / 128;
            double y = (color.G - 127.0) / 128;
            double z = color.B / 255.0;

            var v = new Vector3d(x, y, z);
            v.Normalize();

            return v;
        }

        public Vector3d GetModifiedVector(Vector3d texture)
        {
            // M = [T,B,N]
            var N = this;
            var B = N * new Vector3d(0, 0, 1);
            var T = B * N;

            double x = texture.X * T.X + texture.Y * B.X + texture.Z * N.X;
            double y = texture.X * T.Y + texture.Y * B.Y + texture.Z * N.Y;
            double z = texture.X * T.Z + texture.Y * B.Z + texture.Z * N.Z;

            return new Vector3d(x, y, z);
        }
    }
}
