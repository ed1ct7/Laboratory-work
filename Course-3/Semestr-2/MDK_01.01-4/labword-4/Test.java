import java.util.Scanner;

public class Test{
	public static void main(String[] args){
		Scanner scanner = new Scanner(System.in);
		System.out.println("Введите имя:");
		String name = scanner.nextLine();
		System.out.println("Введите число:");
		int number = scanner.nextInt();
		System.out.println("Спасибо,"+name+"! Вы ввели число " + number);

	}
}