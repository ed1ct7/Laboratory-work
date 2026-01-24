package Matrix;

class MatrixElement {
    private double value;

    public MatrixElement() {}

    public MatrixElement(double value) {
        this.value = value;
    }

    public double getValue() { return value; }
    public void setValue(double value) { this.value = value; }
}
