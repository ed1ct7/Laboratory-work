using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF_Geometric
{
    public static class TagProperties
    {
        // Attached Property for CornerRadius
        public static readonly DependencyProperty CornerRadiusTagProperty =
            DependencyProperty.RegisterAttached(
                "CornerRadiusTag",
                typeof(CornerRadius),
                typeof(TagProperties),
                new PropertyMetadata(new CornerRadius(0)));

        public static CornerRadius GetCornerRadiusTag(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(CornerRadiusTagProperty);
        }

        public static void SetCornerRadiusTag(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(CornerRadiusTagProperty, value);
        }

        // Attached Property for Placeholder
        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.RegisterAttached(
                "PlaceholderText",
                typeof(string),
                typeof(TagProperties),
                new PropertyMetadata(string.Empty, OnPlaceholderTextChanged));

        public static string GetPlaceholderText(DependencyObject obj)
        {
            return (string)obj.GetValue(PlaceholderTextProperty);
        }

        public static void SetPlaceholderText(DependencyObject obj, string value)
        {
            obj.SetValue(PlaceholderTextProperty, value);
        }

        private static void OnPlaceholderTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                if (e.NewValue != null)
                {
                    textBox.Loaded += TextBox_Loaded;
                    textBox.TextChanged += TextBox_TextChanged;
                    textBox.GotFocus += TextBox_GotFocus;
                    textBox.LostFocus += TextBox_LostFocus;
                }
                else
                {
                    textBox.Loaded -= TextBox_Loaded;
                    textBox.TextChanged -= TextBox_TextChanged;
                    textBox.GotFocus -= TextBox_GotFocus;
                    textBox.LostFocus -= TextBox_LostFocus;
                }
            }
        }

        private static void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePlaceholderVisibility(sender as TextBox);
        }

        private static void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePlaceholderVisibility(sender as TextBox);
        }

        private static void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Background = new VisualBrush()
                {
                    Stretch = Stretch.None,
                    AlignmentX = AlignmentX.Left,
                    AlignmentY = AlignmentY.Center,
                    Visual = new TextBlock()
                    {
                        Text = GetPlaceholderText(textBox),
                        Foreground = Brushes.Gray,
                        FontStyle = FontStyles.Italic,
                        Margin = new Thickness(5, 0, 0, 0)
                    }
                };
            }
        }

        private static void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdatePlaceholderVisibility(sender as TextBox);
        }

        private static void UpdatePlaceholderVisibility(TextBox textBox)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Background = new VisualBrush()
                {
                    Stretch = Stretch.None,
                    AlignmentX = AlignmentX.Left,
                    AlignmentY = AlignmentY.Center,
                    Visual = new TextBlock()
                    {
                        Text = GetPlaceholderText(textBox),
                        Foreground = Brushes.Gray,
                        FontStyle = FontStyles.Italic,
                        Margin = new Thickness(5, 0, 0, 0)
                    }
                };
            }
            else
            {
                textBox.Background = Brushes.Transparent;
            }
        }
    }
}