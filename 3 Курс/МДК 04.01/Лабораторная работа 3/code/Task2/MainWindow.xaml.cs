using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Windows;


namespace Task2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowVM();
        }
    }
}