//Даны две дроби A / B и C / D. (А, В, С, D натуральные числа). Составить функции,
//возвращающие числитель и знаменатель дроби – результата деления дроби на дробь.
//Ответ должен быть несократимой дробью.

#include <iostream>
using namespace std;

struct Fraction {
    Fraction(int numerator, int denominator) {
        this->numerator = numerator;
        this->denominator = denominator;
    }
    Fraction(const Fraction& other)
    {
        this->numerator = other.numerator;
        this->denominator = other.denominator;
    }
    int getNumerator() {
        return numerator;
    }
    int getDenominator() {
        return denominator;
    }
    int getFractionRemains() {
        return numerator % denominator;
    }
    void shortenAFraction() {
        int gcd = findGCD(abs(numerator), abs(denominator)); // Находим НОД числителя и знаменателя
        if (gcd > 1) {
            numerator /= gcd;
            denominator /= gcd;
        }
        // Если знаменатель отрицательный, делаем числитель отрицательным вместо знаменателя
        if (denominator < 0) {
            numerator = -numerator;
            denominator = -denominator;
        }
    }
    // Вспомогательная функция для нахождения наибольшего общего делителя (алгоритм Евклида)
    int findGCD(int a, int b) {
        while (b != 0) {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
    void coutFunc() {
        cout << "Дробь = " << numerator << " / " << denominator << endl;
    }
private:
    int numerator = 0;
    int denominator = 0;
};

Fraction FractionDivision(Fraction First, Fraction Second) {
    Fraction Result(First.getNumerator() * Second.getDenominator(),
        First.getDenominator() * Second.getNumerator());
    Result.shortenAFraction();
    return Result;
}

int main()
{
    setlocale(LC_ALL, "ru");

    int A = 3; int B = 5;
    Fraction First(A, B);

    int C = 6; int D = 7;
    Fraction Second(C, D);

    FractionDivision(First, Second).coutFunc();

    return 0;
}