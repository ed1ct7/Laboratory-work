using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Triangle_and_Matrix_classes.Models;
using Triangle_and_Matrix_classes.ViewModels.Commands;

namespace Triangle_and_Matrix_classes.ViewModel
{
    public class MatrixViewModel : INotifyPropertyChanged
    {
        public MatrixViewModel()
        {
            MatrixF = new Matrix();
            MatrixS = new Matrix();
            MatrixR = new Matrix();
            OnResize();
            CalculateCommand = new RelayCommand(Calculate);
        }

        public ICommand CalculateCommand { get; }
        private void Calculate(object parameter)
        {
            switch (SelectedItem)
            {
                case "+":
                    MatrixR.MatrixElements = MatrixF.Summation(MatrixS).MatrixElements;
                    break;
                case "-":
                    MatrixR.MatrixElements = MatrixF.Subtraction(MatrixS).MatrixElements;
                    break;
                case "*":
                    MatrixR.MatrixElements = MatrixF.Multiplication(MatrixS).MatrixElements;
                    break;
                case "compare":
                    switch (MatrixF.CompareTo(MatrixS))
                    {
                        case -1:
                            resultString = "-1";
                            break;
                        case 0:
                            resultString = "0";
                            break;
                        case 1:
                            resultString = "1";
                            break;
                    }
                    ;
                    break;
            }
        }

        public string resultString
        {
            get; 
            set {
                OnPropertyChanged();    
            }
        }

        public bool isCompare
        {
            get {
                return (SelectedItem == "compare" ? true : false);
            }
        }

        public ObservableCollection<string> Operations{
            get {  return Matrix.Operations; }  
            }

        private string _selectedItem;

        public string SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value;
                OnPropertyChanged();
            }
        }

        private Matrix _matrixF;
        public Matrix MatrixF
        {
            get => _matrixF;
            set
            {
                _matrixF = value;
                OnPropertyChanged();
            }
        }

        private Matrix _matrixS;
        public Matrix MatrixS
        {
            get => _matrixS;
            set
            {
                _matrixS = value;
                OnPropertyChanged();
            }
        }

        private Matrix _matrixR;
        public Matrix MatrixR
        {
            get => _matrixR;
            set
            {
                _matrixR = value;
                OnPropertyChanged();
            }
        }

        public int Size
        {
            get => MatrixF.Size;
            set
            {
                MatrixF.Size = value;
                MatrixS.Size = value;
                MatrixR.Size = value;
                OnPropertyChanged();
                OnResize();
            }
        }

        public void OnResize()
        {
            MatrixF.MatrixElements.Clear();
            MatrixS.MatrixElements.Clear();
            MatrixR.MatrixElements.Clear();
            for (int i = 0; i < Size * Size; i++)
            {
                MatrixF.MatrixElements.Add(new MatrixElement());
                MatrixS.MatrixElements.Add(new MatrixElement());
                MatrixR.MatrixElements.Add(new MatrixElement());
            }
            OnPropertyChanged(nameof(MatrixF.MatrixElements));
            OnPropertyChanged(nameof(MatrixS.MatrixElements));
            OnPropertyChanged(nameof(MatrixR.MatrixElements));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}