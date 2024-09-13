using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CG_lab10
{
    public partial class MainForm : Form
    {
        List<List<PointNormalized3D>> Sides;
        List<PointNormalized3D> Axes;
        double[,] T, P; // Transformation, projection
        
        const int scale = 24;
        const int k = 10;
        public enum Projections
        {
            Frontal,
            OnePointXY,
            Orthogonal_45,
            Orthogonal_634,
        };
        Projections curProjection;

        Graphics g;
        Pen axisPen = new Pen(Brushes.Black, 10);
        Pen pointPen = new Pen(Brushes.OrangeRed, 3);
        Pen xPen = new Pen(Brushes.Red, 3);
        Pen yPen = new Pen(Brushes.Green, 3);
        Pen zPen = new Pen(Brushes.Blue, 3);
        Pen gridPenPositive = new Pen(Brushes.LightGray);
        Pen gridPenNegative = new Pen(Brushes.Black);
        private bool drawGrid = true;

        public MainForm()
        {
            InitializeFigure();
            InitializeAxes();

            T = new double[4, 4]
            {
                {1, 0, 0, 0},
                {0, 1, 0, 0},
                {0, 0, 1, 0},
                {0, 0, 0, 1}
            };

            curProjection = Projections.Frontal;

            DoubleBuffered = true;
            InitializeComponent();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            int cx = ClientSize.Width / 2;
            int cy = ClientSize.Height / 2;

            SetProjectionType(curProjection);
            if (drawGrid)
                DrawGrid(g, cx, cy);
            g.DrawEllipse(axisPen, cx - 2, cy - 2, 4, 4);
            DrawAxes(g, cx, cy);

            List<List<PointNormalized3D>> _Sides = new();
            foreach (List<PointNormalized3D> Pts in Sides)
            {
                List<PointNormalized3D>  _Pts = MultiplyMatricies(Pts, T);
                _Pts = NormalizePoints(_Pts);
                _Sides.Add(_Pts);

                _Pts = MultiplyMatricies(_Pts, P);
                _Pts = NormalizePoints(_Pts);

                for (int i = 0; i < _Pts.Count(); i++)
                {
                    var cPoint = _Pts[i];
                    var nPoint = _Pts[(i + 1) % _Pts.Count()];

                    g.DrawLine(pointPen, (int)(cx + cPoint.x * scale), (int)(cy - cPoint.y * scale),
                                         (int)(cx + nPoint.x * scale), (int)(cy - nPoint.y * scale));
                }

            }
            Sides = _Sides;

            T = new double[4, 4]
            {
                {1, 0, 0, 0},
                {0, 1, 0, 0},
                {0, 0, 1, 0},
                {0, 0, 0, 1}
            };
        }

        private void InitializeFigure()
        {
            Sides = new List<List<PointNormalized3D>>
            {
                new List<PointNormalized3D>
                {
                    new PointNormalized3D(0, 0, -2),
                    new PointNormalized3D(0, 0, 2),
                    new PointNormalized3D(-2, 1, 2),
                    new PointNormalized3D(-2, 1, -2),
                },

                new List<PointNormalized3D>
                {
                    new PointNormalized3D(0, 0, -2),
                    new PointNormalized3D(0, 0, 2),
                    new PointNormalized3D(0.8, 1, 2),
                    new PointNormalized3D(0.8, 1, -2),
                },

                new List<PointNormalized3D>
                {
                    new PointNormalized3D(0, 0, 2),
                    new PointNormalized3D(-2, 1, 2),
                    new PointNormalized3D(0.8, 1, 2),
                },

                new List<PointNormalized3D>
                {
                    new PointNormalized3D(0, 0, -2),
                    new PointNormalized3D(-2, 1, -2),
                    new PointNormalized3D(0.8, 1, -2),
                },

                new List<PointNormalized3D>
                {
                    new PointNormalized3D(-2, 1, 2),
                    new PointNormalized3D(-2, 1, -2),
                    new PointNormalized3D(0.8, 1, -2),
                    new PointNormalized3D(0.8, 1, 2),
                },
            };
        }

        private void InitializeAxes()
        {
            Axes = new List<PointNormalized3D>
            {
                new PointNormalized3D(20, 0, 0),
                new PointNormalized3D(0, 20, 0),
                new PointNormalized3D(0, 0, 20)
            };
        }

        private void DrawAxes(Graphics g, int cx, int cy)
        {
            List<PointNormalized3D> _Axes = MultiplyMatricies(Axes, P);
            g.DrawLine(xPen, cx, cy, (int)(cx + _Axes[0].x * scale), (int)(cy - _Axes[0].y * scale));
            g.DrawLine(yPen, cx, cy, (int)(cx + _Axes[1].x * scale), (int)(cy - _Axes[1].y * scale));
            g.DrawLine(zPen, cx, cy, (int)(cx - _Axes[2].x * scale), (int)(cy + _Axes[2].y * scale));
        }

        private void DrawGrid(Graphics g, int cx, int cy)
        {
            int gridSize = 50;

            // Вертикально
            for (int x = cx + gridSize; x < ClientSize.Width; x += gridSize)
                g.DrawLine(gridPenPositive, x, 0, x, ClientSize.Height);
            for (int x = cx; x > 0; x -= gridSize)
                g.DrawLine(gridPenNegative, x, 0, x, ClientSize.Height);

            // Горизонтально
            for (int y = cy; y < ClientSize.Height; y += gridSize)
                g.DrawLine(gridPenNegative, 0, y, ClientSize.Width, y);
            for (int y = cy - gridSize; y > 0; y -= gridSize)
                g.DrawLine(gridPenPositive, 0, y, ClientSize.Width, y);

/*            // Глубина
            for (int y = cy + gridSize; y < ClientSize.Height; y += gridSize)
                g.DrawLine(gridPenPositive, 0, y, ClientSize.Width, y);
            for (int y = cy - gridSize; y > 0; y -= gridSize)
                g.DrawLine(gridPenNegative, 0, y, ClientSize.Width, y);*/
        }

        public List<PointNormalized3D> MultiplyMatricies(List<PointNormalized3D> Pts, double[,] T)
        {
            int rows = Pts.Count();
            List<PointNormalized3D> result = new();

            for (int i = 0; i < rows; i++)
            {
                double x = Pts[i].x;
                double y = Pts[i].y;
                double z = Pts[i].z;
                double N = Pts[i].N;

                result.Add(new PointNormalized3D(0, 0, 0));

                result[i].x = T[0, 0] * x + T[1, 0] * y + T[2, 0] * z + T[3, 0] * N;
                result[i].y = T[0, 1] * x + T[1, 1] * y + T[2, 1] * z + T[3, 1] * N;
                result[i].z = T[0, 2] * x + T[1, 2] * y + T[2, 2] * z + T[3, 2] * N;
                result[i].N = T[0, 3] * x + T[1, 3] * y + T[2, 3] * z + T[3, 3] * N;
            }

            return result;
        }

        public List<PointNormalized3D> NormalizePoints(List<PointNormalized3D> Pts)
        {
            for (int i = 0; i < Pts.Count(); i++)
            {
                if (Pts[i].N != 1d)
                {
                    Pts[i].x = Pts[i].x / Pts[i].N;
                    Pts[i].y = Pts[i].y / Pts[i].N;
                    Pts[i].z = Pts[i].z / Pts[i].N;
                    Pts[i].N = 1;
                }
            }

            return Pts;
        }

        private void SetProjectionType(Projections type)
        {
            double angle;

            switch (type)
            {
                case Projections.Frontal:
                    P = new double[4, 4]
                    {
                        {1, 0, 0, 0},
                        {0, 1, 0, 0},
                        {0, 0, 0, 0},
                        {0, 0, 0, 1}
                    };
                    break;
                case Projections.OnePointXY:
                    P = new double[4, 4]
                    {
                        {1, 0, 0, 0},
                        {0, 1, 0, 0},
                        {0, 0, 0, 1d/k},
                        {0, 0, 0, 1}
                    };
                    break;
                case Projections.Orthogonal_45:
                    angle = 45d * (Math.PI) / 180;

                    P = new double[4, 4]
                    {
                        {1,               0,               0, 0},
                        {0,               1,               0, 0},
                        {Math.Cos(angle), Math.Sin(angle), 0, 0},
                        {0,               0,               0, 1}
                    };
                    break;
                case Projections.Orthogonal_634:
                    angle = 63.4 * (Math.PI) / 180;

                    P = new double[4, 4]
                    {
                        {1,                   0,                   0, 0},
                        {0,                   1,                   0, 0},
                        {Math.Cos(angle) / 2, Math.Sin(angle) / 2, 0, 0},
                        {0,                   0,                   0, 1}
                    };
                    break;
            }
        }

        private void MovePoints(double _x, double _y, double _z)
        {
            T = new double[4, 4]
            {
                {1, 0, 0, 0},
                {0, 1, 0, 0},
                {0, 0, 1, 0},
                {_x, _y, _z, 1}
            };
        }

        private void ScalePoints(double _x, double _y, double _z, double _h)
        {
            T = new double[4, 4]
            {
                {_x, 0, 0, 0},
                {0, _y, 0, 0},
                {0, 0, _z, 0},
                {0, 0,  0, _h}
            };
        }

        private double GradToRad(double angle)
        {
            return angle * (Math.PI) / 180;
        }
        
        private void RotateX(double angle)
        { 
            angle = GradToRad(angle);

            T = new double[4, 4]
            {
                {1, 0,                0,               0},
                {0, Math.Cos(angle),  Math.Sin(angle), 0},
                {0, -Math.Sin(angle), Math.Cos(angle), 0},
                {0, 0,                0,               1}
            };
        }

        private void RotateY(double angle)
        {
            angle = GradToRad(angle);

            T = new double[4, 4]
            {
                {Math.Cos(angle), 0, -Math.Sin(angle), 0},
                {0,               1, 0,                0},
                {Math.Sin(angle), 0, Math.Cos(angle),  0},
                {0,               0, 0,                1}
            };
        }

        private void RotateZ(double angle)
        {
            angle = GradToRad(angle);

            T = new double[4, 4]
            {
                {Math.Cos(angle),  Math.Sin(angle), 0, 0},
                {-Math.Sin(angle), Math.Cos(angle), 0, 0},
                {0,                0,               1, 0},
                {0,                0,               0, 1}
            };
        }



        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.ResizeRedraw, false);
            Invalidate();
        }

        private void buttonMove_Click(object sender, EventArgs e)
        {
            MovePoints((double)numericUpDown1.Value, (double)numericUpDown2.Value, -(double)numericUpDown3.Value);
            Refresh();
        }

        private void buttonScale_Click(object sender, EventArgs e)
        {
            ScalePoints((double)numericUpDown4.Value, (double)numericUpDown5.Value, (double)numericUpDown6.Value, 1);
            Refresh();
        }

        private void buttonRotate_Click(object sender, EventArgs e)
        {
            RotateX((double)numericUpDown7.Value);
            Refresh();
            RotateY((double)numericUpDown8.Value);
            Refresh();
            RotateZ((double)numericUpDown9.Value);
            Refresh();
        }

        private void buttonMirrorX_Click(object sender, EventArgs e)
        {
            ScalePoints(-1, 1, 1, 1);
            Refresh();
        }

        private void buttonMirrorY_Click(object sender, EventArgs e)
        {
            ScalePoints(1, -1, 1, 1);
            Refresh();
        }

        private void buttonMirrorZ_Click(object sender, EventArgs e)
        {
            ScalePoints(1, 1, -1, 1);
            Refresh();
        }

        private void buttonGrid_Click(object sender, EventArgs e)
        {
            drawGrid = !drawGrid;
            Refresh();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            InitializeFigure();
            Refresh();
        }

        private void buttonFront_Click(object sender, EventArgs e)
        {
            curProjection = Projections.Frontal;
            Refresh();
        }

        private void buttonOnePoint_Click(object sender, EventArgs e)
        {
            curProjection = Projections.OnePointXY;
            Refresh();
        }

        private void buttonOrt_45_Click(object sender, EventArgs e)
        {
            curProjection = Projections.Orthogonal_45;
            Refresh();
        }

        private void buttonOrt_634_Click(object sender, EventArgs e)
        {
            curProjection = Projections.Orthogonal_634;
            Refresh();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.U)
                curProjection = Projections.Frontal;
            if (e.KeyCode == Keys.I)
                curProjection = Projections.OnePointXY;
            if (e.KeyCode == Keys.O)
                curProjection = Projections.Orthogonal_45;
            if (e.KeyCode == Keys.P)
                curProjection = Projections.Orthogonal_634;

            if (e.KeyCode == Keys.C)
                InitializeFigure();
            if (e.KeyCode == Keys.G)
                drawGrid = !drawGrid;

            // X
            if (ModifierKeys != Keys.Control && ModifierKeys != Keys.Shift)
            {
                // Scale
                if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
                    ScalePoints(1.25, 1, 1, 1);
                if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
                    ScalePoints(0.8, 1, 1, 1);

                // Move
                if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
                    MovePoints(-1, 0, 0);
                if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
                    MovePoints(1, 0, 0);

                // Rotate OX
                if (e.KeyCode == Keys.Q)
                    RotateX(-45);
                if (e.KeyCode == Keys.E)
                    RotateX(45);

                // Mirror OX
                if (e.KeyCode == Keys.R)
                    ScalePoints(-1, 1, 1, 1);
            }
            // Y
            else if (ModifierKeys == Keys.Control && ModifierKeys != Keys.Shift)
            {
                // Scale
                if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
                    ScalePoints(1, 1.25, 1, 1);
                if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
                    ScalePoints(1, 0.8, 1, 1);

                // Move
                if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
                    MovePoints(0, -1, 0);
                if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
                    MovePoints(0, 1, 0);

                // Rotate OY
                if (e.KeyCode == Keys.Q)
                    RotateY(-45);
                if (e.KeyCode == Keys.E)
                    RotateY(45);

                // Mirror OY
                if (e.KeyCode == Keys.R)
                    ScalePoints(1, -1, 1, 1);
            }
            // Z
            else if (ModifierKeys != Keys.Control && ModifierKeys == Keys.Shift)
            {
                // Scale
                if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
                    ScalePoints(1, 1, 1.25, 1);
                if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
                    ScalePoints(1, 1, 0.8, 1);

                // Move
                if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
                    MovePoints(0, 0, -1);
                if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
                    MovePoints(0, 0, 1);

                // Rotate OZ
                if (e.KeyCode == Keys.Q)
                    RotateZ(-45);
                if (e.KeyCode == Keys.E)
                    RotateZ(45);

                // Mirror OZ
                if (e.KeyCode == Keys.R)
                    ScalePoints(1, 1, -1, 1);
            }


            Refresh();
        }
    }
}
