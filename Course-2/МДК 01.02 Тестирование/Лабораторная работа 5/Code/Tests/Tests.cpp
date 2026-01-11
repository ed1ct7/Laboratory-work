#include "pch.h"
#include "CppUnitTest.h"
#include "../Code/FractionDivision.cpp"
#include "../Code/SimpleNumber.cpp"
#include "../Code/SimpleTask.cpp"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace FractionDivisionTests
{
    TEST_CLASS(SimpleNumberTest) {
    public:
        // Тесты для функции isPrime()
        TEST_METHOD(IsPrime_ForNegativeNumbers) {
            Assert::IsFalse(isPrime(-1));
            Assert::IsFalse(isPrime(-10));
            Assert::IsFalse(isPrime(-100));
        }

        TEST_METHOD(IsPrime_ForZeroAndOne) {
            Assert::IsFalse(isPrime(0));
            Assert::IsFalse(isPrime(1));
        }

        TEST_METHOD(IsPrime_ForSmallPrimes) {
            Assert::IsTrue(isPrime(2));
            Assert::IsTrue(isPrime(3));
            Assert::IsTrue(isPrime(5));
            Assert::IsTrue(isPrime(7));
            Assert::IsTrue(isPrime(11));
            Assert::IsTrue(isPrime(13));
        }

        TEST_METHOD(IsPrime_ForSmallNonPrimes) {
            Assert::IsFalse(isPrime(4));
            Assert::IsFalse(isPrime(6));
            Assert::IsFalse(isPrime(8));
            Assert::IsFalse(isPrime(9));
            Assert::IsFalse(isPrime(10));
            Assert::IsFalse(isPrime(12));
        }

        TEST_METHOD(IsPrime_ForLargePrimes) {
            Assert::IsTrue(isPrime(997));
            Assert::IsTrue(isPrime(7919));
            Assert::IsTrue(isPrime(104729));
        }

        TEST_METHOD(IsPrime_ForLargeNonPrimes) {
            Assert::IsFalse(isPrime(1000));
            Assert::IsFalse(isPrime(7920));
            Assert::IsFalse(isPrime(104730));
        }

        // Тесты для функции nextPrime()
        TEST_METHOD(NextPrime_ForNegativeNumbers) {
            Assert::AreEqual(nextPrime(-10), 2);
            Assert::AreEqual(nextPrime(-1), 2);
        }

        TEST_METHOD(NextPrime_ForZeroAndOne) {
            Assert::AreEqual(nextPrime(0), 2);
            Assert::AreEqual(nextPrime(1), 2);
        }

        TEST_METHOD(NextPrime_ForPrimes) {
            Assert::AreEqual(nextPrime(2), 3);
            Assert::AreEqual(nextPrime(3), 5);
            Assert::AreEqual(nextPrime(11), 13);
            Assert::AreEqual(nextPrime(997), 1009);
        }

        TEST_METHOD(NextPrime_ForNonPrimes) {
            Assert::AreEqual(nextPrime(4), 5);
            Assert::AreEqual(nextPrime(10), 11);
            Assert::AreEqual(nextPrime(14), 17);
            Assert::AreEqual(nextPrime(1000), 1009);
        }
    };
    TEST_CLASS(SimpleTask) {
    public:
        // Тесты для функции Min3()
        TEST_METHOD(Min3_AllEqual) {
            Assert::AreEqual(Min3(5, 5, 5), 5);
            Assert::AreEqual(Min3(0, 0, 0), 0);
            Assert::AreEqual(Min3(-3, -3, -3), -3);
        }

        TEST_METHOD(Min3_FirstIsMin) {
            Assert::AreEqual(Min3(1, 2, 3), 1);
            Assert::AreEqual(Min3(-5, 0, 5), -5);
            Assert::AreEqual(Min3(10, 20, 30), 10);
        }

        TEST_METHOD(Min3_SecondIsMin) {
            Assert::AreEqual(Min3(2, 1, 3), 1);
            Assert::AreEqual(Min3(0, -5, 5), -5);
            Assert::AreEqual(Min3(20, 10, 30), 10);
        }

        TEST_METHOD(Min3_ThirdIsMin) {
            Assert::AreEqual(Min3(3, 2, 1), 1);
            Assert::AreEqual(Min3(5, 0, -5), -5);
            Assert::AreEqual(Min3(30, 20, 10), 10);
        }

        TEST_METHOD(Min3_WithNegativeNumbers) {
            Assert::AreEqual(Min3(-1, -2, -3), -3);
            Assert::AreEqual(Min3(-10, -5, -1), -10);
            Assert::AreEqual(Min3(0, -1, 1), -1);
        }

        // Тесты для функции Max3()
        TEST_METHOD(Max3_AllEqual) {
            Assert::AreEqual(Max3(7, 7, 7), 7);
            Assert::AreEqual(Max3(0, 0, 0), 0);
            Assert::AreEqual(Max3(-4, -4, -4), -4);
        }

        TEST_METHOD(Max3_FirstIsMax) {
            Assert::AreEqual(Max3(3, 2, 1), 3);
            Assert::AreEqual(Max3(5, 0, -5), 5);
            Assert::AreEqual(Max3(30, 20, 10), 30);
        }

        TEST_METHOD(Max3_SecondIsMax) {
            Assert::AreEqual(Max3(1, 3, 2), 3);
            Assert::AreEqual(Max3(-5, 0, 5), 5);
            Assert::AreEqual(Max3(10, 30, 20), 30);
        }

        TEST_METHOD(Max3_ThirdIsMax) {
            Assert::AreEqual(Max3(1, 2, 3), 3);
            Assert::AreEqual(Max3(-5, 0, 5), 5);
            Assert::AreEqual(Max3(10, 20, 30), 30);
        }

        TEST_METHOD(Max3_WithNegativeNumbers) {
            Assert::AreEqual(Max3(-3, -2, -1), -1);
            Assert::AreEqual(Max3(-1, -5, -10), -1);
            Assert::AreEqual(Max3(-1, 0, 1), 1);
        }

        TEST_METHOD(Max3_EdgeCases) {
            Assert::AreEqual(Max3(INT_MAX, 0, -1), INT_MAX);
            Assert::AreEqual(Max3(INT_MIN, INT_MIN + 1, INT_MIN + 2), INT_MIN + 2);
        }

        TEST_METHOD(Min3_EdgeCases) {
            Assert::AreEqual(Min3(INT_MAX, INT_MAX - 1, INT_MAX - 2), INT_MAX - 2);
            Assert::AreEqual(Min3(INT_MIN, 0, 1), INT_MIN);
        }
    };
    TEST_CLASS(FractionDivisionTests)
    {
    public:
        TEST_METHOD(DivideNormalFractions)
        {
            Fraction first(3, 5);
            Fraction second(6, 7);

            Fraction result = FractionDivision(first, second);

            Assert::AreEqual(result.getNumerator(), 7);
            Assert::AreEqual(result.getDenominator(), 10);
        }

        TEST_METHOD(DivideAndSimplifyResult)
        {
            Fraction first(4, 9);
            Fraction second(2, 3);

            Fraction result = FractionDivision(first, second);

            Assert::AreEqual(result.getNumerator(), 2);
            Assert::AreEqual(result.getDenominator(), 3);
        }

        TEST_METHOD(DivideByOne)
        {
            Fraction first(5, 8);
            Fraction second(1, 1);

            Fraction result = FractionDivision(first, second);

            Assert::AreEqual(result.getNumerator(), 5);
            Assert::AreEqual(result.getDenominator(), 8);
        }

        TEST_METHOD(DivideByFractionToGetReciprocal)
        {
            Fraction first(1, 1);
            Fraction second(3, 4);

            Fraction result = FractionDivision(first, second);

            Assert::AreEqual(result.getNumerator(), 4);
            Assert::AreEqual(result.getDenominator(), 3);
        }

        TEST_METHOD(DivideFractionByZero) {
            Fraction f1(1, 2);
            Fraction zero(0, 1);

            try {
                FractionDivision(f1, zero);
                assert(false && "Ожидаемое исключение не было вызвано");
            }
            catch (const runtime_error& e) {
                assert(string(e.what()) == "Ошибка: деление на ноль");
            }
            catch (...) {
                assert(false && "Ожидаемое исключение не было вызвано");
            }

            try {
                Fraction invalid(1, 0);
                assert(false && "Ожидаемое исключение не было вызвано");
            }
            catch (const invalid_argument& e) {
                assert(string(e.what()) == "Знаменатель не может быть равен нулю");
            }
            catch (...) {
                assert(false && "Ожидаемое исключение не было вызвано");
            }
        }

        TEST_METHOD(DivideNegativeFractions)
        {
            Fraction first(-1, 2);
            Fraction second(3, -4);

            Fraction result = FractionDivision(first, second);

            Assert::AreEqual(result.getNumerator(), 2);
            Assert::AreEqual(result.getDenominator(), 3);
        }

        TEST_METHOD(DivideLargeNumbers)
        {
            Fraction first(1000, 999);
            Fraction second(100, 999);

            Fraction result = FractionDivision(first, second);

            Assert::AreEqual(result.getNumerator(), 10);
            Assert::AreEqual(result.getDenominator(), 1);
        }

        TEST_METHOD(ResultIsIrreducible)
        {
            Fraction first(7, 11);
            Fraction second(13, 17);

            Fraction result = FractionDivision(first, second);

            Assert::AreEqual(result.getNumerator(), 7 * 17);
            Assert::AreEqual(result.getDenominator(), 11 * 13);
        }
    };
}