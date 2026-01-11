try:
    print("Введите 4 числа:")
    arr = []                                        #Создание массива, в который будет производится ввод чисел
    for i in range(4):                              #Цикл для ввода значений
        arr.append(int(input(f"Введите число {i+1}: ")))
except TypeError:
    print("Ошибка типа данных")
except ValueError:
    print("Ошибка значения")
except:
    print("Ошибка")

evenNumsArr = [i for i in arr if (i % 2) == 0]      #Запись в новый массив всех чётных чисел
maxNumber = max(evenNumsArr)                        #Нахождение максимального элемента массива с помощью функции max

if len(evenNumsArr) == 0:                           #Проверка на наличие чётных чисел в массиве
    print("Чётные числа не найдены")
else:
    print(f"Максимальное чётное число: {maxNumber}") #Вывод максимального чётного числа