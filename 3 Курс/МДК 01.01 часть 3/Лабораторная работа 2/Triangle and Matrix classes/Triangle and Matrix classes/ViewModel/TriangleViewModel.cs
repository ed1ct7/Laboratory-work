using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Triangle_and_Matrix_classes.Interfaces;
using Triangle_and_Matrix_classes.Models;
using Triangle_and_Matrix_classes.ViewModel.Commands;

namespace Triangle_and_Matrix_classes.ViewModel
{
    public class TriangleViewModel : INotifyPropertyChanged, IViewModelBase
    {
        private Triangle _triangle;
        private string _result;

        public TriangleViewModel()
        {
            _triangle = new Triangle(3, 4, 5);
            CalculateCommand = new RelayCommand(Calculate);
            UpdateResult();
        }

        public double SideA
        {
            get => _triangle.Sides.A;
            set
            {
                if (Math.Abs(_triangle.Sides.A - value) > double.Epsilon)
                {
                    _triangle.Sides.A = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Sides));
                    UpdateAngles();
                }
            }
        }

        public double SideB
        {
            get => _triangle.Sides.B;
            set
            {
                if (Math.Abs(_triangle.Sides.B - value) > double.Epsilon)
                {
                    _triangle.Sides.B = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Sides));
                    UpdateAngles();
                }
            }
        }

        public double SideC
        {
            get => _triangle.Sides.C;
            set
            {
                if (Math.Abs(_triangle.Sides.C - value) > double.Epsilon)
                {
                    _triangle.Sides.C = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Sides));
                    UpdateAngles();
                }
            }
        }

        public Parameters Sides => _triangle.Sides;

        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged();
            }
        }

        public ICommand CalculateCommand { get; }

        public void Calculate(object parameter)
        {
            UpdateResult();
        }

        private void UpdateAngles()
        {
            _triangle.CalculateAngles();
            OnPropertyChanged(nameof(_triangle.Angles));
        }

        private void UpdateResult()
        {
            if (_triangle.IsExist())
            {
                var perimeter = _triangle.Perimeter();
                var area = _triangle.Area();
                var angles = _triangle.Angles;
                var isIsosceles = _triangle.Isosceles;

                Result = $"Треугольник существует!\n" +
                        $"Стороны: A={SideA:F2}, B={SideB:F2}, C={SideC:F2}\n" +
                        $"Углы: ∠A={angles.A:F2}°, ∠B={angles.B:F2}°, ∠C={angles.C:F2}°\n" +
                        $"Периметр: {perimeter:F2}\n" +
                        $"Площадь: {area:F2}\n" +
                        $"Равнобедренный: {(isIsosceles ? "Да" : "Нет")}";
            }
            else
            {
                Result = "Треугольник с такими сторонами не существует!\n" +
                        "Проверьте выполнение неравенства треугольника: \n" +
                        "a + b > c, a + c > b, b + c > a";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}