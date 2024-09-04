using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CG_lab7
{
    public partial class MainForm : Form
    {
        private Timer timer;
        private int currentSpiralType = 0;
        private const int maxSpiralTypes = 4;
        private int maxClothoidIter = 10000;

        private int centerX, centerY, margin;
        float scale;
        private double angle = 0;
        private PointF upperPoint = new PointF(0, 0);

        private Font textFont;

        public MainForm()
        {
            DoubleBuffered = true; // Убирает мерцание при обновлении

            timer = new Timer();
            timer.Interval = 16; // ~62,5 кадров в секунду
            timer.Tick += Timer_Tick;
            timer.Start();

            margin = 20;
            scale = (Math.Min(ClientSize.Width, ClientSize.Height) - margin) / 3;
            textFont = new Font("Arial", 14);

            InitializeComponent();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            angle += 0.05; // Скорость поворота в полярных координатах(итоговой анимации)

            // Если поставить слишком большим, идёт overflow error - часть спиралей очень быстро растёт
            if (angle > Math.PI * 6) // Смена спирали после 3 циклов рисования
            {
                angle = 0;
                currentSpiralType++;
                if (currentSpiralType > maxSpiralTypes)
                    currentSpiralType = 0;
                scale = (Math.Min(ClientSize.Width, ClientSize.Height) - margin) / 3;
            }
            
            Invalidate();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            centerX = ClientSize.Width / 2;
            centerY = ClientSize.Height / 2;

            SolidBrush textBrush = new SolidBrush(Color.Black);

            switch (currentSpiralType)
            {
                case 0: 
                    DrawParabolicSpiral(g, centerX, centerY, scale);
                    g.DrawString("Спираль Ферма (\"параболическая\")", textFont, textBrush, upperPoint);
                    break;
                case 1: 
                    DrawLogarithmicSpiral(g, centerX, centerY, scale);
                    g.DrawString("Логарифмическая спираль (k = 0.1)", textFont, textBrush, upperPoint);
                    break;
                case 2:
                    DrawArchimedeanSpiral(g, centerX, centerY, scale);
                    g.DrawString("Арифметическая спираль(спираль Архимеда)", textFont, textBrush, upperPoint);
                    break;
/*                case 3: 
                    DrawClothoidSpiral(g, centerX, centerY, scale);
                    g.DrawString("Клотоида (Спираль Корню/Эйлера)", textFont, textBrush, upperPoint);
                    break;*/
                case 3:
                    DrawRose(g, centerX, centerY, scale);
                    g.DrawString("Роза(?)", textFont, textBrush, upperPoint);
                    break;

                default:
                    DrawParabolicSpiral(g, centerX, centerY, scale);
                    DrawLogarithmicSpiral(g, centerX, centerY, scale);
                    DrawArchimedeanSpiral(g, centerX, centerY, scale);
                    DrawRose(g, centerX, centerY, scale);

                    g.DrawString("Все сразу", textFont, textBrush, upperPoint);
                    break;
            }
        }

        private void UpdateScale(float prev_scale, float x, float y)
        {
            scale = Math.Min(prev_scale, (Math.Min(x, y) - margin) / 3);
        }

        private void DrawParabolicSpiral(Graphics g, float centerX, float centerY, float scale)
        {
            Pen pen = new Pen(Color.Blue, 2);
            PointF prevPoint = new PointF(centerX, centerY);
            
            for (float t = 0; t <= angle; t += 0.01f)
            {
                double r = (float)Math.Sqrt(t);
                float x = centerX + (float)(r * Math.Cos(t)) * scale;
                float y = centerY + (float)(r * Math.Sin(t)) * scale;

                if (prevPoint.X != x || prevPoint.Y != y)
                {
                    g.DrawLine(pen, prevPoint, new PointF(x, y));
                    prevPoint = new PointF(x, y);
                }
                
                UpdateScale(scale, x, y);
            } 
        }

        private void DrawLogarithmicSpiral(Graphics g, float centerX, float centerY, float scale)
        {
            Pen pen = new Pen(Color.Red, 2);
            PointF prevPoint = new PointF(centerX, centerY);

            for (float t = 0; t <= angle; t += 0.01f)
            {
                // k = 1/10
                double r = (float)Math.Exp(t / 10);
                float x = centerX + (float)(r * Math.Cos(t) * scale);
                float y = centerY + (float)(r * Math.Sin(t) * scale);

                if (prevPoint.X != x || prevPoint.Y != y)
                {
                    g.DrawLine(pen, prevPoint, new PointF(x, y));
                    prevPoint = new PointF(x, y);
                }

                UpdateScale(scale, x, y);
            }
        }

        private void DrawArchimedeanSpiral(Graphics g, float centerX, float centerY, float scale)
        {
            Pen pen = new Pen(Color.Green, 2);
            PointF prevPoint = new PointF(centerX, centerY);

            for (float t = 0; t <= angle; t += 0.01f)
            {
                float r = t;
                float x = centerX + (float)(r * Math.Cos(t) * scale);
                float y = centerY + (float)(r * Math.Sin(t) * scale);

                if (prevPoint.X != x || prevPoint.Y != y)
                {
                    g.DrawLine(pen, prevPoint, new PointF(x, y));
                    prevPoint = new PointF(x, y);
                }

                UpdateScale(scale, x, y);
            }
        }

        // Слишком сложно в построении с масштабированием, в итоге полноценно не реализовано
        /*private void DrawClothoidSpiral(Graphics g, float centerX, float centerY, float scale)
        {
            Pen pen = new Pen(Color.Purple, 2);
            PointF prevPoint = new PointF(centerX, centerY);

            for (float t = 0; t <= angle; t += 0.01f)
            {
                double r = (float)(t);
                float normalizationValue = (float)Math.Sqrt(Math.PI / 2);
                float x = centerX + (float)(FresnelCosNormalized(r) * normalizationValue * scale);
                float y = centerY + (float)(FresnelSinNormalized(r) * normalizationValue * scale);

                if (prevPoint.X != x || prevPoint.Y != y)
                {
                    g.DrawLine(pen, prevPoint, new PointF(x, y));
                    prevPoint = new PointF(x, y);
                }

                UpdateScale(scale, x, y);
            }
        }

        private static double FresnelCosNormalized(double t)
        {
            return Math.Cos(Math.PI / 2 * t * t);
        }

        private static double FresnelSinNormalized(double t)
        {
            return Math.Sin(Math.PI / 2 * t * t);
        }*/

        // Есть какое-то смещение из-за округления, коэффициент k не работает должным образом
        private void DrawRose(Graphics g, float centerX, float centerY, float scale)
        {
            Pen pen = new Pen(Color.Black, 2);
            PointF prevPoint = new PointF(centerX, centerY);

            for (float t = 0; t <= angle; t += 0.01f)
            {
                double k = 4;
                double r = Math.Cos((t / Math.PI));
                float x = centerX + (float)(r * Math.Cos(t) * scale);
                float y = centerY + (float)(r * Math.Sin(t) * scale);

                if (prevPoint.X != x || prevPoint.Y != y)
                {
                    g.DrawLine(pen, prevPoint, new PointF(x, y));
                    prevPoint = new PointF(x, y);
                }

                UpdateScale(scale, x, y);
            }
        }
    }
}
