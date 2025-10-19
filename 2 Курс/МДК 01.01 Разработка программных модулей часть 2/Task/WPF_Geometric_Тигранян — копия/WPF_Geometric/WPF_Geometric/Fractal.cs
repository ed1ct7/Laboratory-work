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
    // Перечисление типов фракталов
    public enum FractileType
    {
        KochSnowflake,    // Снежинка Коха
        SierpinskiTriangle, // Треугольник Серпинского
        SquareFractal     // Квадратный фрактал
    }
    internal class Fractal : GeometricObject
    {
        public FractileType fractileType { get; set; } // Тип фрактала
        public int Depth { get; set; } = 4;            // Глубина фрактала

        public Fractal() : base()
        {
            col = Colors.Yellow;
            brush = new SolidColorBrush(col);
            fractileType = FractileType.KochSnowflake;
        }
        public Fractal(int x, int y, Color col, FractileType fractileType, int depth = 5)
            : base(x, y, col)
        {
            this.fractileType = fractileType;
            brush = new SolidColorBrush(col);
            Depth = depth;
        }
        public override void Draw(Canvas canvas)
        {
            switch (fractileType)
            {
                case FractileType.KochSnowflake:
                    DrawKochSnowflake(canvas);
                    break;
                case FractileType.SierpinskiTriangle:
                    DrawSierpinskiTriangle(canvas);
                    break;
                case FractileType.SquareFractal:
                    DrawSquareFractal(canvas);
                    break;
            }
        }
        // Метод для отрисовки снежинки Коха
        private void DrawKochSnowflake(Canvas canvas)
        {
            // Размер стороны треугольника
            double size = 200;
            // Вычисление координат вершин равностороннего треугольника
            System.Windows.Point p1 = new System.Windows.Point(x, y + size * Math.Sqrt(3) / 2);
            System.Windows.Point p2 = new System.Windows.Point(x + size, y + size * Math.Sqrt(3) / 2);
            System.Windows.Point p3 = new System.Windows.Point(x + size / 2, y);

            // Отрисовка трех сторон снежинки Коха с рекурсией
            DrawKochLine(canvas, p1, p2, Depth);
            DrawKochLine(canvas, p2, p3, Depth);
            DrawKochLine(canvas, p3, p1, Depth);
        }
        // Рекурсивный метод для отрисовки линии Коха
        private void DrawKochLine(Canvas canvas, System.Windows.Point start, System.Windows.Point end, int depth)
        {
            // Базовый случай рекурсии - отрисовка прямой линии
            if (depth == 0)
            {
                // Создание объекта линии
                Line line = new Line
                {
                    X1 = start.X,
                    Y1 = start.Y,
                    X2 = end.X,
                    Y2 = end.Y,
                    Stroke = brush, // Кисть для обводки
                    StrokeThickness = 1 // Толщина линии
                };
                // Добавление линии на холст
                canvas.Children.Add(line);
                return;
            }
            // Вычисление промежуточных точек для кривой Коха
            System.Windows.Point p1 = start;
            System.Windows.Point p2 = new System.Windows.Point((2 * start.X + end.X) / 3, (2 * start.Y + end.Y) / 3);
            System.Windows.Point p3 = new System.Windows.Point(
                (start.X + end.X) / 2 - (end.Y - start.Y) * Math.Sqrt(3) / 6,
                (start.Y + end.Y) / 2 + (end.X - start.X) * Math.Sqrt(3) / 6);
            System.Windows.Point p4 = new System.Windows.Point((start.X + 2 * end.X) / 3, (start.Y + 2 * end.Y) / 3);
            System.Windows.Point p5 = end;

            // Рекурсивная отрисовка четырех сегментов кривой Коха
            DrawKochLine(canvas, p1, p2, depth - 1);
            DrawKochLine(canvas, p2, p3, depth - 1);
            DrawKochLine(canvas, p3, p4, depth - 1);
            DrawKochLine(canvas, p4, p5, depth - 1);
        }
        // Метод для отрисовки треугольника Серпинского
        private void DrawSierpinskiTriangle(Canvas canvas)
        {
            // Размер стороны треугольника
            double size = 200;
            // Вычисление координат вершин треугольника
            System.Windows.Point top = new System.Windows.Point(x + size / 2, y);
            System.Windows.Point left = new System.Windows.Point(x, y + size);
            System.Windows.Point right = new System.Windows.Point(x + size, y + size);
            // Вызов рекурсивного метода отрисовки
            DrawSierpinski(canvas, top, left, right, Depth);
        }
        // Рекурсивный метод для отрисовки треугольника Серпинского
        private void DrawSierpinski(Canvas canvas, System.Windows.Point a, System.Windows.Point b, System.Windows.Point c, int depth)
        {
            if (depth == 0)
            {
                Polygon triangle = new Polygon
                {
                    Points = new PointCollection { a, b, c }, // Установка вершин
                    Stroke = brush, // Кисть для обводки
                    Fill = brush,   // Кисть для заливки
                    StrokeThickness = 1 // Толщина обводки
                };
                // Добавление треугольника на холст
                canvas.Children.Add(triangle);
                return;
            }
            // Вычисление середин сторон треугольника
            System.Windows.Point ab = new System.Windows.Point((a.X + b.X) / 2, (a.Y + b.Y) / 2);
            System.Windows.Point bc = new System.Windows.Point((b.X + c.X) / 2, (b.Y + c.Y) / 2);
            System.Windows.Point ca = new System.Windows.Point((c.X + a.X) / 2, (c.Y + a.Y) / 2);
            // Рекурсивная отрисовка трех подтреугольников
            DrawSierpinski(canvas, a, ab, ca, depth - 1);
            DrawSierpinski(canvas, ab, b, bc, depth - 1);
            DrawSierpinski(canvas, ca, bc, c, depth - 1);
        }

        // Метод для отрисовки квадратного фрактала
        private void DrawSquareFractal(Canvas canvas)
        {
            // Вызов рекурсивного метода отрисовки вложенных квадратов
            DrawNestedSquares(canvas, new System.Windows.Point(x + 100, y + 100), 150, Depth);
        }

        // Рекурсивный метод для отрисовки вложенных квадратов
        private void DrawNestedSquares(Canvas canvas, System.Windows.Point center, double size, int depth)
        {
            // Условие выхода из рекурсии
            if (depth <= 0) return;

            // Создание объекта прямоугольника
            Rectangle square = new Rectangle
            {
                Width = size,  // Ширина
                Height = size, // Высота
                Stroke = brush, // Кисть для обводки
                StrokeThickness = 1, // Толщина обводки
                Fill = Brushes.Transparent // Прозрачная заливка
            };
            // Установка позиции прямоугольника на холсте
            Canvas.SetLeft(square, center.X - size / 2);
            Canvas.SetTop(square, center.Y - size / 2);
            // Добавление прямоугольника на холст
            canvas.Children.Add(square);

            // Рекурсивная отрисовка меньших квадратов по углам
            double newSize = size / 2.5;
            DrawNestedSquares(canvas, new System.Windows.Point(center.X - size / 2, center.Y - size / 2), newSize, depth - 1); // Левый верхний квадрат
            DrawNestedSquares(canvas, new System.Windows.Point(center.X + size / 2, center.Y - size / 2), newSize, depth - 1); // Правый верхний квадрат
            DrawNestedSquares(canvas, new System.Windows.Point(center.X - size / 2, center.Y + size / 2), newSize, depth - 1); // Левый нижний квадрат
            DrawNestedSquares(canvas, new System.Windows.Point(center.X + size / 2, center.Y + size / 2), newSize, depth - 1); // Правый нижний квадрат
        }
    }
}