# Последняя задача из раздела «Строки» ЛР 5-7.

import random
import string
import cProfile

def main():
    print("""Алгоритм генерации паролям (рандомно и с данными, вводимыми пользователем). 
    Учитывать требования к безопасности пароля, состоящего из алфавитно-цифрового набора 
    и специальных символов с длиной не менее 6 символьных позиций.\n""")

    try:
        cdr_choice_arr = {0: string.ascii_letters, 1: string.digits, 2: string.punctuation}  # Let, Dig, Pun
        cdp_choice_count_arr = [0, 0, 0] #Хранение количетсва каждого вида символа
        choice = int(input("Выберите действие: \n1: Ввод пароля самостоятельно \n2: Генерация пароля \n"))
        while  not(choice == 1 or choice == 2):
            print("Не коректный выбор, повторите попытку\n")
            choice = int(input("Выберите действие: \n1: Ввод пароля самостоятельно \n2: Генерация пароля \n"))
        match choice: #Выбор пользователя
            case 1:
                password = input("Введите пароль: ") #Ввод пароля
                for i in password:
                    if i in string.ascii_letters:
                        cdp_choice_count_arr[0] += 1
                    elif i in string.digits:
                        cdp_choice_count_arr[1] += 1
                    elif i in string.punctuation:
                        cdp_choice_count_arr[2] += 1
                while 0 in cdp_choice_count_arr:  #Улучшение пароля если он недостаточно мощный
                        password += "".join(random.choice(cdr_choice_arr[cdp_choice_count_arr.index(0)]))
                        cdp_choice_count_arr[cdp_choice_count_arr.index(0)] += 1
                while len(password) < 6:
                    cdp_rand_choice = random.randint(0, 2)
                    password += "".join(random.choice(cdr_choice_arr[cdp_rand_choice]))
                print(password)
            case 2: #Генерация пароля
                pos_count = int(input("Введите количество символов для пароля (минимум 6): ")) #Ввод количества символов
                while pos_count < 6:  #Проверка на количество символов
                    print("Было введено менее шести символов, повторите попытку ввода\n")
                    pos_count = int(input("Введите количество символов для пароля (минимум 6): "))
                password = ""
                tbool = True
                for i in range(pos_count):  #Заполнение
                    tbool = True
                    while tbool:  #Проверка и замена в случае отсутствия одного из вида символов
                        cdp_rand_choice = random.randint(0, 2)
                        tbool = (cdp_choice_count_arr[cdp_rand_choice] > pos_count - 2)
                    password += "".join(random.choice(cdr_choice_arr[cdp_rand_choice]))  #Рандомное заполнение
                print(password)
    except TypeError:
        print("Ошибка типа данных")
    except ValueError:
        print("Ошибка значения")
    except:
        print("Ошибка")

print()
cProfile.run("main()")