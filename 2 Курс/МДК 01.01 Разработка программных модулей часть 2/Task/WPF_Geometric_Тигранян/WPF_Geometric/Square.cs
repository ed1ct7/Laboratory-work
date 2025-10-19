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
    internal class Square:GeometricObject
    {
        private int side;
       
        public Square() :base() 
        {
            side = 1;
            col = Colors.Yellow;
            brush = new SolidColorBrush(col);
        }

        public Square(int x, int y, int side, Color col )
            : base(x, y, col)
        {
            this.side = side;
            brush = new SolidColorBrush(col);
        }

        public override string Print(string message)
        {
            return base.Print(message) + $" a = {side}";
        }

        public override void Draw(Canvas canvas)
        {
            Rectangle square = new Rectangle
            {
                Width = side, // Ширина 
                Height = side, // Высота 
                Stroke = brush, //Brushes.Green,
                Fill = brush //Brushes.Red // Цвет заливки
            };

            Canvas.SetLeft(square, x ); 
            Canvas.SetTop(square, y );

            // Добавляем square на Canvas
            canvas.Children.Add(square);
        }
    }
}
