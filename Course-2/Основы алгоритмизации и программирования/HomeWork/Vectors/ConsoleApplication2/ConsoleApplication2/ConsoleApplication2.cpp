//Вариант 2
//Дана последовательность целых чисел.Определить сумму нечетных
//элементов и если она положительная, то удалить максимальные элементы.В
//противном случае заменить четные элементы на 0. (элементы, у которых
//    значение четное число) Использовать функции с предикатом.Все данные
//    вводить с клавиатуры.

#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;

int main()
{
    setlocale(LC_ALL, "ru");
    vector<int> vec;
    cout << "Ввод элементов в вектор, для остановки введите 0" << endl;
    int temp, i = 1;

    for (;;)
    {
        cout << "Элемент №" << i << "\t";
        cin >> temp;
        if (temp == 0) {
            break;
        }
        vec.push_back(temp);
        i++;
    }

    if (vec.empty()) {
        cout << "Был введён пустой вектор";
        return 0;
    }

    int uneven = 0;
    for (auto iter = vec.begin(); iter < vec.end(); iter++)
    {
        if (find_if(iter, vec.end(), [] (int x) {return (x % 2 != 0 && x != 0); }) == vec.end()) {break;}
        uneven += *find_if(iter, vec.end(), [] (int x) {return (x % 2 != 0 && x != 0); });
        iter = find_if(iter, vec.end(), [] (int x) {return (x % 2 != 0 && x != 0); });
    }
        
    if (uneven > 0) {
        int max = *max_element(vec.begin(), vec.end());
        auto iter = remove(vec.begin(), vec.end(), max);
        vec.erase(iter, vec.end());
    }else {
        replace_if(vec.begin(), vec.end(), [] (int x) {return (x % 2 == 0 && x != 0); }, 0);
    }

    cout << "Полученный вектор: \t";

    for (auto iter = vec.begin(); iter < vec.end(); iter++)
    {
        cout << *iter << "; ";
    }

    return 0;
}
