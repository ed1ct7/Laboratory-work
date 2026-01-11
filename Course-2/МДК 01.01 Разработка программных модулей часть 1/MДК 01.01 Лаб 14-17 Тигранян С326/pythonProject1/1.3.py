# Первая циклическая задача из ЛР 1-4.

import cProfile

def main():
    t_bool = True
    while t_bool:
        try:
            # Ввод курса рубля
            course = float(input("Введите курс рубля к доллару "))
            candy_price = 20
            # Проверка условия на минусовой курс
            if course <= 0:
                print("Курс не может быть минусовым значением")
                t_bool = True
            else:
                t_bool = False
        except TypeError:
            print("Ошибка типа данных")
        except ValueError:
            print("Ошибка значения")
        except:
            print("Ошибка")

    for i in range(1000000):
        print(f"{i+1}$ {(i+1)*course}p {round(((i+1)*course)/candy_price, 2)}кг") #Цикл переборам

print()
cProfile.run("main()")
