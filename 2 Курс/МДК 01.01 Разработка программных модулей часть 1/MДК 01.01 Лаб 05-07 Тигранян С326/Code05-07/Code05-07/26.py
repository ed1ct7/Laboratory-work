print("""Пользователь вводит два списка.
Программа определяет совпадают ли множества их элесентов.\n""")

print("Введите значения, для остановки ввода введите stop")

one = []
two = []
tbool = True
i = 1
print("Первый список")
while tbool:
    tempStr = input(f"Значение {i}: ")
    if tempStr != "stop":
        one.append(tempStr)
    elif tempStr == "stop":
        tbool = False
    tempStr = ""
    i += 1
tbool = True
i = 1
print("Второй список")
while tbool:
    tempStr = input(f"Значение {i}: ")
    if tempStr != "stop":
        two.append(tempStr)
    elif tempStr == "stop":
        tbool = False
    tempStr = ""
    i += 1

if set(sorted(one)) == set(sorted(two)):
    print("Множество двух списков совпадают")
else:
    print("Множество двух списков не совпадают")