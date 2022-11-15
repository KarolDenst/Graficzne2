using Graficzne2.Objects;

namespace Graficzne2
{
    internal class LightSource
    {
        public double Kd;
        public double Ks;
        public double M;
        public Color LightColor;
        public Point3d LightLocation;

        public LightSource(double kd, double ks, double m, Color color, Point3d point)
        {
            Kd = kd;
            Ks = ks;
            M = m;
            LightColor = color;
            LightLocation = point;
        }

        public Color GetColor(Vector3d L, Vector3d N, Color objectColor)
        {
            Vector3d R = 2 * (N.X * L.X + N.Y * L.Y + N.Z * L.Z) * N - L;
            Vector3d V = new Vector3d(0, 0, 1);
            double cos1 = Vector3d.GetCosBetweenNormal(N, L);
            if (cos1 < 0) cos1 = 0;
            double cos2 = Math.Pow(Vector3d.GetCosBetweenNormal(V, R), M);
            if (cos2 < 0) cos2 = 0;

            double red = (objectColor.R * LightColor.R) * (Kd * cos1 + Ks * cos2) / 255;
            if (red > 255) red = 255;
            double green = (objectColor.G * LightColor.G) * (Kd * cos1 + Ks * cos2) / 255;
            if (green > 255) green = 255;
            double blue = (objectColor.B * LightColor.B) * (Kd * cos1 + Ks * cos2) / 255;
            if (blue > 255) blue = 255;

            return Color.FromArgb(255, (int)red, (int)green, (int)blue);
        }

        public void Rotate(double degrees, double middleX, double middleY)
        {
            double radians = Math.PI / 180 * degrees;
            Vector3d v = new Vector3d(LightLocation.X - middleX, LightLocation.Y - middleY, 0);
            double length = v.GetLength();
            v.RotateRadians(radians);
            double newLength = v.GetLength();
            v = length / newLength * v;
            LightLocation.X = middleX + v.X;
            LightLocation.Y = middleY + v.Y;
        }
    }
}
