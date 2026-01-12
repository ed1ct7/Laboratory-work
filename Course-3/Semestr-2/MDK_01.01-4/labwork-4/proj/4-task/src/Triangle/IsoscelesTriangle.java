package Triangle;

public class IsoscelesTriangle extends Triangle {

    public IsoscelesTriangle() {
        super();
    }

    public IsoscelesTriangle(double a, double b, double c) {
        super(a, b, c);
    }

    public boolean isIsoscelesTriangle() {
        if (!isExist()) return false;

        final double eps = 1e-9;
        double a = getSides().getA();
        double b = getSides().getB();
        double c = getSides().getC();

        return Math.abs(a - b) < eps || Math.abs(a - c) < eps || Math.abs(b - c) < eps;
    }

    @Override
    public String toString() {
        return "Isosceles Triangle with sides: " + getSides()
                + "\nAngles: " + getAngles()
                + String.format("\nPerimeter: %.2f\nArea: %.2f",
                perimeter(), area());
    }
}