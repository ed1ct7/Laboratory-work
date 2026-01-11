using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPF_Geometric
{
    internal class Triangle : GeometricObject
    {
        public double a { get; private set; }
        public double b { get; private set; }
        public double c { get; private set; }

        public System.Windows.Point A { get; set; }
        public System.Windows.Point B { get; set; }
        public System.Windows.Point C { get; set; }

        public Triangle() : base()
        {
            a = 1; b = 1; c = 1;
            A = new System.Windows.Point(x, y);
            B = new System.Windows.Point(x + c, y);
            C = new System.Windows.Point(x + c / 2, y - Math.Sqrt(b * b - (c / 2) * (c / 2)));
        }

        public Triangle(int x, int y, double a, double b, double c, Color col) : base(x, y, col)
        {
            if (!(a + b > c && a + c > b && b + c > a))
            {
                throw new Exception("Треугольник с такими сторонами не существует");
            }

            this.a = a;
            this.b = b;
            this.c = c;

            A = new System.Windows.Point(x, y);
            B = new System.Windows.Point(x + c, y);
            C = new System.Windows.Point(x + (b * b - a * a + c * c) / (2 * c), y 
                - //determine up or down
                Math.Sqrt(b * b - (x + (b * b - a * a + c * c) / (2 * c) - x) *
                (x + (b * b - a * a + c * c) / (2 * c) - x)));
        }

        public override void Draw(Canvas canvas)
        {
            Polygon triangle = new Polygon
            {
                Stroke = brush,
                Fill = brush,
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
        }
    }
}