import random
import string
print("""Алгоритм генерации паролям (рандомно и с данными, вводимыми пользователем). 
Учитывать требования к безопасности пароля, состоящего из алфавитно-цифрового набора 
и специальных символов с длиной не менее 6 символьных позиций.\n""")

try:
    CDPChoiceArr = {0: string.ascii_letters, 1: string.digits, 2: string.punctuation}  # Let, Dig, Pun
    CDPChoiceCountArr = [0, 0, 0] #Хранение количетсва каждого вида символа
    choice = int(input("Выберите действие: \n1: Ввод пароля самостоятельно \n2: Генерация пароля \n"))
    while  not(choice == 1 or choice == 2):
        print("Не коректный выбор, повторите попытку\n")
        choice = int(input("Выберите действие: \n1: Ввод пароля самостоятельно \n2: Генерация пароля \n"))
    match choice: #Выбор пользователя
        case 1:
            password = input("Введите пароль: ") #Ввод пароля
            for i in password:
                if i in string.ascii_letters:
                    CDPChoiceCountArr[0] += 1
                elif i in string.digits:
                    CDPChoiceCountArr[1] += 1
                elif i in string.punctuation:
                    CDPChoiceCountArr[2] += 1
            while 0 in CDPChoiceCountArr:  #Улучшение пароля если он недостаточно мощный
                    password += "".join(random.choice(CDPChoiceArr[CDPChoiceCountArr.index(0)]))
                    CDPChoiceCountArr[CDPChoiceCountArr.index(0)] += 1
            while len(password) < 6:
                CDPRandChoice = random.randint(0, 2)
                password += "".join(random.choice(CDPChoiceArr[CDPRandChoice]))
            print(password)
        case 2: #Генерация пароля
            posCount = int(input("Введите количество символов для пароля (минимум 6): ")) #Ввод количества символов
            while posCount < 6:  #Проверка на количество символов
                print("Было введено менее шести символов, повторите попытку ввода\n")
                posCount = int(input("Введите количество символов для пароля (минимум 6): "))
            password = ""
            tbool = True
            for i in range(posCount):  #Заполнение
                tbool = True
                while tbool:  #Проверка и замена в случае отсутствия одного из вида символов
                    CDPRandChoice = random.randint(0, 2)
                    tbool = (CDPChoiceCountArr[CDPRandChoice] > posCount - 2)
                password += "".join(random.choice(CDPChoiceArr[CDPRandChoice]))  #Рандомное заполнение
            print(password)
except TypeError:
    print("Ошибка типа данных")
except ValueError:
    print("Ошибка значения")
except:
    print("Ошибка")
