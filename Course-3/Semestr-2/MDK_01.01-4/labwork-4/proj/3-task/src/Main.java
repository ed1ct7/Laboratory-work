/*Создать приложение, в котором для товара стоимостью a руб. b коп. при оплате
        за него c руб. d коп. вычисляется, сколько сдачи требуется получить.
        */

import java.util.Scanner;

class Main{
    public static void main(String[] args) {
        int a = 7; // рублей
        int b = 85;  // копеек
        int c;
        int d;
        int testChecker = 0;
        Scanner sc = new Scanner(System.in);
        try {
            System.out.print("Продукт стоит рублей 98 копеек\n");
            System.out.print("Введите количество рублей: ");
            c = sc.nextInt();
            while (c < 0) {
                System.out.print("Было введено недопустимое количество рублей: \n");
                System.out.print("Введите количество рублей: ");
                c = sc.nextInt();
            }

            System.out.print("Введите количество копеек: ");
            d = sc.nextInt();
            while (d > 99 || d < 0) {
                System.out.print("Было введено недопустимое количество копеейк: \n");
                System.out.print("Введите количество копеек: ");
                d = sc.nextInt();
            }

            System.out.println("Вы ввели: " + c + " рублей " + d + " копеек\n");


            if (c < a || (c == a && d < b)) {
                testChecker = 0;
                System.out.print("Введена не достаточная сумма\n");
            } else if (c == a && d == b) {
                testChecker = 1;
                System.out.print("Сдачи нет\n");
            } else {
                c -= a;
                if (d >= b) {
                    d -= b;
                } else {
                    int t = 100 + d - b;
                    d = t;
                    c--;
                }
                testChecker = 2;
                System.out.printf("Сдача: %d руб., %d коп.", c, d);
            }
            sc.close();
        }
         catch (Exception e) {
            System.out.println("Введено не корректное значение");
            sc.next();
         }
    }
}