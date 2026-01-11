using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Ink;
using System.Diagnostics;

namespace code
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private double _brushSize = 5;
        private bool _updatingBrushSize = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public double BrushSize
        {
            get => _brushSize;
            set
            {
                if (Math.Abs(_brushSize - value) > double.Epsilon && !_updatingBrushSize)
                {
                    _updatingBrushSize = true;
                    _brushSize = value;
                    OnPropertyChanged(nameof(BrushSize));
                    UpdateBrushSize();
                    _updatingBrushSize = false;
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Initialize brush color
            BrushColorComboBox.SelectionChanged += BrushColorComboBox_SelectionChanged;
            UpdateBrushColor();

            // Initialize brush size controls
            if (BrushSizeSlider != null)
            {
                BrushSizeSlider.ValueChanged += BrushSizeSlider_ValueChanged;
                BrushSizeSlider.Value = BrushSize;
            }

            if (BrushSizeTextBox != null)
            {
                BrushSizeTextBox.TextChanged += BrushSizeTextBox_TextChanged;
                BrushSizeTextBox.Text = BrushSize.ToString("F0");
            }

            // Initialize drawing mode
            InkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            StatusBarRight.Text = "Режим: Рисование";
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.Tag != null)
            {
                switch (element.Tag.ToString())
                {
                    case "Закрытие приложения":
                        Application.Current.Shutdown();
                        break;
                    case "О программе":
                        MessageBox.Show("Группа С326 Тигранян Эдуард", "О программе");
                        break;
                }
            }
        }

        private void MenuItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is FrameworkElement element && element.Tag != null)
            {
                StatusBarLeft.Text = element.Tag.ToString();
            }
        }

        private void MenuItem_MouseLeave(object sender, MouseEventArgs e)
        {
            StatusBarLeft.Text = "";
            StatusBarRight.Text = "";
        }

        private void ChangeBackgroundColor(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.Tag != null)
            {
                var color = (Color)ColorConverter.ConvertFromString(element.Tag.ToString());
                InkCanvas.Background = new SolidColorBrush(color);
                Background = new SolidColorBrush(color);
            }
        }

        private void ColorChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.SelectedItem is ComboBoxItem item && item.Tag != null)
            {
                InkCanvas.Background = new SolidColorBrush(
                    (Color)ColorConverter.ConvertFromString(item.Tag.ToString()));
            }
        }

        private void BrushColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateBrushColor();
        }

        private void UpdateBrushColor()
        {
            if (BrushColorComboBox?.SelectedItem is ComboBoxItem item && item.Tag != null)
            {
                var color = (Color)ColorConverter.ConvertFromString(item.Tag.ToString());
                InkCanvas.DefaultDrawingAttributes.Color = color;
            }
        }

        private void BrushSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Prevent recursive updates
            if (Math.Abs(BrushSize - e.NewValue) > double.Epsilon)
            {
                BrushSize = e.NewValue;
                if (BrushSizeTextBox != null && !_updatingBrushSize)
                {
                    BrushSizeTextBox.Text = e.NewValue.ToString("F0");
                }
            }
        }

        private void BrushSizeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(BrushSizeTextBox.Text, out double newSize) &&
                Math.Abs(BrushSize - newSize) > double.Epsilon && !_updatingBrushSize)
            {
                BrushSize = newSize;
                if (BrushSizeSlider != null)
                {
                    BrushSizeSlider.Value = newSize;
                }
            }
        }

        private void UpdateBrushSize()
        {
            if (InkCanvas?.DefaultDrawingAttributes == null) return;

            try
            {
                InkCanvas.DefaultDrawingAttributes.Height = BrushSize;
                InkCanvas.DefaultDrawingAttributes.Width = BrushSize;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating brush size: {ex.Message}");
            }
        }

        private void ModeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (InkCanvas == null) return;

            if (DrawRadioButton?.IsChecked == true)
            {
                InkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                StatusBarRight.Text = "Режим: Рисование";
            }
            else if (EditRadioButton?.IsChecked == true)
            {
                InkCanvas.EditingMode = InkCanvasEditingMode.Select;
                StatusBarRight.Text = "Режим: Редактирование";
            }
            else if (EraseRadioButton?.IsChecked == true)
            {
                InkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
                StatusBarRight.Text = "Режим: Удаление";
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}