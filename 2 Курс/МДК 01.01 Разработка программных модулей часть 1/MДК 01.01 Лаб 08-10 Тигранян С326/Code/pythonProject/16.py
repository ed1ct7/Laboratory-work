print("""6.	Создайте класс «Продажа», с полями: вид товара (например, техника, мебель) и 
списком сумм месячных продаж за полгода. Определите методы задания вида товара, 
списка значений сумм продаж и определения самого удачного месяца (выводит значение 
суммы и название соответствующего месяца). Продемонстрируйте работу с полями и 
методами класса на нескольких объектах.\n""")

# Заранее определённые переменные
goodsType = ["техника", "мебель", "молоко", "i9 14900K"]
months = ['Январь', 'Февраль','Март', 'Апрель','Май', 'Июнь',
          'Июль', 'Август','Сентябрь', 'Октябрь','Ноябрь', 'Декабрь',]

# Класс продаж
class Sales:
    def __init__(self, goods_type = None, month_profit = None):
        self.goods_type = goods_type
        self.month_profit = month_profit

    # Метод для выбора товара
    def change_goods_type(self):
        try:
            print("Виды товаров: ")
            for i in range(len(goodsType)):
                print(f"Товар {i + 1} - {goodsType[i]}")
            self.goods_type = int(input("\nВведите номер товара: "))
            # Проверка на действительность выбора
            while not (1 <= self.goods_type <= len(goodsType)):
                print("Номер товара нeдействителен, ввыберите новый\n")
                self.goods_type = int(input("Введите номер товара\n"))
        except (TypeError, ValueError):
            print("Ошибка типа данных, повторите попытку\n")
            self.change_goods_type()

    # Метод для задания прибили за каждый месяц
    def month_profit_input(self):
        # Проверка на ввод
        try:
            # Ввод первого месяца
            self.month_profit = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
            print("Введите список значений сумм продаж")
            first_month = input("Введите первый месяц: ")
            while not (first_month in months):
                print("Некоректный ввод месяца, проследите, что бы месяц "
                      "начинался с большой буквы и был написан правильно\n")
                first_month = input("Введите первый месяц: ")
            k = months.index(first_month)
            j = 0
            # Ввод продаж
            while j < 6:
                if k >= 12:
                    k = 0
                self.month_profit[k] = int(input(f"Прибыль за {months[k]}: "))
                k += 1
                j += 1
        # В случае ошибке метод вызывает сам себя
        except (TypeError, ValueError):
            print("Ошибка ввода данных повторите попытку:\n")
            self.month_profit_input()

    # Метод для определения лучшего месяца
    def best_month(self):
        # Проверка на количество месяцев с лучшей прибелью
        # Если месяц не один то метод возвращает список индексов номеров этих месяцев
        # В ином случае метод возвращает индекс только одного месяца
        if self.month_profit.count(max(self.month_profit)) > 1:
            enters = []
            for i in range(len(self.month_profit)):
                if self.month_profit[i] == max(self.month_profit):
                    enters.append(i)
            return enters
        else:
            return self.month_profit.index(max(self.month_profit))


good = Sales()
good.change_goods_type()
print()
good.month_profit_input()

if type(good.best_month()) == list:
    print("\nЛучшие месяцы: ")
    print("Прибиль лучших месяцев - ",good.month_profit[good.best_month()[0]])
    for i in good.best_month():
        print(f'\t{months[i]}')
else:
    print(f'{months[good.best_month()]} - {good.month_profit[good.best_month()]}')


