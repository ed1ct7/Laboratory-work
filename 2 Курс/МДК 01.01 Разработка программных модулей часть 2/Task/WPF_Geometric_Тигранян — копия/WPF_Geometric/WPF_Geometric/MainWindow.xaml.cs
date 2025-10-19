using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPF_Geometric
{
    public partial class MainWindow : Window
    {
        private Color _selectedColor = Colors.Blue; // Текущий выбранный цвет по умолчанию
        private SolidColorBrush _selectedColorBrush; // Кисть с текущим цветом
        private bool _myPictureMode = false; // Флаг режима рисования пользовательской фигуры
        public MainWindow()
        {
            InitializeComponent();
            _selectedColorBrush = new SolidColorBrush(_selectedColor); // Создание кисти с выбранным цветом
            Loaded += MainWindow_Loaded;                               // Cобытие загрузки окна

            FractalTypeComboBox.ItemsSource = new List<string>         // Инициализация ComboBox типами фракталов
            {
                "Koch Snowflake",
                "Sierpinski Triangle",
                "Square Fractal"
            };
        }

        // Метод для получения выбранного типа фрактала
        public FractileType GetSelectedFractalType()
        {
            return (FractileType)FractalTypeComboBox.SelectedIndex;
        }

        // Обработчик события загрузки окна
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DrawCoordinatePlane();                    // Отрисовка координатной плоскости
            myCanvas.MouseDown += MyCanvas_MouseDown; // Cобытие клика мыши по холсту
        }

        // Метод для отрисовки координатной плоскости
        private void DrawCoordinatePlane()
        {
            // Получение размеров холста
            int width = (int)myCanvas.ActualWidth;
            int height = (int)myCanvas.ActualHeight;
            int step = 20; // Шаг сетки

            // Установка преобразований для центрального расположения координат
            myCanvas.RenderTransform = new TransformGroup
            {
                Children = new TransformCollection
                {
                    new ScaleTransform(1, -1), // Инверсия оси Y
                    new TranslateTransform(width / 2, height / 2) // Смещение в центр
                }
            };

            // Отрисовка вертикальных линий сетки
            for (int i = -width / 2; i <= width / 2; i += step)
            {
                DrawLine(i, -height / 2, i, height / 2, Brushes.LightGray);
            }

            // Отрисовка горизонтальных линий сетки
            for (int i = -height / 2; i <= height / 2; i += step)
            {
                DrawLine(-width / 2, i, width / 2, i, Brushes.LightGray);
            }

            // Отрисовка основных осей координат
            DrawLine(-width / 2, 0, width / 2, 0, Brushes.Black); // Ось X
            DrawLine(0, -height / 2, 0, height / 2, Brushes.Black); // Ось Y
        }

        // Метод для отрисовки линии на холсте
        private void DrawLine(double x1, double y1, double x2, double y2, Brush color)
        {
            Line line = new Line
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Stroke = color,
                StrokeThickness = 1
            };
            myCanvas.Children.Add(line);
        }

        // Свойство для выбранного цвета с логикой обновления
        public Color SelectedColor
        {
            get => _selectedColor;
            set
            {
                _selectedColor = value;
                _selectedColorBrush = new SolidColorBrush(value);
            }
        }

        // Свойство для кисти с выбранным цветом
        public SolidColorBrush SelectedColorBrush
        {
            get => _selectedColorBrush;
            set => _selectedColorBrush = value;
        }

        // Метод валидации ввода чисел в текстовые поля
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string newText = textBox.Text.Insert(textBox.SelectionStart, e.Text);
            // Проверка ввода на цифру или минус
            bool isDigit = char.IsDigit(e.Text, 0);
            bool isMinus = e.Text == "-";
            // Блокировка недопустимых символов
            if (!isDigit && !isMinus)
            {
                e.Handled = true;
                return;
            }
            // Дополнительная проверка для минуса
            if (isMinus)
            {
                // Минус разрешен только в начале и только один
                if (textBox.SelectionStart != 0 || textBox.Text.Contains("-"))
                {
                    e.Handled = true;
                }
            }
        }

        // Генератор случайных чисел
        Random rnd = new Random();

        ///////////////////////////////////////////////////////////////////////////////
        // События нажатия кнопок
        private void buttonPoint_Click(object sender, RoutedEventArgs e)
        {
            _myPictureMode = false; // Отключение режима пользовательской фигуры

            // Парсинг координат или генерация случайных значений
            int x = int.TryParse(TextBoxX.Text, out int xVal) ? xVal : rnd.Next(-200, 200);
            int y = int.TryParse(TextBoxY.Text, out int yVal) ? yVal : rnd.Next(-200, 200);

            // Создание и отрисовка точки
            GeometricObject point = new GeometricObject(x, y, SelectedColor);
            point.Draw(myCanvas);
            labelInfo.Content = point.Print($"Я - точка!");
        }
        private void buttonSquare_Click(object sender, RoutedEventArgs e)
        {
            _myPictureMode = false; // Отключение режима пользовательской фигуры

            // Парсинг параметров или генерация случайных значений
            int x = int.TryParse(TextBoxX.Text, out int xVal) ? xVal : rnd.Next(-200, 200);
            int y = int.TryParse(TextBoxY.Text, out int yVal) ? yVal : rnd.Next(-200, 200);
            int a = int.TryParse(TextBoxA.Text, out int aVal) ? aVal : rnd.Next(10, 150);

            // Создание и отрисовка квадрата
            Square square = new Square(x, y, a, SelectedColor);
            square.Draw(myCanvas);
            labelInfo.Content = square.Print($"Я квадрат!");
        }
        private void buttonTriangle_Click(object sender, RoutedEventArgs e)
        {
            _myPictureMode = false; // Отключение режима пользовательской фигуры

            // Парсинг параметров или установка значений по умолчанию
            int x = int.TryParse(TextBoxX.Text, out int xVal) ? xVal : rnd.Next(-200, 200);
            int y = int.TryParse(TextBoxY.Text, out int yVal) ? yVal : rnd.Next(-200, 200);
            int a = int.TryParse(TextBoxA.Text, out int aVal) ? aVal : 50;
            int b = int.TryParse(TextBoxB.Text, out int bVal) ? bVal : 100;
            int c = int.TryParse(TextBoxC.Text, out int cVal) ? cVal : 125;

            // Создание и отрисовка треугольника
            Triangle triangle = new Triangle(x, y, a, b, c, SelectedColor);
            triangle.Draw(myCanvas);
            labelInfo.Content = triangle.Print($"Я треугольник!");
        }
        ////////////
        private void buttonMyPicture_Click(object sender, RoutedEventArgs e)
        {
            // Переключение состояния режима
            _myPictureMode = !_myPictureMode;

            if (_myPictureMode)
            {
                // Обновление интерфейса для активного режима
                labelInfo.Content = "Режим создания моей фигуры: кликните на холст, чтобы создать фигуру";
                buttonMyFigure.Background = Brushes.LightGreen;
            }
            else
            {
                // Обновление интерфейса для неактивного режима
                labelInfo.Content = "Режим создания моей фигуры выключен";
                buttonMyFigure.Background = SystemColors.ControlBrush;
            }
        }
        // Обработчик клика мыши по холсту
        private void MyCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_myPictureMode)
            {
                // Получение позиции клика относительно холста
                Point position = e.GetPosition(myCanvas);

                // Извлечение координат X и Y
                double x = position.X;
                double y = position.Y;

                // Парсинг параметров фигуры из текстовых полей с значениями по умолчанию
                int a = int.TryParse(TextBoxA.Text, out int aVal) ? aVal : 50;
                int b = int.TryParse(TextBoxB.Text, out int bVal) ? bVal : 100;
                int c = int.TryParse(TextBoxC.Text, out int cVal) ? cVal : 125;

                // Создание и отрисовка пользовательской фигуры
                MyPicture myPicture = new MyPicture((int)x, (int)y, a, b, c, 6, SelectedColor);
                myPicture.Draw(myCanvas, 0.7, 50);

                // Обновление информационной метки
                labelInfo.Content = myPicture.Print($"Моя фигура создана");
            }
        }
        ////////////
        private void buttonFractal_Click(object sender, RoutedEventArgs e)
        {
            _myPictureMode = false; // Отключение режима пользовательской фигуры

            // Парсинг координат или генерация случайных значений
            int x = int.TryParse(TextBoxX.Text, out int xVal) ? xVal : rnd.Next(-200, 200);
            int y = int.TryParse(TextBoxY.Text, out int yVal) ? yVal : rnd.Next(-200, 200);

            // Создание и отрисовка фрактала
            Fractal fractal = new Fractal(x, y, Colors.Blue, GetSelectedFractalType());
            fractal.Draw(myCanvas);
            labelInfo.Content = "Фрактал";
        }
        private void buttonMySquare_Click(object sender, RoutedEventArgs e)
        {
            _myPictureMode = false; // Отключение режима пользовательской фигуры

            // Парсинг параметров или генерация случайных значений
            int x = int.TryParse(TextBoxX.Text, out int xVal) ? xVal : rnd.Next(-200, 200);
            int y = int.TryParse(TextBoxY.Text, out int yVal) ? yVal : rnd.Next(-200, 200);
            int a = int.TryParse(TextBoxA.Text, out int aVal) ? aVal : rnd.Next(10, 150);

            // Создание и отрисовка пользовательского квадрата
            MySquare mysquare = new MySquare(x, y, a, SelectedColor);
            mysquare.Draw(myCanvas);
            labelInfo.Content = mysquare.Print($"Я квадрат!");
        }
        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            _myPictureMode = false; // Отключение режима пользовательской фигуры
            myCanvas.Children.Clear(); // Очистка холста
            DrawCoordinatePlane(); // Повторная отрисовка координатной плоскости
            buttonMyFigure.Background = SystemColors.ControlBrush; // Сброс цвета кнопки
        }
        private void ColorPickerButton_Click(object sender, RoutedEventArgs e)
        {
            // Создание диалога выбора цвета
            var colorDialog = new System.Windows.Forms.ColorDialog();
            colorDialog.Color = System.Drawing.Color.FromArgb(
                _selectedColor.A, _selectedColor.R, _selectedColor.G, _selectedColor.B);

            // Проверка результата диалога
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Обновление выбранного цвета
                SelectedColor = Color.FromArgb(
                    colorDialog.Color.A,
                    colorDialog.Color.R,
                    colorDialog.Color.G,
                    colorDialog.Color.B);

                // Обновление кисти
                SelectedColorBrush = new SolidColorBrush(SelectedColor);
            }
        }
        ///////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////
    }
}