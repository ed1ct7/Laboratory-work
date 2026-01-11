//Создать программно текстовый файл. Вывести содержимое 
//файла на экран. Подсчитать в текстовом файле количество 
//строк, которые начинаются и заканчиваются одинаковым символом
//(без учета регистра).

#include <iostream>
#include <string>
#include <fstream>
#include <windows.h>
#include <cstdio> 
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
        cout << "\t\tМеню \n1 - Перейти в меню записи\n2 - Считать данные"
            "\n3 - Выполнение задания\n4 - Удалить файл\n0 - Конец работы программы" << endl;
        cout << "Выбор:\t"; cin >> choice;
        while (choice > 4 || choice < 0) {
            cout << endl << "Введенной недопустимое значение, повторите ввод" << endl;
            cout << "Выбор:\t"; cin >> choice;
        }
        switch (choice) {
        case 0:
            exit(EXIT_SUCCESS);
        case 1:
            cout << endl << "Вводите строки для ввода, для остановки ввода введите endl" << endl;
            this->insert(); break;
        case 2:
            this->read(); break;
        case 3:
            this->linesCounter(); break;
        case 4:
            this->deleteFile(); break;
        }     
    }
    void insert() {
        this->file.open(this->name, fstream::app | fstream::out);
        cin.ignore(numeric_limits<streamsize>::max(), '\n');
        for (;;) {
            getline(cin, this->line);
            if (this->line == "endl") { break; }
            this->file << this->line << endl;
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
        if (counter == 1) {
            cout << "Файл пуст" << endl;
        }
        cout << endl;
        this->file.close();  this->interFace();
    }
    void linesCounter() {
        this->file.open(this->name, fstream::in);
        while (getline(this->file, this->line)) {
            if (this->line == "") { continue; }
            if (tolower(unsigned char(this->line[0])) == tolower(unsigned char(this->line[this->line.size() - 1]))
                && this->line.size() > 1) {
                this->lines_counter++;
            }
        }
        cout << "\nКоличество строк, у которых совпадают первый и последний символ: " 
            << this->lines_counter << "\n\n";
        this->lines_counter = 0;
        this->file.close();  this->interFace();
    }
    int get_lines_counter() {
        return this->lines_counter;
    }
    void deleteFile() {
        int status = remove(this->name.c_str());
        cout << endl;
        if (status != 0) {
            cout << "Ошибка удаления файла\n";
        }
        else {
            cout << "Файл удален\n";
        }
    }
private:
    int lines_counter = 0;
    string name, line = "";
    fstream file;
};
int main() {
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);
    setlocale(LC_ALL, "ru");
    Notepad a("aboba.txt");
}