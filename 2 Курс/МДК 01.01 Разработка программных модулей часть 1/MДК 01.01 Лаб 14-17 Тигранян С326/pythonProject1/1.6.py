# Последняя задача из раздела «Словари» ЛР 5-7.

import typing
import statistics
import cProfile

def main():
    print("""Пользователь вводит перечень номеров студентов по журналу (не менее 8). 
    Каждому номеру ставится в соответствие список, содержащий фамилию студента и его оценки по 4 предметам. 
    Программа выводит фамилии студентов и соответствующий им средний балл.\n""")

    try:
        print('Вводите фамилии, для остановки ввода введите "stop"')
        def inputf(*args, func=lambda x: x) -> typing.Union[list, bool]:
            arr = []
            args = [*args]
            pupupu = True
            while pupupu:
                for i in range(len(args)):
                    if func(args[i]):
                        arr.append(args[i])
                        pupupu = False
                    else:
                        print("\nВведенно не допустимое значение, повторите попытку")
                        arr = []
                        pupupu = True
                        args[i] = int(input(f"Введите иное значение для {i + 1} предмета: "))
                        break
            return arr
        arr_student_full_info = {}
        arr_student_info = {}
        j = 0
        tbool = True
        while tbool:
            print(f"Данные {j + 1} ученика")
            second_name = input("Фамилия: ")
            if second_name == "stop" and j + 1 < 2:
                print("""Было введено менее 8 студентов повторите попытку ввода\n""")
                continue
            elif second_name == "stop":
                tbool = False
                break
            tempStr = []
            tempStr += (inputf(second_name) +
                        inputf(int(input("Оценка по матиматике: ")), int(input("Оценка по Русскому: ")),
                               int(input("Оценка по ОБЖ: ")), int(input("Оценка по Физкультуре: ")),
                               func=lambda x: 0 <= x <= 5))
            print()
            arr_student_full_info[j + 1] = tempStr
            arr_student_info [tempStr[0]] = tempStr[1] + round(statistics.mean(tempStr[2:]), 2)
            j += 1
        print("Студент \t| Средний баЛ")
        for i in arr_student_info.keys():
            print(f"{i} \t\t| {arr_student_info[i]}")
    except (TypeError, ValueError):
        print("Ошибка значения")
    except:
        print("Ошибка")

print()
cProfile.run("main()")
