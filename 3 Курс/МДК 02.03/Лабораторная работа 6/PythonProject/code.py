# -*- coding: utf-8 -*-
import random
import tkinter as tk
from tkinter import messagebox


def find_saddle_points(matrix):
    """Ищет нижнюю/верхнюю цену и все седловые точки (целые числа)."""
    m = len(matrix)
    n = len(matrix[0])

    row_mins = [min(row) for row in matrix]            # минимум в каждой строке
    maximin = max(row_mins)                            # нижняя цена

    col_maxs = [max(matrix[i][j] for i in range(m))    # максимум в каждом столбце
                for j in range(n)]
    minimax = min(col_maxs)                            # верхняя цена

    saddle_points = []
    if maximin == minimax:
        for i in range(m):
            for j in range(n):
                if (matrix[i][j] == maximin and
                    row_mins[i] == maximin and
                    col_maxs[j] == maximin):
                    saddle_points.append((i, j))

    return row_mins, col_maxs, maximin, minimax, saddle_points


def reduce_dominated(matrix, row_labels, col_labels, log):
    """Удаляет строго доминируемые строки и столбцы."""
    while True:
        changed = False
        m = len(matrix)
        n = len(matrix[0])

        # Доминируемые строки (A — максимизатор)
        i = 0
        while i < m and not changed:
            for k in range(m):
                if k == i:
                    continue
                ge_all = True   # k[j] >= i[j] для всех j
                greater = False # и > хотя бы для одного j
                for j in range(n):
                    if matrix[k][j] < matrix[i][j]:
                        ge_all = False
                        break
                    if matrix[k][j] > matrix[i][j]:
                        greater = True
                if ge_all and greater:
                    log(f"Строка R{row_labels[i]} доминируется строкой R{row_labels[k]} и удаляется.")
                    matrix.pop(i)
                    row_labels.pop(i)
                    changed = True
                    break
            if not changed:
                i += 1
        if changed:
            continue

        # Доминируемые столбцы (B — минимизатор)
        m = len(matrix)
        n = len(matrix[0])
        j = 0
        while j < n and not changed:
            for l in range(n):
                if l == j:
                    continue
                le_all = True   # l[i] <= j[i] для всех i
                less = False    # и < хотя бы для одного i
                for i in range(m):
                    if matrix[i][l] > matrix[i][j]:
                        le_all = False
                        break
                    if matrix[i][l] < matrix[i][j]:
                        less = True
                if le_all and less:
                    log(f"Столбец C{col_labels[j]} доминируется столбцом C{col_labels[l]} и удаляется.")
                    for i in range(m):
                        matrix[i].pop(j)
                    col_labels.pop(j)
                    changed = True
                    break
            if not changed:
                j += 1

        if not changed:
            break

    return matrix, row_labels, col_labels


def solve_2x2_mixed(matrix):
    """
    Решение игры 2×2 в смешанных стратегиях.
    Возвращает (p1, p2, q1, q2, value) либо None.
    """
    a11, a12 = matrix[0]
    a21, a22 = matrix[1]

    D = (a11 + a22) - (a12 + a21)
    D = float(D)
    if D == 0.0:
        return None

    # Игрок A (строки)
    p1 = float(a22 - a21) / D
    p2 = 1.0 - p1

    # Игрок B (столбцы)
    q1 = float(a22 - a12) / D
    q2 = 1.0 - q1

    if not (0.0 <= p1 <= 1.0 and 0.0 <= q1 <= 1.0):
        return None

    value = float(a11 * a22 - a12 * a21) / D
    return p1, p2, q1, q2, value


def matrix_to_str(matrix, row_labels, col_labels):
    """Простой текстовый вывод матрицы."""
    if not matrix:
        return "Матрица пуста."
    lines = ["Матрица выплат (A — строки, B — столбцы):"]
    lines.append("     " + " ".join(f"C{c}" for c in col_labels))
    lines.append("    " + "-" * (3 * len(col_labels) + 2))
    for i, row in enumerate(matrix):
        row_str = " ".join(f"{val:3d}" for val in row)
        lines.append(f"R{row_labels[i]:2d} | {row_str}")
    return "\n".join(lines)


class MatrixGameApp:
    def __init__(self, root):
        self.root = root
        self.root.title("Матричная игра M×N (упрощённо)")
        self.root.geometry("800x500")

        self.mode_var = tk.StringVar(value="manual")
        self.m_var = tk.IntVar(value=3)
        self.n_var = tk.IntVar(value=3)
        self.matrix_entries = []

        self.create_widgets()

    # ---------- GUI ----------
    def create_widgets(self):
        top = tk.Frame(self.root)
        top.pack(fill="x", padx=5, pady=5)

        tk.Label(top, text="M (строки):").grid(row=0, column=0, sticky="w")
        self.m_spin = tk.Spinbox(top, from_=2, to=20, width=5, textvariable=self.m_var)
        self.m_spin.grid(row=0, column=1)

        tk.Label(top, text="N (столбцы):").grid(row=0, column=2, sticky="w")
        self.n_spin = tk.Spinbox(top, from_=2, to=20, width=5, textvariable=self.n_var)
        self.n_spin.grid(row=0, column=3)

        tk.Label(top, text="Заполнение:").grid(row=1, column=0, sticky="w")
        tk.Radiobutton(top, text="Вручную", variable=self.mode_var, value="manual",
                       command=self.update_mode).grid(row=1, column=1, sticky="w")
        tk.Radiobutton(top, text="Случайно", variable=self.mode_var, value="random",
                       command=self.update_mode).grid(row=1, column=2, sticky="w")

        tk.Label(top, text="Диапазон случайных целых:").grid(row=2, column=0, sticky="w")
        self.min_entry = tk.Entry(top, width=7)
        self.max_entry = tk.Entry(top, width=7)
        self.min_entry.grid(row=2, column=1)
        self.max_entry.grid(row=2, column=2)
        self.min_entry.insert(0, "-10")
        self.max_entry.insert(0, "10")

        tk.Button(top, text="Создать матрицу",
                  command=self.create_matrix).grid(row=0, column=4, rowspan=2, padx=10)

        self.matrix_frame = tk.Frame(self.root, bd=1, relief="sunken")
        self.matrix_frame.pack(padx=5, pady=5)

        btn_frame = tk.Frame(self.root)
        btn_frame.pack(fill="x", padx=5, pady=5)
        tk.Button(btn_frame, text="Рассчитать игру",
                  command=self.solve_game).pack()

        out_frame = tk.Frame(self.root)
        out_frame.pack(fill="both", expand=True, padx=5, pady=5)
        self.output = tk.Text(out_frame, wrap="word")
        scroll = tk.Scrollbar(out_frame, command=self.output.yview)
        self.output.configure(yscrollcommand=scroll.set)
        self.output.grid(row=0, column=0, sticky="nsew")
        scroll.grid(row=0, column=1, sticky="ns")
        out_frame.rowconfigure(0, weight=1)
        out_frame.columnconfigure(0, weight=1)

        self.update_mode()

    def update_mode(self):
        state = "normal" if self.mode_var.get() == "random" else "disabled"
        self.min_entry.config(state=state)
        self.max_entry.config(state=state)

    def log(self, text):
        self.output.insert("end", text + "\n")
        self.output.see("end")

    # ---------- Матрица ----------
    def get_dimensions(self):
        try:
            m = int(self.m_spin.get())
            n = int(self.n_spin.get())
        except ValueError:
            messagebox.showerror("Ошибка", "M и N должны быть целыми.")
            return None, None
        if not (2 <= m <= 20 and 2 <= n <= 20):
            messagebox.showerror("Ошибка", "M и N должны быть от 2 до 20.")
            return None, None
        return m, n

    def create_matrix(self):
        m, n = self.get_dimensions()
        if m is None:
            return

        if self.mode_var.get() == "random":
            try:
                a = int(self.min_entry.get())
                b = int(self.max_entry.get())
                if a > b:
                    a, b = b, a
            except ValueError:
                messagebox.showerror("Ошибка", "Диапазон случайных значений — целые числа.")
                return
        else:
            a = b = None

        for w in self.matrix_frame.winfo_children():
            w.destroy()
        self.matrix_entries = []

        tk.Label(self.matrix_frame, text="").grid(row=0, column=0)
        for j in range(1, n + 1):
            tk.Label(self.matrix_frame, text=f"C{j}").grid(row=0, column=j, padx=2, pady=2)

        for i in range(1, m + 1):
            tk.Label(self.matrix_frame, text=f"R{i}").grid(row=i, column=0, padx=2, pady=2)
            row_entries = []
            for j in range(1, n + 1):
                e = tk.Entry(self.matrix_frame, width=6, justify="center")
                e.grid(row=i, column=j, padx=2, pady=2)
                row_entries.append(e)
            self.matrix_entries.append(row_entries)

        if self.mode_var.get() == "random":
            for i in range(m):
                for j in range(n):
                    self.matrix_entries[i][j].insert(0, str(random.randint(a, b)))

        self.output.delete("1.0", "end")
        self.log("Матрица создана.")

    def read_matrix(self):
        if not self.matrix_entries:
            raise ValueError("Матрица ещё не создана.")
        m = len(self.matrix_entries)
        n = len(self.matrix_entries[0])
        matrix = []
        for i in range(m):
            row = []
            for j in range(n):
                t = self.matrix_entries[i][j].get().strip()
                if not t:
                    raise ValueError(f"Пустая ячейка (строка {i+1}, столбец {j+1}).")
                try:
                    val = int(t)
                except ValueError:
                    raise ValueError(
                        f"Некорректное целое число (строка {i+1}, столбец {j+1})."
                    )
                row.append(val)
            matrix.append(row)
        return matrix

    # ---------- Решение игры ----------
    def solve_game(self):
        self.output.delete("1.0", "end")
        try:
            matrix = self.read_matrix()
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

        # 1. Седловые точки
        row_mins, col_maxs, maximin, minimax, saddles = find_saddle_points(matrix)
        self.log("Минимумы по строкам: " + " ".join(map(str, row_mins)))
        self.log("Максимумы по столбцам: " + " ".join(map(str, col_maxs)))
        self.log(f"Нижняя цена (maximin): {maximin}")
        self.log(f"Верхняя цена (minimax): {minimax}")
        self.log("")

        if saddles:
            self.log("Седловые точки найдены.")
            self.log(f"Цена игры v = {maximin}")
            for i, j in saddles:
                self.log(
                    f"(R{row_labels[i]}, C{col_labels[j]}) со значением {matrix[i][j]}"
                )
            return
        else:
            self.log("Седловых точек нет.")
            self.log("")

        # 2. Удаление доминируемых стратегий
        self.log("=== Удаление доминируемых стратегий ===")
        matrix, row_labels, col_labels = reduce_dominated(
            matrix, row_labels, col_labels, self.log
        )
        self.log("")
        self.log("Матрица после упрощения:")
        self.log(matrix_to_str(matrix, row_labels, col_labels))
        self.log("")

        m2 = len(matrix)
        n2 = len(matrix[0])

        # Повторная проверка седловых точек
        row_mins2, col_maxs2, maximin2, minimax2, saddles2 = find_saddle_points(matrix)
        if saddles2:
            self.log("После упрощения появились седловые точки.")
            self.log(f"Цена игры v = {maximin2}")
            for i, j in saddles2:
                self.log(
                    f"(R{row_labels[i]}, C{col_labels[j]}) со значением {matrix[i][j]}"
                )
            return

        # 3. Если 2×2 — смешанные стратегии
        if m2 == 2 and n2 == 2:
            self.log("Размер матрицы после упрощения: 2×2.")
            res = solve_2x2_mixed(matrix)
            if res is None:
                self.log("Смешанное решение не найдено (нет корректных вероятностей).")
                return
            p1, p2, q1, q2, v = res
            self.log("Оптимальные смешанные стратегии:")
            self.log(f"Игрок A: P(R{row_labels[0]}) = {p1:.3f}, P(R{row_labels[1]}) = {p2:.3f}")
            self.log(f"Игрок B: P(C{col_labels[0]}) = {q1:.3f}, P(C{col_labels[1]}) = {q2:.3f}")
            self.log(f"Цена игры v = {v:.3f}")
        else:
            self.log(
                f"После упрощения размер матрицы {m2}×{n2}.\n"
                "До 2×2 не сведено, смешанные стратегии не считаем."
            )


if __name__ == "__main__":
    root = tk.Tk()
    app = MatrixGameApp(root)
    root.mainloop()
