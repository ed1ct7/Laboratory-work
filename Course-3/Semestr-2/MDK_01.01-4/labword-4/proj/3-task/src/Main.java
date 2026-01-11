import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        int a = 100; // рублей
        int b = 98;  // копеек

        Scanner sc = new Scanner(System.in);

        System.out.println("Продукт стоит 100 рублей 98 копеек");
        System.out.print("Введите количество рублей: ");
        int c = sc.nextInt();     // <-- прочитали рубли

        System.out.print("Введите количество копеек: ");
        int d = sc.nextInt();     // <-- прочитали копейки

        System.out.println("Вы ввели: " + c + " рублей " + d + " копеек");

        sc.close();
    }
}