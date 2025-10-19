﻿#include <string>
#include <iostream>

using namespace std;

class Stroka_strochenka {
public:

	Stroka_strochenka(int a) {
		cout << "Введите строчку ";
		getline(cin, this->strochka);
	};

	void CoutStr() {
		cout << this->strochka;
		cout << endl;
	};

	void Cout3Str() {
		cout << "Exercise 1" << endl;
		cout << this->strochka << "," << this->strochka << "," << this->strochka << ",";
		cout << endl;
	}

	int getSymbol_count() {
		return this->strochka.length();
	}

	string getStr() {
		return this->strochka;
	}

	void prikol_Num2() {

		cout << "Exercise 2" << endl;

		if (this->strochka.size() > 5) {
			string tempStr{ this->strochka, 0, 3 };
			string tempStr2 = this->strochka.substr(this->strochka.length() - 3);

			cout << tempStr << " " << tempStr2;
		}
		else {
			for (size_t i = 0; i < this->strochka.length(); i++)
			{
				cout << this->strochka[0];
			}
		}
	}

	void deleteSpaces() {

		cout << "deleteSpaces function used" << endl;

		string newStr = "";
		bool isSpace = false;

		for (int i = 0; i < strochka.length(); i++) {
			if (strochka[0] == ' ') {
				strochka.erase(0, 1);
			}
		}

		for (int i = strochka.length(); i > 0; i--) {
			if (strochka[strochka.length()] == ' ') {
				strochka.pop_back();
			}
		}

		for (int i = 0; i < strochka.length(); i++) {

			if (strochka[i] == ' ') {
				if (isSpace)  continue;
				isSpace = true;
			}

			else {
				isSpace = false;
			}

			newStr += strochka[i];
		}
		this->strochka = newStr;
	}

	void reversWords() {

		cout << "reversWords function used" << endl;

		deleteSpaces();

		string newStr = "";
		string MAX = "";
		string MIN = "";
		string tempStrochkaMAX = this->strochka;
		string tempStrochkaMIN = this->strochka;
		string tempStrochka = this->strochka;
		string Main = "";
		int spaceCount = 0;

		for (int i = strochka.length(); i > 0; i--) {
			if (strochka[i] == ' ') {
				spaceCount++;
			}
		}

		for (size_t i = 0; i < (spaceCount + 1); i++) // finds max
		{
			int tempLength = tempStrochkaMAX.length();
			for (int i = 0; i < tempLength; i++) {
				if (tempStrochkaMAX[0] == ' ') {
					tempStrochkaMAX.erase(0, 1);
				}
			} // erase spaces beffore

			for (int i = 0; i < tempStrochkaMAX.length(); i++)
			{
				if (tempStrochkaMAX[i] != ' ') {
					newStr += tempStrochkaMAX[i];
				}
				else {
					break;
				}
			}

			if (newStr.length() > MAX.length())
			{
				MAX = newStr;
			}
			else if (newStr.length() == MAX.length()) {
				MAX = newStr;
			}

			tempStrochkaMAX.erase(0, (newStr.length())); // erase first word

			newStr = ""; // reloads newStr
		}

		MIN = MAX;

		for (size_t i = 0; i < (spaceCount + 1); i++) // finds min
		{
			int tempLength = tempStrochkaMIN.length();

			for (int i = 0; i < tempLength; i++) {
				if (tempStrochkaMIN[0] == ' ') {
					tempStrochkaMIN.erase(0, 1);
				}
			} // erase spaces beffore

			for (int i = 0; i < tempStrochkaMIN.length(); i++)
			{
				if (tempStrochkaMIN[i] != ' ') {
					newStr += tempStrochkaMIN[i];
				}
				else {
					break;
				}
			}

			if (MIN.length() > newStr.length())
			{
				MIN = newStr;
			}
			else if (newStr.length() == MIN.length()) {
				MIN = newStr;
			}

			tempStrochkaMIN.erase(0, (newStr.length())); // erase first word

			newStr = ""; // reloads newStr
		}

		cout << "Max is " << MAX << endl;
		cout << "Min is " << MIN << endl;

		for (size_t i = 0; i < (spaceCount + 1); i++) // finds min
		{
			int tempLength = tempStrochka.length();

			for (int i = 0; i < tempLength; i++) {
				if (tempStrochka[0] == ' ') {
					tempStrochka.erase(0, 1);
				}
			} // erase spaces beffore

			for (int i = 0; i < tempStrochka.length(); i++) {
				if (tempStrochka[i] != ' ') {
					newStr += tempStrochka[i];
				}
				else {
					break;
				}
			}
			if (MAX == newStr) {
				Main += (MIN + " ");
			}
			else if (MIN == newStr) {
				Main += (MAX + " ");
			}
			else {
				Main += (newStr + " ");
			}

			tempStrochka.erase(0, (newStr.length())); // erase first word

			newStr = ""; // reloads newStr
		}

		cout << "Main is " << Main << endl;
		this->strochka = Main;
	}

private:
	string strochka;
};

int main()
{
	Stroka_strochenka str1(3);
	str1.reversWords();

	return 0;
}
