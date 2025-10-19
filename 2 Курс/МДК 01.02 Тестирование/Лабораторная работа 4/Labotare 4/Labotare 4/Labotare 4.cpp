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
    int getDenumerator() {
        return denominator;
    }
    int getFractionRemains() {
        return numerator % denominator;
    }
    void shortenAFraction() {
        if (numerator % 2 == 0 && denominator % 2 == 0) {
            numerator /= 2;
            denominator /= 2;
        }
        else if (numerator % 3 == 0 && denominator % 3 == 0) {
            numerator /= 3;
            denominator /= 3;
        }
        else if (numerator % 5 == 0 && denominator % 5 == 0) {
            numerator /= 5;
            denominator /= 5;
        }
        else if (numerator % 7 == 0 && denominator % 7 == 0) {
            numerator /= 7;
            denominator /= 7;
        }
        else if (getFractionRemains() == 0) {
            numerator /= denominator;
            denominator = 1;
        }
    }
    void coutFunc() {
        cout << "Дробь = " << numerator << " / " << denominator << endl;
    }
private:
    int numerator = 0;
    int denominator = 0;
};

Fraction FractionDivision(Fraction First, Fraction Second) {
    Fraction Result(First.getNumerator() * Second.getDenumerator(),
        First.getDenumerator() * Second.getNumerator());
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