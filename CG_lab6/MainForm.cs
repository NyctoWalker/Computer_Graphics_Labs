using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CG_lab6
{
    public partial class MainForm : Form
    {
        private Random _random = new Random();
        private Dictionary<string, int> itemProb;
        private List<Image> imList = new();
        private string imagePath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName.ToString() + "\\Images";
        private List<(Color, Color)> colorPairs;

        private string ChooseObject(Dictionary<string, int> items)
        {
            int sum = 0;
            List<int> steps = new();
            List<string> keys = new();

            foreach (var item in items)
            {
                sum += item.Value;
                keys.Add(item.Key);

                if (steps.Count() != 0)
                    steps.Add(steps[steps.Count - 1] + item.Value);
                else
                    steps.Add(item.Value);
            }

            int pick = _random.Next(1, sum + 1);

            for (int i = 0; i < steps.Count(); i++)
            {
                if (pick > steps[i])
                    continue;
                else
                    return keys[i];
            }

            return "";
        }

        private void DrawBook(Graphics g, PointF origin, float height, float width, int depth, int depthOffset, int margin, Color cFront, Color cSide)
        {
            int trueDepth = depth - depthOffset;
            float x0 = origin.X + margin - trueDepth;
            float y0 = origin.Y + trueDepth;

            SolidBrush front = new(cFront);
            SolidBrush side = new(cSide);
            LinearGradientBrush paper = new LinearGradientBrush
                (
                new PointF(x0, y0 - height),
                new PointF(x0, y0 - height - trueDepth - 1), // Добавочная единица потому что при округлении градиент слетал на 1 пиксель
                Color.WhiteSmoke, // Front
                Color.DarkSlateGray // Back
                );

            PointF[] pFront = new PointF[]
            {
                new PointF(x0, y0), // lb
                new PointF(x0 + width - margin * 2, y0), // rb
                new PointF(x0 + width - margin * 2, y0 - height), // ru
                new PointF(x0, y0 - height), // lu
            };
            g.FillPolygon(front, pFront);

            PointF[] pPaper = new PointF[]
            {
                new PointF(x0 + width - margin * 2, y0 - height), // rb
                new PointF(x0, y0 - height), // lb
                new PointF(x0 + trueDepth, y0 - height - trueDepth), // lu
                new PointF(x0 + trueDepth + width - margin * 2, y0 - height - trueDepth), // ru
            };
            g.FillPolygon(paper, pPaper);

            PointF[] pSide = new PointF[]
            {
                new PointF(x0 + width - margin * 2, y0), // lb
                new PointF(x0 + width - margin * 2, y0 - height), // lu
                new PointF(x0 + trueDepth + width - margin * 2, y0 - height - trueDepth), // ru
                new PointF(x0 + trueDepth + width - margin * 2, y0 - trueDepth), // rb
            };
            g.FillPolygon(side, pSide);
        }

        public MainForm()
        {
            itemProb = new Dictionary<string, int>
            {
                {"Books", 6},
                {"Empty", 2},
                {"Image", 1},
            };

            colorPairs = new List<(Color, Color)>
            {
                (Color.Red, Color.DarkRed),
                (Color.AliceBlue, Color.Blue),
                (Color.DarkBlue, Color.Black),
                (Color.LightYellow, Color.Yellow),
                (Color.Orange, Color.DarkOrange),
                (Color.Green, Color.DarkGreen),
                (Color.MediumPurple, Color.DarkViolet),
            };

            foreach (string file in Directory.GetFiles(imagePath))
                imList.Add(Image.FromFile(file));

            InitializeComponent();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);

            int margin = 80;
            int depth = 20;

            int w0 = margin + depth;
            int w1 = Width - margin - depth;
            int cx = Width / 2;

            int h0 = margin + depth;
            int h1 = Height - margin - depth;
            int cy = Height / 2;

            SolidBrush back = new(Color.FromArgb(64, 0, 0));
            g.FillRectangle(back, w0, h0, w1 - w0, h1 - h0);

            #region LeftBorder
            SolidBrush lb = new(Color.SaddleBrown);

            Point[] left = new Point[]
            {
                new Point(w0, h0),
                new Point(w0 - depth, h0 + depth),
                new Point(w0 - depth, h1 + depth),
                new Point(w0, h1),
            };
            g.FillPolygon(lb, left);
            #endregion



            #region Shelves

            int iterations = 4; // Количество полок. Количество рядов меньше на 1.
            float h_diff = (h1 - h0) / (iterations - 1);
            
            int n_items = 6;
            float w_diff = (w1 - w0) / (n_items);

            for (int i = 0; i < iterations; i++)
            {
                float h_new = i == iterations-1 ? h0 : h1 - h_diff * i;
                
                LinearGradientBrush shelves = new LinearGradientBrush
                (
                new PointF(cx, h_new + depth),
                new PointF(cx, h_new),
                Color.SandyBrown, // Front
                Color.SaddleBrown // Back
                );

                PointF[] shelf = new PointF[]
                {
                new PointF(w0 - depth, h_new + depth),
                new PointF(w0, h_new),
                new PointF(w1, h_new),
                new PointF(w1 - depth, h_new + depth),
                };

                g.FillPolygon(shelves, shelf);

                // Заполнение предметами
                if (i != iterations - 1)
                {
                    // Деление по n сегментам
                    for (int j = 0; j < n_items; j++)
                    {
                        float w_new = w0 + w_diff * j;

                        string item = ChooseObject(itemProb);
                        int _margin = 2;
                        int _depthOffset = 5;
                        double _scale = 0.75;

                        switch (item)
                        {
                            case "Books":
                                int n_books = (int)(w_diff / 25);
                                float book_width = n_books > 0 ? w_diff / n_books : w_diff;

                                for (int k = 0; k < n_books; k++)
                                {
                                    var c = colorPairs[_random.Next(colorPairs.Count())];
                                    // NextDouble даёт 0 - 1
                                    float book_height = h_diff / 2 + (float)(_random.NextDouble() / 3) * h_diff;
                                    int _addOffset = (int)(_depthOffset * (float)(_random.NextDouble() - 0.5));

                                    DrawBook(g, new PointF(w_new + k*book_width, h_new), book_height, book_width, depth, _depthOffset + _addOffset, _margin, c.Item1, c.Item2);
                                }
                                break;
                            case "Image":
                                if (imList.Count() > 0)
                                {
                                    float cw = (w_new) + (depth - _depthOffset) / 2;
                                    float ch = (h_new - h_diff / 2) + (_depthOffset - depth) / 2;

                                    float img_h = (float)(h_diff * _scale);
                                    float img_w = (float)(w_diff * _scale) - _margin * 2;

                                    RectangleF imgRect = new RectangleF
                                    (
                                        cw,
                                        ch,
                                        img_w,
                                        img_h
                                    );

                                    g.DrawImage(imList[_random.Next(imList.Count())], imgRect);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            #endregion



            SolidBrush rb = new(Color.SandyBrown);

            #region RightBorder
            Point[] right = new Point[]
            {
                new Point(w1, h0),
                new Point(w1 - depth, h0 + depth),
                new Point(w1 - depth, h1 + depth),
                new Point(w1, h1),
            };
            g.FillPolygon(rb, right);
            #endregion

        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.ResizeRedraw, false);
            Invalidate();
        }
    }
}
