using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    internal class Program
    {
        static int Main(string[] args)
        {
            for (; ; )
            {
                Console.WriteLine("Меню\n1 - Первая лабораторная работа\n2 - Вторая лабораторная работа 1" +
                    "\n3 - Вторая лабораторная работа 2\n0 - Выход");
                Console.Write("Выберите действие: ");
                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 0: return 0;
                        case 1:
                            firstLaboratory(); break;
                        case 2:
                            secondLaboratoryPartO(); break;
                        case 3:
                            secondLaboratoryPartT(); break;
                        default:
                            Console.WriteLine("Недоступная опция повторите выбор\n"); break;
                    }
                    return 0;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка типа данных попробуйте ещё раз\n");
                    continue;
                }
            }
        }
        ///////////////////////////////////
        static int firstLaboratory()
        {
            for (; ; )
            {
                Console.WriteLine("Меню\n1 - Решения первого задания\n2 - Решение второго задания" +
                    "\n3 - Решение третьего задания\n0 - Выход");
                Console.Write("Выберите действие: ");
                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 0: return 0;
                        case 1:
                            Console.Write("Введите числа a: ");
                            double a = Convert.ToDouble(Console.ReadLine());
                            Console.Write("Введите числа b: ");
                            double b = Convert.ToDouble(Console.ReadLine());
                            Console.Write("Введите числа h: ");
                            double h = Convert.ToDouble(Console.ReadLine());
                            TableCOUT(a, b, h); break;
                        case 2:
                            Console.Write("Введите число: ");
                            int n = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine(Task12(n)); break;
                        case 3:
                            Console.Write("Введите число e < 1: ");
                            double e = Convert.ToDouble(Console.ReadLine());
                            Task13(e);
                            break;
                        default:
                            Console.WriteLine("Недоступная опция повторите выбор\n"); break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка типа данных попробуйте ещё раз\n");
                    continue;
                }
            }
        }
        static double Task11(double x)
        {
            if (((x * x) + 1) + Math.Log(x / 2) == 0 || x / 2 <= 0) { return 0; }
            return (2 / (x * x + 1) + Math.Log(x / 2));
        }
        static void TableCOUT(double a, double b, double h)
        {
            if ((a > b) && (h < 0))
            {
                for (; a > b; a += h)
                {
                    if (Task11(a) == 0)
                    {
                        Console.Write(Math.Round(a, 3) + "\t  |  " + "___" + "\n");
                    }
                    else
                    {
                        Console.Write(Math.Round(a, 3) + "\t  |  " + Math.Round(Task11(a), 3) + "\n");
                    }
                }
            }
            else if ((a < b) && (h > 0))
            {
                for (; a < b; a += h)
                {
                    if (Task11(a) == 0)
                    {
                        Console.Write(Math.Round(a, 3) + "\t  |  " + "___" + "\n");
                    }
                    else
                    {
                        Console.Write(Math.Round(a, 3) + "\t  |  " + Math.Round(Task11(a), 3) + "\n");
                    }
                }
            }
            else { Console.Write("Недопустимый шаг\n"); }
        }
        static int Task12(int n)
        {
            int result = 0;
            for (int i = 1; i <= (2 * n - 1); i += 2)
            {
                result += Factorial(i);
            }
            return result;
        }
        static int Factorial(int x)
        {
            switch (x)
            {
                case 0: return 1;
                case 1: return 1;
                default:
                    int fact = 1;
                    for (int i = 1; i < x; i++)
                    {
                        fact *= (i + 1);
                    }
                    return fact;
            }
        }
        static void Task13(double e)
        {
            double sum = 0, a = 1;
            Console.WriteLine("Слагаемые: ");

            int n = 1;
            for (; a > e; ++n)
            {
                a = Math.Pow(3, n + 1) / Factorial(n + 2);
                sum += a;
                Console.WriteLine($"n = {n:D2}\ta = {a:F4}  s = {sum:F8}");
            }
            n -= 1;

            Console.WriteLine(
                "Значение суммы ряда " + Math.Round(sum, 4) +
                "\nКоличество слагаемых: " + n + "\n");
        }
        ///////////////////////////////////
        static int secondLaboratoryPartO()
        {
            for (; ; )
            {
                Console.WriteLine("Меню\n1 - Решения первого задания\n2 - Решение второго задания" +
                    "\n0 - Выход");
                Console.Write("Выберите действие: ");
                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 0: return 0;
                        case 1:
                            Task21(); break;
                        case 2:
                            Task22(); break;
                        default:
                            Console.WriteLine("Недоступная опция повторите выбор\n"); break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка типа данных попробуйте ещё раз\n");
                    continue;
                }
            }
        }
        static int Task21()
        {
            //Расположите в массиве R сначала положительные, а
            //затем отрицательные элементы массива Z

            Random randomeNum = new Random();
            Console.Write("\nВведите N: ");
            int N = Convert.ToInt32(Console.ReadLine());
            int[] Z = new int[N];
            int[] R = new int[N];
            for (int i = 0; i < N; i++)
            {
                Z[i] = randomeNum.Next(-4, 4);
            }

            Console.WriteLine("Изначальный массив:");
            for (int i = 0; i < N; i++)
            {
                Console.Write(Z[i] + " ");
            }

            ///////
            int k = 0;
            for (int i = 0; i < N; i++)
            {
                if (Z[i] > 0)
                {
                    R[k] = Z[i];
                    k++;
                }
            }
            for (int i = 0; i < N; i++)
            {
                if (Z[i] == 0)
                {
                    R[k] = Z[i];
                    k++;
                }
            }
            for (int i = 0; i < N; i++)
            {
                if (Z[i] < 0)
                {
                    R[k] = Z[i];
                    k++;
                }
            }
            ///////
            Console.WriteLine("\nОтсортированный массив: ");
            for (int i = 0; i < N; i++)
            {
                Console.Write(R[i] + " ");
            }
            Console.WriteLine("\n");
            return 0;
        }
        static int Task22()
        {
            //Транспонировать матрицу и выведите на печать
            //элементы главной диагонали и диагонали,
            //расположенной под главной.Результаты
            //разместите в двух строках.

            Random randomeNum = new Random();
            int ROWS = 10;
            int COLS = 8;

            int[,] matrix = new int[ROWS, COLS];
            int[,] temp = new int[COLS, ROWS];

            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    matrix[i, j] = randomeNum.Next(-100, 100);
                }
            }

            Console.WriteLine("Изначальная матрица");
            coutMatrix(matrix, ROWS, COLS);

            for (int i = 0; i < COLS; i++)
            {
                for (int j = 0; j < ROWS; j++)
                {
                    temp[i, j] = matrix[j, i];
                }
            }

            Console.WriteLine("Транспонированная матрицаы\n");
            coutMatrix(temp, COLS, ROWS);

            Console.Write("\nГлавная диагональ: ");
            for (int i = 0; i < COLS; i++)
            {
                Console.Write(temp[i, i] + " ");
            }
            Console.Write("\nДиагональ под главной: ");
            for (int i = 1; i < COLS; i++)
            {
                Console.Write(temp[i, i - 1] + " ");
            }
            Console.WriteLine("\n");

            return 0;
        }
        static void coutMatrix(int[,] matrix, int ROWS, int COLS)
        {
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                    Console.Write($"{matrix[i, j]}".PadRight(5 ,' '));
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        ///////////////////////////////////
        static int secondLaboratoryPartT()
        {
            for (; ; )
            {
                    Console.WriteLine("Меню\n1 - Решения первого задания\n2 - Решение второго задания" +
                    "\n0 - Выход");
                    Console.Write("Выберите действие: ");
                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 0: return 0;
                        case 1:
                            Task23(); break;
                        case 2:
                            Task24(); break;
                        default:
                            Console.WriteLine("Недоступная опция повторите выбор\n"); break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка типа данных попробуйте ещё раз\n");
                    continue;
                }
            }
        }
        static void Task23()
        {
            //Дана строка, содержащая последовательность слов. Напечатать те слова
            //последовательности, которые отличны от последнего слова и удовлетворяют
            //следующему свойству: в слове нет повторяющихся букв.

            Console.Write("Введите строку: ");
            string str = Console.ReadLine() ?? "";
            while (string.IsNullOrWhiteSpace(str))
            {
                Console.Write("Пустой ввод, повторите попытку: ");
                str = Console.ReadLine() ?? "";
            }

            List<string> words = new List<string>();

            string currentWord = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != ' ')
                {
                    currentWord += str[i];
                }
                else
                {
                    if (!string.IsNullOrEmpty(currentWord))
                    {
                        words.Add(currentWord);
                        currentWord = "";
                    }
                }
            }

            if (!string.IsNullOrEmpty(currentWord))
            {
                words.Add(currentWord);
            }

            string lastWord = words.Count > 0 ? words[words.Count - 1] : "";

            List<string> resultWords = new List<string>();

            foreach (string word in words)
            {
                if (word != lastWord)
                {
                    if (word.Distinct().Count() == word.Length)
                    {
                        resultWords.Add(word);
                    }
                }
            }

            if (resultWords.Count > 0)
            {
                Console.WriteLine("Слова, удовлетворяющие условиям:");
                foreach (string word in resultWords)
                {
                    Console.WriteLine(word);
                }
            }
            else
            {
                Console.WriteLine("Нет слов удоволетворяющих условиям\n");
            }
        }
        static void Task24()
        {
            //Ввести строку, состоящую только из цифр и букв.Распечатать те группы цифр, в
            //которых цифра 7 встречается не более двух раз.
            // Ввод строки
            Console.WriteLine();
            string input;
            while (true)
            {
                Console.WriteLine("Введите строку, состоящую из цифр и букв:");
                input = Console.ReadLine() ?? "";

                for (int i = 0; i < input.Length; i++)
                {
                    if (!char.IsLetterOrDigit(input[i]))
                    {
                        Console.WriteLine("В строке есть символ не являющийся цифрой или буквой\n\n");
                        input = "";
                        continue;
                    }
                }
                break;
            }

            int SevenNumCounter = 0;
            string temp = "";
            bool isDigitCol = false;

            Console.WriteLine("Группы цифр не содержащие больше двух 7: ");
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsDigit(input[i])) {
                    temp += input[i];
                    isDigitCol = true;
                    if (input[i] == '7')
                    {
                        SevenNumCounter += 1;
                    }
                    if (i == input.Length - 1) {
                        if (SevenNumCounter < 3)
                        {
                            Console.Write(temp + " ");
                        }
                    }
                }
                else if (isDigitCol)
                {
                    if (SevenNumCounter < 3)
                    {
                        Console.Write(temp + " ");
                    }
                    SevenNumCounter = 0;
                    temp = "";
                    isDigitCol = false;
                }
            }
            if (temp == "")
            {
                Console.Write("В строке нет последовательности цифр в которых 7 встречается меньше 2 раз\n");
            }

            Console.WriteLine();
        }
    }
}
