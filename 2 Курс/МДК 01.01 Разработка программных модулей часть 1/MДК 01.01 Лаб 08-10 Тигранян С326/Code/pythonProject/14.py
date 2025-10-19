print("""4.	Создайте класс «Транспорт», с полями: тип транспорта (например, автомобиль, мотоцикл, катер)
и грузоподъёмность. Определите методы задания типа транспорта и определения грузоподъёмности 
в зависимости от типа. Создайте производный класс «Автомобиль», имеющий поле вес груза. 
Определите методы задания веса груза (не должен превышать предельную грузоподъёмность) 
и увеличения веса (с интервалом в 10 кг, выводит сообщение о 
текущем весе, в случае превышения – вывод соответствующего сообщения).
Продемонстрируйте работу с полями и методами производного класса на 
нескольких объектах.\n""")

#Заранее определённые переменные
transportDict = {"автомобиль" : 650.0, "мотоцикл" : 275.0, "контейнеровоз" : 513130000.0}
transportType = ["", "автомобиль", "мотоцикл", "контейнеровоз"]

#Класс транcпорта
class Transport:
    def __init__(self, tr_type = None, load_capacity = None):
        self.tr_type = tr_type
        self.load_capacity = load_capacity

    # Метод для выбора типа транспорта
    def change_transport_type(self):
        try:
            print("Выбор транспорта")
            for i in range(1, len(transportType)):
                print(f"Транспорт {i} - {transportType[i]}")
            self.tr_type = int(input("Введите новый тип транспорта\n"))
            while not (1 <= self.tr_type < len(transportDict.keys())+1):
                print("Транспорт недействителен, введите новый\n")
                self.tr_type = int(input("Выберите тип транспорта\n"))
        except (TypeError, ValueError):
            print("Ошибка типа данных, повторите попытку\n")
            self.change_transport_type()

    # Метод для определения грузаподъёмности в зависимости от транcпорта
    def load_capacity_determine(self):
        self.load_capacity = transportDict[transportType[self.tr_type]]

#Класс Автомобиль наследник класса Транспорт
class Car(Transport):
    def __init__(self, tr_type = None, load_capacity = None, load = 0):
        Transport.__init__(self, tr_type, load_capacity)
        self.load = load

    # Метод для ввода груза
    def input_load(self):
        self.load = float(input("Введите вес: "))
        while not (0 <= self.load <= transportDict["автомобиль"]):
            print("Введено не допустимое значение веса, повторите попытку\n")
            self.load = float(input("Введите вес: "))

    # Метод для увеличения на 10 груза
    def increace_load(self):
        self.load += 10
        if self.load > transportDict["автомобиль"]:
            print("Вес превышен")
            self.load -= 10
        print("Текущий вес: ", self.load)


try:
    Mercedes = Car()
    Mercedes.input_load()
    Mercedes.increace_load()
    Mercedes.increace_load()
    Mercedes.increace_load()
except (TypeError,ValueError):
    print("Ошибка значения или типа данных")