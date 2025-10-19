#include "pch.h"
#include "CppUnitTest.h"
#include "D:\Laboratory\МДК 01.02 Тестирование\Лабораторная работа 4\code Task 1\code Task 1\code.cpp"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace FractionDivisionTests
{
    TEST_CLASS(FractionDivisionTests)
    {
    public:

        // Тест 1: Деление обычных дробей
        TEST_METHOD(DivideNormalFractions)
        {
            Fraction first(3, 5);
            Fraction second(6, 7);

            Fraction result = FractionDivision(first, second);

            Assert::AreEqual(result.getNumerator(), 7);
            Assert::AreEqual(result.getDenominator(), 10);
        }

        // Тест 2: Деление с сокращением результата
        TEST_METHOD(DivideAndSimplifyResult)
        {
            Fraction first(4, 9);
            Fraction second(2, 3);

            Fraction result = FractionDivision(first, second);

            Assert::AreEqual(result.getNumerator(), 2);
            Assert::AreEqual(result.getDenominator(), 3);
        }

        // Тест 3: Деление дроби на единицу
        TEST_METHOD(DivideByOne)
        {
            Fraction first(5, 8);
            Fraction second(1, 1);

            Fraction result = FractionDivision(first, second);

            Assert::AreEqual(result.getNumerator(), 5);
            Assert::AreEqual(result.getDenominator(), 8);
        }

        // Тест 4: Деление на дробь (обратная дробь)
        TEST_METHOD(DivideByFractionToGetReciprocal)
        {
            Fraction first(1, 1);
            Fraction second(3, 4);

            Fraction result = FractionDivision(first, second);

            Assert::AreEqual(result.getNumerator(), 4);
            Assert::AreEqual(result.getDenominator(), 3);
        }

        // Тест 5: Деление нуля на дробь
        TEST_METHOD(DivideZeroByFraction)
        {
            Fraction first(0, 1);
            Fraction second(2, 3);

            Fraction result = FractionDivision(first, second);

            Assert::AreEqual(result.getNumerator(), 0);
            Assert::AreEqual(result.getDenominator(), 1);
        }

        // Тест 6: Деление отрицательных дробей
        TEST_METHOD(DivideNegativeFractions)
        {
            Fraction first(-1, 2);
            Fraction second(3, -4);

            Fraction result = FractionDivision(first, second);

            Assert::AreEqual(result.getNumerator(), 2);
            Assert::AreEqual(result.getDenominator(), 3);
        }

        // Тест 7: Деление дробей с большими числами
        TEST_METHOD(DivideLargeNumbers)
        {
            Fraction first(1000, 999);
            Fraction second(100, 999);

            Fraction result = FractionDivision(first, second);

            Assert::AreEqual(result.getNumerator(), 10);
            Assert::AreEqual(result.getDenominator(), 1);
        }

        // Тест 8: Проверка на несократимость результата
        TEST_METHOD(ResultIsIrreducible)
        {
            Fraction first(7, 11);  // Простое число в числителе
            Fraction second(13, 17); // Простое число в знаменателе

            Fraction result = FractionDivision(first, second);

            // Проверяем что дробь 7*17 / 11*13 не сокращается
            Assert::AreEqual(result.getNumerator(), 7 * 17);
            Assert::AreEqual(result.getDenominator(), 11 * 13);
        }
    };
}