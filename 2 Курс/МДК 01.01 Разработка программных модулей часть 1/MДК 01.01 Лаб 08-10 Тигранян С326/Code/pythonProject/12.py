import math
print("""2.	Создайте класс «Круг» с полем, принимающим значение стороны круга, и методами 
для получения периметра и площади. Для одного объекта класса выведите площадь, для 
другого периметр и площадь.\n""")

# Класс круг
class Circle:
    def __init__(self, r = 0):
        self.r = r
    # Метод для вычисление площади
    def squire(self):
        return round(math.pi * self.r**2, 2)
    # Метод для вычисление периметра
    def perimeter(self):
        return round(2 * math.pi * self.r, 2)

try:
    # Ввод данных
    rad1 = int(input("Введите радиус 1 круга: "))
    rad2 = int(input("Введите радиус 2 круга: "))
    # Проверка данных
    while rad1 <= 0 or rad2 <= 0:
        print("Введено некоректное значение радиуса, повторите попытку ввода\n")
        rad1 = int(input("Введите радиус 1 круга: "))
        rad2 = int(input("Введите радиус 2 круга: "))
    one = Circle(rad1)
    two = Circle(rad2)

    print("Площадь первого круга: ",one.squire())
    print("Площадь и периметр второго круга: ", two.squire(), ", ", two.perimeter())

except (TypeError, ValueError):
    print("Ошибка данных")