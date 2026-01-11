def isPointInsideOfRect(coordsI = [], coordsS = [], size = []): #Функция для возвращения того, находится ли один прямоугольник в другом
    inX = (coordsS[0] <= coordsI[0]) and (coordsI[0] <= coordsS[0] + size[0])
    InY = (coordsS[1] <= coordsI[1]) and (coordsI[1] <= coordsS[1] + size[1])
    return inX and InY

def lengthBetweenPoints(pointA = [], pointB = []): #Функция для нахождения расстояния между точками
    return (pointB[0] - pointA[0])**2 + (pointB[1] - pointA[1])**2

def are_rectangles_intersect(coords, size):        #Функция для нахождения пересечения между прямоугольниками
    return not ((coords[0][0] + size[0][0] or coords[0][1] + size[0][1] <= coords[0][0]) or (coords[1][0] + size[1][0] or coords[1][1] + size[0][1] <= coords[1][0]))

try:
    coords = [[], []] #Двумерный массив для координат нижних левых точек
    size = [[], []]   #Двумерный массив для размеров
    for i in range(2):
        coordNames = {0: 'X', 1: 'Y', 2: 'Длину', 3: 'Ширина'}
        print(f"Введите координаты нижнего левого угла и размер {i + 1} прямоугольника")
        for j in range(2): #Запис размеров
            coords[i].append(int(input(f"Введите {coordNames[j]}: ")))
        for k in range(2, 4): #Запись координат размеров
            size[i].append(int(input(f"Введите {coordNames[k]}: ")))
except TypeError:
    print("Ошибка типа данных")
except ValueError:
    print("Ошибка значения")
except:
    print("Ошибка")

#Проверка условия а

print("Условие а",isPointInsideOfRect(coords[0], coords[1], size[1]))

#Проверка условия б

print("Условие б",(isPointInsideOfRect(coords[0], coords[1], size[1]))or(isPointInsideOfRect(coords[1], coords[0], size[0])))

#Проверка условия в

print("Условие в",are_rectangles_intersect(coords, size))