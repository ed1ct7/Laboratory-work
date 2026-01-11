package Triangle;

import java.util.Objects;

public class Triangle {
    private Parameters sides;
    private Parameters angles;

    public Triangle(double a, double b, double c) {
        this.sides = new Parameters(a, b, c);
        calculateAngles();
    }

    public Triangle() {
        this(1, 1, 1);
    }

    public Parameters getSides() {
        return sides;
    }

    public void setSides(Parameters sides) {
        this.sides = Objects.requireNonNull(sides);
        calculateAngles();
    }

    public Parameters getAngles() {
        return angles;
    }

    protected void setAngles(Parameters angles) {
        this.angles = angles;
    }

    public boolean isIsosceles() {
        // В C# сравнение было через double.Epsilon (почти всегда false для неравных чисел).
        // Для сохранения смысла лучше использовать небольшой допуск:
        final double eps = 1e-9;
        double a = sides.getA(), b = sides.getB(), c = sides.getC();
        return Math.abs(a - b) < eps || Math.abs(a - c) < eps || Math.abs(b - c) < eps;
    }

    public boolean isExist() {
        double a = sides.getA();
        double b = sides.getB();
        double c = sides.getC();

        return a > 0 && b > 0 && c > 0
                && a + b > c
                && a + c > b
                && b + c > a;
    }

    public double perimeter() {
        if (!isExist()) return 0.0;
        return sides.getA() + sides.getB() + sides.getC();
    }

    public double area() {
        if (!isExist()) return 0.0;

        double p = perimeter() / 2.0;
        double a = sides.getA();
        double b = sides.getB();
        double c = sides.getC();

        return Math.sqrt(p * (p - a) * (p - b) * (p - c));
    }

    public void calculateAngles() {
        if (!isExist()) {
            setAngles(new Parameters(0, 0, 0));
            return;
        }

        double a = sides.getA();
        double b = sides.getB();
        double c = sides.getC();

        double angleA = Math.acos((b * b + c * c - a * a) / (2 * b * c)) * (180.0 / Math.PI);
        double angleB = Math.acos((a * a + c * c - b * b) / (2 * a * c)) * (180.0 / Math.PI);
        double angleC = Math.acos((a * a + b * b - c * c) / (2 * a * b)) * (180.0 / Math.PI);

        double sum = angleA + angleB + angleC;
        if (Math.abs(sum - 180.0) > 0.001) {
            double correction = (180.0 - sum) / 3.0;
            angleA += correction;
            angleB += correction;
            angleC += correction;
        }

        setAngles(new Parameters(angleA, angleB, angleC));
    }

    @Override
    public String toString() {
        return "Triangle with sides: " + sides
                + "\nAngles: " + angles
                + String.format("\nPerimeter: %.2f\nArea: %.2f\nIsosceles: %s",
                perimeter(), area(), isIsosceles());
    }
}
