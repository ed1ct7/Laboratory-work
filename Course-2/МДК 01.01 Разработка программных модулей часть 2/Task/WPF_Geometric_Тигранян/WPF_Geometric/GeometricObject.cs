using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPF_Geometric
{
    public class GeometricObject
    {
        public int x { get; private set; }
        public int y { get; private set; }
        public string color { get; private set; }
        public Brush brush { get; set; }
        public Color col { get; set; }
        public GeometricObject()
        {
            x = 0;
            y = 0;
            //col = new Color();
            col = Colors.Red;
            brush = new SolidColorBrush(col);
        }

        public GeometricObject(int x, int y, Color col)
        {
            this.x = x;
            this.y = y;
            this.col = col;
            brush = new SolidColorBrush(col);
        }

        public virtual string Print(string message)
        {
            return $"{message} x = {x}, y = {y}";
        }

        public virtual void Draw(Canvas canvas)
        {
            Ellipse point = new Ellipse
            {
                Width = 4, // Ширина окружности
                Height = 4, // Высота окружности
                Fill = brush
            };

            // Устанавливаем позицию окружности
            Canvas.SetLeft(point, x - 2); // Центрируем окружность по координатам x
            Canvas.SetTop(point, y - 2); // Центрируем окружность по координатам y

            // Добавляем точку на Canvas
            canvas.Children.Add(point);
        }
    }
}
