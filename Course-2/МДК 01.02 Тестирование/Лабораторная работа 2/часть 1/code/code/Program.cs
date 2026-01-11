namespace code
{
    class Program
    {
        static void Main(string[] args)
        {
            double x, y;
            Console.WriteLine("Введите x ");
            x = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите y");
            y = double.Parse(Console.ReadLine());
            if (x == 0 && y == 0)
                Console.WriteLine("Начало координат");
            else if (x == 0 && y != 0)
                Console.WriteLine("Ось Оy");
            else if (x != 0 && y == 0)
                Console.WriteLine("Ось Оx");
            else if (x > 0 && y > 0)
                Console.WriteLine("Квадрат плоскости-1");
            else if (x > 0 && y < 0)
                Console.WriteLine("Квадрат плоскости-4");
            else if (x < 0 && y > 0)
                Console.WriteLine("Квадрат плоскости-2");
            else if (x < 0 && y < 0)
                Console.WriteLine("Квадрат плоскости-3");
        }
    }
}
