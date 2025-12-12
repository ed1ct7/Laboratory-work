import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
from sklearn.linear_model import LinearRegression

try:
    # 1) Чтение данных
    df = pd.read_csv("dataset_ML.csv", sep=";")
    print("Первые строки данных:")
    print(df.head(), "\n")

    # 2) Выбор X и Y (вариант 6 -> Y6)
    X = df["X"].values
    Y = df["Y6"].values

    # 3) reshape для X
    X = X.reshape((-1, 1))
    print("X shape:", X.shape)
    print("Y shape:", Y.shape)
    print("Первые 5 X:", X.flatten()[:5])
    print("Первые 5 Y:", Y[:5], "\n")

    # 4) Обучение модели + параметры + прогноз на 3 года
    model = LinearRegression().fit(X, Y)
    r2 = model.score(X, Y)
    k = float(model.coef_[0])
    b = float(model.intercept_)

    print("Параметры модели:")
    print("R^2 =", r2)
    print("k =", k)
    print("b =", b)
    print(f"Уравнение: y = {k}*x + ({b})\n")

    X_future = np.array([2021, 2022, 2023]).reshape((-1, 1))
    Y_future = model.predict(X_future)

    print("Прогноз (model.predict) на 2021–2023:")
    for year, val in zip(X_future.flatten(), Y_future):
        print(year, "->", val)
    print()

    # 5) Прогноз по формуле с параметрами модели
    Y_future_formula = b + k * X_future
    print("Прогноз (по формуле b + k*x) на 2021–2023:")
    for year, val in zip(X_future.flatten(), Y_future_formula.flatten()):
        print(year, "->", val)
    print()

    # 6) Графики
    Y_fit = model.predict(X)

    plt.figure()
    plt.scatter(X.flatten(), Y)
    plt.title("График исходных данных (Y6)")
    plt.xlabel("X (год)")
    plt.ylabel("Y6")
    plt.show()

    plt.figure()
    plt.plot(X.flatten(), Y, label="Исходные данные")
    plt.plot(X.flatten(), Y_fit, label="Найденная модель")
    plt.scatter(X_future.flatten(), Y_future, label="Прогноз 2021–2023")
    plt.title("Исходные данные, модель и прогноз (Y6)")
    plt.xlabel("X (год)")
    plt.ylabel("Y6")
    plt.legend()
    plt.show()

except FileNotFoundError:
    print("Файл dataset_ML.csv не найден. Положи его в ту же папку, где запускаешь код, или укажи полный путь к файлу.")
except KeyError as e:
    print("В CSV нет нужного столбца:", e)
    print("Проверь, что есть столбцы: X и Y6, а разделитель в файле — ';'.")
