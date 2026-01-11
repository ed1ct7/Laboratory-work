#include <iostream>
#include <stdexcept>
#include <cassert>
using namespace std;

struct Fraction {
    Fraction(int numerator, int denominator) {
        if (denominator == 0) {
            throw invalid_argument("Знаменатель не может быть равен нулю");
        }
        this->numerator = numerator;
        this->denominator = denominator;
    }

    Fraction(const Fraction& other) {
        this->numerator = other.numerator;
        this->denominator = other.denominator;
    }

    int getNumerator() const {
        return numerator;
    }

    int getDenominator() const {
        return denominator;
    }

    void shortenAFraction() {
        int gcd = findGCD(abs(numerator), abs(denominator));
        if (gcd > 1) {
            numerator /= gcd;
            denominator /= gcd;
        }
        if (denominator < 0) {
            numerator = -numerator;
            denominator = -denominator;
        }
    }

    // (алгоритм Евклида)
    int findGCD(int a, int b) {
        while (b != 0) {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

private:
    int numerator;
    int denominator;
};

Fraction FractionDivision(Fraction first, Fraction second) {
    // Проверка деления на ноль (если вторая дробь равна 0)
    if (second.getNumerator() == 0) {
        throw runtime_error("Ошибка: деление на ноль");
    }

    Fraction result(first.getNumerator() * second.getDenominator(),
        first.getDenominator() * second.getNumerator());
    result.shortenAFraction();
    return result;
}
