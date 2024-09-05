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

namespace CG_lab8
{
    public partial class MainForm : Form
    {
        private Point PreviousPoint, point;

        private Bitmap bmp;
        private Pen blackPen;
        private Pen orangePen;
        private Graphics g;
        private Image initialImage;

        private List<Point> polygonPoints;

        public MainForm()
        {
            blackPen = new Pen(Color.Black, 4);
            orangePen = new Pen(Color.Orange, 2);

            InitializeComponent();

            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            pictureBox.Image = bmp;
            g = Graphics.FromImage(pictureBox.Image);
            g.Clear(Color.White);

            polygonPoints = new();

            UpdateSize(pictureBox.Width, pictureBox.Height);
        }

        private void UpdateSize(int w, int h)
        {
            int minWidth = MinimumSize.Width;
            int minHeight = MinimumSize.Height - 100;

            int maxWidth = Screen.PrimaryScreen.Bounds.Width;
            int maxHeight = Screen.PrimaryScreen.Bounds.Height - 100;

            int _width = w < minWidth ? minWidth : w >= maxWidth ? maxWidth : w;
            int _height = h < minHeight ? minHeight : h >= maxHeight ? maxHeight : h;

            this.Size = new Size(_width, _height + 100);
        }



        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog savedialog = new SaveFileDialog();

            //Свойства для savedialog
            savedialog.Title = "Сохранить картинку как ...";
            savedialog.OverwritePrompt = true;
            savedialog.CheckPathExists = true;
            savedialog.Filter =
            "Bitmap File(*.bmp)|*.bmp|" +
            "JPEG File(*.jpg)|*.jpg|" +
            "PNG File(*.png)|*.png";
            savedialog.ShowHelp = true;

            if (savedialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = savedialog.FileName; // Полный путь к файлу
                string strFilExtn = fileName.Remove(0, fileName.Length - 3); // Убираем расширение

                // Сохраняем
                switch (strFilExtn)
                {
                    case "bmp":
                        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case "jpg":
                        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case "png":
                        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    default:
                        break;
                }
            }
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName.ToString() + "\\Images";
            dialog.InitialDirectory = Directory.Exists(path) ? path : @"C:\";

            // Задаем расширения файлов
            dialog.Filter = "Image files (*.BMP, *.JPG, *.PNG)| *.bmp; *.jpg; *.png; ";
            if (dialog.ShowDialog() == DialogResult.OK)//вызываем диалоговое окно
            {
                UpdateImage(Image.FromFile(dialog.FileName));
                polygonPoints.Clear();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (initialImage is not null)
                UpdateImage(initialImage);

            polygonPoints.Clear();
        }

        private void UpdateImage(Image image)
        {
            initialImage = image; //Загружаем изображение

            int width = initialImage.Width;
            int height = initialImage.Height;
            pictureBox.Width = width;
            pictureBox.Height = height;

            bmp = new Bitmap(initialImage, width, height);
            pictureBox.Image = bmp; //записываем изображение в формате bmp
            g = Graphics.FromImage(pictureBox.Image);

            UpdateSize(width, height);
        }



        private void buttonGrayscale_Click(object sender, EventArgs e)
        {
            // Цикл по всему изображению
            for (int i = 0; i < bmp.Width; i++)
                for (int j = 0; j < bmp.Height; j++)
                {
                    int R = bmp.GetPixel(i, j).R;
                    int G = bmp.GetPixel(i, j).G;
                    int B = bmp.GetPixel(i, j).B;
                    Color p = MedianGrayscale(R, G, B); // Прозрачность, R, G, B
                    bmp.SetPixel(i, j, p); //записываем полученный цвет в точку
                }

            Refresh();
        }

        private void buttonTrueGrayscale_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < bmp.Width; i++)
                for (int j = 0; j < bmp.Height; j++)
                {
                    int R = bmp.GetPixel(i, j).R;
                    int G = bmp.GetPixel(i, j).G;
                    int B = bmp.GetPixel(i, j).B;
                    Color p = ConstGrayscale(R, G, B); // Прозрачность, R, G, B
                    bmp.SetPixel(i, j, p); //записываем полученный цвет в точку
                }

            Refresh();
        }

        private Color MedianGrayscale(int R, int G, int B)
        {
            int Gray = (int)((R + G + B) / 3);
            return Color.FromArgb(Gray, Gray, Gray);
        }

        private Color ConstGrayscale(int R, int G, int B)
        {
            int Gray = (int)((R * 0.2126d + G * 0.7152d + B * 0.0722d));
            return Color.FromArgb(Gray, Gray, Gray);
        }



        private void IsolateChannel(string chanel)
        {
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    if (!IsInPolygon(new Point(x, y), polygonPoints))
                    {
                        Color originalColor = bmp.GetPixel(x, y);

                        switch (chanel)
                        {
                            case "red":
                                bmp.SetPixel(x, y, Color.FromArgb(originalColor.R, 0, 0));
                                break;
                            case "green":
                                bmp.SetPixel(x, y, Color.FromArgb(0, originalColor.G, 0));
                                break;
                            case "blue":
                                bmp.SetPixel(x, y, Color.FromArgb(0, 0, originalColor.B));
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        int R = bmp.GetPixel(x, y).R;
                        int G = bmp.GetPixel(x, y).G;
                        int B = bmp.GetPixel(x, y).B;
                        Color p = ConstGrayscale(R, G, B);
                        bmp.SetPixel(x, y, p);
                    }
                }
            }

            Refresh();
        }

        private void buttonRed_Click(object sender, EventArgs e)
        {
            IsolateChannel("red");
        }

        private void buttonGreen_Click(object sender, EventArgs e)
        {
            IsolateChannel("green");
        }

        private void buttonBlue_Click(object sender, EventArgs e)
        {
            IsolateChannel("blue");
        }

        public static bool IsInPolygon(Point testPoint, List<Point> polygon)
        {
            int intersectionCount = 0;
            int vertexCount = polygon.Count;

            for (int i = 0; i < vertexCount; i++)
            {
                Point v1 = polygon[i];
                Point v2 = polygon[(i + 1) % vertexCount];

                // Проверка горизонтального пересечения для testPoint.Y
                if ((v1.Y > testPoint.Y) != (v2.Y > testPoint.Y))
                {
                    double intersectX = (v2.X - v1.X) * (testPoint.Y - v1.Y) / (v2.Y - v1.Y) + v1.X;
                    if (testPoint.X < intersectX)
                        intersectionCount++;
                }
            }
            // true если чётное количество пересечений, иначе false
            return (intersectionCount % 2) == 1;
        }



        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PreviousPoint.X = e.X;
                PreviousPoint.Y = e.Y;
            }
            else if (e.Button == MouseButtons.Right)
            {
                polygonPoints.Add(e.Location);

                // Для наглядности, можно закомментировать
                g.DrawEllipse(orangePen, new Rectangle(e.Location, new Size(2, 2)));

                UpdateImage(pictureBox.Image);
            }

        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            { 
                //запоминаем в point текущее положение курсора мыши
                point.X = e.X;
                point.Y = e.Y;
                
                //соединяем линией предыдущую точку с текущей
                g.DrawLine(blackPen, PreviousPoint, point);
                
                //текущее положение курсора мыши сохраняем в PreviousPoint
                PreviousPoint.X = point.X;
                PreviousPoint.Y = point.Y;
                pictureBox.Invalidate(); //Принудительно вызываем перерисовку
            }
        }
    }
}
