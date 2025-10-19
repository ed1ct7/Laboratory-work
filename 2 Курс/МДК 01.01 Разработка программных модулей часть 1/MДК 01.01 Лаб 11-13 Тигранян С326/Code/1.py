from tkinter import * # Подключение модуля tkinter

root = Tk() # Конструктор графического окна
# Создание заголовка окна
root.title("Графическая программа на Python")
# Задание размера окна
root.geometry("400x650")

# Метод для красного цвета
def func1():
    I1.config(text = "Стой – красный свет!")
    red.config(bg='#ff0000')
    yellow.config(bg='#f0f0f0')
    green.config(bg='#f0f0f0')

# Метод для жёлтого света
def func2():
    I1.config(text="Жди – жёлтый свет!")
    yellow.config(bg='yellow')
    red.config(bg='#f0f0f0')
    green.config(bg='#f0f0f0')

# Метод для зелёного цвета
def func3():
    I1.config(text="Иди – зелёный свет!")
    green.config(bg='green')
    yellow.config(bg='#f0f0f0')
    red.config(bg='#f0f0f0')

I1 = Label(root, text="", width = 400, height = 10)
I1.pack()

red = Button(root, text="Красный", width = 400, height = 10, bg = 'white', command=func1);red.pack()
yellow = Button(root, text="Жёлтый", width = 400, height = 10, bg = 'white',command=func2);yellow.pack()
green = Button(root, text="Зелёный", width = 400, height = 10, bg = 'white',command=func3);green.pack()

root.mainloop()
