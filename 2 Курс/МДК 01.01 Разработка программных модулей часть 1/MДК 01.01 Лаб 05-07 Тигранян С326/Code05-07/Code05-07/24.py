print("""Пользователь вводит список.
Программа определяет в нем наиболее часто встречающееся значение.\n""")

print("Введите значения, для остановки ввода введите stop")
list = []
tbool = True
i = 1
while tbool:
    tempStr = input(f"Значение {i}: ")
    if tempStr != "stop":
        list.append(tempStr)
    elif tempStr == "stop":
        tbool = False
    tempStr = ""
    i += 1

elCount = 0
maxEl = ""
set = set(list)                         # Преобразование списка во множество
for i in set:
    if list.count(i) > elCount:
        elCount = list.count(i)
        maxEl = i
if maxEl == 1:
    print("Не было найдено наиболее часто встречающееся значения")
else:
    print("Наиболее часто встречаемое значение: ",maxEl)