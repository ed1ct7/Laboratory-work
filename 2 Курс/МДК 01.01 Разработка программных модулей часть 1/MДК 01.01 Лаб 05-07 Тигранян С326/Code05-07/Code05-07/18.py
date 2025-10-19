print("""Пользователь вводит строки. Программа выводит их в упорядоченном
виде в записиммости от количества цифр в строке.\n""")

print("Введите строки, для остановки ввода введите stop")
arrStrings = []
i = 1
digCount = []
tbool = True
while tbool:                                     #Запись в массив
    tempStr = input(f"Строка {i}: ")
    if tempStr != "stop":
        arrStrings.append(tempStr)
    elif tempStr == "stop":
        tbool = False
    tempStr = ""
    i += 1
print(arrStrings)


digCount = [0] * len(arrStrings)
for i in range(len(arrStrings)):                #Запись в массив количество цифр в каждой строчке
    for j in arrStrings[i]:
        if j.isdigit():
            digCount[i] += 1
print(digCount)

newArrStrings = []                     #Запис в новый массив упорядоченый старый массив относительно количество цифр
for i in range(len(arrStrings)):
    newArrStrings.append(arrStrings[digCount.index(min(digCount))])
    digCount[digCount.index(min(digCount))] = 99999

print(*newArrStrings)