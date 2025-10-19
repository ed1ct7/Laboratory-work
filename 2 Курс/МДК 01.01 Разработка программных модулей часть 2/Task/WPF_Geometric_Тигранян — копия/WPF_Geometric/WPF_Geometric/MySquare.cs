using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPF_Geometric
{
    internal class MySquare : Square
    {
        public MySquare() : base() { }
        int side;
        public MySquare(int x, int y, int side, Color col)
            : base(x, y, side, col)
        {
            this.side = side;
        }

        public override void Draw(Canvas canvas)
        {
            // Основной квадрат
            base.Draw(canvas);

            // Параметры для повернутого квадрата
            double rotatedSide = side * Math.Sqrt(2) / 2; // Сторона повернутого квадрата
            double offset = (side - rotatedSide) / 2;

            // Квадрат повернутый на 45 градусов
            Rectangle rotatedSquare = new Rectangle
            {
                Width = rotatedSide,
                Height = rotatedSide,
                Stroke = Brushes.Black,
                Fill = Brushes.Transparent,
                RenderTransform = new RotateTransform(45, rotatedSide / 2, rotatedSide / 2)
            };

            Canvas.SetLeft(rotatedSquare, x + offset);
            Canvas.SetTop(rotatedSquare, y + offset);
            canvas.Children.Add(rotatedSquare);

            // Круг
            double circleDiameter = rotatedSide;
            Ellipse circle = new Ellipse
            {
                Width = circleDiameter,
                Height = circleDiameter,
                Stroke = Brushes.Red,
                Fill = Brushes.Transparent
            };

            Canvas.SetLeft(circle, x + offset);
            Canvas.SetTop(circle, y + offset);
            canvas.Children.Add(circle);
        }

        public override string Print(string message)
        {
            return base.Print(message);
        }
    }
}