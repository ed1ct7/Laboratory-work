from tkinter import *
from tkinter import messagebox, font
import random

# Класс игры
class RockPaperScissorsGame(Tk):
    def __init__(self, parent):
        Tk.__init__(self, parent)
        self.root = parent

        # Обьявления пересенных
        self.wins_count = [0,0,0]
        self.temp = 0
        self.choice = ['0']

        # Кол-во сыграных ходов пользователя и бота
        self.user_choices = [0, 0, 0]
        self.computer_choices = [0, 0, 0]

        # Словарь для орпеделения победы и для выбора хода ботом
        self.winComb = {
            'Ножницы' : 'Камень',
            'Бумага' : 'Ножницы',
            'Камень' : 'Бумага'
        }

        # Переменная хранящее выбор бота
        self.bot_choice = ''

        # Переменная хранящяя статистику
        self.stats=''

        # Масштабирование
        self.grid_columnconfigure(0, weight=1)

        self.grid_rowconfigure(0, weight=1)
        self.grid_rowconfigure(1, weight=1)
        self.grid_rowconfigure(2, weight=1)

        self.interface()

    def interface(self):

        # Обьявления canvas
        self.canvas = Canvas(self, width=400, height=550, bg="#313744")
        self.canvas.grid(row=1, column=0, padx=0, columnspan=3, rowspan=4, sticky='NSEW')

        # Шрифт для интерфейса
        title_font = font.Font(family="Helvetica", size=16, weight="bold")

        # Кнопка сброса
        reset_button = Button(self, text="сброс", width=10,
                 height=1, bg="#313744", fg="white", font = title_font,
                 command = lambda: self.reset())
        reset_button.grid(row=0, column=3, padx=0,sticky='NSEW')

        # Кнопка камня
        rock_button = Button(self, text="Камень",
                 command=lambda: self.play("Камень"), width=10, font = title_font,
                 height=1, bg="#313744", fg="white")
        rock_button.grid(row=0, column=0, padx=0,sticky='NSEW')\

        # Кнопка ножниц
        scissors_button = Button(self, text="Ножницы",
                 command=lambda: self.play("Ножницы"),width=10, font = title_font,
                 height=1, bg="#313744", fg="white")
        scissors_button.grid(row=0, column=1, padx=0,sticky='NSEW')

        # Кнопка бумаги
        paper_button = Button(self, text="Бумага",
                 command=lambda: self.play("Бумага"), width=10, font = title_font,
                 height=1, bg="#313744", fg="white")
        paper_button.grid(row=0, column=2, padx=0,sticky='NSEW')

        # Кнопка сохранения
        save_button = Button(self, text="Сохранить статистику",
                 command=self.save_stats, width=20, font = title_font,
                 height=1, bg="#313744", fg="white")
        save_button.grid(row=0, column=4, padx=0, columnspan=3,sticky='NSEW')

        # canvas для окошка вывода победы/пораж/ничьи
        self.canvasW = Canvas(self, width=300, height=300, bg="#313744")
        self.canvasW.grid(row=1, column=3, padx=0, columnspan=2, sticky='NSEW')

        # label для статистики
        self.statistica = Label(self, text = 'Статистика', width=40, height=14, bg="#313744", fg="white",
                                font = font.Font(size=12, weight="bold"))
        self.statistica.grid(row=2, column=3, padx=0, columnspan=2,sticky='NSEW')

        # Масштабирование
        self.grid_columnconfigure(0, weight=1)
        self.grid_columnconfigure(1, weight=1)
        self.grid_columnconfigure(2, weight=1)
        self.grid_columnconfigure(3, weight=1)
        self.grid_columnconfigure(4, weight=1)

        self.grid_rowconfigure(0, weight=1)
        self.grid_rowconfigure(1, weight=1)
        self.grid_rowconfigure(2, weight=1)

    # Метод для задания параметров для изображения
    def image_place(self, pick, st, us):
        img = PhotoImage(file=pick)
        if us == "user":
            self.img_ref_user = img
        elif us == "bot":
            self.img_ref_bot = img
        self.x = (self.canvas.winfo_width() - img.width()) // 2  # Центрируем по горизонтали
        if st == "S":
            self.y = self.canvas.winfo_height() - img.height()  # Позиционируем снизу
        elif st == "N":
            self.y = self.canvas.winfo_height()  # Позиционируем сверху

    # Метод срабатываемые при нажатии кнопок Камень/Ножницы/Бумага
    def play(self, user_choice):

        self.choice.append(user_choice)

        # Анализ бота
        if user_choice == self.choice[len(self.choice) - 2]:  # Инкремент переменной temp в случаи повторения выбора
            self.temp += 1
        else:
            self.temp = 0
        if self.temp > 2:  # Изменение выбора бота при повторении одного и того же выбора больше двух раз
            self.bot_choice = self.winComb[user_choice]
        else:
            self.bot_choice = random.choice(list(self.winComb.keys()))  # Придание боту рандомного выбора

        #######################################################################################

        # Расстановка изображений
        match user_choice:
            case 'Камень':
                self.user_choices[0] += 1
                self.image_place("Камень.png", "N", "user")
                self.canvas.create_image(self.x, self.y, image=self.img_ref_user, anchor="sw")
            case 'Ножницы':
                self.user_choices[1] += 1
                self.image_place("Ножницы.png", "N", "user")
                self.canvas.create_image(self.x, self.y, image=self.img_ref_user, anchor="sw")
            case 'Бумага':
                self.user_choices[2] += 1
                self.image_place("Бумага.png", "N", "user")
                self.canvas.create_image(self.x, self.y, image=self.img_ref_user, anchor="sw")

        temp_bot_choice = self.bot_choice+".png"
        self.image_place(temp_bot_choice, "S", "bot")
        self.canvas.create_image(self.x, self.y, image=self.img_ref_bot, anchor="sw")

        #######################################################################################

        # Вывод поражения/победы/ничьи
        rotAngle = random.randint(-70, 70)
        win_font = font.Font(family="Helvetica", size=25, weight="bold")

        if user_choice == self.winComb[self.bot_choice]:  # Проверка на победу
            self.wins_count[0] += 1
            print("Победа")
            self.canvas.config(bg="#11792b")

            self.canvasW.delete("all")
            self.canvasW.config(bg="#11792b")
            self.canvasW.create_text(self.canvasW.winfo_width() // 2, self.canvasW.winfo_height() // 2,
                                     text = "!!!ПОБЕДА!!!", angle = rotAngle, font = win_font, fill="#ffffff")

        elif self.bot_choice == user_choice:  # Проверка на ничью
            print("Ничья")
            self.wins_count[1] += 1
            self.canvas.config(bg="#2c364e")

            self.canvasW.delete("all")
            self.canvasW.config(bg="#2c364e")
            self.canvasW.create_text(self.canvasW.winfo_width() // 2, self.canvasW.winfo_height() // 2,
                                     text="...НИЧЬЯ...", angle = rotAngle, font = win_font,  fill="#ffffff")

        else:                           # Проверка на поражение
            self.wins_count[2] += 1
            print("Поражение")
            self.canvas.config(bg="#da314a")

            self.canvasW.delete("all")
            self.canvasW.config(bg="#da314a")
            self.canvasW.create_text(self.canvasW.winfo_width() // 2, self.canvasW.winfo_height() // 2,
                                     text="ПОРАЖЕНИЕ(((", angle = rotAngle, font = win_font,  fill="#ffffff")


        self.computer_choices[list(self.winComb.values()).index(self.bot_choice)] += 1

        # Заполнение статистики
        self.stats = (
            f"Количество побед: {self.wins_count[0]}                           \n"
            f"Количество ничей: {self.wins_count[1]}\n"
            f"Количество поражений: {self.wins_count[2]}\n\n"
            
            f"Игрок:\n"
            f"\tСыграно камня: {self.user_choices[0]}\n"
            f"\tСыграно ножниц: {self.user_choices[1]}\n"
            f"\tСыграно бумаги: {self.user_choices[2]}\n"

            f"Бот:\n"
            f"\tСыграно камня: {self.computer_choices[0]}\n"
            f"\tСыграно ножниц: {self.computer_choices[1]}\n"
            f"\tСыграно бумаги: {self.computer_choices[2]}\n"
        )

        self.statistica.config(text =  self.stats, font = font.Font(size=13, weight="bold"),
                            justify = LEFT, relief=RAISED,
                            underline = 0,
                            wraplength = 1000
        )

    # Метод сохранения статистики
    def save_stats(self):
        with open("statistics.txt", "w") as f:
            f.write(f"Количество побед: {self.wins_count[0]}\n"
            f"Количество ничей: {self.wins_count[1]}\n"
            f"Количество поражений: {self.wins_count[2]}\n\n"

            f"Игрок:\n"
            f"\tСыграно камня: {self.user_choices[0]}\n"
            f"\tСыграно ножниц: {self.user_choices[1]}\n"
            f"\tСыграно бумаги: {self.user_choices[2]}\n"

            f"Бот:\n"
            f"\tСыграно камня: {self.computer_choices[0]}\n"
            f"\tСыграно ножниц: {self.computer_choices[1]}\n"
            f"\tСыграно бумаги: {self.computer_choices[2]}\n")
        messagebox.showinfo("Сохранение", "Статистика сохранена в файл statistics.txt")

    # Метод для сброса статистики
    def reset(self):
        self.user_choices = [0,0,0]
        self.computer_choices = [0,0,0]
        self.wins_count = [0,0,0]
        self.temp = 0
        self.choice = ['0']
        self.stats = (
            f"Количество побед: {self.wins_count[0]}                           \n"
            f"Количество ничей: {self.wins_count[1]}\n"
            f"Количество поражений: {self.wins_count[2]}\n\n"
            
            f"Игрок:\n"
            f"\tСыграно камня: {self.user_choices[0]}\n"
            f"\tСыграно ножниц: {self.user_choices[1]}\n"
            f"\tСыграно бумаги: {self.user_choices[2]}\n"

            f"Бот:\n"
            f"\tСыграно камня: {self.computer_choices[0]}\n"
            f"\tСыграно ножниц: {self.computer_choices[1]}\n"
            f"\tСыграно бумаги: {self.computer_choices[2]}\n"
        )
        self.statistica.config(text=self.stats)

game = RockPaperScissorsGame(None)
game.geometry("1000x650")
game.minsize(800, 650)
game.title("Test App")
game.mainloop()
