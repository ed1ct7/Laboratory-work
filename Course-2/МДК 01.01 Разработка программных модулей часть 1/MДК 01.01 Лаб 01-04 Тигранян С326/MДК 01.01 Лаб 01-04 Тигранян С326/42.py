Bool = True
while (Bool):
    try:
        course = float(input("Введите курс рубля к доллару "))
        candyPrice = 20
        if course <= 0:
            print("Курс не может быть минусовым значением")
            Bool = True
        else:
            Bool = False
    except TypeError:
        print("Ошибка типа данных")
    except ValueError:
        print("Ошибка значения")
    except:
        print("Ошибка")

for i in range(100):
    print(f"{i+1}$ {(i+1)*course}p {round(((i+1)*course)/candyPrice, 2)}кг") #Цикл переборам