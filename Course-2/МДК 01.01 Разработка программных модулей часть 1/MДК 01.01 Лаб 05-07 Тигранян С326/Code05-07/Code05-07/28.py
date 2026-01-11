import random
print("""Программа с помощью генератора создаёт двумерный список из N строк и M столбцов. 
Все элементы имеют целый тип и находятся в диапазоне от 10 до 40. 
Также программа определяет, какие строки матрицы имеют хотя бы один элемент, который делится на 5.\n""")

N = random.randint(3, 5)
M = random.randint(3, 5)

arr = [[random.randint(10, 40) for j in range(M)] for i in range(N)]
print("Сгенерированный список:",arr)

nNumber = []
for i in range(N):
    for j in range(M):
        if (arr[i][j] % 5) == 0:
            nNumber.append(i + 1)
            break
print(f"В строке {nNumber} есть элемент делящийся на пять")