using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Documents;
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
            IsOpWithMatrixCommand = new RelayCommand(IsOpWithMatrix);
        }

        public ICommand CalculateCommand { get; }
        public ICommand IsOpWithMatrixCommand { get; }

        private void IsOpWithMatrix(object parameter)
        {
            if (OpWithMatrix == true)
            { 
                Matrix.Operations.Remove("compare"); 
            }
            else {
                Matrix.Operations.Add("compare");
            }
        }

        private double _singlevalue;

        public double SingleValue
        {
            get { return _singlevalue; }
            set { _singlevalue = value;
                OnPropertyChanged();
            }
        }


        private void Calculate(object parameter)
        {
            dynamic value = OpWithMatrix ? (dynamic)SingleValue : MatrixS;
            switch (SelectedItem)
            {
                case "+":
                    MatrixR.MatrixElements = MatrixF.Summation(value).MatrixElements;
                    break;
                case "-":
                    MatrixR.MatrixElements = MatrixF.Subtraction(value).MatrixElements;
                    break;
                case "*":
                    MatrixR.MatrixElements = MatrixF.Multiplication(value).MatrixElements;
                    break;
                case "compare":
                    Result = "Определитель первой матрицы ";
                    switch (MatrixF.CompareTo(MatrixS))
                    {
                        case -1:
                            Result += "<";
                            break;
                        case 0:
                            Result += "=";
                            break;
                        case 1:
                            Result += ">";
                            break;
                    }
                    Result += " второй матрицы";
                    break;
            }
            OnPropertyChanged(nameof(MatrixR));
            OnPropertyChanged(nameof(MatrixR.MatrixElements));
        }

        private string _result;
        public string Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged();
            }
        }

        public bool IsCompare
        {
            get
            {
                return SelectedItem == "compare";
            }
        }

        public ObservableCollection<string> Operations
        {
            get { return Matrix.Operations; }
        }

        private string _selectedItem;
        public string SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsCompare)); // Add this line
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

        private bool _opwithmatrix;

        public bool OpWithMatrix // flase with matrix, true with number
        {
            get { return _opwithmatrix; }
            set { _opwithmatrix = value;
                OnPropertyChanged();
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
            OnPropertyChanged(nameof(MatrixF));
            OnPropertyChanged(nameof(MatrixS));
            OnPropertyChanged(nameof(MatrixR));
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