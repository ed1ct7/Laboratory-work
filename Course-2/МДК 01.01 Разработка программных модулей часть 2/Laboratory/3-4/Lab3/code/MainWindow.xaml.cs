using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Ink;
using System.Diagnostics;

namespace code
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Move initialization code to Loaded event
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Initialize brush color
            BrushColorComboBox.SelectionChanged += BrushColorComboBox_SelectionChanged;
            UpdateBrushColor();

            // Initialize brush size
            BrushSizeSlider.ValueChanged += BrushSizeSlider_ValueChanged;
            BrushSizeTextBox.Text = BrushSizeSlider.Value.ToString();
            UpdateBrushSize();

            // Set initial mode to drawing
            InkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            StatusBarRight.Text = "Режим: Рисование";
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).Tag.ToString() == "Закрытие приложения")
            {
                Application.Current.Shutdown();
            }
            else if (((FrameworkElement)sender).Tag.ToString() == "О программе")
            {
                MessageBox.Show("Группа С326 Тигранян Эдуард");
            }
        }

        private void MenuItem_MouseEnter(object sender, MouseEventArgs e)
        {
            StatusBarLeft.Text = ((FrameworkElement)sender).Tag.ToString();
        }

        private void MenuItem_MouseLeave(object sender, MouseEventArgs e)
        {
            StatusBarLeft.Text = "";
            StatusBarRight.Text = "";
        }

        private void ChangeBackgroundColor(object sender, RoutedEventArgs e)
        {
            InkCanvas.Background =
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(((FrameworkElement)sender).Tag.ToString()));
        }

        private void ColorChanged(object sender, RoutedEventArgs e)
        {
            if (((ComboBox)sender).SelectedItem is ComboBoxItem item)
            {
                InkCanvas.Background = new SolidColorBrush(
                    (Color)ColorConverter.ConvertFromString(item.Tag.ToString()));
            }
        }

        // New methods for graphic editor functionality
        private void BrushColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateBrushColor();
        }

        private void UpdateBrushColor()
        {
            if (BrushColorComboBox.SelectedItem is ComboBoxItem item && item.Tag != null)
            {
                var color = (Color)ColorConverter.ConvertFromString(item.Tag.ToString());
                InkCanvas.DefaultDrawingAttributes.Color = color;
            }
        }

        private void BrushSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateBrushSize();
        }

        private void UpdateBrushSize()
        {
            if (BrushSizeSlider == null || InkCanvas == null || InkCanvas.DefaultDrawingAttributes == null)
                return;
            try
            {
                double size = BrushSizeSlider.Value;
                InkCanvas.DefaultDrawingAttributes.Height = size;
                InkCanvas.DefaultDrawingAttributes.Width = size;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating brush size: {ex.Message}");
            }
        }

        private void ModeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (InkCanvas == null) return;

            if (DrawRadioButton.IsChecked == true)
            {
                InkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                StatusBarRight.Text = "Режим: Рисование";
            }
            else if (EditRadioButton.IsChecked == true)
            {
                InkCanvas.EditingMode = InkCanvasEditingMode.Select;
                StatusBarRight.Text = "Режим: Редактирование";
            }
            else if (EraseRadioButton.IsChecked == true)
            {
                InkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
                StatusBarRight.Text = "Режим: Удаление";
            }
        }
    }
}