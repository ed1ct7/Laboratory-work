using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPF_Geometric
{
    public partial class MainWindow : Window
    {
        private Color _selectedColor = Colors.Blue;
        private string _selectedColorName = "Синий";
        private SolidColorBrush _selectedColorBrush;
        private bool _myPictureMode = false;

        public MainWindow()
        {
            InitializeComponent();
            _selectedColorBrush = new SolidColorBrush(_selectedColor);
            DataContext = this;
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DrawCoordinatePlane();
            myCanvas.MouseDown += MyCanvas_MouseDown;
        }

        private void MyCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_myPictureMode)
            {
                Point position = e.GetPosition(myCanvas);

                double x = position.X;
                double y = position.Y;

                int a = int.TryParse(TextBoxA.Text, out int aVal) ? aVal : 50;
                int b = int.TryParse(TextBoxB.Text, out int bVal) ? bVal : 100;
                int c = int.TryParse(TextBoxC.Text, out int cVal) ? cVal : 125;

                MyPicture myPicture = new MyPicture((int)x, (int)y, a, b, c, 6, SelectedColor); //20
                myPicture.Draw(myCanvas, 0.7, 50);
                labelInfo.Content = myPicture.Print($"Моя фигура создана в точке ({x}, {y})");
            }
        }

        private void DrawCoordinatePlane()
        {
            int width = (int)myCanvas.ActualWidth;
            int height = (int)myCanvas.ActualHeight;
            int step = 20;

            myCanvas.RenderTransform = new TransformGroup
            {
                Children = new TransformCollection
                {
                    new ScaleTransform(1, -1),
                    new TranslateTransform(width / 2, height / 2)
                }
            };

            for (int i = -width / 2; i <= width / 2; i += step)
            {
                DrawLine(i, -height / 2, i, height / 2, Brushes.LightGray);
            }
            for (int i = -height / 2; i <= height / 2; i += step)
            {
                DrawLine(-width / 2, i, width / 2, i, Brushes.LightGray);
            }

            DrawLine(-width / 2, 0, width / 2, 0, Brushes.Black);
            DrawLine(0, -height / 2, 0, height / 2, Brushes.Black);
        }

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

        public Color SelectedColor
        {
            get => _selectedColor;
            set
            {
                _selectedColor = value;
                _selectedColorBrush = new SolidColorBrush(value);
            }
        }

        public SolidColorBrush SelectedColorBrush
        {
            get => _selectedColorBrush;
            set => _selectedColorBrush = value;
        }

        private void ColorPickerButton_Click(object sender, RoutedEventArgs e)
        {
            var colorDialog = new System.Windows.Forms.ColorDialog();
            colorDialog.Color = System.Drawing.Color.FromArgb(
                _selectedColor.A, _selectedColor.R, _selectedColor.G, _selectedColor.B);

            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SelectedColor = Color.FromArgb(
                    colorDialog.Color.A,
                    colorDialog.Color.R,
                    colorDialog.Color.G,
                    colorDialog.Color.B);

                SelectedColorBrush = new SolidColorBrush(SelectedColor);
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        Random rnd = new Random();
        private void buttonPoint_Click(object sender, RoutedEventArgs e)
        {
            _myPictureMode = false; // Disable MyPicture spawning mode
            int x = int.TryParse(TextBoxX.Text, out int xVal) ? xVal : rnd.Next(-200, 200);
            int y = int.TryParse(TextBoxY.Text, out int yVal) ? yVal : rnd.Next(-200, 200);

            GeometricObject point = new GeometricObject(x, y, SelectedColor);
            point.Draw(myCanvas);
            labelInfo.Content = point.Print($"Я - точка!");
        }

        private void buttonSquare_Click(object sender, RoutedEventArgs e)
        {
            _myPictureMode = false; // Disable MyPicture spawning mode
            int x = int.TryParse(TextBoxX.Text, out int xVal) ? xVal : rnd.Next(-200, 200);
            int y = int.TryParse(TextBoxY.Text, out int yVal) ? yVal : rnd.Next(-200, 200);
            int a = int.TryParse(TextBoxA.Text, out int aVal) ? aVal : rnd.Next(10, 150);

            Square square = new Square(x, y, a, SelectedColor);
            square.Draw(myCanvas);
            labelInfo.Content = square.Print($"Я квадрат!");
        }

        private void buttonTriangle_Click(object sender, RoutedEventArgs e)
        {
            _myPictureMode = false; // Disable MyPicture spawning mode
            int x = int.TryParse(TextBoxX.Text, out int xVal) ? xVal : rnd.Next(-200, 200);
            int y = int.TryParse(TextBoxY.Text, out int yVal) ? yVal : rnd.Next(-200, 200);
            int a = int.TryParse(TextBoxA.Text, out int aVal) ? aVal : 50;
            int b = int.TryParse(TextBoxB.Text, out int bVal) ? bVal : 100;
            int c = int.TryParse(TextBoxC.Text, out int cVal) ? cVal : 125;

            Triangle triangle = new Triangle(x, y, a, b, c, SelectedColor);
            triangle.Draw(myCanvas);
            labelInfo.Content = triangle.Print($"Я треугольник!");
        }

        private void buttonMyPicture_Click(object sender, RoutedEventArgs e)
        {
            // Toggle MyPicture spawning mode
            _myPictureMode = !_myPictureMode;

            if (_myPictureMode)
            {
                labelInfo.Content = "Режим создания моей фигуры: кликните на холст, чтобы создать фигуру";
                buttonMyFigure.Background = Brushes.LightGreen;
            }
            else
            {
                labelInfo.Content = "Режим создания моей фигуры выключен";
                buttonMyFigure.Background = SystemColors.ControlBrush;
            }
        }

        private void buttonFractal_Click(object sender, RoutedEventArgs e)
        {
            _myPictureMode = false; // Disable MyPicture spawning mode
            labelInfo.Content = "Функция фрактала пока не реализована";
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            _myPictureMode = false; // Disable MyPicture spawning mode
            myCanvas.Children.Clear();
            DrawCoordinatePlane();
            buttonMyFigure.Background = SystemColors.ControlBrush; // Reset button color
        }
    }
}