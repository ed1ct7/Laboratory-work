#include "pch.h"
#include "CppUnitTest.h"

#include "D:\Laboratory\МДК 01.02 Тестирование\Лабораторная работа 4\Labotare 4\Labotare 4\Labotare 4.cpp""

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace test
{
	TEST_CLASS(test)
	{
	public:
		TEST_METHOD(TestNumeratorWhichDividedInto2)
		{
			int A = 3; int B = 5;
			Fraction First(A, B);

			int C = 6; int D = 7;
			Fraction Second(C, D);

			Assert::AreEqual(FractionDivision(First, Second).getNumerator(), 7);
		}
		TEST_METHOD(TestDenumeratorWhichDividedInto2)
		{
			int A = 3; int B = 5;
			Fraction First(A, B);

			int C = 6; int D = 7;
			Fraction Second(C, D);

			Assert::AreEqual(FractionDivision(First, Second).getDenumerator(), 10);
		}
		///////////////////////////////////////////////////////////////////////////
		TEST_METHOD(TestNumeratorWhichDividedInto3)
		{
			int A = 3; int B = 5;
			Fraction First(A, B);

			int C = 6; int D = 7;
			Fraction Second(C, D);

			Assert::AreEqual(FractionDivision(First, Second).getNumerator(), 7);
		}
		TEST_METHOD(TestDenumeratorWhichDividedInto3)
		{
			int A = 3; int B = 5;
			Fraction First(A, B);

			int C = 6; int D = 7;
			Fraction Second(C, D);

			Assert::AreEqual(FractionDivision(First, Second).getDenumerator(), 10);
		}
		///////////////////////////////////////////////////////////////////////////
		TEST_METHOD(TestNumeratorWhichDividedInto5)
		{
			int A = 3; int B = 5;
			Fraction First(A, B);

			int C = 6; int D = 7;
			Fraction Second(C, D);

			Assert::AreEqual(FractionDivision(First, Second).getNumerator(), 7);
		}
		TEST_METHOD(TestDenumeratorWhichDividedInto5)
		{
			int A = 3; int B = 5;
			Fraction First(A, B);

			int C = 6; int D = 7;
			Fraction Second(C, D);

			Assert::AreEqual(FractionDivision(First, Second).getDenumerator(), 10);
		}
		///////////////////////////////////////////////////////////////////////////
		TEST_METHOD(TestNumeratorWhichDividedInto7)
		{
			int A = 3; int B = 5;
			Fraction First(A, B);

			int C = 6; int D = 7;
			Fraction Second(C, D);

			Assert::AreEqual(FractionDivision(First, Second).getNumerator(), 7);
		}
		TEST_METHOD(TestDenumeratorWhichDividedInto7)
		{
			int A = 3; int B = 5;
			Fraction First(A, B);

			int C = 6; int D = 7;
			Fraction Second(C, D);

			Assert::AreEqual(FractionDivision(First, Second).getDenumerator(), 10);
		}
		///////////////////////////////////////////////////////////////////////////
		TEST_METHOD(TestNumeratorWhichDividedIntoItself)
		{
			int A = 3; int B = 5;
			Fraction First(A, B);

			int C = 6; int D = 7;
			Fraction Second(C, D);

			Assert::AreEqual(FractionDivision(First, Second).getNumerator(), 7);
		}
		TEST_METHOD(TestNumeratorWhichDividedIntoItself)
		{
			int A = 3; int B = 5;
			Fraction First(A, B);

			int C = 6; int D = 7;
			Fraction Second(C, D);

			Assert::AreEqual(FractionDivision(First, Second).getDenumerator(), 10);
		}
	};
}
