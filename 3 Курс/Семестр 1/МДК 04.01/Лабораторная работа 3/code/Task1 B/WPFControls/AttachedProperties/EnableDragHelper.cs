using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ORM_Individual.WPFControls.AttachedProperties
{
    public static class DragBehavior
    {
        public static readonly DependencyProperty IsDraggableProperty =
            DependencyProperty.RegisterAttached(
                "IsDraggable",
                typeof(bool),
                typeof(DragBehavior),
                new PropertyMetadata(false, OnIsDraggableChanged));

        public static void SetIsDraggable(DependencyObject element, bool value) =>
            element.SetValue(IsDraggableProperty, value);

        public static bool GetIsDraggable(DependencyObject element) =>
            (bool)element.GetValue(IsDraggableProperty);

        private static void OnIsDraggableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element)
            {
                element.MouseLeftButtonDown -= StartDrag;
                element.MouseLeftButtonUp -= StopDrag;
                element.MouseMove -= DoDrag;

                if ((bool)e.NewValue)
                {
                    element.MouseLeftButtonDown += StartDrag;
                    element.MouseLeftButtonUp += StopDrag;
                    element.MouseMove += DoDrag;
                }
            }
        }

        private static Point _offset;
        private static bool _isDragging = false;

        private static void StartDrag(object sender, MouseButtonEventArgs e)
        {
            if (sender is UIElement element)
            {
                _isDragging = true;
                _offset = e.GetPosition(element);
                element.CaptureMouse();
            }
        }

        private static void DoDrag(object sender, MouseEventArgs e)
        {
            if (_isDragging && sender is UIElement element)
            {
                var parent = VisualTreeHelper.GetParent(element) as Canvas;
                if (parent != null)
                {
                    Point currentPosition = e.GetPosition(parent);
                    Canvas.SetLeft(element, currentPosition.X - _offset.X);
                    Canvas.SetTop(element, currentPosition.Y - _offset.Y);
                }
            }
        }

        private static void StopDrag(object sender, MouseButtonEventArgs e)
        {
            _isDragging = false;
            if (sender is UIElement element)
                element.ReleaseMouseCapture();
        }
    }
}