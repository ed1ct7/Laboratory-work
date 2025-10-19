using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task_Org
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public class GeometricFigure
    {
        public GeometricFigure(double x = 0, double y = 0, string color = "белый")
        {
            this.x = x; 
            this.y = y;
            this.color = color;
        }
        public void print(string message)
        {
            Console.WriteLine(message);
        }

        public void move(double dx, double dy){
            this.x += dx;
            this.y += dy;
        }

        double x, y;
        string color;
    }

    public class Square : GeometricFigure {
        double x, y;
        int side;
        string color;
        Square(double x = 0, double y = 0, int side = 1, string color = "белый")
        {
            this.x = x;
            this.y = y;
            this.side = side;
            this.color = color;
        }
    }
}