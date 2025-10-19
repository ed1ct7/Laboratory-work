namespace code
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите N: ");
            int N = Convert.ToInt32(Console.ReadLine());
            while (N <= 0)
            {
                Console.WriteLine("Размер массива должен быть больше 0");
                Console.Write("Введите N: ");
                N = Convert.ToInt32(Console.ReadLine());
            }
            double[] arr = new double[N];

            Console.WriteLine("Введите элементы массива:");
            for (int i = 0; i < N; i++)
            {
                arr[i] = Convert.ToDouble(Console.ReadLine());
            }

            int twoPositiveNumCounter = 0;
            for (int i = 0; i < N - 1; i++)
            {
                if (arr[i] > 0 && arr[i + 1] > 0)
                {
                    twoPositiveNumCounter++;
                }
            }

            Console.WriteLine("Число соседств двух положительных чисел: " + twoPositiveNumCounter);
        }
    }
}