using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CG_lab9
{
    public partial class MainForm : Form
    {
        List<PointNormalized> Pts;
        List<List<PointNormalized>> Figures;
        double[,] T;
        const int scale = 10;

        Graphics g;
        Pen gridPen = new Pen(Brushes.LightGray);
        Pen axisPen = new Pen(Brushes.DeepSkyBlue, 2);
        Pen pointPen = new Pen(Brushes.BlueViolet, 3);

        public MainForm()
        {
            InitializeFigures();
            T = new double[3, 3]
            {
                {1, 0, 0},
                {0, 1, 0},
                {0, 0, 1}
            };

            InitializeComponent();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            int cx = ClientSize.Width / 2;
            int cy = ClientSize.Height / 2;

            DrawGrid(g, cx, cy);
            DrawAxes(g, cx, cy);
            DrawStepLines(g, cx, cy);

            List<List<PointNormalized>> _Figures = new();
            foreach (List<PointNormalized> P in Figures)
            {
                Pts = MultiplyMatricies(P, T);
                Pts = NormalizePoints(Pts);
                _Figures.Add(Pts);

                for (int i = 0; i < Pts.Count(); i++)
                {
                    var cPoint = Pts[i];
                    var nPoint = Pts[(i + 1) % Pts.Count()];

                    g.DrawLine(pointPen, (int)(cx + cPoint.x * scale), (int)(cy - cPoint.y * scale),
                                         (int)(cx + nPoint.x * scale), (int)(cy - nPoint.y * scale));
                }
            }
            Figures = _Figures;

            T = new double[3, 3]
            {
                {1, 0, 0},
                {0, 1, 0},
                {0, 0, 1}
            };
        }

        private void InitializeFigures()
        {
            Figures = new List<List<PointNormalized>>
            {
                new List<PointNormalized>
                {
                    new PointNormalized(-5, -3),
                    new PointNormalized(0, -7),
                    new PointNormalized(5, -3),
                },

                new List<PointNormalized>
                {
                    new PointNormalized(5, 1),
                    new PointNormalized(5, -3),
                    new PointNormalized(-5, -3),
                    new PointNormalized(-5, 1),
                },
            };

            Figures.Add(InitializeCircle(new PointNormalized(-5, 1),
                                         new PointNormalized(5, 1),
                                         200));
        }

        public static List<PointNormalized> InitializeCircle(PointNormalized Point1, 
                                                             PointNormalized Point2, 
                                                             int density)
        {
            double centerX = (Point1.x + Point2.x) / 2;
            double centerY = Math.Max(Point1.y, Point2.y);

            double radius = Math.Abs(centerX - Point1.x);
            List<PointNormalized> points = new List<PointNormalized>();

            double angleStep = Math.PI / density;
            
            for (int i = 0; i <= density; i++)
            {
                double angle = Math.PI - i * angleStep;
                double x = centerX + radius * Math.Cos(angle);
                double y = centerY + radius * Math.Sin(angle);

                points.Add(new PointNormalized(x, y));
            }

            return points;
        }

        public List<PointNormalized> MultiplyMatricies(List<PointNormalized> Pts, double[,] T)
        {
            int rows = Pts.Count();
            List<PointNormalized> result = new();

            for (int i = 0; i < rows; i++)
            {
                double x = Pts[i].x;
                double y = Pts[i].y;
                double N = Pts[i].N;

                result.Add(new PointNormalized(0, 0));

                result[i].x = T[0, 0] * x + T[0, 1] * y + T[0, 2] * N;
                result[i].y = T[1, 0] * x + T[1, 1] * y + T[1, 2] * N;
                result[i].N = T[2, 0] * x + T[2, 1] * y + T[2, 2] * N;
            }

            return result;
        }

        public List<PointNormalized> NormalizePoints(List<PointNormalized> Pts)
        {
            for (int i = 0; i < Pts.Count(); i++)
            {
                Pts[i].x = Pts[i].x / Pts[i].N;
                Pts[i].y = Pts[i].y / Pts[i].N;
                Pts[i].N = 1;
            }

            return Pts;
        }

        private void DrawAxes(Graphics g, int cx, int cy)
        {
            g.DrawLine(axisPen, cx, 0, cx, ClientSize.Height);
            g.DrawLine(axisPen, 0, cy, ClientSize.Width, cy);
        }

        private void DrawStepLines(Graphics g, int cx, int cy)
        {
            int stepSize = 100;

            // x+
            for (int x = cx + stepSize; x < ClientSize.Width; x += stepSize)
                g.DrawLine(axisPen, x, cy + 10, x, cy-10);
            // x-
            for (int x = cx - stepSize; x > 0; x -= stepSize)
                g.DrawLine(axisPen, x, cy + 10, x, cy-10);

            //y-
            for (int y = cy + stepSize; y < ClientSize.Height; y += stepSize)
                g.DrawLine(axisPen, cx - 10, y, cx + 10, y);
            //y+
            for (int y = cy - stepSize; y > 0; y -= stepSize)
                g.DrawLine(axisPen, cx - 10, y, cx + 10, y);
        }

        private void DrawGrid(Graphics g, int cx, int cy)
        {
            int gridSize = 50;

            // Вертикально
            for (int x = cx + gridSize; x < ClientSize.Width; x += gridSize)
                g.DrawLine(gridPen, x, 0, x, ClientSize.Height);
            for (int x = cx - gridSize; x > 0; x -= gridSize)
                g.DrawLine(gridPen, x, 0, x, ClientSize.Height);

            // Горизонтально
            for (int y = cy + gridSize; y < ClientSize.Height; y += gridSize)
                g.DrawLine(gridPen, 0, y, ClientSize.Width, y);
            for (int y = cy - gridSize; y > 0; y -= gridSize)
                g.DrawLine(gridPen, 0, y, ClientSize.Width, y);
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.ResizeRedraw, false);
            Invalidate();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            InitializeFigures();
            Refresh();
        }

        private void buttonMove_Click(object sender, EventArgs e)
        {
            T = new double[3, 3]
            {
                {1, 0, (double)(numericUpDown1.Value)},
                {0, 1, (double)(numericUpDown2.Value)},
                {0, 0, 1}
            };

            Refresh();
        }

        private void buttonScale_Click(object sender, EventArgs e)
        {
            T = new double[3, 3]
            {
                {(double)(numericUpDown3.Value), 0, 0},
                {0, (double)(numericUpDown4.Value), 0},
                {0, 0, 1}
            };
            Refresh();
        }

        private void buttonMirrorX_Click(object sender, EventArgs e)
        {
            T = new double[3, 3]
            {
                {-1, 0, 0},
                {0, 1, 0},
                {0, 0, 1}
            };
            Refresh();
        }

        private void buttonMirrorY_Click(object sender, EventArgs e)
        {
            T = new double[3, 3]
            {
                {1, 0, 0},
                {0, -1, 0},
                {0, 0, 1}
            };
            Refresh();
        }

        private void buttonRotate_Click(object sender, EventArgs e)
        {
            double angle = (double)numericUpDown5.Value * (Math.PI) / 180;

            // Вокруг 0, 0
            T = new double[3, 3]
            {
                { Math.Cos(angle), Math.Sin(angle), 0},
                {-Math.Sin(angle), Math.Cos(angle), 0},
                { 0,               0,               1}
            };
            Refresh();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            // Mowe
            if (e.KeyCode == Keys.S)
                T = new double[3, 3]
                {
                    {1, 0, 0},
                    {0, 1, -1},
                    {0, 0, 1}
                };
            if (e.KeyCode == Keys.W)
                T = new double[3, 3]
                {
                    {1, 0, 0},
                    {0, 1, 1},
                    {0, 0, 1}
                };
            if (e.KeyCode == Keys.A)
                T = new double[3, 3]
                {
                    {1, 0, -1},
                    {0, 1, 0},
                    {0, 0, 1}
                };
            if (e.KeyCode == Keys.D)
                T = new double[3, 3]
                {
                    {1, 0, 1},
                    {0, 1, 0},
                    {0, 0, 1}
                };

            // Rotate
            if (e.KeyCode == Keys.Left)
            {
                double angle = -45 * (Math.PI) / 180;

                T = new double[3, 3]
                {
                    { Math.Cos(angle), Math.Sin(angle), 0},
                    {-Math.Sin(angle), Math.Cos(angle), 0},
                    { 0,               0,               1}
                };
            }
            if (e.KeyCode == Keys.Right)
            {
                double angle = 45 * (Math.PI) / 180;

                T = new double[3, 3]
                {
                    { Math.Cos(angle), Math.Sin(angle), 0},
                    {-Math.Sin(angle), Math.Cos(angle), 0},
                    { 0,               0,               1}
                };
            }

            // Scale
            if (e.KeyCode == Keys.Up)
                T = new double[3, 3]
                {
                    {1.2, 0, 0},
                    {0, 1.2, 0},
                    {0, 0, 1}
                };
            if (e.KeyCode == Keys.Down)
                T = new double[3, 3]
                {
                    {0.8, 0, 0},
                    {0, 0.8, 0},
                    {0, 0, 1}
                };

            // Mirror
            if (e.KeyCode == Keys.Q)
                T = new double[3, 3]
                {
                    {-1, 0, 0},
                    {0, 1, 0},
                    {0, 0, 1}
                };
            if (e.KeyCode == Keys.E)
                T = new double[3, 3]
                {
                    {1, 0, 0},
                    {0, -1, 0},
                    {0, 0, 1}
                };

            // Clear
            if (e.KeyCode == Keys.C)
                InitializeFigures();

            Refresh();
        }
    }
}
