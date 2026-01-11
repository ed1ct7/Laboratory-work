#include <iostream>
#include <string>
#include <fstream>
#include <windows.h>
#undef max
using namespace std;

class Notepad {
public:
    Notepad(string name = "filename.txt") {
        this->name = name;
        this->file.open(this->name, fstream::app | fstream::out);
        if (!this->file.is_open()) {
            this->file.open(this->name, fstream::trunc | fstream::out);
            if (!this->file.is_open()) {
                cerr << "Не удалось создать файл" << endl;
                exit(EXIT_FAILURE);
            }
        }
        this->file.close(); this->interFace();
    }
    void interFace() {
        unsigned short choice;
        cout << "\t\tМеню \n1 - Перейти в меню записи\n2 - Считать данные\n0 - Конец работы программы" << endl;
        cin >> choice;
        while (choice > 2 || choice < 0) {
            cout << endl << "Введенной недопустимое значение, повторите ввод" << endl;
            cin >> choice;
        }
        switch (choice) {
        case 0:
            exit(EXIT_SUCCESS);
        case 1:
            cout << endl << "Введите строку для ввода, для остановки ввода введите endl" << endl;
            this->insert(); break;
        case 2:
            this->read(); break;
        }
    }
    void insert() {
        this->file.open(this->name, fstream::app | fstream::out);
        cin.ignore(numeric_limits<streamsize>::max(), '\n');
        for (;;) {
            getline(cin, this->line);
            if (this->line == "endl") { break; }
            this->file << this->line << endl;
            cout << "Строка записана.\n\nПродолжить ввод данных в файл?\nСтрока - продолжить\nendl - завершить\n\n";
        }
        cout << endl;
        this->file.close(); this->interFace();
    }
    void read() {
        this->file.open(this->name, fstream::in);
        cout << endl;
        int counter = 1;
        while (getline(this->file, this->line)) {
            cout << "Строка №" << counter << "\t" << this->line << endl;
            counter++;
        }
        cout << endl;
        this->file.close();  this->interFace();
    }
private:
    string name, line = "";
    fstream file;
};
int main() {
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);
    setlocale(LC_ALL, "ru");
    Notepad a("aboba.txt");
}
