//Дан массив A (N). Осуществить циклический 
//сдвиг элементов массива, стоящих после 
//последнего минимального  на 2 вправо.

#include <iostream> 
#include <iomanip>
using namespace std;

int main()
{
    setlocale(LC_ALL, "ru");
    int N, min, minIndex, temp;
    cout << "Введите количество элементов в массиве:\t";
    cin >> N;
    while (N <= 1) {
        cout << "Некоректный ввод количества элементов в массиве.\nПовторите попытку ввода" << endl << endl;
        cout << "Введите количество элементов в массиве:\t";
        cin >> N;
    }
    cout << endl;

    int arr[1000];

    // Ввод и присвоение
    for (size_t i = 0; i < N; i++)
    {
        cout << "Элемент " << i+1 << ":\t";
        cin >> arr[i];
        if (i == 0) {
            min = arr[0];
        }
        if (min >= arr[i]) {
            min = arr[i];
            minIndex = i;
        }
    }
    cout << endl;

    // Вывод исходного массива
    cout << endl << "Исходный массив: " << endl;
    for (size_t i = 0; i < N; i++)
    {
        cout << arr[i] << " ";
    }
    cout << endl;

    //Проверка на смысл переноса
    if ((N - minIndex) <= 2) {
        cout << "Сдвиг не имеет смысла так как после последнего минимального элемента меньше двух элементов" << endl;
    }
    else {
        // Обработка массива
        for (size_t i = 0; i < 2; i++)
        {
            temp = arr[N - 1];
            for (size_t i = N; i > minIndex + 1; i--)
            {
                arr[i] = arr[i - 1];
            }
            arr[minIndex + 1] = temp;
        }

        // Вывод обработанного массива
        cout << endl << "Полученный массив: " << endl;
        for (size_t i = 0; i < N; i++)
        {
            cout << arr[i] << " ";
        }
        cout << endl;
    }

    return 0;
}
