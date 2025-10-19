//Скопируйте текст без заголовка из раздела IV.«Поиск через файл» в файл TASK_1_NAME.txt
//(вместо NAME впишите свою фамилию на латинице).Напишите скрипт(код), выполняющий поиск
//слов «print» в TASK_1_NAME.txt и выводящий
// общее число таких слов, 
// номер(а) строк, где оно встречается более одного раза, 
// а также строки, где оно встречается в сочетании с круглыми скобками.

#include <iostream>
#include <fstream>
#include <string> 
#include <stdio.h>
#include <locale.h>  
#include <Windows.h>
#include <locale>
#include <conio.h>
#include <vector>

using namespace std;

class Line {
public:
    int amount = 0;
    Line(string line) {
        this->line = line;
    }
    bool checkWordForSeperatingSymbols(char charToCheck) {
        char separatingSymbols[10] = { ' ', '.', ',', '\t' };
        for (int i = 0; i < line.size(); ++i) {
            if (separatingSymbols[i] == charToCheck) {
                return true;
            }
        }
        return false;
    }
    int checkWordExist(string word) {
        int Conditions = 0;
        bool flagIsExist = false;
        string tempWord;
        size_t printPos = line.find(word);
        for (size_t i = 0; i < line.size(); i++)
        {
            bool hasBracket = false;
            bool printExist = false;
            size_t printPos = line.find(word);
            if (printPos != string::npos) {
                if (printPos != 0) {
                    if (checkWordForSeperatingSymbols(line[printPos - 1])) {
                        printExist = true;
                    }
                }
                else {
                    printExist = true;
                }
                if (printPos + 5 != line.length()) {
                    if (line[printPos + word.size()] == '(') {
                        printExist = true;
                        Conditions += 100;
                    }
                    else if (line.find_first_not_of(" ", line.find_first_of(" ", printPos)) != string::npos) {
                        if (line[line.find_first_not_of(" ", line.find_first_of(" ", printPos))] == '(') {
                            printExist = true;
                            Conditions += 100;
                        }
                        else if (checkWordForSeperatingSymbols(line[printPos + word.size()])) {
                            printExist = true;
                        }
                        else {
                            printExist = false;
                        }
                    }
                    else if (checkWordForSeperatingSymbols(line[printPos + word.size()])) {
                        printExist = true;
                    }
                    else {
                        printExist = false;
                    }
                }
            }
            if (printExist) {
                this->amount += 1;
                if (flagIsExist) {
                    Conditions += 10;
                }
                else {
                    Conditions += 1;
                    flagIsExist = true;
                }
                size_t nextPos = printPos + word.size();
                if (nextPos < line.length()) {
                    line = line.substr(nextPos);
                }
                else {
                    break;
                }
            }
            else {
                break;
            }
        }
        return Conditions;
        }
private:
    string line;
};

void vectorCout(vector<int> vector) {
    for (auto i : vector)
    {
        cout << i << ", ";
    }
}

int main()
{
    size_t OverallAmount, LinesMore1, LinesWithBracket;
    setlocale(LC_ALL, "ru_RU.UTF-8");
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);
    string line;
    string path = "TASK_1_TIGRANYAN.txt";
    fstream in(path);
    vector<int> moreThanTwo;
    vector<int> hasBracket;
    int i = 0;
    int overall = 0;
    int cond = 0;

    if (in.is_open()) {
        while (getline(in,line)){
            Line lineLine(line);
            cond = lineLine.checkWordExist("print");
            cout << i << ": " << cond << ": " << lineLine.amount << ": " << line << endl;
            overall += lineLine.amount;

            if ((cond / 10) % 10 == 1) {
                cout << endl << lineLine.checkWordExist("print") << endl;
                moreThanTwo.push_back(i);
            }
            if ((cond / 100) % 10 == 1) {
                hasBracket.push_back(i);
            }

            i++;
        }
    }
    in.close(); 

    setlocale(LC_ALL, "ru");
    cout << endl;
    cout << "Количество слов print: " << overall << endl;
    cout << "Строки в которых больше 2 слов print: "; vectorCout(moreThanTwo); cout << endl;
    cout << "Строки в которых print с круглыми скобками: "; vectorCout(hasBracket); cout << endl;
    return 0;
}

//Скопируйте текст без заголовка из раздела IV.«Поиск через файл» в файл TASK_1_NAME.txt
//(вместо NAME впишите свою фамилию на латинице).Напишите скрипт(код), выполняющий поиск
//слов «print» в TASK_1_NAME.txt и выводящий
// общее число таких слов, 
// номер(а) строк, где оно встречается более одного раза, 
// а также строки, где оно встречается в сочетании с круглыми скобками.