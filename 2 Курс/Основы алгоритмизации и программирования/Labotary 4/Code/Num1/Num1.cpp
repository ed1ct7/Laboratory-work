#include <iostream>
#include <math.h>
#include <iomanip>

using namespace std;

int main()
{
	setlocale(LC_ALL, "ru");
	float x = 0, S = 0; int K = 0;
	cout << "Введите x: ";
	cin >> x;
	x = int(x * 100.0) / 100.0;
	cout << "Введите K: ";
	cin >> K;

	if (fabs(x) < 1.00) {
		size_t k = 1;
		while (k <= K) {
			S += ((k * k) * (x * x) - k * x + 2) / (k * x);
			k++;
		}
		S += 3.00 / 5;
		cout << "Ответ: " << fixed << setprecision(2) << S;
	}
	else {
		cout << "Некоректное значение ввода\n";
	}
}
