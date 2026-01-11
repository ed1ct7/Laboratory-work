//Вариант № 23
//Библиотека - хранилище книг. По запросу заданных автора и названия книги вывести :
//1. если книга имеется, то - ее название и автора.
//2. если данной книги нет, то вывести список книг запрашиваемого автора.
//3. если нет книг данного автора, то вывести : "Книги нет".

#include <iostream>
#include <string>
#include <vector>
#include <cctype>
#include <windows.h>

using namespace std;

struct Libriary {
public:
    void interface_f() {
        int menu = 0;
        bool isWorking = true;

        while (isWorking)
        {
            cout << endl;
            cout << "Библиотека" << endl;
            cout << "1: Добавить книгу" << endl;
            cout << "2: Удалить книгу" << endl;
            cout << "3: Найти книгу по автору и названию" << endl;
            cout << "4: Вывести все книги" << endl;
            cout << "5: Закрыть программу" << endl;
            cout << "Выбор: ";

            cin >> menu;
            cout << endl;
            switch (menu) {
            case 1:
                addNewBook();
                cout << "книга добавленна" << endl << endl;
                break;
            case 2:
                deleteBook();
                break;
            case 3:
                findByNameAndAuthor();
                break;
            case 4:
                showAllBooks();
                break;
            case 5:
                isWorking = false;
                break;
            }
        }
    }

    void showAllBooks() {
        if (this->bookCount)
        {
            for (size_t i = 0; i < this->bookCount; i++)
            {
                cout << i + 1 << ". \t" << this->arr[i].getName() << endl;
            }
        }
        else {
            cout << "Книг нет" << endl << endl;
        }
    }

    void addNewBook() {
        book newC;
        this->arr.push_back(newC);
        this->bookCount++;
    }

    void deleteBook() {
        cout << "выберити книгу для удаления" << endl;
        showAllBooks();
        int numToDelete;
        cin >> numToDelete;
        vector<book>::iterator it;
        it = this->arr.begin() + numToDelete;
        this->arr.erase(it, it);
    }

    void findByNameAndAuthor() {
        string book, author;
        cout << "Введите автора: \t"; cin >> author;
        cout << "Введите название книги: "; cin >> book;
        cout << endl;
        int index = 0; bool isValid = false;
        for (int i = 0; i < this->bookCount; i++)
        {
            if (this->arr[i].getName() == book && this->arr[i].getAuthor() == author) {
                this->arr[i].coutEverething();
                cout << endl << endl;
                isValid = true;
                break;
            }
        }
        if (!isValid) {
            cout << "Поиск книг ..." << endl;
            allBooksByAuthor(author);
        }
    }

    void allBooksByAuthor(string author) {
        bool booksExist = false;
        for (int i = 0; i < this->bookCount; i++)
        {
            if (this->arr[i].getAuthor() == author) {
                cout << "\t" << this->arr[i].getName() << endl;
                booksExist = true;
            }
        }
        if (!booksExist) {
            cout << "Книги автора " << author << " небыли найденны" << endl << endl;
        }
    }

private:
    struct book {
    public:
        string getName() {return this->name;}
        string getAuthor() {return this->author;}
        book() {
            cout << "Автор: ";
            cin >> this ->author;
            cout << "Название книги: ";
            cin >> this->name;
            cout << "Дата публикации: ";
            cin >> this->dataPublished;
            cout << "ISBN: ";
            cin >> this->ISBN;
        }
        void coutEverething() {
            cout << "Автор: " << this->author << endl;
            cout << "Название: " << this->name << endl;
            cout << "Дата публикации: " << this->dataPublished << endl;
            cout << "ISBN: " << this->ISBN << endl;
        }
    private:
        string name;
        string author;
        int dataPublished;
        string ISBN;
    };
    vector<book> arr;
    int bookCount = 0;
};

int main()
{
    setlocale(LC_ALL, "ru");
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);
    Libriary a;
    a.interface_f();

    return 0;
}