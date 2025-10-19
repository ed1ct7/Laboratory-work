using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPF_Geometric
{
    internal class MyPicture : Triangle
    {
        public double n { get; private set; }

        public MyPicture() : base() {
            n = 4;
        }

        public MyPicture(int x, int y, double a, double b, double c, int n, Color col) : base(x,y,a,b,c,col)
        {

            y = Convert.ToInt32(C.Y);
            this.n = n;

            A = new System.Windows.Point(x, y);
            B = new System.Windows.Point(x + c, y);
            C = new System.Windows.Point(x + (b * b - a * a + c * c) / (2 * c), y
                + //determine up or down
                Math.Sqrt(b * b - (x + (b * b - a * a + c * c) / (2 * c) - x) *
                (x + (b * b - a * a + c * c) / (2 * c) - x)));

        }
        public void Rotate(double angle)
        {
            RotateTransform rotateTransform = new RotateTransform(angle, x, y);
            A = rotateTransform.Transform(A);
            B = rotateTransform.Transform(B);
            C = rotateTransform.Transform(C);
        }

        public SolidColorBrush GetRandomBrush()
        {
            Random random = new Random();

            byte r = (byte)random.Next(256);
            byte g = (byte)random.Next(256);
            byte b = (byte)random.Next(256);

            return new SolidColorBrush(Color.FromRgb(r, g, b));
        }

        public async void Draw(Canvas canvas, double angle, int delay)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10000);
            for (int j = 0; j < 360/(4*angle); j++)
            {
                brush = GetRandomBrush();
                Rotate(angle);
                for (int i = 0; i < n; i++)
                {
                    Polygon triangle = new Polygon
                    {
                        Stroke = brush,
                        Fill = null,
                        StrokeThickness = 2
                    };

                    PointCollection points = new PointCollection
                    {
                        A,
                        B,
                        C
                    };

                    triangle.Points = points;
                    canvas.Children.Add(triangle);
                    Rotate(360 / n);
                }
                await Task.Delay(delay);
                //canvas.Children.Clear();
            }
        }
    }
}
