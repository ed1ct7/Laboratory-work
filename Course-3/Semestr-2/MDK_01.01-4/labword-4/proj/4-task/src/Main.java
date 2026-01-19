import Triangle.*;
import Matrix.*;

class Main{
     public static void main(String[] args) {
            Triangle t = new Triangle(3, 4, 5);
            System.out.println(t);

            IsoscelesTriangle it = new IsoscelesTriangle(5, 5, 6);
            System.out.println("\n" + it);
    }
}