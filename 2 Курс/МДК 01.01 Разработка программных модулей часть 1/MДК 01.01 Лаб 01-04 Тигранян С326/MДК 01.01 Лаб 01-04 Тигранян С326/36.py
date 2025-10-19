Bool = True
while (Bool):
    try:
        print("Введите пятизначное число:")
        num = int(input()) #Ввод числа
        if len(str(num)) > 5 or len(str(num)) < 5: #Проверка на пятизнадчсность
            print("Число не является пятизначным: \n")
            Bool = True
        else:
            listNum = list(str(num))           #Перевод числа в строку и затем в массив
            listNum[1] = 0                     #Изменение 2 разряда на 0
            listNum[3] = 0                     #Изменение 4 разряда на 0
            string = ''.join(str(el) for el in listNum) #Перевод из массива в строку
            num = int(string)                  #Перевод из строки в число
            print(f"Полученное число: {num}")  #Вывод получнного числа
            Bool = False
    except TypeError:
        print("Ошибка типа данных")
    except ValueError:
        print("Ошибка значения")
    except:
        print("Ошибка")