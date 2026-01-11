using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Triangle_and_Matrix_classes.Models;

namespace Triangle_and_Matrix_classes.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private TriangleViewModel _triangleVM;
        public TriangleViewModel TriangleVM
        {
            get => _triangleVM;
            set { _triangleVM = value; OnPropertyChanged(); }
        }

        private MatrixViewModel _matrixVM;
        public MatrixViewModel MatrixVM
        {
            get => _matrixVM;
            set { _matrixVM = value; OnPropertyChanged(); }
        }

        // Текущая ViewModel для отображения
        public object CurrentViewModel
        {
            get
            {
                switch (SelectedObject)
                {
                    case "Треугольник":
                        return TriangleVM;
                    case "Матрица":
                        return MatrixVM;
                    default:
                        return null;
                }
            }
        }

        public MainViewModel()
        {
            Objects = new List<string>() { "Треугольник", "Матрица" };
            TriangleVM = new TriangleViewModel();
            MatrixVM = new MatrixViewModel();
        }

        private string _selectedObject;
        public string SelectedObject
        {
            get { return _selectedObject; }
            set
            {
                _selectedObject = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        private List<string> _objects;
        public List<string> Objects
        {
            get { return _objects; }
            set { _objects = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}