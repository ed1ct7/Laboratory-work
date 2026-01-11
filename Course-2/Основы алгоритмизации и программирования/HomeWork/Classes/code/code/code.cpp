//Написать функции, вычисляющие площадь и периметр треугольника, 
//Вершины двух треугольников заданы массивом точек (Point mas[6]).
//Точки являются объектом класса Point.
//
//Для инициализации и получения данных использовать функции Set и Get

#include <iostream>
#include <cmath>

using namespace std;

class Point {
public:
    Point(double x = 0, double y = 0) {
        this->x = x;
        this->y = y;
    }
    void set_x(double x) {
        this->x = x;
    }
    void set_y(double y) {
        this->y = y;
    }
    double get_x() {
        return this->x;
    }
    double get_y() {
        return this->y;
    }
private:
    double x, y;
};

double distance(Point a, Point b) {
    return sqrt(pow(b.get_x() - a.get_x(), 2) + pow(b.get_y() - a.get_y(), 2));
}

double triangle_area(Point a, Point b, Point c) {
    double A = distance(a, b);
    double B = distance(b, c);
    double C = distance(c, a);
    double p = (A + B + C) / 2;
    return sqrt(p * (p - A) * (p - B) * (p - C));
}

double triangle_perimeter(Point a, Point b, Point c) {
    double A = distance(a, b);
    double B = distance(b, c);
    double C = distance(c, a);
    return A + B + C;
}

bool triangle_exist(Point a, Point b, Point c) {
    return floor((triangle_area(a, b, c)) * 1000 + 0.5) / 100 != 0;
}

int main()
{
    setlocale(LC_ALL, "ru");

    Point mas[6]{};

    char arr[3]{ 'A','B','C' };
    double x, y;
    for (size_t i = 0; i < 2; i++)
    {
        cout << "Введите координаты точек треугольника " << arr[i] << ":" << endl;
        for (size_t j = 0; j < 3; j++)
        {
            cout << "\tКоординаты точки " << arr[j] << endl;
            cout << "\tx = "; cin >> x;
            mas[3 * i + j].set_x(x);
            cout << "\ty = "; cin >> y;
            mas[3 * i + j].set_y(y);
        }
        cout << endl;
    }
    cout << endl;

    for (size_t i = 0; i < 2; i++)
    {
        cout << "Треугольник: " << arr[i] << ":" << endl;
        cout << "\tДлины сторон: " << endl;
        cout << "\tСторона А = " << distance(mas[3 * i + 0], mas[3 * i + 1]) << endl;
        cout << "\tСторона B = " << distance(mas[3 * i + 1], mas[3 * i + 2]) << endl;
        cout << "\tСторона C = " << distance(mas[3 * i + 0], mas[3 * i + 2]) << endl;
        cout << endl;

        if (triangle_exist(mas[3 * i + 0], mas[3 * i + 1], mas[3 * i + 2])) {
            for (size_t j = 0; j < 3; j++)
            {
                cout << "\tКоординаты точки " << arr[j] << endl;
                cout << "\tx = " << mas[3 * i + j].get_x();
                cout << "\ty = " << mas[3 * i + j].get_y() << endl;
            }
            cout << endl;
            cout << "Площадь треугольника " << arr[i] << ":\t\t" << 
                triangle_area(mas[3 * i + 0], mas[3 * i + 1], mas[3 * i + 2]) << endl;
            cout << "Периметр треугольника " <<arr[i] << ":\t" << 
                triangle_perimeter(mas[3 * i + 0], mas[3 * i + 1], mas[3 * i + 2]) << endl << endl;
        }
        else { cout << "Треугольника " << arr[i] << " не существует" << endl << endl; }
    }
    cout << endl;
}
