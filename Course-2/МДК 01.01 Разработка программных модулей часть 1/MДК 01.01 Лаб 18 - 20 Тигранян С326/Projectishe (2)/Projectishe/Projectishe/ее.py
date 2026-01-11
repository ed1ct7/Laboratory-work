# Импортирование нужных методов
from tkinter import *
from tkinter import ttk
from tkinter import messagebox, font
import webbrowser
from validate_email import validate_email
import sqlite3
import random
import string
from PIL import Image
import re
# Кастомная библиотека для tkinter для создание более современново дизайна
from customtkinter import *


# Родительский класс для всех последующих окон хранящий в себе необходимые переменные и методы
class Parent(CTk):
    # Переменные необходимые для дальнейшей работы программы
    login = 'unknown'  # Переменная хранящяя текущий логин
    bg_cl = "#141a2a"  # Переменная хранящяя цвет заднего фона
    fg_cl = 'white'  # Переменная хранящяя цвет переднего фона (шрифта)
    log_cl = '#1C202B'  # Переменная хранящяя цвет заднего фона для загаловков
    bg_cl_aureg = '#212633'  # Переменная хранящяя цвет заднего фона для некоторых окон
    font_color = 'white'

    set_appearance_mode("dark")  # Modes: system (default), light, dark
    set_default_color_theme("dark-blue")  # Themes: blue (default), dark-blue, green
    mode = "dark"

    def __init__(self, parent):
        CTk.__init__(self, parent)
        self.root = parent

    # Метод возвращает дефолтный фон
    @staticmethod
    def default_font_set(family='Arial', size=18):
        return CTkFont(family, size, "bold")

    # Метод для создания нового окна и закрытия старого
    @staticmethod
    def new_window(create, self_f=None):
        if self_f is not None:
            self_f.destroy()
        new_window = create(None)
        new_window.mainloop()

    # Метод для создания таблицы
    @staticmethod
    def create_grid(self, row_index_amount=1, row_width=1, column_index_amount=1, column_width=1):
        for i in range(column_index_amount):
            self.grid_columnconfigure(i, weight=column_width)
        for i in range(row_index_amount):
            self.grid_rowconfigure(i, weight=row_width)

    # Метод для добавления кнопки
    @staticmethod
    def create_button(parent, text, command, row, column, padx=5, pady=5, sticky="NSEW", **kwargs):
        button = CTkButton(parent, text=text, command=command, **kwargs)
        button.grid(row=row, column=column, sticky=sticky, padx=padx, pady=pady)
        return button

    @staticmethod
    def on_validate(string_to_check, r, ignor):
        if bool(re.compile(r).fullmatch(string_to_check) or string_to_check == ignor):
            return True  # Разрешить изменение
        return False  # Запретить изменение


# Класс основного окна игры
class RockPaperScissorsGame(Parent):
    def __init__(self, parent):
        Parent.__init__(self, parent)
        # Обьявления пересенных
        self.y = None
        self.x = None
        self.img_ref_bot = None
        self.img_ref_user = None
        self.statistica = None
        self.canvas_result = None
        self.image_autorization = None
        self.canvas = None
        self.root = parent

        connection2 = sqlite3.connect('database.db')
        cursor = connection2.cursor()

        sqlite_select_query = """SELECT * from leaderBoard where login = ?"""
        db_list = tuple(cursor.execute(sqlite_select_query, (Parent.login,)).fetchall())

        index = 0
        for i in range(len(db_list)):
            if Parent.login in db_list[i]:
                index = i

        connection2.commit()
        connection2.close()

        self.wins_count = [db_list[index][1], db_list[index][2], db_list[index][3]]
        self.temp = 0
        self.choice = ['0']

        # Кол-во сыграных ходов пользователя и бота
        self.user_choices = [db_list[index][4], db_list[index][5], db_list[index][6]]
        self.computer_choices = [db_list[index][7], db_list[index][8], db_list[index][9]]

        # Словарь для орпеделения победы и для выбора хода ботом
        self.winComb = {
            'Ножницы': 'Камень',
            'Бумага': 'Ножницы',
            'Камень': 'Бумага'
        }

        # Переменная хранящее выбор бота
        self.bot_choice = ''

        # Переменная хранящяя статистику
        self.stats = ''
        self.interface()

    def interface(self):

        self.geometry("940x740")

        # Обьявления canvas
        self.canvas = CTkCanvas(self, width=400, height=550, bg="#313744")
        self.canvas.grid(row=1, column=0, padx=0, columnspan=3, rowspan=4, sticky='NSEW')
        self.config(bg=Parent.bg_cl)
        self.canvas.create_text((self.canvas.winfo_width() / 2, self.canvas.winfo_height() / 2),
                                text="Выберите камень, ножницы или бумагу", angle=50)

        # Создание таблицы
        self.create_grid(self, row_index_amount=5, column_index_amount=3)

        # Кнопки "Камень", "Ножницы", "Бумага"
        for idx, item in enumerate(("Камень", "Ножницы", "Бумага")):
            self.create_button(self,
                               corner_radius=0,
                               border_width=1, padx=0, pady=0,
                               text=item, font=Parent.default_font_set(size=30),
                               command=lambda choice=item: self.play(choice),
                               row=0, column=idx, border_color='white',
                               width=100, height=40, fg_color=Parent.bg_cl, text_color=Parent.fg_cl
                               )

        # Кнопка сохранения
        self.create_button(self, corner_radius=0,
                           border_width=1, padx=0, pady=0,
                           text="Сохранить статистику",
                           command=self.save_stats, font=Parent.default_font_set(size=30),
                           row=0, column=3,
                           width=200, height=40, fg_color=Parent.bg_cl, text_color=Parent.fg_cl
                           )

        # Кнопка перехода на приветственный экран
        self.image_autorization = CTkImage(
            dark_image=Image.open("Menu.png"), size=(70, 70))
        self.create_button(self, corner_radius=0,
                           border_width=1, padx=0, pady=0,
                           image=self.image_autorization, text="", command=lambda: self.new_window(Welcome, self),
                           width=70, height=70, fg_color=Parent.bg_cl, bg_color=Parent.bg_cl, row=0, column=4)

        # Canvas для вывода результатов
        self.canvas_result = CTkCanvas(self, width=300, height=300, bg=Parent.bg_cl)
        self.canvas_result.grid(row=1, column=3, columnspan=2, sticky='NSEW', padx=0, pady=0, )

        # Label для статистики
        self.statistica = CTkLabel(
            self, text='Статистика', width=40, height=14, bg_color=Parent.bg_cl,
            fg_color=Parent.bg_cl, font=Parent.default_font_set(size=24), text_color=Parent.font_color)
        self.statistica.grid(row=2, column=3, columnspan=2, sticky='NSEW', padx=0, pady=0)

    # Метод для задания параметров для изображения
    def image_place(self, pick, st, us):
        img = PhotoImage(file=pick)
        if us == "user":
            self.img_ref_user = img
        elif us == "bot":
            self.img_ref_bot = img
        self.x = (self.canvas.winfo_width() - img.width()) // 2  # Центрируем по горизонтали
        if st == "S":
            self.y = self.canvas.winfo_height() - img.height() - 40  # Позиционируем сверху
        elif st == "N":
            self.y = self.canvas.winfo_height()  # Позиционируем снизу

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

        temp_bot_choice = self.bot_choice + ".png"
        self.image_place(temp_bot_choice, "S", "bot")
        self.canvas.create_image(self.x, self.y, image=self.img_ref_bot, anchor="sw")

        #######################################################################################

        # Вывод поражения/победы/ничьи
        rot_angle = random.randint(-70, 70)
        win_font = font.Font(family="Helvetica", size=25, weight="bold")

        if user_choice == self.winComb[self.bot_choice]:  # Проверка на победу
            self.wins_count[0] += 1
            print("Победа")
            self.canvas.config(bg="#11792b")

            self.canvas_result.delete("all")
            self.canvas_result.config(bg="#11792b")
            self.canvas_result.create_text(self.canvas_result.winfo_width() // 2,
                                           self.canvas_result.winfo_height() // 2,
                                           text="!!!ПОБЕДА!!!", angle=rot_angle, font=win_font, fill="#ffffff")

        elif self.bot_choice == user_choice:  # Проверка на ничью
            print("Ничья")
            self.wins_count[1] += 1
            self.canvas.config(bg="#2c364e")

            self.canvas_result.delete("all")
            self.canvas_result.config(bg="#2c364e")
            self.canvas_result.create_text(self.canvas_result.winfo_width() // 2,
                                           self.canvas_result.winfo_height() // 2,
                                           text="...НИЧЬЯ...", angle=rot_angle, font=win_font, fill="#ffffff")

        else:  # Проверка на поражение
            self.wins_count[2] += 1
            print("Поражение")
            self.canvas.config(bg="#da314a")

            self.canvas_result.delete("all")
            self.canvas_result.config(bg="#da314a")
            self.canvas_result.create_text(self.canvas_result.winfo_width() // 2,
                                           self.canvas_result.winfo_height() // 2,
                                           text="ПОРАЖЕНИЕ(((", angle=rot_angle, font=win_font, fill="#ffffff")

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

        self.statistica.configure(text=self.stats, font=self.default_font_set(size=20),
                                  justify=LEFT,
                                  underline=0,
                                  wraplength=1000,
                                  padx=20,
                                  pady=20
                                  )

        connection = sqlite3.connect('database.db')
        cursor = connection.cursor()

        sqlite_update_query = """Update leaderBoard set win_count =?,draw_count =?,lost_count=?,cobble_player=?,
                scissors_player=?,papper_player=?,cobble_bot=?,scissors_bot=?,papper_bot=?
                where login = ?"""
        column_values = (self.wins_count[0], self.wins_count[1], self.wins_count[2],
                         self.user_choices[0], self.user_choices[1], self.user_choices[2], self.computer_choices[0],
                         self.computer_choices[1], self.computer_choices[2], Parent.login)
        cursor.execute(sqlite_update_query, column_values)

        connection.commit()
        connection.close()

    # Метод сохранения статистики в txt файл
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


# Класс стартового окна
class StartScreen(Parent):
    def __init__(self, parent):
        Parent.__init__(self, parent)
        self.logo_image = None
        self.root = parent
        self.interface()

    # Метод интерфейса
    def interface(self):
        # Заголовок
        self.title("Камень, Ножницы, Бумага")
        self.geometry("1000x740")
        self.config(bg=self.bg_cl_aureg)  # Цвет фона
        self.resizable(False, False)

        (CTkLabel(self, text="Добро пожаловать в игру!", font=self.default_font_set(size=40),
                  text_color=Parent.font_color,
                  width=1000, height=60, bg_color=self.fg_cl, fg_color=self.bg_cl)
         .grid(row=0, columnspan=3, sticky='NSEW'))

        self.logo_image = CTkImage(dark_image=Image.open("StartScreenImageDarkTheme.png"),
                                   size=(400, 670))
        CTkLabel(self, image=self.logo_image, text='').grid(row=1, rowspan=5, column=1)

        # Кнопки
        buttons = [
            ("Начать игру", lambda: self.new_window(Autorisation, self)),
            ("Правила игры", lambda: webbrowser.open
            ("https://ru.wikipedia.org/wiki/%D0%9A%D0%B0%D0%BC%D0%B5%D0%BD%D1%8C,"
             "_%D0%BD%D0%BE%D0%B6%D0%BD%D0%B8%D1%86%D1%8B,_%D0%B1%D1%83%D0%BC%D0%B0%D0%B3%D0%B0")),
            ("Ссылка на проэкт", lambda: webbrowser.open
            ("https://github.com/ed1ct7?tab=repositories")),
            ("Университет", lambda: webbrowser.open
            ("https://guap.ru/?ysclid=m4it8ih8fv471846935#")),
            ("Выход рабочий стол", self.destroy)
        ]

        for i, (text, command) in enumerate(buttons, start=1):
            self.create_button(
                self,
                text=text,
                hover_color="#55a19d",
                fg_color=self.bg_cl,
                corner_radius=0,
                font=self.default_font_set(size=36),
                text_color=Parent.font_color,
                width=180,
                height=100,
                command=command,
                row=i,
                column=0,
                padx=30,
                pady=5,
                sticky='EW'
            )


# Класс окна для авторизации пользователя
class Autorisation(Parent):
    def __init__(self, parent):
        Parent.__init__(self, parent)
        self.remember_user_box = None
        self.error_password = None
        self.error_login = None
        self.image_autorization = None
        self.entry_password = None
        self.image_home = None
        self.login_entry = None
        self.root = parent
        self.interface()

    def interface(self):
        self.title("Окно авторизации")
        self.geometry("600x550")
        self.config(bg=self.bg_cl_aureg)
        self.resizable(False, False)

        row_counter = 0

        # Заголовок
        (CTkLabel(self, text="Авторизация", font=self.default_font_set(size=40),
                  bg_color=self.log_cl, width=600, height=70, text_color=Parent.font_color, )
         .grid(row=row_counter, columnspan=3, pady=10, padx=1, sticky="NSEW"))

        row_counter += 1
        # Логин
        self.login_entry = CTkEntry(self, placeholder_text="Введите логин",
                                    font=self.default_font_set(size=32),
                                    width=550, height=100,
                                    validate="key",
                                    validatecommand=(self.register
                                                     (lambda value:
                                                      self.on_validate(value, r'^[a-zA-Z0-9._%+-]*$',
                                                                       'Введите логин')), "%P")
                                    )
        self.login_entry.grid(row=row_counter, columnspan=3, pady=(10, 0), padx=1)

        row_counter += 1

        self.error_login = CTkLabel(self, text='', bg_color=self.bg_cl_aureg,
                                    font=self.default_font_set(size=20), text_color='#da314a')
        self.error_login.grid(row=row_counter, columnspan=3, pady=(5, 15))

        row_counter += 1

        # Пароль
        self.entry_password = CTkEntry(self, placeholder_text="Введите пароль",
                                       font=self.default_font_set(size=32),
                                       width=550, height=100, show='*',
                                       validate="key",
                                       validatecommand=(self.register
                                                        (lambda value:
                                                         self.on_validate(value, r'^[^[^а-яА-Я]*$]*$',
                                                                          'Введите пароль')), "%P")
                                       )
        self.entry_password.grid(row=row_counter, column=0, columnspan=3, pady=(2, 0), padx=1)

        row_counter += 1

        self.error_password = CTkLabel(self, text='', bg_color=self.bg_cl_aureg,
                                       font=self.default_font_set(size=20), text_color='#da314a')
        self.error_password.grid(row=row_counter, columnspan=3, pady=(5, 5))

        row_counter += 1

        self.remember_user_box=CTkCheckBox(self, text="Запомнить вход",font=self.default_font_set(size=30),
                                           bg_color=self.bg_cl_aureg, checkbox_width=40, checkbox_height=40)
        self.remember_user_box.grid(row=row_counter, column=0, columnspan=2, padx=(28, 1), pady=10, sticky='W')

        row_counter += 1

        reg_button = CTkButton(self, text="Войти", command=self.autorize, width=340, height=60,
                               font=self.default_font_set(size=32))
        reg_button.grid(row=row_counter, column=1, padx=1, pady=20)

        # Навигация
        self.image_home = CTkImage(dark_image=Image.open("HomeButton.png"),
                                   size=(50, 50))
        back_to_start = CTkButton(self, image=self.image_home, text="", width=100,
                                  command=lambda: self.new_window(StartScreen, self))
        back_to_start.grid(row=row_counter, column=0, padx=(22, 1), pady=10)

        self.image_autorization = CTkImage(dark_image=Image.open("RegScreen.png"),
                                           size=(50, 50))
        back_to_auth = CTkButton(self, image=self.image_autorization, text="", width=100,
                                 command=lambda: self.new_window(Registration, self))
        back_to_auth.grid(row=row_counter, column=2, padx=(1, 22), pady=10)

    def autorize(self):
        if self.entry_password.get() != '' and self.login_entry.get() != '':
            self.error_login.configure(text="")
            self.error_password.configure(text="")
            # Устанавливаем соединение с базой данных
            connection = sqlite3.connect('database.db')
            cursor = connection.cursor()
            # Создаем таблицу Users
            t_bool = True

            try:
                db_list = tuple(cursor.execute('SELECT * FROM Users').fetchall())
            except sqlite3.Error:
                db_list = ((),)
                t_bool = True
            for i in range(len(db_list)):
                if db_list[i][0] == self.login_entry.get() and db_list[i][1] == self.entry_password.get():
                    Parent.login = self.login_entry.get()
                    messagebox.showinfo("Вход", f"Добро пожайловать {Parent.login}")
                    self.new_window(Welcome, self)
                    t_bool = False
            if t_bool:
                self.error_login.configure(text="Аккаунт с таким логином и паролем не найден")

            connection.commit()
            connection.close()
        else:
            if self.entry_password.get() == '':
                self.error_password.configure(text="Поле пароль не заполненно")
            else:
                self.error_password.configure(text="")

            if self.login_entry.get() == '':
                self.error_login.configure(text="Поле логин не заполненно")
            else:
                self.error_login.configure(text="")


# Класс окна для регистрации пользователя
class Registration(Parent):
    def __init__(self, parent):
        Parent.__init__(self, parent)
        self.radio_button_gender = None
        self.age_error_label = None
        self.email_error_label = None
        self.image_autorization = None
        self.image_home = None
        self.gender_var = None
        self.entry_age = None
        self.email_entry = None
        self.entry_password = None
        self.login_entry = None
        self.root = parent
        self.interface()

    # Метод интерфейса
    def interface(self):
        self.title("Окно регистрации")
        self.geometry("650x760")
        self.config(bg=self.bg_cl_aureg)
        self.resizable(False, False)

        # переменная для упрощённого добавления виджета между других виджетов
        row_num = 0

        # Создание таблицы
        self.create_grid(self, 10, 1, 5, 1)

        # Заголовок
        (CTkLabel(self, text="Регистрация аккаунта", font=self.default_font_set(size=40),
                  bg_color=self.log_cl, text_color=Parent.font_color)
         .grid(row=row_num, columnspan=5, pady=0, padx=0, sticky="NSEW"))

        row_num += 1

        # entry для логина
        self.login_entry = CTkEntry(self, placeholder_text="Введите логин", width=600, height=50,
                                    font=self.default_font_set(size=24),
                                    validate="key",
                                    validatecommand=(self.register
                                                     (lambda value:
                                                      self.on_validate(value, r'^[a-zA-Z0-9._%+-]*$',
                                                                       'Введите логин')), "%P")
                                    )
        self.login_entry.grid(row=row_num, columnspan=5, column=0, pady=10, padx=20)

        row_num += 1

        # entry для пароля
        self.entry_password = CTkEntry(self, placeholder_text="Введите пароль", width=480, height=50,
                                       font=self.default_font_set(size=24),
                                       validate="key",
                                       validatecommand=(self.register
                                                        (lambda value:
                                                         self.on_validate(value, r'^[^а-яА-Я]*$',
                                                                          'Введите пароль')), "%P"
                                                        )
                                       )
        self.entry_password.grid(row=row_num, column=0, columnspan=4, padx=(5, 1))

        (CTkButton(self, text="Ген",
                   command=self.password_generator, width=100, height=50,
                   font=self.default_font_set(size=24), text_color=Parent.font_color, )
         .grid(row=row_num, column=3, columnspan=2, sticky='E', padx=(0, 25)))

        row_num += 1

        # Почта
        self.email_entry = CTkEntry(self, placeholder_text="Введите почту", width=600, height=50,
                                    font=self.default_font_set(size=24),
                                    validate="key",
                                    validatecommand=(self.register
                                                     (lambda value:
                                                      self.on_validate(value, r'^[a-zA-Z0-9._%+@-]*$',
                                                                       'Введите почту')), "%P"
                                                     )
                                    )
        self.email_entry.grid(row=row_num, columnspan=5, column=0, pady=10, padx=20)

        row_num += 1

        self.email_error_label = CTkLabel(self, bg_color=Parent.bg_cl_aureg, text='')
        self.email_error_label.grid(row=row_num, columnspan=5, column=0)

        row_num += 1

        (CTkLabel(self, text="Введите год рождения", font=Parent.default_font_set(size=24),
                  text_color=Parent.font_color,
                  bg_color=Parent.log_cl, width=140, height=50)
         .grid(row=row_num, columnspan=5, pady=5, padx=0, sticky='NSEW'))

        row_num += 1

        # Год рождения
        self.entry_age = CTkComboBox(self,
                                     values=[str(i) for i in range(1950, 2025)],
                                     width=600 - 40, height=50,
                                     font=self.default_font_set(size=24))
        self.entry_age.grid(row=row_num, columnspan=5, pady=5, padx=10)

        row_num += 1

        # Лейбл для вывода ошибки при вводе возраста
        self.age_error_label = CTkLabel(self, bg_color=Parent.bg_cl_aureg, text='')
        self.age_error_label.grid(row=row_num, columnspan=5, column=0)

        row_num += 1

        # Лейбл для пола
        (CTkLabel(self, text="Выберите пол", font=Parent.default_font_set(size=24), text_color=Parent.font_color,
                  bg_color=self.log_cl, width=140, height=50)
         .grid(row=row_num, columnspan=5, pady=(5, 0), padx=0, sticky='NSEW'))

        row_num += 1

        (CTkLabel(self, text='', bg_color=Parent.bg_cl_aureg, fg_color=Parent.log_cl, height=50, width=400,
                  corner_radius=10000)
         .grid(row=row_num, columnspan=5, rowspan=2,
               pady=5, padx=0))
        self.gender_var = StringVar(value="Мужчина")

        i = 1
        for gender in ["Муж", "Жен", "Другое"]:
            self.radio_button_gender = CTkRadioButton(self, text=gender,
                                                      variable=self.gender_var,
                                                      value=gender,
                                                      bg_color=Parent.log_cl,
                                                      text_color=Parent.font_color,
                                                      font=self.default_font_set(size=28))
            self.radio_button_gender.grid(row=row_num, column=i, pady=5, padx=1)
            i += 1
        self.radio_button_gender.select("Другое")

        row_num += 1

        # (CTkLabel(self, bg_color=self.bg_cl_aureg, text='', text_color=Parent.bg_cl).
        #  grid(row=row_num, columnspan=5, column=0))

        row_num += 1

        reg_button = CTkButton(self, text="Зарегистрировать", command=self.reg,
                               width=420, height=80,
                               text_color=Parent.font_color,
                               font=self.default_font_set(size=32))
        reg_button.grid(row=row_num, column=1, columnspan=3, padx=1, pady=40)

        # Навигация
        self.image_home = CTkImage(dark_image=Image.open("HomeButton.png"), size=(70, 70))
        back_to_start = CTkButton(self, image=self.image_home, text="", width=50,
                                  command=lambda: self.new_window(StartScreen, self))
        back_to_start.grid(row=row_num, column=0, padx=5, pady=10)

        self.image_autorization = CTkImage(dark_image=Image.open("BackButton.png"), size=(70, 70))
        back_to_auth = CTkButton(self, image=self.image_autorization, text="", width=50,
                                 command=lambda: self.new_window(Autorisation, self))
        back_to_auth.grid(row=row_num, column=4, padx=5, pady=10)

    # Метод для генерации пароля
    def password_generator(self):
        cdr_rand_choice = 0
        self.entry_password.delete(0, END)
        cdp_choice_arr = {0: string.ascii_letters, 1: string.digits, 2: string.punctuation}  # Let, Dig, Pun
        cdp_choice_count_arr = [0, 0, 0]  # Хранение количетсва каждого вида символа
        pos_count = 15
        password = ""
        for i in range(pos_count):  # Заполнение
            tbool = True
            while tbool:  # Проверка и замена в случае отсутствия одного из вида символов
                cdr_rand_choice = random.randint(0, 2)
                tbool = (cdp_choice_count_arr[cdr_rand_choice] > pos_count - 2)
            password += "".join(random.choice(cdp_choice_arr[cdr_rand_choice]))  # Рандомное заполнение
        self.entry_password.insert(0, password)

    # Метод для регистрации аккаунта
    def reg(self):
        try:
            # Устанавливаем соединение с базой данных
            if (self.email_entry.get() == '' or self.entry_password.get() == '' or self.login_entry.get() == ''
                    ' ' in self.email_entry.get() or ' ' in self.entry_password.get() or ' ' in self.login_entry.get()):

                self.email_error_label.configure(text="Логин, пароль и email обязательные поля для заполнения",
                                                 font=self.default_font_set(size=20),
                                                 text_color='#da314a',
                                                 anchor='nw'
                                                 )

            elif not validate_email(self.email_entry.get()):
                self.email_error_label.configure(text="Введён несуществующий email",
                                                 font=self.default_font_set(size=20),
                                                 text_color='#da314a',
                                                 anchor='nw'
                                                 )
            else:
                self.email_error_label.configure(text="",
                                                 bg_color=self.bg_cl_aureg
                                                 )
                connection = sqlite3.connect('database.db')
                cursor = connection.cursor()
                # Создаем таблицу Users
                cursor.execute("""
                                            CREATE TABLE IF NOT EXISTS Users (
                                            login TEXT,
                                            password TEXT,
                                            email TEXT,
                                            gender TEXT,
                                            age INT,
                                            color_mode BOOL
                                            );
                                            """)
                t_bool = True
                db_list = tuple(cursor.execute('SELECT * FROM Users').fetchall())
                for i in range(len(db_list)):
                    if self.login_entry.get() in db_list[i] or self.email_entry.get() in db_list[i]:
                        t_bool = False

                if self.entry_age.get() != '':
                    age = int(self.entry_age.get())
                else:
                    age = ''

                if t_bool:
                    cursor.execute('INSERT INTO Users (login, password, email, gender, age) '
                                   'VALUES (?, ?, ?, ?, ?)',
                                   (self.login_entry.get(), self.entry_password.get(),
                                    self.email_entry.get(), self.gender_var.get(), age))

                    messagebox.showinfo("УспЭх", "Аккаунт зарегестрирован")

                    connection.commit()
                    connection.close()

                    connection2 = sqlite3.connect('database.db')

                    cursor = connection2.cursor()
                    # Создаем таблицу Users
                    cursor.execute("""
                                                    CREATE TABLE IF NOT EXISTS leaderBoard (
                                                    login TEXT,
                                                    win_count INT,
                                                    draw_count INT,
                                                    lost_count INT,
                                                    cobble_player INT,
                                                    scissors_player INT,
                                                    papper_player INT,
                                                    cobble_bot INT,
                                                    scissors_bot INT,
                                                    papper_bot INT
                                                    );
                                                    """)

                    cursor.execute('INSERT INTO leaderBoard (login, win_count, draw_count, lost_count, '
                                   'cobble_player, scissors_player, papper_player,'
                                   'cobble_bot, scissors_bot, papper_bot) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)',
                                   (self.login_entry.get(), 0, 0, 0, 0, 0, 0, 0, 0, 0))
                    connection2.commit()
                    connection2.close()

                    connection3 = sqlite3.connect('database.db')

                    cursor = connection3.cursor()
                    # Создаем таблицу Users
                    cursor.execute("""
                                                                       CREATE TABLE IF NOT EXISTS preference (
                                                                       login TEXT,
                                                                       remember INT,
                                                                       
                                                                       );
                                                                       """)

                    cursor.execute('INSERT INTO leaderBoard (login, win_count, draw_count, lost_count, '
                                   'cobble_player, scissors_player, papper_player,'
                                   'cobble_bot, scissors_bot, papper_bot) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)',
                                   (self.login_entry.get(), 0, 0, 0, 0, 0, 0, 0, 0, 0))
                    connection2.commit()
                    connection2.close()

                    self.new_window(Autorisation, self)

                else:
                    self.age_error_label.configure(text="Недопустимый год рождения",
                                                   font=self.default_font_set(size=20),
                                                   text_color='#da314a',
                                                   anchor='nw'
                                                   )
                    # Сохраняем изменения и закрываем соединение

        except (TypeError, ValueError):
            self.age_error_label.configure(text="Недопустимое значение для года рождения",
                                           font=self.default_font_set(size=20),
                                           text_color='#da314a',
                                           anchor='nw'
                                           )


# Класс для приветственного окна и меню
class Welcome(Parent):
    def __init__(self, parent):
        Parent.__init__(self, parent)
        self.root = parent
        self.interface()

    # Метод для интерфейса
    def interface(self):
        # Задание заднего цвета окна
        self.config(bg=self.bg_cl_aureg)
        # Запрет изменения размера окна
        self.resizable(False, False)
        # Задание размеров окну
        self.geometry("1050x550")

        # Заголовок
        CTkLabel(
            self,
            text="Камень, ножницы, бумага", width=1050, height=60,
            text_color=Parent.font_color,
            font=Parent.default_font_set(size=35),
            bg_color=Parent.bg_cl
        ).grid(row=0, column=0, columnspan=3, pady=10, sticky="NSEW")

        # Кнопки
        buttons = [
            ("Играть", lambda: self.new_window(RockPaperScissorsGame, self)),
            ("Поменять тему", lambda: self.change_theme()),
            ("Выйти из аккаунта", lambda: self.new_window(StartScreen, self)),
            ("Выйти на рабочий стол", self.destroy)
        ]

        # Создание кнопок для приветственного экрана
        for i, (text, command) in enumerate(buttons, start=1):
            self.create_button(
                self,
                text=text,
                hover_color="#55a19d",
                fg_color=Parent.bg_cl,
                bg_color=Parent.bg_cl_aureg,
                text_color=Parent.font_color,
                corner_radius=10,
                font=Parent.default_font_set(size=30),
                width=10,
                height=70,
                command=command,
                row=i,
                column=0, padx=20, pady=5, sticky="EW"
            )

        # Таблица лидеров
        CTkLabel(
            self,
            text="Таблица лидеров",
            font=Parent.default_font_set(size=30),
            text_color=Parent.font_color,
            bg_color=Parent.bg_cl,
            height=60,
            width=100,
        ).grid(row=5, column=1, columnspan=1, pady=10, sticky="NSEW")

        # Показ текущего пользователя
        CTkLabel(
            self,
            text="Пользователь: " + Parent.login,
            font=Parent.default_font_set(size=30),
            text_color=Parent.font_color,
            bg_color=Parent.bg_cl
        ).grid(row=5, column=0, columnspan=1, pady=10, sticky="NSEW")

        # Стили для Treeview
        style = ttk.Style()
        style.theme_use("clam")
        style.configure("Treeview",
                        background=Parent.bg_cl,
                        foreground=Parent.fg_cl,
                        rowheight=40,
                        fieldbackground=Parent.bg_cl,
                        text_color=Parent.font_color,
                        font=Parent.default_font_set(size=23))
        style.configure("Treeview.Heading",
                        background=Parent.bg_cl,
                        foreground=Parent.fg_cl,
                        text_color=Parent.font_color,
                        font=Parent.default_font_set(size=27))
        style.map("Treeview",
                  background=[("selected", "#55a19d")],
                  foreground=[("selected", Parent.fg_cl)])

        # Treeview
        tree = ttk.Treeview(self, columns=("Name", "Win amount", "K/D", "Game played"), show="headings", height=8)
        tree.grid(row=1, column=1, rowspan=4, padx=10, pady=10, sticky="NSEW")
        tree.heading("Name", text="Имя")
        tree.heading("Win amount", text="Побед")
        tree.heading("K/D", text="K/D")
        tree.heading("Game played", text="Сыграно")
        tree.column("Name", width=150, anchor="center")
        tree.column("Win amount", width=150, anchor="center")
        tree.column("K/D", width=150, anchor="center")
        tree.column("Game played", width=150, anchor="center")

        # Пример данных
        connection2 = sqlite3.connect('database.db')
        cursor = connection2.cursor()

        sqlite_select_query = """SELECT * from leaderBoard"""
        db_list = tuple(cursor.execute(sqlite_select_query, ).fetchall())

        connection2.commit()
        connection2.close()

        # Заполнение таблицы
        data = []
        print(db_list)
        for i in range(len(db_list)):
            try:
                data.append((db_list[i][0], db_list[i][1],
                             round(db_list[i][1] + db_list[i][2] +
                                   db_list[i][3] / db_list[i][1], 2),
                             db_list[i][1] + db_list[i][2] + db_list[i][3]))
                # Проверка на отсутствия побед
            except ZeroDivisionError:
                data.append((db_list[i][0], db_list[i][1], 0,
                             db_list[i][1] + db_list[i][2] + db_list[i][3]))

        for item in data:
            tree.insert("", "end", values=item)

        # Добавление функциональности сортировки
        for col in ("Name", "Win amount", "K/D", "Game played"):
            tree.heading(col, command=lambda _col=col: self.treeview_sort_column(tree, _col, False))

        # Сортировка таблицы
    def treeview_sort_column(self, treeview, col, reverse):
        data = [(treeview.set(child, col), child) for child in treeview.get_children('')]

        # Определить тип данных для сортировки
        try:
            # Если данные могут быть преобразованы в числа
            data.sort(key=lambda t: float(t[0]) if t[0] else 0, reverse=reverse)
        except ValueError:
            # Если данные являются строками
            data.sort(key=lambda t: t[0], reverse=reverse)

        # Переставить элементы Treeview в отсортированном порядке
        for index, (_, child) in enumerate(data):
            treeview.move(child, '', index)

        # Изменить порядок сортировки при следующем клике
        treeview.heading(col, command=lambda: self.treeview_sort_column(treeview, col, not reverse))

    # Метод для изменения темы
    def change_theme(self):
        if self.mode == "dark":
            set_appearance_mode("light")
            Parent.mode = "light"
            Parent.bg_cl = '#21A3C7'
            Parent.bg_cl_aureg = 'white'
            Parent.fg_cl = 'white'
            Parent.log_cl = '#3471ac'
            Parent.font_color = 'white'
            Parent.new_window(Welcome, self)
        else:
            set_appearance_mode("dark")
            Parent.bg_cl = "#141a2a"  # Переменная хранящяя цвет заднего фона
            Parent.fg_cl = 'white'  # Переменная хранящяя цвет переднего фона (шрифта)
            Parent.log_cl = '#1C202B'  # Переменная хранящяя цвет заднего фона для загаловков
            Parent.bg_cl_aureg = '#212633'  # Переменная хранящяя цвет заднего фона для некоторых окон
            Parent.font_color = 'white'
            set_appearance_mode("dark")  # Modes: system (default), light, dark
            set_default_color_theme("dark-blue")  # Themes: blue (default), dark-blue, green
            Parent.mode = "dark"
            Parent.new_window(Welcome, self)
            # Clear text box


# Запуск стартового окна
menu = StartScreen(None)
menu.geometry("950x740")
menu.minsize(200, 200)
menu.mainloop()
