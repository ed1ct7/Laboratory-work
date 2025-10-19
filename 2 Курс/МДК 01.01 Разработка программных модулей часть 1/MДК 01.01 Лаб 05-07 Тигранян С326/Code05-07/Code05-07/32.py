#TODO Добавить проверку на количество символов в номере карты и проверку на int
import hashlib

print("""Пользователь вводит словарь из 5 пар, где ключами являются номера банковских ячеек, а значениями – 
кодовые слова для доступа к ним. По запросу с кодовым словом программа выводит номер соответствующей 
ячейки и хеш кодового слова по алгоритму MD5.\n""")

try:
    cardDict = {}
    for i in range(5):

        tbool = True
        while tbool:
            tempKey = int(input("Введите номер банковской карты: "))
            if len(str(tempKey)) != 16 or tempKey < 0:
                print("Некоректный номер банковской карты, повторите попытку ввода\n")
            else:
                tbool = False

        tempValue = input("Введите кодовое слово для доступа: ")
        cardDict[tempKey] = tempValue

    valueD = input("\nВведите кодовое слово для нахождения банковской карты: ")
    if valueD in cardDict.values():
        for key, value in cardDict.items():
            if value == valueD:
                print(key)
                print(hashlib.md5(str.encode(valueD)))
                break
    else:
        print("Не было найдено ключей по такому кодовому слову")
except (ValueError, TypeError):
    print("Ошибка значения")
except:
    print("Ошибка")