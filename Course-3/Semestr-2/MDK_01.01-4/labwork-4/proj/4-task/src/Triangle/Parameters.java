package Triangle;

public class Parameters {
    private double a;
    private double b;
    private double c;

    public Parameters() {}

    public Parameters(double a, double b, double c) {
        this.a = a;
        this.b = b;
        this.c = c;
    }

    public double getA() { return a; }
    public void setA(double a) { this.a = a; }

    public double getB() { return b; }
    public void setB(double b) { this.b = b; }

    public double getC() { return c; }
    public void setC(double c) { this.c = c; }

    @Override
    public String toString() {
        return String.format("A: %.2f, B: %.2f, C: %.2f", a, b, c);
    }
}