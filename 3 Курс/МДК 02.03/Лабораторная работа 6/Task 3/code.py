# -*- coding: utf-8 -*-
import random
import tkinter as tk
from tkinter import messagebox

import matplotlib
matplotlib.use("TkAgg")
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg
import matplotlib.pyplot as plt

EPS = 1e-9


def find_saddle_points(matrix):
    """Находит нижнюю/верхнюю цену и все седловые точки."""
    m = len(matrix)
    n = len(matrix[0])

    # Нижняя цена (maximin) — максимум из минимумов по строкам
    row_mins = [min(row) for row in matrix]
    maximin = max(row_mins)

    # Верхняя цена (minimax) — минимум из максимумов по столбцам
    col_maxs = []
    for j in range(n):
        col_maxs.append(max(matrix[i][j] for i in range(m)))
    minimax = min(col_maxs)

    saddle_points = []
    if abs(maximin - minimax) < EPS:
        # Ищем все седловые точки
        for i in range(m):
            for j in range(n):
                if (
                    abs(matrix[i][j] - maximin) < EPS
                    and abs(matrix[i][j] - row_mins[i]) < EPS
                    and abs(matrix[i][j] - col_maxs[j]) < EPS
                ):
                    saddle_points.append((i, j))

    return row_mins, col_maxs, maximin, minimax, saddle_points


def reduce_dominated(matrix, row_labels, col_labels, log_func):
    """Удаляет строго доминируемые стратегии (строки/столбцы)."""
    changed = True
    while changed:
        changed = False
        m = len(matrix)
        n = len(matrix[0])

        # Удаляем доминируемые строки (игрок A — максимизатор)
        i = 0
        while i < m:
            dominated = False
            dom_by = None
            for k in range(m):
                if k == i:
                    continue
                ge_all = True
                greater_some = False
                for j in range(n):
                    if matrix[k][j] < matrix[i][j] - EPS:
                        ge_all = False
                        break
                    if matrix[k][j] > matrix[i][j] + EPS:
                        greater_some = True
                if ge_all and greater_some:
                    dominated = True
                    dom_by = row_labels[k]
                    break
            if dominated:
                log_func(
                    f"Стратегия игрока A (строка) R{row_labels[i]} "
                    f"строго доминируется стратегией R{dom_by} и удаляется."
                )
                matrix.pop(i)
                row_labels.pop(i)
                changed = True
                break
            else:
                i += 1
        if changed:
            continue

        # Удаляем доминируемые столбцы (игрок B — минимизатор)
        m = len(matrix)
        n = len(matrix[0])
        j = 0
        while j < n:
            dominated = False
            dom_by = None
            for l in range(n):
                if l == j:
                    continue
                le_all = True
                less_some = False
                for i in range(m):
                    if matrix[i][l] > matrix[i][j] + EPS:
                        le_all = False
                        break
                    if matrix[i][l] < matrix[i][j] - EPS:
                        less_some = True
                if le_all and less_some:
                    dominated = True
                    dom_by = col_labels[l]
                    break
            if dominated:
                log_func(
                    f"Стратегия игрока B (столбец) C{col_labels[j]} "
                    f"строго доминируется стратегией C{dom_by} и удаляется."
                )
                for i in range(m):
                    matrix[i].pop(j)
                col_labels.pop(j)
                changed = True
                break
            else:
                j += 1

    return matrix, row_labels, col_labels


def solve_2x2_mixed(matrix):
    """
    Решение игры 2x2 в смешанных стратегиях (аналитически).
    Возвращает (p1, p2, q1, q2, value) либо None, если решения нет.
    """
    a11, a12 = matrix[0]
    a21, a22 = matrix[1]

    D = (a11 + a22) - (a12 + a21)  # знаменатель
    if abs(D) < EPS:
        return None

    # Игрок A (строки)
    p1 = (a22 - a21) / D
    p2 = 1 - p1

    # Игрок B (столбцы)
    q1 = (a22 - a12) / D
    q2 = 1 - q1

    # Проверяем, что вероятности в [0,1]
    if not (0 - EPS <= p1 <= 1 + EPS and 0 - EPS <= q1 <= 1 + EPS):
        return None

    value = (a11 * a22 - a12 * a21) / D

    # Обрезаем численные погрешности
    p1 = max(0.0, min(1.0, p1))
    p2 = max(0.0, min(1.0, p2))
    q1 = max(0.0, min(1.0, q1))
    q2 = max(0.0, min(1.0, q2))

    return p1, p2, q1, q2, value


def matrix_to_str(matrix, row_labels, col_labels):
    """Форматированный вывод матрицы в строку."""
    if not matrix:
        return "Матрица пуста."
    m = len(matrix)
    n = len(matrix[0])
    lines = []
    lines.append("Матрица выплат (игрок A — строки, игрок B — столбцы):")
    header = "      " + " ".join(f"  C{c:2d}" for c in col_labels)
    lines.append(header)
    lines.append("     " + "-" * (4 * n + 2))
    for i, row in enumerate(matrix):
        row_str = " ".join(f"{val:7.2f}" for val in row)
        lines.append(f"R{row_labels[i]:2d} | {row_str}")
    return "\n".join(lines)

class MatrixGameApp:
    def __init__(self, root):
        self.root = root
        self.root.title("Матричная игра M×N")
        self.root.geometry("900x600")

        self.mode_var = tk.StringVar(value="manual")  # manual / random
        self.m_var = tk.IntVar(value=3)
        self.n_var = tk.IntVar(value=3)

        self.matrix_entries = []  # список списков Entry

        self.create_widgets()

    def create_widgets(self):
        # Верхняя панель
        top_frame = tk.Frame(self.root)
        top_frame.pack(fill="x", padx=5, pady=5)

        # Размеры M и N
        tk.Label(top_frame, text="M (строки):").grid(row=0, column=0, padx=3, pady=2, sticky="w")
        self.m_spin = tk.Spinbox(top_frame, from_=2, to=20, width=5, textvariable=self.m_var)
        self.m_spin.grid(row=0, column=1, padx=3, pady=2)

        tk.Label(top_frame, text="N (столбцы):").grid(row=0, column=2, padx=3, pady=2, sticky="w")
        self.n_spin = tk.Spinbox(top_frame, from_=2, to=20, width=5, textvariable=self.n_var)
        self.n_spin.grid(row=0, column=3, padx=3, pady=2)

        # Режим ввода
        tk.Label(top_frame, text="Заполнение:").grid(row=1, column=0, padx=3, pady=2, sticky="w")
        tk.Radiobutton(
            top_frame,
            text="Вручную",
            variable=self.mode_var,
            value="manual",
            command=self.update_mode_state,
        ).grid(row=1, column=1, padx=3, pady=2, sticky="w")
        tk.Radiobutton(
            top_frame,
            text="Случайно",
            variable=self.mode_var,
            value="random",
            command=self.update_mode_state,
        ).grid(row=1, column=2, padx=3, pady=2, sticky="w")

        # Диапазон случайных значений
        tk.Label(top_frame, text="Диапазон случайных значений:").grid(
            row=2, column=0, padx=3, pady=2, sticky="w"
        )
        self.min_entry = tk.Entry(top_frame, width=7)
        self.min_entry.grid(row=2, column=1, padx=3, pady=2)
        self.max_entry = tk.Entry(top_frame, width=7)
        self.max_entry.grid(row=2, column=2, padx=3, pady=2)
        self.min_entry.insert(0, "-10")
        self.max_entry.insert(0, "10")

        # Кнопка "Создать матрицу"
        self.create_btn = tk.Button(top_frame, text="Создать матрицу", command=self.create_matrix)
        self.create_btn.grid(row=0, column=4, rowspan=2, padx=10, pady=2, sticky="ns")

        # Рамка для матрицы
        self.matrix_frame = tk.Frame(self.root, bd=1, relief="sunken")
        self.matrix_frame.pack(padx=5, pady=5)

        # Кнопка "Рассчитать"
        button_frame = tk.Frame(self.root)
        button_frame.pack(fill="x", padx=5, pady=5)
        self.solve_btn = tk.Button(button_frame, text="Рассчитать игру", command=self.solve_game)
        self.solve_btn.pack()

        # Вывод результата
        output_frame = tk.Frame(self.root)
        output_frame.pack(fill="both", expand=True, padx=5, pady=5)

        self.output = tk.Text(output_frame, wrap="word")
        scroll = tk.Scrollbar(output_frame, command=self.output.yview)
        self.output.configure(yscrollcommand=scroll.set)

        self.output.grid(row=0, column=0, sticky="nsew")
        scroll.grid(row=0, column=1, sticky="ns")

        output_frame.rowconfigure(0, weight=1)
        output_frame.columnconfigure(0, weight=1)

        self.update_mode_state()

    def update_mode_state(self):
        """Включаем/отключаем поля диапазона для случайного заполнения."""
        if self.mode_var.get() == "random":
            self.min_entry.config(state="normal")
            self.max_entry.config(state="normal")
        else:
            self.min_entry.config(state="disabled")
            self.max_entry.config(state="disabled")

    def log(self, text):
        self.output.insert("end", text + "\n")
        self.output.see("end")

    def get_dimensions(self):
        try:
            m = int(self.m_spin.get())
            n = int(self.n_spin.get())
        except ValueError:
            messagebox.showerror("Ошибка", "M и N должны быть целыми числами.")
            return None, None
        if not (2 <= m <= 20 and 2 <= n <= 20):
            messagebox.showerror("Ошибка", "M и N должны быть в диапазоне [2; 20].")
            return None, None
        return m, n

    def create_matrix(self):
        """Создать сетку для матрицы и при необходимости заполнить случайно."""
        m, n = self.get_dimensions()
        if m is None:
            return

        # Диапазон случайных значений
        if self.mode_var.get() == "random":
            try:
                a = float(self.min_entry.get().replace(",", "."))
                b = float(self.max_entry.get().replace(",", "."))
                if a > b:
                    a, b = b, a
            except ValueError:
                messagebox.showerror("Ошибка", "Диапазон случайных значений задан неверно.")
                return
        else:
            a = b = None  # не используется

        # Очистить старую матрицу
        for w in self.matrix_frame.winfo_children():
            w.destroy()
        self.matrix_entries = []

        # Заголовки столбцов
        tk.Label(self.matrix_frame, text="").grid(row=0, column=0, padx=2, pady=2)
        for j in range(1, n + 1):
            tk.Label(self.matrix_frame, text=f"C{j}", padx=3).grid(row=0, column=j, padx=2, pady=2)

        # Строки со значениями
        for i in range(1, m + 1):
            tk.Label(self.matrix_frame, text=f"R{i}", padx=3).grid(row=i, column=0, padx=2, pady=2)
            row_entries = []
            for j in range(1, n + 1):
                e = tk.Entry(self.matrix_frame, width=7, justify="center")
                e.grid(row=i, column=j, padx=2, pady=2)
                row_entries.append(e)
            self.matrix_entries.append(row_entries)

        # Заполнение случайно (если выбрано)
        if self.mode_var.get() == "random":
            for i in range(m):
                for j in range(n):
                    val = round(random.uniform(a, b), 2)
                    self.matrix_entries[i][j].insert(0, str(val))

        self.output.delete("1.0", "end")
        self.log("Матрица создана. Введите значения или используйте сгенерированные.")

    def read_matrix_from_entries(self):
        """Чтение матрицы из полей ввода."""
        if not self.matrix_entries:
            raise ValueError("Матрица ещё не создана.")
        m = len(self.matrix_entries)
        n = len(self.matrix_entries[0])
        matrix = []
        for i in range(m):
            row = []
            for j in range(n):
                text = self.matrix_entries[i][j].get().strip()
                if not text:
                    raise ValueError(
                        f"Пустая ячейка в позиции (строка {i + 1}, столбец {j + 1})."
                    )
                try:
                    value = float(text.replace(",", "."))
                except ValueError:
                    raise ValueError(
                        f"Некорректное число в позиции (строка {i + 1}, столбец {j + 1})."
                    )
                row.append(value)
            matrix.append(row)
        return matrix

    def show_graphical_solution_2x2(self, matrix, row_labels, col_labels, p1, p2, q1, q2, value):
        """
        Окно с графическим методом для 2×2:
        по оси X — q = P(первый столбец),
        по оси Y — выигрыш A при выборе каждой строки.
        """
        a11, a12 = matrix[0]
        a21, a22 = matrix[1]

        # Значения q от 0 до 1
        q_vals = [i / 100 for i in range(101)]
        line1 = [q * a11 + (1 - q) * a12 for q in q_vals]
        line2 = [q * a21 + (1 - q) * a22 for q in q_vals]

        win = tk.Toplevel(self.root)
        win.title("Графический метод решения 2×2 игры")

        fig, ax = plt.subplots(figsize=(5, 4))
        ax.plot(q_vals, line1, label=f"Строка R{row_labels[0]}")
        ax.plot(q_vals, line2, label=f"Строка R{row_labels[1]}")

        # Отмечаем оптимальную точку (q*, v)
        if 0 - EPS <= q1 <= 1 + EPS:
            ax.plot([q1], [value], "o")
            ax.annotate(
                f"q* = {q1:.2f}\nv = {value:.2f}",
                xy=(q1, value),
                xytext=(min(q1 + 0.1, 1.0), value),
                arrowprops=dict(arrowstyle="->"),
            )

        ax.set_xlabel(f"q = P(C{col_labels[0]})")
        ax.set_ylabel("Выигрыш игрока A")
        ax.set_title("Графический метод (зависимость от стратегии B)")
        ax.grid(True)
        ax.legend()

        canvas = FigureCanvasTkAgg(fig, master=win)
        canvas.draw()
        canvas.get_tk_widget().pack(fill="both", expand=True)

        self.log("Открылось окно с графиком (графический метод решения 2×2 игры).")

    def solve_game(self):
        """Основная логика решения игры."""
        self.output.delete("1.0", "end")

        try:
            matrix = self.read_matrix_from_entries()
        except ValueError as e:
            messagebox.showerror("Ошибка ввода", str(e))
            return

        m = len(matrix)
        n = len(matrix[0])
        row_labels = list(range(1, m + 1))
        col_labels = list(range(1, n + 1))

        self.log("=== Исходная игра ===")
        self.log(matrix_to_str(matrix, row_labels, col_labels))
        self.log("")

        # Шаги 2–4: maximin, minimax, седловые точки
        row_mins, col_maxs, maximin, minimax, saddles = find_saddle_points(matrix)

        self.log("Минимумы по строкам: " + " ".join(f"{x:.2f}" for x in row_mins))
        self.log("Максимумы по столбцам: " + " ".join(f"{x:.2f}" for x in col_maxs))
        self.log(f"Нижняя цена игры (maximin): {maximin:.4f}")
        self.log(f"Верхняя цена игры (minimax): {minimax:.4f}")
        self.log("")

        if saddles:
            self.log("Седловые точки найдены.")
            self.log(f"Цена игры v = {maximin:.4f}")
            for (i, j) in saddles:
                self.log(
                    f"Седловая точка: ячейка (R{row_labels[i]}, C{col_labels[j]}) "
                    f"со значением {matrix[i][j]:.4f}"
                )
                self.log(
                    f"Решение в чистых стратегиях: "
                    f"игрок A выбирает строку R{row_labels[i]}, "
                    f"игрок B — столбец C{col_labels[j]}."
                )
            return
        else:
            self.log("Седловых точек в исходной игре нет.")
            self.log("")

        # Шаг 5: удаляем доминируемые стратегии
        self.log("=== Упрощение матрицы (удаление строго доминируемых стратегий) ===")
        matrix, row_labels, col_labels = reduce_dominated(
            matrix, row_labels, col_labels, self.log
        )
        self.log("")
        self.log("Матрица после упрощения:")
        self.log(matrix_to_str(matrix, row_labels, col_labels))
        self.log("")

        m2 = len(matrix)
        n2 = len(matrix[0])

        # Проверяем седловые точки после упрощения
        row_mins2, col_maxs2, maximin2, minimax2, saddles2 = find_saddle_points(matrix)
        if saddles2:
            self.log("После упрощения появились седловые точки.")
            self.log(f"Цена игры v = {maximin2:.4f}")
            for (i, j) in saddles2:
                self.log(
                    f"Седловая точка: ячейка (R{row_labels[i]}, C{col_labels[j]}) "
                    f"со значением {matrix[i][j]:.4f}"
                )
                self.log(
                    f"Решение в чистых стратегиях: "
                    f"игрок A выбирает строку R{row_labels[i]}, "
                    f"игрок B — столбец C{col_labels[j]}."
                )
            return

        # Шаги 5–6: если 2×2 и нет седловой точки — решаем в смешанных + графически
        if m2 == 2 and n2 == 2:
            self.log("Размерность игры после упрощения — 2×2.")
            self.log("Решаем игру в смешанных стратегиях (и показываем графический метод).")
            result = solve_2x2_mixed(matrix)
            if result is None:
                self.log(
                    "Решение в смешанных стратегиях (с вероятностями внутри (0,1)) не существует."
                )
                return
            p1, p2, q1, q2, value = result
            self.log("")
            self.log("Оптимальные смешанные стратегии:")
            self.log(
                f"Игрок A (строки):\n"
                f"  P(R{row_labels[0]}) = {p1:.4f}\n"
                f"  P(R{row_labels[1]}) = {p2:.4f}"
            )
            self.log(
                f"Игрок B (столбцы):\n"
                f"  P(C{col_labels[0]}) = {q1:.4f}\n"
                f"  P(C{col_labels[1]}) = {q2:.4f}"
            )
            self.log(f"Цена игры (математическое ожидание выигрыша игрока A): v = {value:.4f}")

            # Графический метод: отдельное окно с графиком
            self.show_graphical_solution_2x2(
                matrix, row_labels, col_labels, p1, p2, q1, q2, value
            )
        else:
            self.log(
                f"После упрощения размерность игры {m2}×{n2}.\n"
                f"Свести игру к размерности 2×2 не удалось, поэтому "
                f"решение в смешанных стратегиях по данному алгоритму не вычисляется."
            )


if __name__ == "__main__":
    root = tk.Tk()
    app = MatrixGameApp(root)
    root.mainloop()
