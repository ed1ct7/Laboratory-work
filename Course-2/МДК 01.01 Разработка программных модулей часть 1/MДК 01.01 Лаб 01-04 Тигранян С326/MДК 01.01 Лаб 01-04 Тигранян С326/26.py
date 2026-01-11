Bool = True
while (Bool):
    try:                                                                #Ввод данных
        yearlyPercent = float(input("Введите ежегодный процент: "))
        initialСontr = float(input("Введите изначальный вклад: "))
        term = int(input("Введите количество лет: "))
        if ((yearlyPercent <= 0) or (initialСontr <= 0) or (term <= 0)):    #Проверка на минусовое значение
            print("\nМинусовое значение ввода данных\n")
            Bool = True
        else:
            Bool = False
    except TypeError:
        print("Ошибка типа данных")
    except ValueError:
        print("Ошибка значения")
    except:
        print("Ошибка")


else:
    print(round(initialСontr * (1 + yearlyPercent/100)**term, 2)) #Формула