//Описать рекурсивную функцию вычисления значения по формуле :
// Корень (1 + корень (2 + корень (n))

#include <iostream>
#include <cmath>

using namespace std;

double recursiveSqrt(int current, int n) {
    if (current == n) {
        return sqrt(n);
    }
    return sqrt(current + recursiveSqrt(current + 1, n));
}

int main() {
    setlocale(LC_ALL, "ru");

    int n;
    cout << "Введите значение n: ";
    cin >> n;
    while (n <= 0) {
        cout << "N должно быть > 0\n" << endl;
        cout << "Введите значение n: ";
        cin >> n;
    }

    double result = recursiveSqrt(1, n);
    cout << "Результат: " << result << endl;

    return 0;
}