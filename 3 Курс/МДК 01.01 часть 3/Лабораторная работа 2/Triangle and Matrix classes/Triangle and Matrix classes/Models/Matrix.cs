using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Triangle_and_Matrix_classes.Models
{
    public class MatrixElement : INotifyPropertyChanged
    {
        private double _value;
        public double Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Matrix : INotifyPropertyChanged, IComparable<Matrix>
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public static readonly ObservableCollection<string> Operations
            = new ObservableCollection<string>
            {
                "+", "-", "*", "compare"
            };

        public Matrix()
        {
            MatrixElements = new ObservableCollection<MatrixElement>();
        }

        public Matrix(ObservableCollection<MatrixElement> matrixElements)
        {
            this.MatrixElements = matrixElements;
        }

        private int _size = 2;
        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }

        private ObservableCollection<MatrixElement> _matrixElements;
        public ObservableCollection<MatrixElement> MatrixElements
        {
            get { return _matrixElements; }
            set { _matrixElements = value;
                OnPropertyChanged();
            }
        }

        // Проверка, является ли матрица верхнетреугольной
        public bool IsUpperTriangular
        {
            get
            {
                for (int i = 1; i < Size; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (GetElement(i, j) != 0)
                            return false;
                    }
                }
                return true;
            }
        }

        // Проверка, является ли матрица нижнетреугольной
        public bool IsLowerTriangular
        {
            get
            {
                for (int i = 0; i < Size; i++)
                {
                    for (int j = i + 1; j < Size; j++)
                    {
                        if (GetElement(i, j) != 0)
                            return false;
                    }
                }
                return true;
            }
        }

        // Сложение матриц
        public Matrix Summation(Matrix other)
        {
            if (Size != other.Size)
                throw new ArgumentException("Матрицы должны быть одного размера");

            var result = new Matrix();
            result.Size = Size;
            result.MatrixElements = new ObservableCollection<MatrixElement>();

            for (int i = 0; i < Size * Size; i++)
            {
                result.MatrixElements.Add(new MatrixElement
                {
                    Value = this.MatrixElements[i].Value + other.MatrixElements[i].Value
                });
            }

            return result;
        }

        // Вычитание матриц
        public Matrix Subtraction(Matrix other)
        {
            if (Size != other.Size)
                throw new ArgumentException("Матрицы должны быть одного размера");

            var result = new Matrix();
            result.Size = Size;
            result.MatrixElements = new ObservableCollection<MatrixElement>();

            for (int i = 0; i < Size * Size; i++)
            {
                result.MatrixElements.Add(new MatrixElement
                {
                    Value = this.MatrixElements[i].Value - other.MatrixElements[i].Value
                });
            }

            return result;
        }

        // Умножение матриц
        public Matrix Multiplication(Matrix other)
        {
            if (Size != other.Size)
                throw new ArgumentException("Матрицы должны быть одного размера");

            var result = new Matrix();
            result.Size = Size;
            result.MatrixElements = new ObservableCollection<MatrixElement>();

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < Size; k++)
                    {
                        sum += GetElement(i, k) * other.GetElement(k, j);
                    }
                    result.MatrixElements.Add(new MatrixElement { Value = sum });
                }
            }

            return result;
        }

        // Умножение матрицы на число
        public Matrix Multiplication(double number)
        {
            var result = new Matrix();
            result.Size = Size;
            result.MatrixElements = new ObservableCollection<MatrixElement>();

            for (int i = 0; i < Size * Size; i++)
            {
                result.MatrixElements.Add(new MatrixElement
                {
                    Value = this.MatrixElements[i].Value * number
                });
            }
            return result;
        }
        // Вспомогательный метод для получения элемента по индексам
        private double GetElement(int row, int col)
        {
            return MatrixElements[row * Size + col].Value;
        }

        // Вспомогательный метод для установки элемента по индексам
        private void SetElement(int row, int col, double value)
        {
            MatrixElements[row * Size + col].Value = value;
        }


        public double Determinant
        {
            get
            {
                if (Size == 1) return GetElement(0, 0);
                if (Size == 2) return GetElement(0, 0) * GetElement(1, 1) - GetElement(0, 1) * GetElement(1, 0);
                if (Size == 3)
                {
                    double deter = 0;
                    double temp = 1;

                    //
                    for (int i = 0, j = 0, l = 0;
                        l <= Size - 1;
                        l = (i >= Size ? l += 1 : l), // количество итераций
                        j = (i >= Size ? l : (++j >= Size ? 0 : j)), // проход по столбцам
                        i = (i >= Size ? 0 : i += 1)) // проход по строкам
                    {
                        if (i >= Size)
                        {
                            deter += temp;
                            temp = 1;
                        }
                        else
                        {
                            temp *= GetElement(i, j);
                        }
                    }

                    // 
                    temp = 1;
                    for (int i = 0, j = Size - 1, l = 0;
                        l <= Size - 1;
                        l = (i >= Size ? l += 1 : l), // количество итераций
                        j = (i >= Size ? Size - 1 - l : (--j < 0 ? Size - 1 : j)), // проход по столбцам backwards
                        i = (i >= Size ? 0 : i += 1)) // проход по строкам
                    {
                        if (i >= Size)
                        {
                            deter -= temp;
                            temp = 1;
                        }
                        else
                        {
                            temp *= GetElement(i, j);
                        }
                    }

                    return deter;
                }
                else
                {

                    double deter = 0;
                    int[] perm = new int[Size];
                    bool[] used = new bool[Size];
                    int sign = 1;

                    for (int i = 0; i < Size; i++)
                    {
                        perm[i] = i;
                        used[i] = true;
                    }

                    for (int iteration = 0; ; iteration++)
                    {
                        double product = 1;
                        for (int i = 0; i < Size; i++)
                        {
                            product *= GetElement(i, perm[i]);
                        }
                        deter += sign * product;

                        if (!NextPermutation(perm, used, ref sign))
                            break;
                    }

                    return deter;
                }
            }
        }

        private bool NextPermutation(int[] perm, bool[] used, ref int sign)
        {
            int n = perm.Length;
            int k = n - 2;

            while (k >= 0 && perm[k] >= perm[k + 1])
                k--;

            if (k < 0)
                return false;

            int l = n - 1;
            while (perm[k] >= perm[l])
                l--;

            int temp = perm[k];
            perm[k] = perm[l];
            perm[l] = temp;

            Array.Reverse(perm, k + 1, n - k - 1);

            sign *= -1;

            return true;
        }


        public double Volume()
        {
            return Determinant;
        }

        public int CompareTo(Matrix? other)
        {
            if (other == null) throw new Exception("Нельзя сравнить объекты");
            return Volume().CompareTo(other.Volume());
        }
    }
}