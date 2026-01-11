# Последняя задача из раздела «Списки» ЛР 5-7.

import random
import cProfile

def main():
    print("""Программа с помощью генератора создаёт двумерный список из N строк и M столбцов. 
    Все элементы имеют целый тип и находятся в диапазоне от 10 до 40. 
    Также программа определяет, какие строки матрицы имеют хотя бы один элемент, который делится на 5.\n""")

    n = 100000
    m = 5

    arr = [[random.randint(10, 40) for j in range(n)] for i in range(m)]
    print("Сгенерированный список:",arr)

    n_number = []
    for i in range(n):
        for j in range(m):
            if (arr[i][j] % 5) == 0:
                n_number.append(i + 1)
                break
    print(f"В строке {n_number} есть элемент делящийся на пять")

print()
cProfile.run("main()")
