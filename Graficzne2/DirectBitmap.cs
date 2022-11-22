using Graficzne2;
using Graficzne2.Objects;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

// https://stackoverflow.com/questions/24701703/c-sharp-faster-alternatives-to-setpixel-and-getpixel-for-bitmaps-for-windows-f
internal class DirectBitmap : IDisposable
{
    public Bitmap Bitmap { get; private set; }
    public Int32[] Bits { get; private set; }
    public bool Disposed { get; private set; }
    public int Height { get; private set; }
    public int Width { get; private set; }
    public Vector3d[,] NormalMap { get;private set; }
    public Color[,] Texture { get; private set; }

    protected GCHandle BitsHandle { get; private set; }

    public DirectBitmap(int width, int height, Bitmap normalsMap, Bitmap texture)
    {
        Width = width;
        Height = height;
        Bits = new Int32[width * height];
        BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
        Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
        SetUpNormalMap(normalsMap);
        SetUpTexture(texture);
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

    public void DrawScanLineFromCorners(int y, int x1, int x2, Face face, LightSource light, Color objectColor, bool useNormals, bool useTexture)
    {
        Vector3d v1, v2, v3;
        if (useNormals)
        {
            v1 = face.V1.GetModifiedVector(NormalMap[(int)face.P1.X, (int)face.P1.Y]);
            v2 = face.V2.GetModifiedVector(NormalMap[(int)face.P2.X, (int)face.P2.Y]);
            v3 = face.V3.GetModifiedVector(NormalMap[(int)face.P3.X, (int)face.P3.Y]);
        }
        else
        {
            v1 = face.V1;
            v2 = face.V2;
            v3 = face.V3;
        }

        Color color1, color2, color3;

        if (useTexture)
        {
            color1 = Texture[(int)face.P1.X, (int)face.P1.Y];
            color2 = Texture[(int)face.P2.X, (int)face.P2.Y];
            color3 = Texture[(int)face.P3.X, (int)face.P3.Y];
        }
        else
        {
            color1 = objectColor;
            color2 = objectColor;
            color3 = objectColor;
        }

        Color colorP1 = light.GetColor(face.P1.GetVersorToPoint(light.LightLocation), v1, color1);
        Color colorP2 = light.GetColor(face.P2.GetVersorToPoint(light.LightLocation), v2, color2);
        Color colorP3 = light.GetColor(face.P3.GetVersorToPoint(light.LightLocation), v3, color3);

        for (int x = x1 + 1; x <= x2; x++)
        {
            Color color = GetColor(face, colorP1, colorP2, colorP3, new Point(x, y));
            SetPixel(x, y, color);
        }
    }

    public void DrawScanLine(int y, int x1, int x2, Face face, LightSource light, Color objectColor, bool useNormals, bool useTexture)
    {
        for (int x = x1 + 1; x <= x2; x++)
        {
            Point3d p = face.GetPointByXY(x, y);

            Point p2 = new Point(x, y);
            Vector3d N = Utils.GetVector(face, p2);

            if (useNormals) N = N.GetModifiedVector(NormalMap[x, y]);
            Color color;
            if (useTexture) color = light.GetColor(p.GetVersorToPoint(light.LightLocation), N, Texture[x, y]);
            else color = light.GetColor(p.GetVersorToPoint(light.LightLocation), N, objectColor);
            SetPixel(x, y, color);
        }
    }

    public void DrawCloudScanLine(int y, int x1, int x2, Color color)
    {
        if (y >= Height || y <= 0) 
            return;

        x1 = Math.Max(x1, 1);
        x2 = Math.Min(x2, Width - 1);


        for (int x = x1 + 1; x <= x2; x++)
        {
            SetPixel(x, y, color);
        }
    }

    private Color GetColor(Face face, Color p1Color, Color p2Color, Color p3Color, Point p)
    {
        double area = face.Area;
        Point p1 = face.P1.To2d();
        Point p2 = face.P2.To2d();
        Point p3 = face.P3.To2d();

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

    public void SetUpNormalMap(Bitmap bitmap)
    {
        NormalMap = new Vector3d[bitmap.Width, bitmap.Height];
        for (int i = 0; i < bitmap.Width; i++)
        {
            for (int j = 0; j < bitmap.Height; j++)
            {
                NormalMap[i, j] = Vector3d.GetFromColor(bitmap.GetPixel(i, j));
            }
        }
    }

    public void SetUpTexture(Bitmap bitmap)
    {
        Texture = new Color[bitmap.Width, bitmap.Height];

        for(int i = 0; i < Width; i++)
        {
            for(int j = 0; j < Height; j++)
            {
                Texture[i, j] = bitmap.GetPixel(i, Height - 1 - j);
            }
        }
    }

}