using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Timers;
using System.Windows.Forms;
using Graficzne2.Enums;
using Graficzne2.Objects;
using Timer = System.Windows.Forms.Timer;

namespace Graficzne2
{
    public partial class Form1 : Form
    {
        Face[] faces;
        Graphics graphics;
        Pen pen = new Pen(Color.Black);
        DirectBitmap bitmap;
        LightSource lightSource;
        Timer timer;
        bool useTexture = false;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Form1()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            InitializeComponent();

            SetUpCanvas();
            SetUpLightSource();
            SetUpTimer();
            SetUpFaces();
            Draw();

            var p = Directory.GetCurrentDirectory();
        }

        private void SetUpCanvas()
        {
            bitmap = new DirectBitmap(canvas.Width, canvas.Height);
            canvas.Image = bitmap.Bitmap;
            graphics = Graphics.FromImage(bitmap.Bitmap);
            graphics.Clear(Color.White);
        }

        private void SetUpLightSource() =>
            lightSource = new LightSource(0.5, 0.5, 1, Color.White, new Point3d(Constants.LightSourceX, Constants.LightSourceY, Constants.MinLightHeight));

        private void SetUpTimer()
        {
            timer = new Timer();
            timer.Tick += new EventHandler(OnTimedEvent);
            timer.Enabled = false;
            timer.Interval = 1000 / Constants.fps;
        }

        private void SetUpFaces()
        {
            double scale = canvas.Width / 2;
            double offset = 1;
            faces = Utils.SetUpFaceArrayFromFile(Constants.PathToSphere, scale, offset);
        }

        private void DrawTriangles()
        {
            foreach (var face in faces)
            {
                graphics.DrawLine(pen, face.P1.TwoDInverseY(canvas.Height), face.P2.TwoDInverseY(canvas.Height));
                graphics.DrawLine(pen, face.P1.TwoDInverseY(canvas.Height), face.P3.TwoDInverseY(canvas.Height));
                graphics.DrawLine(pen, face.P3.TwoDInverseY(canvas.Height), face.P2.TwoDInverseY(canvas.Height));
            }
            canvas.Refresh();
        }

        private void ColorFacesSolid()
        {
            foreach(var face in faces)
            {
                face.ColorSolid(bitmap, colorDialog.Color);
            }
        }

        private void ColorFaces()
        {
            Point center = new Point(canvas.Width / 2, canvas.Height / 2);
            if (interpolateCornersButton.Checked)
            {
                foreach (var face in faces)
                {
                    face.Color(bitmap, colorDialog.Color, lightSource, center);
                }
            }
            else if (interpolateEachButton.Checked)
            {
                foreach (var face in faces)
                {
                    face.ColorEachPoint(bitmap, colorDialog.Color, lightSource, center, useTexture);
                }
            }
        }

        private void Draw()
        {
            ColorFaces();
            if (drawTrianglesCheckbox.Checked) DrawTriangles();
            canvas.Refresh();
        }

        private void DrawIfTimerNotRunning()
        {
            if (!timer.Enabled) Draw();
        }

        private void OnTimedEvent(object source, EventArgs e)
        {
            lightSource.Rotate(Constants.RotationDegrees, canvas.Width / 2, canvas.Height / 2);
            Draw();
        }

        private void drawButton_Click(object sender, EventArgs e)
        {
            if (timer.Enabled) timer.Enabled = false;
            else timer.Enabled = true;
        }

        private void chooseColorButton_Click(object sender, EventArgs e)
        {
            colorDialog.ShowDialog();
            DrawIfTimerNotRunning();
        }

        private void lightColorButton_Click(object sender, EventArgs e)
        {
            if (lightColorDialog.ShowDialog() == DialogResult.OK)
                lightSource.LightColor = lightColorDialog.Color;
            DrawIfTimerNotRunning();
        }

        private void kdBar_Scroll(object sender, EventArgs e)
        {
            lightSource.Kd = (double)kdBar.Value / 10;
            DrawIfTimerNotRunning();
        }

        private void ksBar_Scroll(object sender, EventArgs e)
        {
            lightSource.Ks = (double)ksBar.Value / 10;
            DrawIfTimerNotRunning();
        }

        private void mBar_Scroll(object sender, EventArgs e)
        {
            lightSource.M = mBar.Value * 10 + 1;
            DrawIfTimerNotRunning();
        }

        private void zBar_Scroll(object sender, EventArgs e)
        {
            lightSource.LightLocation.Z = Constants.MinLightHeight + zBar.Value * 50;
            DrawIfTimerNotRunning();
        }

        private void interpolateCornersButton_CheckedChanged(object sender, EventArgs e)
        {
            DrawIfTimerNotRunning();
        }

        private void interpolateEachButton_CheckedChanged(object sender, EventArgs e)
        {
            DrawIfTimerNotRunning();
        }

        private void drawTrianglesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            DrawIfTimerNotRunning();
        }

        private void loadTextureButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Texture";
                //dlg.Filter = "bmp files (*.bmp)|*.bmp";
                dlg.InitialDirectory = Constants.TextureLocation;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    bitmap.SetUpTexture(new Bitmap(dlg.FileName));
                    useTexture = true;
                    DrawIfTimerNotRunning();
                }
            }
        }
    }
}