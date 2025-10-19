monthDays = [0,31,28,31,30,31,30,31,31,30,31,30,31] #Массив для хранения количества дней в месяцах
Bool = True
while (Bool):
    try:
        print("Введите дату (число, месяц, год):")
        arr = []
        for i in range(3):
            arr.append(int(input()))                #Ввод данных
        if ((arr[1] > 12) or (arr[0]>monthDays[arr[1]]) or arr[2] < 0):   #Проверка на допустимость введённых значений
            print("Введённая дата недействительна, повторите попытку ввода\n")
            Bool = True
        else:
            print("Дата действительна")
            Bool = False
    except TypeError:
        print("Ошибка типа данных")
    except ValueError:
        print("Ошибка значения")
    except:
        print("Ошибка")

