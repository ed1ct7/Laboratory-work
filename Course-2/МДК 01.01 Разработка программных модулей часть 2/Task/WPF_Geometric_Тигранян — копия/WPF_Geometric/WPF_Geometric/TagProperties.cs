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
    }
}