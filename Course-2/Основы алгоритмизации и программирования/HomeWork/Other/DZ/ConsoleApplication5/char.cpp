//Дана строка.Словом текста считается любая последовательность букв русского алфавита;
//между соседними словами - не менее одного пробела, за последним словом – точка.Найти
//и сохранить в строке те слова последовательности, которые отличны от последнего слова,
//предварительно преобразовав каждое из них по следующему правилу : удалить из слова
//последнюю букву.Все остальные слова удалить.

#include <iostream>
#include <string>
#include <windows.h>
#include <algorithm>
#include <cctype>

using namespace std;

string tolower(const string& str) {
    string result = str;
    transform(result.begin(), result.end(), result.begin(), [](unsigned char c) { return tolower(c); });
    return result;
}

int main()
{
    setlocale(LC_ALL, "ru");
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);

    string str, last_word, current_word;
    int choice;
    bool checker = true, one_symbol = false;

    cout << "Ведите строку: " << endl;
    getline(cin, str);

    while (str.length() == 0) {
        cout << "Ошибка ввода, повторите попытку" << endl;
        getline(cin, str);
    }

    if (str.find(".", 0) == str.npos) {
        str.push_back('.');
    }

    cout << "Удалить лишние пробелы (если есть)?\n\t0. Оставить\n\t1. Удалить\nВыбор:\t";
    cin >> choice;
    while (choice > 1 || choice < 0) {
        cout << "Ошибка ввода, повторите попытку" << endl;
        cin >> choice;
    }

    // удаление всего после точки
    str.erase(str.find_first_of('.') + 1, str.length());

    // удаление пробелов до и символов при таковом выборе пользователя
    if (choice)
    {
        str.erase(0, str.find_first_not_of(' '));
        if (str[str.length() - 2] == ' ') {
            str.erase(str.substr(0, str.length() - 2).find_last_not_of(' ') + 1,
                str.length() - 2 - str.substr(0, str.length() - 2).find_last_not_of(' '));
        }
        // один сивол при удалении пробелов
        if (str.length() == 2) {
            checker = false;
            str.erase(str.find_first_not_of(' '), 1);
            cout << "Строка была полностью удалена" << endl;
        }
    }
    else {
        // один символ в слове
        if (str.find_first_not_of(' ') == str.substr(0, str.length() - 2).find_last_not_of(' ')) {
            checker = false;
            str.erase(str.find_first_not_of(' '), 1);
            cout << "Строка была полностью удалена" << endl;
        }
    }
    if (checker) {
        // цикл удалении последнего символа и записи последнеего слова
        do
        {
            // если в последнем слове 1 символ, то слово удаляется и ищется предыдущее слово
            while (str[(str.substr(0, str.length() - 1).find_last_not_of(' ')) - 1] == ' ') {
                {
                    str.erase(str.substr(0, str.length() - 1).find_last_not_of(' '), 1);
                    if ((str.substr(0, str.length() - 1).find_last_not_of(' ')) - 1 == str.npos) {
                        str = "";
                        cout << "Строка была полностью удалена" << endl;
                        goto error;
                    }
                }
            }

            // удален последнего элемента
            str.erase(str.substr(0, str.length() - 1).find_last_not_of(' '), 1);

            // переменная хранящяя последнее слово
            last_word = str.substr(
                // начальный индекс
                str.substr(0, str.substr(0, str.length() - 1).
                    find_last_not_of(' ')).find_last_of(' ') + 1,
                // конечный индекс
                str.substr(0, str.length() - 1).
                find_last_not_of(' ') -
                // начальный индекс
                str.substr(0, str.substr(0, str.length() - 1).
                    find_last_not_of(' ')).find_last_of(' '));
        } while (last_word == " ");

        if (choice && str.find(" ", str.substr(0, str.length() - 1).find_last_not_of(' ')) != str.npos) {
            str.erase(str.substr(0, str.length() - 1).find_last_not_of(' ') + 1,
                str.length() - 2 - str.substr(0, str.length() - 1).find_last_not_of(' '));
        }

        for (size_t i = str.find_first_not_of(' '); i < str.length() - 1;) {

            // присваевание i индекс начала нового слова
            i = str.find_first_not_of(' ', i);

            if ((str.find(" ", i) == str.npos) ||
                (str.find_first_not_of(' ', str.find(" ", i)) == str.length() - 1)) {
                // если после слова стоит точка или первый символ не пробел после конца слова равен ".", 
                // то это последнее слово, значит проверять его не имеет смысла
                break;
            }
            else {

                // в переменную поочерёдно записываются слова
                current_word = str.substr(0, str.length() - 1).
                    // первый индекс начало слова (первый не пробел), второй его конец (первый пробел)
                    substr(i, str.find_first_of(' ', i) - i);

                if (current_word.length() == 1) {
                    if (choice) {
                        str.erase(i, str.find_first_not_of(' ', i + 1) - i);
                        current_word = "";
                        if (str.find(" ", i) == str.npos) {
                            break;
                        }
                        one_symbol = true;
                    }
                    else {
                        str.erase(i, 1);
                    }
                }
                else {
                    one_symbol = false;
                    str.erase(str.find(" ", i) - 1, 1);
                    current_word.pop_back();
                }

                // удаление слова при его совпадении с последним словом
                if (tolower(current_word) == tolower(last_word)) {
                    str.erase(i, str.find_first_of(' ', i) - i + 1);
                    if (choice && str[str.find_first_not_of(' ', i) - 1] == ' ') {
                        str.erase(i, str.find_first_not_of(' ', i) - i);
                    }
                    // присваевание i начало следующего слова
                    i = str.find_first_not_of(' ', i);
                }
                else {
                    // присваевает i пробел после конца слова
                    if (!one_symbol) {
                        i = str.find(" ", i);
                        if (choice) {
                            str.erase(i, str.find_first_not_of(' ', i) - i - 1);
                        }
                    }
                }
            }
        }
    }
error:
    cout << "Полученная строка: " << "|" << str << "|" << endl;

    return 0;
}