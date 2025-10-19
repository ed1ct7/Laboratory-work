arr = []
i = 0
tbool = True
while tbool:                                     # Запись в массив
    tempStr = input(f"Элемент {i + 1}: ")
    if tempStr != "stop":
        arr.append(tempStr)
    elif tempStr == "stop":
        tbool = False
    tempStr = ""
    i += 1

elementToChange = input("Введите строку которая будет заменятся: ")
elementChange = input("Введите строку на которую будет заменятся: ")

if elementToChange in arr:
    for i in range(len(arr)):
        if arr[i] == elementToChange:
            arr[i] = elementChange
else:
    print("Не было найдено данного элемента")
print(*arr)