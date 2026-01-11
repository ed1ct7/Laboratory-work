using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;


namespace FirstProject.Classes
{
   public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public Point()
        {
            X = 0; Y = 0;
        }
        public Point(double x, double y)
        {
            X = x; Y = y;
        }
    }
   public class GeometricFigure
   {
        public GeometricFigure() {
            this.firstPoint = new Point(0, 0);
            this.color = Colors.White;
        }
        public GeometricFigure(Point firstPoint, Color color)
        {
            this.firstPoint = firstPoint;
            this.color = color;
        }
        public void print(string message)
        {
            Console.WriteLine(message);
        }

        public void move(double dx, double dy)
        {
            this.firstPoint.X += dx;
            this.firstPoint.Y += dy;
        }

        public Color color { get; private set; }
        public Point firstPoint { get; private set; }
    }
    public class Square : GeometricFigure
    {
        public Square() :base()
        {
            side = 1;
        }
        public Square(Point firstPoint, double side, Color color) : base(firstPoint, color)
        {
            this.side = side;
        }
        public double side { get; private set; }

        public new void draw()
        {

        }
    }
    public class Triangle : GeometricFigure
    {
        public double a { get; private set; }
        public double b { get; private set; }
        public double c { get; private set; }

        public Point A { get; private set; }
        public Point B { get; private set; }
        public Point C { get; private set; }

        public Triangle() : base()
        {
            double a = 1; double b = 1; double c = 1;
            A = new Point(firstPoint.X, firstPoint.Y);
            B = new Point(firstPoint.X + c, firstPoint.Y);
            C = new Point(((b * b - a * a - A.X * A.X + B.X * B.X) / 2 * (B.X - A.X)),
                A.Y + Math.Pow(Math.Sqrt((b * b - (((b * b - a * a - A.X * A.X + B.X * B.X) / 2 * (B.X - A.X)) - A.X))), 2));

        }

        public Triangle(Point firstPoint, double a, double b, double c, Color color) : base(firstPoint, color)
        {
            if (!(b + a > c || b + c > a || a + c > b))
            {
                throw new Exception("Ошибку");
            }

            A = new Point(firstPoint.X, firstPoint.Y);
            B = new Point(firstPoint.X + c, firstPoint.Y);
            C = new Point(((b * b - a * a - A.X * A.X + B.X * B.X) / 2 * (B.X - A.X)),
                A.Y + Math.Pow(Math.Sqrt((b * b - (((b * b - a * a - A.X * A.X + B.X * B.X) / 2 * 
                (B.X - A.X)) - A.X))), 2));

            this.a = a;
            this.b = b;
            this.c = c;
        }
        public new void print(string message)
        {
            Console.WriteLine(message + " Класс Tria");
        }
    }
}
