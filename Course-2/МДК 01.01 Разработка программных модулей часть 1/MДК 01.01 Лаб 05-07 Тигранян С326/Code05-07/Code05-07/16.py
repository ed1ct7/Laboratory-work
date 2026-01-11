print("""Программа проверяет строку введённую пользователем состоящую из слов, разделённых символами,
которые перечисленные во второй строке.
Программа должна показать все слова\n""")

stringWords = input("Введите слова: ") #Ввод строки слов пользователем
stringSep = []
arrWord = []

print("Введите разделительные символы, для остановки ввода введите stop")
tbool = True
while(tbool):
    tempSt = input("Введите раздлительные символы: ")
    if tempSt == "stop":
        tbool = False
    else:
        stringSep.append(tempSt)#Ввод строки разделительных символов пользователем
    tempSt = ""

tbool = True
i = 0
l = 0
while(tbool):
    while stringWords[0] in stringSep:  #Убирает все начальные разделители
        stringWords = stringWords[1:]
        i = 0

    while stringWords[-1] in stringSep:  #Убирает все конечные разделители
        stringWords = stringWords[:-1]
        i = 0

    for j in range(len(stringSep)):
        if i + 1 == len(stringWords):   #Проверяет конечное это слово или нет
            arrWord.append(stringWords)
            tbool = False
            break
        elif stringWords[i] == stringSep[j]: #Записывает и убирает первое слово
            arrWord.append(stringWords[:i])
            stringWords = stringWords[i:]
            i = 0
    i += 1

print("Введённые слова: ",arrWord)