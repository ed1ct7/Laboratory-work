# В задаче на двумерный список вместо генератора использовать цикл с поэлементным добавлением
# элементов в список. Выполнить профилирование кода. Вычислить по формуле (1) относительную
# разницу производительности в процентах
# (сравнить работу генератора и поэлементного добавления в цикле, при необходимости увеличить размерность списка).

import cProfile # module to profile code

# main function
def main():
    # function which generate an arr using cycle
    def cicle():
        n = 100000
        m = 4
        arr = []
        for i in range(n):
            arr.append([0] * m)
            for j in range(m):
                arr[i][j] = i + j
        return arr
    # function which generate an arr using generator
    def generator():
        # rows
        n = 100000
        # columns
        m = 4
        # returns generated arr
        return [[i + j for j in range(m)] for i in range(n)]
    cicle()
    generator()

# profile function
cProfile.run("main()")
