#include <iostream>
#include <conio.h>
#include <limits>
#include <string>
using namespace std;

class Matrix {
public:
    Matrix(size_t N) {
        this->N = N;
        if (this->N == 0) {
            arr = nullptr;
            return;
        }
        this->arr = new double* [this->N];
        for (size_t i = 0; i < this->N; i++)
        {
            this->arr[i] = new double[this->N];
            for (size_t j = 0; j < this->N; j++)
                this->arr[i][j] = 0;
        }
    }
    Matrix(const Matrix& other) {
        this->N = other.N;
        if (this->N == 0) {
            arr = nullptr;
            return;
        }
        this->arr = new double* [this->N];
        for (size_t i = 0; i < this->N; i++) {
            this->arr[i] = new double[this->N];
            for (size_t j = 0; j < this->N; j++)
                this->arr[i][j] = other.arr[i][j];
        }
    }
    void coutf() {
        for (size_t i = 0; i < this->N; i++)
        {
            for (size_t j = 0; j < this->N; j++)
                cout << this->arr[i][j] << " ";
            cout << "\n";
        }
        cout << "\n";
    }
    void cinf() {
        for (size_t i = 0; i < this->N; i++) {
            for (size_t j = 0; j < this->N; j++)
            {
                error:
                double value{ 0 };
                bool isNegative = false;
                bool hasDecimalPoint = false;
                bool hasDigit = false;
                bool digitAfterPoint = false;
                string str = "";
                while (true) {
                    if (_kbhit()) {
                        char ch = _getch();
                        if (ch == 13) {
                            cout << "\t";
                            break;
                        }
                        else if (ch == '-') {
                            if (hasDigit == true) {
                                cout << "Недопустимый ввод, повторите попытку: \n";
                                goto error;
                            }
                            else {
                                isNegative = true;
                                cout << ch;
                            }
                        }
                        else if (ch >= '0' && ch <= '9' || ch == ',' && !hasDecimalPoint) {
                            if (ch == ',') {
                                if (hasDigit == false) {
                                    cout << "Недопустимый ввод, повторите попытку: \n";
                                    goto error;
                                }
                                hasDecimalPoint = true;
                                str += ch;
                                cout << ch;
                            }
                            else {
                                cout << ch;
                                if (hasDecimalPoint == true)
                                {
                                    digitAfterPoint = true;
                                }
                                hasDigit = true;
                                str += ch;
                            }
                        }
                        else if (ch == 8) { // Обработка Backspace
                            if (!str.empty()) {
                                char lastChar = str.back();
                                if (lastChar == '-') {
                                    isNegative = false;
                                }
                                else if (lastChar == ',') {
                                    hasDecimalPoint = false;
                                    digitAfterPoint = false;
                                }
                                else if (lastChar >= '0' && lastChar <= '9') {
                                    if (hasDecimalPoint && !digitAfterPoint) {
                                        digitAfterPoint = false;
                                    }
                                }
                                str.pop_back();
                                cout << "\b \b";
                            }
                        }
                    }
                }
                if (hasDigit == false || (hasDecimalPoint == true && digitAfterPoint == false)) {
                    cout << "Недопустимый ввод, повторите попытку: \n";
                    goto error;
                }
                value = stod(str);
                if (isNegative) {
                    value *= -1;
                }
                this->arr[i][j] = value;
            }
            cout << endl;
        }
        cout << endl;
    }
    Matrix multByNumber(double x) {
        Matrix Temp(this->N);
        for (size_t i = 0; i < this->N; i++)
        {
            for (size_t j = 0; j < this->N; j++)
                Temp.getarr()[i][j] = this->arr[i][j] * x;
        }
        return Temp;
    }
    double** getarr() { return this->arr; }
    size_t getN() {return this->N;}
    ~Matrix() {
        if (arr) {
            for (size_t i = 0; i < this->N; i++)
                delete[] this->arr[i];
            delete[] this->arr;
        }
    }
private:
    size_t N = 0;
    double** arr;
};

Matrix multiplication(Matrix A, Matrix B) {
    if (A.getN() != B.getN()) {
        cout << "Ошибка: несовместимые размеры матриц" << endl;
        return Matrix(0);
    }
    Matrix Temp(A.getN());
    for (size_t i = 0; i < A.getN(); i++) {
        for (size_t j = 0; j < B.getN(); j++) {
            Temp.getarr()[i][j] = 0;
            for (size_t k = 0; k < A.getN(); k++)
                Temp.getarr()[i][j] += A.getarr()[i][k] * B.getarr()[k][j];
        }
    }
    return Temp;
}

Matrix summation(Matrix A, Matrix B) {
    if (A.getN() != B.getN()) {
        cout << "Ошибка: несовместимые размеры матриц" << endl;
        return Matrix(0);
    }
    Matrix Temp(A.getN());
    for (size_t i = 0; i < Temp.getN(); i++)
    {
        for (size_t j = 0; j < A.getN(); j++)
            Temp.getarr()[i][j] = A.getarr()[i][j] + B.getarr()[i][j];
    }
    return Temp;
}

Matrix substraction(Matrix A, Matrix B) {
    if (A.getN() != B.getN()) {
        cout << "Ошибка: несовместимые размеры матриц" << endl;
        return Matrix(0);
    }
    Matrix Temp(A.getN());
    for (size_t i = 0; i < Temp.getN(); i++)
    {
        for (size_t j = 0; j < A.getN(); j++)
            Temp.getarr()[i][j] = A.getarr()[i][j] - B.getarr()[i][j];
    }
    return Temp;
}

int main()
{
    setlocale(LC_ALL, "ru");

    int n;
    cout << "Введите n: ";
    cin >> n;
    while (n <= 0) {
        cout << "Ошибка ввода n, повторите попытку...\n";
        cout << "Введите n: ";
        cin >> n;
    }
    cin.ignore(numeric_limits<streamsize>::max(), '\n');

    cout << "Введите значения для матрицы A:\n";
    Matrix A(n);
    A.cinf();

    cout << "Введите значения для матрицы B:\n";
    Matrix B(n);
    B.cinf();

    cout << "Полученный результат:\n";
    Matrix C = multiplication(summation(A.multByNumber(2.5), B), substraction(A, multiplication(B,B)));
    C.coutf();

    return 0;
}