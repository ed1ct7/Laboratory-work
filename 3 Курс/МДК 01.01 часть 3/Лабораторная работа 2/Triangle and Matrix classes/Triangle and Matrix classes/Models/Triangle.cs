using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

//3.Создать класс треугольник, поля класса – длины 3-х сторон. Предусмотреть в 
//классе методы проверки существования треугольника, вычисления и вывода сведений 
//о фигуре – длины сторон, углы, периметр, площадь. Создать производный класс – 
//равнобедренный треугольник, предусмотреть в классе проверку, является ли треугольник
//равнобедренным.

namespace Triangle_and_Matrix_classes.Models
{
    public struct Parameters
    {
        private double _a;
        public double A
        {
            get { return _a; }
            set { _a = value; }
        }
        private double _b;
        public double B
        {
            get { return _b; }
            set { _b = value; }
        }
        private double _c;
        public double C
        {
            get { return _c; }
            set { _c = value; }
        }
    };
    public class Triangle : INotifyPropertyChanged
    {
        public Triangle(double a, double b, double c)
        {
            _sides = new Parameters { A = a, B = b, C = c };
        }
        public Triangle() { }


        private Parameters _sides;
        public Parameters Sides
        {
            get { return _sides; }
            set { _sides = value;
                OnPropertyChanged(); }
        }

        private Parameters _agles;
        public Parameters Angles
        {
            get { return _agles; }
            set
            {
                _agles = value;
                OnPropertyChanged();
            }
        }

        public bool IsExist()
        {
            return true;
        }
        public int Perimetr()
        {
            return 0;
        }
        public int Area()
        {
            return 0;
        }
        public bool IsIsosceles()
        {
            return true;
        }
        public void CalculateAngles()
        {

        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
