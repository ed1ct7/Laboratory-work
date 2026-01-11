import random
import string

print("""Программа сформирюет строку из 10 символов. 
На четных позициях должны находится четные цифры, на нечетных позициях - буквы.\n""")

generatedString = ""

for i in range(1,11):
    if i % 2 == 0:
        generatedString += str(random.randrange(0,10, 2)) #Вставка случайных чётных цифр
    else:
        generatedString += random.choice(string.ascii_letters) #Вставка случайных букв

print(generatedString)