using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WPF_Geometric
{
    public class Basic : DependencyObject
    {
        public static Basic Instance { get; } = new Basic();

        public static readonly DependencyProperty ButtonWidthProperty =
            DependencyProperty.Register(
                "ButtonWidth",
                typeof(double),
                typeof(Basic),
                new PropertyMetadata(150.0));

        public static readonly DependencyProperty ButtonHeightProperty =
            DependencyProperty.Register(
                "ButtonHeight",
                typeof(double),
                typeof(Basic),
                new PropertyMetadata(75.0));

        public double ButtonWidth
        {
            get { return (double)GetValue(ButtonWidthProperty); }
            set { SetValue(ButtonWidthProperty, value); }
        }

        public double ButtonHeight
        {
            get { return (double)GetValue(ButtonHeightProperty); }
            set { SetValue(ButtonHeightProperty, value); }
        }

        // Настройки Цвета кнопки

        public static readonly DependencyProperty ButtonBGProperty =
            DependencyProperty.Register(
                "ButtonBG",
                typeof(Brush),
                typeof(Basic),
        new PropertyMetadata(new SolidColorBrush(Color.FromRgb(255, 0, 100))));

        public static readonly DependencyProperty ButtonFGProperty =
            DependencyProperty.Register(
                "ButtonFG",
                typeof(Brush),
                typeof(Basic),
                new PropertyMetadata(Brushes.White));

        public static readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.Register(
                "BorderBrush",
                typeof(Brush),
                typeof(Basic),
                new PropertyMetadata(Brushes.Purple));

        public Brush ButtonBG
        {
            get { return (Brush)GetValue(ButtonBGProperty); }
            set { SetValue(ButtonBGProperty, value); }
        }
        public Brush ButtonFG
        {
            get { return (Brush)GetValue(ButtonFGProperty); }
            set { SetValue(ButtonFGProperty, value); }
        }
        public Brush ButtonBorderBrush
        {
            get { return (Brush)GetValue(BorderBrushProperty); }
            set { SetValue(BorderBrushProperty, value); }
        }

        public static readonly DependencyProperty WindowBGProperty =
            DependencyProperty.Register(
                "WindowBG",
                typeof(Brush),
                typeof(Basic),
        new PropertyMetadata(new SolidColorBrush(Color.FromRgb(17, 18, 19))));

        public static readonly DependencyProperty WindowACProperty =  // Additional Color
            DependencyProperty.Register(
                "WindowAC",
                typeof(Brush),
                typeof(Basic),
                new PropertyMetadata(Brushes.Black));

        public Brush WindowBG
        {
            get { return (Brush)GetValue(WindowBGProperty); }
            set { SetValue(WindowBGProperty, value); }
        }
        public Brush WindowAC
        {
            get { return (Brush)GetValue(WindowACProperty); }
            set { SetValue(WindowACProperty, value); }
        }

        /// <summary>
        /// /////////////////////////////////////
        /// </summary>

        public static readonly DependencyProperty TextBoxBGProperty =  // Additional Color
            DependencyProperty.Register(
                "TextBoxBG",
                typeof(Brush),
                typeof(Basic),
                new PropertyMetadata(new SolidColorBrush(Color.FromRgb(25, 26, 27))));

        public Brush TextBoxBG
        {
            get { return (Brush)GetValue(TextBoxBGProperty); }
            set { SetValue(TextBoxBGProperty, value); }
        }

        //Example How to Use//
        /*
         * Button
            Background="{Binding ButtonBG, Source={x:Static local:Basic.Instance}}" 
            Foreground="{Binding ButtonFG, Source={x:Static local:Basic.Instance}}" 
            BorderBrush="{Binding ButtonBorderBrush, Source={x:Static local:Basic.Instance}}"
        */

        public static readonly DependencyProperty HeadFontSizeProperty =
            DependencyProperty.Register(
                "HeadFontSize",
                typeof(double),
                typeof(Basic),
                new PropertyMetadata(30.0));

        public double HeadFontSize
        {
            get { return (double)GetValue(HeadFontSizeProperty); }
            set { SetValue(HeadFontSizeProperty, value); }
        }
    }
}