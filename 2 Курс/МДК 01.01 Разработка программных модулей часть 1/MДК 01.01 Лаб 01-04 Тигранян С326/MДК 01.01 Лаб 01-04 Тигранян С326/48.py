import random

Bool = True
while (Bool):
    try:
        playerWinCount = 0
        botWinCount = 0
        movesPlayed  = [0,0,0]

        numberOfGames = int(input("Введите количество игр: ")) #Ввод количества игр
        temp = 0
        choice = [0]                                           #Обьявление необоходиммых переменных
        botChoice = 0
        for i in range(1, numberOfGames + 1):
            easeDic = {                                        #Словарь для интерфейса
                1: "Ножницы",
                2: "Камень",
                3: "Бумага"
            }
            winComb = {                                        #Словарь для орпеделения победы
                1: 2,
                2: 3,
                3: 1
            }
            print("Выберете камень, ножницы, бумага")
            choice.append(int(input("Ножницы : 1, Камень : 2, Бумага : 3 \n"))) #Выбор игрока

            if choice[i] > 3 or choice[i] < 0:          #Проверка на допустимость введённого значения
                Bool = True
                print("Недопустимое значение")
                break
            else:
                Bool = False

            print(f"Ваш выбор: {easeDic[choice[i]]}")   #Вывод выбора игрока

            ################
            match(choice[i]):
                case 1:
                    movesPlayed[0] += 1
                case 2:
                    movesPlayed[1] += 1
                case 3:
                    movesPlayed[1] += 1

            if choice[i] == choice[i - 1]:              #Инкремент переменной temp в случаи повторения выбора
                temp += 1
            else:
                temp = 0

            ################

            if temp > 2:                                #Изменение выбора бота при повторении одного и того же выбора больше двух раз
                botChoice = winComb[choice[i - 1]]
            else:
                botChoice = random.randint(1, 3)  #Придание боту рандомного выбора

            print(f"Выбор бота: {easeDic[botChoice]}")  #Вывор выбора бота

            if choice[i] == winComb[botChoice]:         #Проверка на победу
                print("Вы выиграли")
                playerWinCount += 1
            elif botChoice == choice[i]:                #Проверка на ничью
                print("Ничья")
            else:
                botWinCount += 1
                print("Вы проиграли")                   #При не выполнении предыдущих условий вывод поражения
        print(f"Количество выигранных игр ботом: {botWinCount}")
        print(f"Количество выигранных игр игроком: {playerWinCount}")
        print(f"Было сыграно: \n Ножницы: {movesPlayed[0]} раз \n Камень: {movesPlayed[1]} раз \n Бумага: {movesPlayed[2]} раз")
    except TypeError:
        print("Ошибка типа данных")                     #Обработка except
    except ValueError:
        print("Ошибка значения")
    except:
        print("Ошибка")

