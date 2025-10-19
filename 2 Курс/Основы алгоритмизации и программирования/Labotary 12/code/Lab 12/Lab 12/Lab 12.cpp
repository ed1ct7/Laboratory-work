//Заменить минимальные положительные элементы в массиве целых чисел 
//на среднее арифметическое его значений.Создать функции для вычисления:
//  среднего значения элементов массива 
//  определения минимального положительного элемента.
//  формирования массива случайными числами;
#include <iostream>
#include <cstdlib>
#include <ctime>
#include <limits>
#include <string>
#include <sstream>

using namespace std;

template <typename T>
class Vector {
public:
    Vector() {
        this->capacity = 1;
        this->length = 0;
        this->arr = new T[1];
    }

    Vector(const Vector& other) {
        this->capacity = other.capacity;
        this->length = other.length;
        this->arr = new T[other.capacity];
        for (size_t i = 0; i < capacity; i++)
        {
            *(arr + i) = *(other.arr + i);
        }
    }

    void coutf() {
        for (size_t i = 0; i < this->length; i++)
        {
            cout << *(this->arr + i) << " ";
        }
        cout << "\n";
    }

    void cinf() {
        cout << "Вводите элементы массива (или 'stop' для завершения):\n";
        while (true) {
            string input;
            cin >> input;
            if (input == "stop") {
                cout << endl;
                break; // Выход из цикла
            }
            T temp;
            stringstream ss(input);
            ss >> temp;
            if (ss.fail() || !ss.eof()) {
                cout << "Ошибка: неверный формат ввода. Повторите попытку.\n";
                continue;
            }
            push_back(temp);
        }
    }

    void push_back(T data) {
        if (length == capacity) {
            T* temp = new T[2 * capacity];
            for (size_t i = 0; i < length; i++)
            {
                *(temp + i) = *(arr + i);
            }
            capacity *= 2;
            delete[] arr;
            arr = temp;
        }
        *(this->arr + length) = data;
        length++;
    }
    
    void clear() {
        delete[] arr;
        arr = new T[1];
        capacity = 1;
        length = 0;
    }

    T* getarr() { return this->arr; }
    
    size_t getLength() { return this->length; }
    
    size_t getCapacity() { return this->capacity; }

    ~Vector() { delete[] this->arr; }

private:
    size_t capacity, length;
    T* arr;
};

double average(Vector<double>& vec);
double minPositiveNum(Vector<double>& vec);
void randomFill(Vector<double>& vec, size_t N, bool wipeOut, double min, double max);

int main()
{
    setlocale(LC_ALL, "ru");
    srand(time(0));
    Vector<double> a;

    //randomFill(a, 10, true, 0, 0);
    a.cinf();
    a.coutf();

    double avg = average(a);
    double minPos = minPositiveNum(a);
    if (minPos == 0) {
        cout << "Нет положительных чисел" << endl;
        return 1;
    }

    cout << "Среднее: " << avg << endl;
    cout << "Минимальное позитивное число: " << minPos << endl;

    // Замена минимальных положительных элементов на среднее арифметическое
    for (size_t i = 0; i < a.getLength(); i++) {
        if (a.getarr()[i] == minPos) {
            a.getarr()[i] = avg;
        }
    }

    a.coutf();
    return 0;
}

double average(Vector<double>& vec)
{
    double sum = 0;
    for (size_t i = 0; i < vec.getLength(); i++) {
        sum += *(vec.getarr() + i);
    }
    return sum / vec.getLength();
}

double minPositiveNum(Vector<double>& vec)
{
    double minPositiveNum = numeric_limits<double>::max();
    bool foundPositive = false;
    for (size_t i = 0; i < vec.getLength(); i++) {
        if (*(vec.getarr() + i) > 0) {
            foundPositive = true;
            if (*(vec.getarr() + i) < minPositiveNum) {
                minPositiveNum = *(vec.getarr() + i);
            }
        }
    }
    if (!foundPositive) {
        return 0;
        //throw runtime_error("Нет позитивных чисел(");
    }
    return minPositiveNum;
}

void randomFill(Vector<double>& vec, size_t N, bool wipeOut, double min, double max)
{
    if (N > 0){
        if (wipeOut = true) {
            vec.clear();
            for (size_t i = 0; i < N; i++) {
                vec.push_back(trunc((double)rand() / (double)RAND_MAX * (max - min) + min));
            }
        }
        else if (wipeOut = false) {
            for (size_t i = vec.getLength(); i < N + vec.getLength(); i++) {
                vec.push_back(trunc((double)rand() / (double)RAND_MAX * (max - min) + min));
            }
        }
    } else { cout << "N должно быть больше 0\n" << endl; }
}