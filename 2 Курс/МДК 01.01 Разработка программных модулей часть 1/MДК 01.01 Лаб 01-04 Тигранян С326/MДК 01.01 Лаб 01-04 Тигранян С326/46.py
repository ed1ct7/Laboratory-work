try:
    bool = True
    evenNumbersCount = 0
    while (bool):
        temp = int(input("Введите число: "))
        if temp == 0:
            bool = False
        elif (temp % 2) == 0:
            evenNumbersCount += 1
    print(f"Количество введённых чётных чисел: {evenNumbersCount}")
except TypeError:
    print("Ошибка типа данных")
except ValueError:
    print("Ошибка значения")
except:
    print("Ошибка")