# Решение задачи с генератором двумерного списка оформить в виде функции,
# возвращающей список, со входными параметрами в виде числа строк и столбцов. Вызвать функцию несколько раз в
# основной программе (т.е. сгенерировать несколько списков с одинаковым
# количеством элементов, но разным значение строк и столбцов). Выполнить профилирование кода.

import cProfile # module to profile code

# function which returns randomly generated arr with length n and width m
def generator(rows: int, columns: int) -> list:
    # generator of the list
    return [[i for j in range(rows)] for i in range(columns)]

# main function
def main():
    # rows
    i = 490000
    # columns
    j = 1
    # temp bool variable
    t_bool = True
    while t_bool:
        # call of generation function to see average time of the generator function
        generator(i, j)
        i //= 2
        j *= 2
        # check parameters to avoid float value
        if i <= 1 or j >= 490000:
            t_bool = False

# profile code
cProfile.run("main()")
