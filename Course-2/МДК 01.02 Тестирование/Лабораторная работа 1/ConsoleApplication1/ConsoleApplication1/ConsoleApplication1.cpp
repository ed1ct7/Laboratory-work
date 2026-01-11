#define _USE_MATH_DEFINES
#include <iostream>
#include <math.h>

using namespace std;

double func(double x, double y) {
    return (2 * log(x) * cos(x)) / (1 - pow(sin(y), 2));
}

int main()
{
    setlocale(LC_ALL, "ru");
    double x, y;
    cout << "Введите значения x и y: \n";
    cout << "x = "; cin >> x;
    while (x<=0) {
        cout << "Ошибка ввода x должен быть > 0, повторите попытку ввода" << endl;
        cout << "x = "; cin >> x;
    }
    cout << "y = "; cin >> y;
    while (abs(sin(y)) == 1) {
        cout << "Ошибка ввода y не может быть равен PI/2 + N" << endl;
        cout << "y = "; cin >> y;
    }
    double t = func(x, y);
    cout << "Результат выполнения программы: \nt = " << t;
    return 0;
}