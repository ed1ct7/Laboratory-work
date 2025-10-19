# Пример с использованием регулярного выражения.

import re
import cProfile  # Подключение модуля для профилирования

# Создание строки
some_text = "some_text (888) 501-7526 some_text"
def main(txt):
    # Добавление нагрузки
    s = txt * 100000000
    pat = re.compile(r'(\(\d{3}\)\s*\d{3}-\d{4})')
    print(pat.search(s).group())
    pass

main(some_text)
cProfile.run("main(some_text)")