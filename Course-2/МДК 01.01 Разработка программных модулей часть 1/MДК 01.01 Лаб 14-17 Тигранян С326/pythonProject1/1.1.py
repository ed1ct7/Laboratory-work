# Вывод хена по алгоритму MD5

from hashlib import md5
import cProfile  # Подключение модуля для профилирования

def main():
    # Создание строки
    string_to_encode = "Password" * 10000000
    result_hash = md5(string_to_encode.encode())
    # Вывод хеша
    print(result_hash.hexdigest())

cProfile.run("main()")