using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Triangle_and_Matrix_classes.Models;

//3.Создать класс треугольник, поля класса – длины 3-х сторон. Предусмотреть в 
//классе методы проверки существования треугольника, вычисления и вывода сведений 
//о фигуре – длины сторон, углы, периметр, площадь. Создать производный класс – 
//равнобедренный треугольник, предусмотреть в классе проверку, является ли треугольник
//равнобедренным.

namespace Triangle_and_Matrix_classes.ViewModel
{
    public class TriangleViewModel : INotifyPropertyChanged
    {
        public TriangleViewModel() {
            Triangle triangle = new Triangle();
        }
        
        private Parameters _sides;
        public Parameters Sides
        {
            get { return _sides; }
            set
            {
                _sides = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
