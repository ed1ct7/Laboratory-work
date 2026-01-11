#include <iostream>
#include <vector>
#include <chrono>
#include <random>
#include <algorithm>
#include <iomanip>

using namespace std;

// Сортировка методом выбора с обменом
void selectionSort(vector<double>& arr) {
    int n = arr.size();
    for (int i = 0; i < n - 1; i++) {
        int min_idx = i;
        for (int j = i + 1; j < n; j++) {
            if (arr[j] < arr[min_idx]) {
                min_idx = j;
            }
        }
        swap(arr[i], arr[min_idx]);
    }
}

// Сортировка методом слияния
void merge(double* l, double* m, double* r, double* temp) {
    double* cl = l, * cr = m;
    int cur = 0;
    while (cl < m && cr < r) {
        if (*cl < *cr) temp[cur++] = *cl, cl++;
        else temp[cur++] = *cr, cr++;
    }
    while (cl < m) temp[cur++] = *cl, cl++;
    while (cr < r) temp[cur++] = *cr, cr++;
    cur = 0;
    for (double* i = l; i < r; i++)
        *i = temp[cur++];
}

void _mergesort(double* l, double* r, double* temp) {
    if (r - l <= 1) return;
    double* m = l + (r - l) / 2;
    _mergesort(l, m, temp);
    _mergesort(m, r, temp);
    merge(l, m, r, temp);
}

void mergeSort(vector<double>& arr) {
    vector<double> temp(arr.size());
    _mergesort(arr.data(), arr.data() + arr.size(), temp.data());
}

// Функция случайного заполнения вектора
void fillArray(vector<double>& arr) {
    srand(static_cast<unsigned int>(time(nullptr)));
    for (auto& num : arr) {
        // Генерация числа от 1.0 до 10000.0
        num = 1.0 + (rand() / (RAND_MAX / (10000.0 - 1.0)));
    }
}

// Вывод фрагмента массива
void printArray(const vector<double>& arr, int m = 0, int n = -1) {
    if (n == -1) n = arr.size();
    cout << "[";
    for (int i = m; i < n; i++) {
        cout << fixed << setprecision(2) << arr[i];
        if (i < n - 1) cout << ", ";
    }
    cout << "]\n";
}

// Измерение времени выполнения функции
template<typename Func>
double measureTime(Func func) {
    auto start = chrono::high_resolution_clock::now();
    func();
    auto end = chrono::high_resolution_clock::now();
    chrono::duration<double> duration = end - start;
    return duration.count();
}

int main() {
    setlocale(LC_ALL, "ru");
    vector<int> sizes = {10, 100, 1000, 10000, 100000 };

    for (int size : sizes) {
        vector<double> original(size);
        fillArray(original);

        cout << "\nРазмер массива = " << size << "\n";

        if (size <= 20) {
            cout << "Оригинальный массив: ";
            printArray(original);
        }

        // Сортировка выбором
        vector<double> selectionArr = original;
        double selectionTime = measureTime([&]() {
            selectionSort(selectionArr);
            });

        cout << "Скорость методом выбора: " << fixed << setprecision(6)
            << selectionTime << " сек.\n";
        if (size <= 20) {
            cout << "Отсортированный массив: ";
            printArray(selectionArr);
        }

        // Сортировка слиянием
        vector<double> mergeArr = original;
        double mergeTime = measureTime([&]() {
            mergeSort(mergeArr);
            });

        cout << "Скорость методом слияния: " << fixed << setprecision(6)
            << mergeTime << " сек.\n";
        if (size <= 20) {
            cout << "Отсортированный массив: ";
            printArray(mergeArr);
        }
    }

    return 0;
}