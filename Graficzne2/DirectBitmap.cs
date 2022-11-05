// https://stackoverflow.com/questions/24701703/c-sharp-faster-alternatives-to-setpixel-and-getpixel-for-bitmaps-for-windows-f

using Graficzne2;
using Graficzne2.Objects;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

internal class DirectBitmap : IDisposable
{
    public Bitmap Bitmap { get; private set; }
    public Int32[] Bits { get; private set; }
    public bool Disposed { get; private set; }
    public int Height { get; private set; }
    public int Width { get; private set; }

    protected GCHandle BitsHandle { get; private set; }

    public DirectBitmap(int width, int height)
    {
        Width = width;
        Height = height;
        Bits = new Int32[width * height];
        BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
        Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
    }

    public void SetPixel(int x, int y, Color colour)
    {
        y = Height - y;

        int index = x + (y * Width);
        int col = colour.ToArgb();

        Bits[index] = col;
    }

    public Color GetPixel(int x, int y)
    {
        int index = x + (y * Width);
        int col = Bits[index];
        Color result = Color.FromArgb(col);

        return result;
    }

    public void Dispose()
    {
        if (Disposed) return;
        Disposed = true;
        Bitmap.Dispose();
        BitsHandle.Free();
    }

    public void DrawScanLine(int y, int x1, int x2, Color color)
    {
        for(int x = x1; x <= x2; x++)
        {
            SetPixel(x, y, color);
        }
    }

    public void DrawScanLine(int y, int x1, int x2, Face face, Color p1Color, Color p2Color, Color p3Color)
    {
        for (int x = x1; x <= x2; x++)
        {
            Color color = GetColor(face, p1Color, p2Color, p3Color, new Point(x, y));
            SetPixel(x, y, color);
        }
    }

    public void DrawScanLine(int y, int x1, int x2, Face face, LightSource light, Color objectColor, Point center)
    {
        for (int x = x1; x <= x2; x++)
        {
            Point3d p = face.GetPointByXY(x, y);
            Vector3d N = new Vector3d(p.X - center.X, p.Y - center.Y, p.Z);
            N.Normalize();
            Color color = light.GetColor(p.GetVersorToPoint(light.LightLocation), N, objectColor);
            SetPixel(x, y, color);
        }
    }

    private Color GetColor(Face face, Color p1Color, Color p2Color, Color p3Color, Point p)
    {
        double area = face.Area;
        Point p1 = face.P1.TwoD();
        Point p2 = face.P2.TwoD();
        Point p3 = face.P3.TwoD();

        double p1Area = Geometry.Get2dArea(p, p2, p3);
        double p2Area = Geometry.Get2dArea(p, p1, p3);
        double p3Area = Geometry.Get2dArea(p, p2, p1);

        double p1Ratio = p1Area / area;
        double p2Ratio = p2Area / area;
        double p3Ratio = p3Area / area;

        double red = p1Color.R * p1Ratio + p2Color.R * p2Ratio + p3Color.R * p3Ratio;
        if (red > 255) red = 255;
        double green = p1Color.G * p1Ratio + p2Color.G * p2Ratio + p3Color.G * p3Ratio;
        if (green > 255) green = 255;
        double blue = p1Color.B * p1Ratio + p2Color.B * p2Ratio + p3Color.B * p3Ratio;
        if (blue > 255) blue = 255;

        return Color.FromArgb(255, (int)red, (int)green, (int)blue);
    }
}