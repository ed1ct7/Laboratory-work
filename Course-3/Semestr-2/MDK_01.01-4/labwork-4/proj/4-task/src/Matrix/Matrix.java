package Matrix;

import java.util.ArrayList;
import java.util.List;

public class Matrix implements Comparable<Matrix> {

    public static final List<String> OPERATIONS = List.of("+", "-", "*", "compare");

    private int size = 2;
    private List<MatrixElement> matrixElements;

    public Matrix() {
        this.matrixElements = new ArrayList<>();
    }

    public Matrix(List<MatrixElement> matrixElements) {
        this.matrixElements = matrixElements != null ? matrixElements : new ArrayList<>();
    }

    public int getSize() { return size; }
    public void setSize(int size) { this.size = size; }

    public List<MatrixElement> getMatrixElements() { return matrixElements; }
    public void setMatrixElements(List<MatrixElement> matrixElements) {
        this.matrixElements = matrixElements != null ? matrixElements : new ArrayList<>();
    }

    // Проверка, является ли матрица верхнетреугольной
    public boolean isUpperTriangular() {
        for (int i = 1; i < size; i++) {
            for (int j = 0; j < i; j++) {
                if (getElement(i, j) != 0.0) return false;
            }
        }
        return true;
    }

    // Проверка, является ли матрица нижнетреугольной
    public boolean isLowerTriangular() {
        for (int i = 0; i < size; i++) {
            for (int j = i + 1; j < size; j++) {
                if (getElement(i, j) != 0.0) return false;
            }
        }
        return true;
    }

    // Сложение матриц
    public Matrix summation(Matrix other) {
        if (other == null) throw new IllegalArgumentException("other == null");
        if (size != other.size) throw new IllegalArgumentException("Матрицы должны быть одного размера");

        Matrix result = new Matrix();
        result.size = size;
        result.matrixElements = new ArrayList<>(size * size);

        for (int i = 0; i < size * size; i++) {
            result.matrixElements.add(new MatrixElement(
                    this.matrixElements.get(i).getValue() + other.matrixElements.get(i).getValue()
            ));
        }
        return result;
    }

    public Matrix summation(double other) {
        Matrix result = new Matrix();
        result.size = size;
        result.matrixElements = new ArrayList<>(size * size);

        for (int i = 0; i < size * size; i++) {
            result.matrixElements.add(new MatrixElement(
                    this.matrixElements.get(i).getValue() + other
            ));
        }
        return result;
    }

    // Вычитание матриц
    public Matrix subtraction(Matrix other) {
        if (other == null) throw new IllegalArgumentException("other == null");
        if (size != other.size) throw new IllegalArgumentException("Матрицы должны быть одного размера");

        Matrix result = new Matrix();
        result.size = size;
        result.matrixElements = new ArrayList<>(size * size);

        for (int i = 0; i < size * size; i++) {
            result.matrixElements.add(new MatrixElement(
                    this.matrixElements.get(i).getValue() - other.matrixElements.get(i).getValue()
            ));
        }
        return result;
    }

    public Matrix subtraction(double other) {
        Matrix result = new Matrix();
        result.size = size;
        result.matrixElements = new ArrayList<>(size * size);

        for (int i = 0; i < size * size; i++) {
            result.matrixElements.add(new MatrixElement(
                    this.matrixElements.get(i).getValue() - other
            ));
        }
        return result;
    }

    // Умножение матриц
    public Matrix multiplication(Matrix other) {
        if (other == null) throw new IllegalArgumentException("other == null");
        if (size != other.size) throw new IllegalArgumentException("Матрицы должны быть одного размера");

        Matrix result = new Matrix();
        result.size = size;
        result.matrixElements = new ArrayList<>(size * size);

        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                double sum = 0.0;
                for (int k = 0; k < size; k++) {
                    sum += getElement(i, k) * other.getElement(k, j);
                }
                result.matrixElements.add(new MatrixElement(sum));
            }
        }
        return result;
    }

    // Умножение матрицы на число
    public Matrix multiplication(double number) {
        Matrix result = new Matrix();
        result.size = size;
        result.matrixElements = new ArrayList<>(size * size);

        for (int i = 0; i < size * size; i++) {
            result.matrixElements.add(new MatrixElement(
                    this.matrixElements.get(i).getValue() * number
            ));
        }
        return result;
    }

    // Вспомогательный метод для получения элемента по индексам
    private double getElement(int row, int col) {
        return matrixElements.get(row * size + col).getValue();
    }

    // Вспомогательный метод для установки элемента по индексам
    @SuppressWarnings("unused")
    private void setElement(int row, int col, double value) {
        matrixElements.get(row * size + col).setValue(value);
    }

    public double getDeterminant() {
        if (size == 1) return getElement(0, 0);
        if (size == 2) return getElement(0, 0) * getElement(1, 1) - getElement(0, 1) * getElement(1, 0);
        if (size == 3) {
            double deter = 0.0;
            double temp = 1.0;

            for (int i = 0, j = 0, l = 0;
                 l <= size - 1;
                 l = (i >= size ? l + 1 : l),
                         j = (i >= size ? l : (++j >= size ? 0 : j)),
                         i = (i >= size ? 0 : i + 1)) {

                if (i >= size) {
                    deter += temp;
                    temp = 1.0;
                } else {
                    temp *= getElement(i, j);
                }
            }

            temp = 1.0;
            for (int i = 0, j = size - 1, l = 0;
                 l <= size - 1;
                 l = (i >= size ? l + 1 : l),
                         j = (i >= size ? size - 1 - l : (--j < 0 ? size - 1 : j)),
                         i = (i >= size ? 0 : i + 1)) {

                if (i >= size) {
                    deter -= temp;
                    temp = 1.0;
                } else {
                    temp *= getElement(i, j);
                }
            }
            return deter;
        }

        double deter = 0.0;
        int[] perm = new int[size];
        int sign = 1;

        for (int i = 0; i < size; i++) perm[i] = i;

        while (true) {
            double product = 1.0;
            for (int i = 0; i < size; i++) {
                product *= getElement(i, perm[i]);
            }
            deter += sign * product;

            int newSign = nextPermutationInducedSignFlip(perm);
            if (newSign == 0) break;
            sign *= newSign;
        }

        return deter;
    }

    private int nextPermutationInducedSignFlip(int[] perm) {
        int n = perm.length;
        int k = n - 2;
        while (k >= 0 && perm[k] >= perm[k + 1]) k--;
        if (k < 0) return 0;

        int l = n - 1;
        while (perm[k] >= perm[l]) l--;

        int tmp = perm[k];
        perm[k] = perm[l];
        perm[l] = tmp;

        // reverse tail
        for (int i = k + 1, j = n - 1; i < j; i++, j--) {
            int t = perm[i];
            perm[i] = perm[j];
            perm[j] = t;
        }

        return -1;
    }

    public double volume() {
        return getDeterminant();
    }

    @Override
    public int compareTo(Matrix other) {
        if (other == null) throw new IllegalArgumentException("Нельзя сравнить объекты");
        return Double.compare(this.volume(), other.volume());
    }
}
