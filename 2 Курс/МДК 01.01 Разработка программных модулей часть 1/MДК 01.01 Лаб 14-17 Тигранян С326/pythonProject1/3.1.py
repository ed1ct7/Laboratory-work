# Выполнить создание двумерного списка с большим объёмом данных
# (не менее 10000 элементов). Выполнить профилирование кода.

import cProfile # module to profile code

# rows
n = 700
# columns
m = 700

# function which returns randomly generated arr with length n and width m
def main() -> list:
    # generator of the list
    return [[i for j in range(n)] for i in range(m)]

# profilation of the code
cProfile.run("main()")