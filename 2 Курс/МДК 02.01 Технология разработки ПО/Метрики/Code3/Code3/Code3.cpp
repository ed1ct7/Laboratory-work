#include <iostream>
#include <cmath>
using namespace std;

int main()
{
    setlocale(LC_ALL, "ru");
    cout << "Введите Кординаты точки" << endl;
    float x, y;
    cout << "Ввод x: " << endl;
    cin >> x;
    cout << "Ввод y: " << endl;
    cin >> y;
    
    if (y >= 0 && (
        (abs(x) + y <= 3 && y < 3) ||
        (abs(x) + y - 3 <= 2 && y >= 3) ||
        (abs(x) + y - 5 <= 1 && y >= 5)
        )) {
        cout << "Точка принадлежит фигуре";
    }
    else {
        cout << "Точка не принадлежит фигуре";
    }
}
