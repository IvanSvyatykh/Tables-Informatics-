using System;
using System.Data;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace Tables
{
    public class Tables
    {    
        static int NormalizeData(string str)
        {
            str = str.Trim();
            if (int.TryParse(str, out var data)) return data;
            else
            {
                Console.WriteLine("Некорректный ввод данных, данные должны быть целым числом.");
                Console.ReadKey();
                Console.Clear();
                Main();
                return 0;
            }
        }
        static double NormalizeDataSecond(string str)
        {
            str = str.Trim();
            if (int.TryParse(str, out var data)) return data;
            else if (double.TryParse(str, out var data2)) return data2;
            else
            {
                Console.WriteLine("Некорректный ввод данных, данные должны быть целым числом или числом с плавающей точкой.");
                Console.ReadKey();
                Console.Clear();
                Main();
                return 0;
            }
        }
        static void ReadDatazForSecond()
        {
            Console.Write("Введите минимальное число из отрезка: ");
            double x0 = NormalizeDataSecond(Console.ReadLine());
            Console.Write("Введите максимальное число из отрезка: ");
            double xMax = NormalizeDataSecond(Console.ReadLine());
            Console.Write("Введите длину шага: ");
            double step = NormalizeDataSecond(Console.ReadLine());
            Console.ReadKey();
            Console.Clear();
            if (x0 > xMax)
            {
                Console.Clear();
                Console.WriteLine($"Ошибка {x0} больше чем {xMax}.");
                Console.ReadKey();
                Console.Clear();
                Main();
            }
            WriteTable(x0, xMax, step);

        }
        static void ReadDatazForFirst()
        {
            Console.Write("Ввдите длину таблички: ");
            int width = NormalizeData(Console.ReadLine());
            Console.Write("Введите ширину таблички: ");
            int height = NormalizeData(Console.ReadLine());
            Console.Write("Введите верхнюю грань множества натуральных чисел из которых будет производиться выборка: ");
            int roof = NormalizeData(Console.ReadLine());
            Console.ReadKey();
            Console.Clear();
            Feelling(width, height, roof);

        }
        static int Length(double number)
        {
            string str = number.ToString();
            return str.Length;
        }
        static void Feelling(int width, int height, int roof)
        {
            int[,] data = new int[height, width];
            Random rnd = new Random();
            int maxx = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    data[i, j] = rnd.Next(roof);
                    maxx = Math.Max(maxx, Length(data[i, j]));
                }
            }
            WriteTable(width, height, maxx, data);
        }
        static void WriteSeparator(int width, int maxx)
        {
            for (int i = 0; i < width * maxx + 1 + width; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }
        static void WriteTable(int width, int height, int maxx, int[,] data)
        {
            int X = 0;
            int Y = 0;
            if (Console.WindowWidth < width * maxx + 1 + width)
            {
                Console.Clear();
                Console.WriteLine("Внимание выход за границы консоли, введите меньшее значение");
                Console.ReadKey();
                Console.Clear();
                Main();
            }
            for (int i = 0; i < height; i++)
            {
                WriteSeparator(width, maxx);
                Y++;
                for (int j = 0; j < width; j++)
                {
                    Console.Write($"|{data[i, j]}");
                    X += maxx;
                    X++;
                    Console.SetCursorPosition(X, Y);

                }
                Console.SetCursorPosition(X, Y);
                Console.Write("|");
                X = 0;
                Y++;
                Console.WriteLine();
            }
            WriteSeparator(width, maxx);
        }
        static void WriteTable(double x0, double xMax, double step)
        {
            int X = 0;
            int Y = 0;
            int count = 0;
            int width = (int)((xMax - x0) / step);
            width += 2;
            int height = 3;
            int maxx = 0;
            double[] dataY = new double[width];
            if (Console.WindowWidth < width * maxx + 1 + width)
            {
                Console.Clear();
                Console.WriteLine("Внимание выход за границы консоли, введите меньшее значение");
                Console.ReadKey();
                Console.Clear();
                Main();
            }
            for (int i = 0; i < width; i++)
            {
                dataY[i] = Function(x0 + step * count);
                count++;
                maxx = Math.Max(maxx, Length(Function(x0 + step * count)));
            }

            for (int i = 0; i < height; i++)
            {
                WriteSeparator(width, maxx);
                Y++;
                for (int j = 0; j < width; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        Console.Write("№");
                        X += maxx;
                        X++;
                        Console.SetCursorPosition(X, Y);
                    }
                    else if (i == 0)
                    {
                        Console.Write($"|{j}");
                        X += maxx;
                        X++;
                        Console.SetCursorPosition(X, Y);
                    }
                    else if (i == 1 && j == 0)
                    {
                        Console.Write($"X");
                        X += maxx;
                        X++;
                        Console.SetCursorPosition(X, Y);
                    }
                    else if (i == 1 && j != 0)
                    {
                        Console.Write($"|{x0 + step * (j - 1)}");
                        X += maxx;
                        X++;
                        Console.SetCursorPosition(X, Y);
                    }
                    else if (i == 2 && j == 0)
                    {
                        Console.Write($"Y");
                        X += maxx;
                        X++;
                        Console.SetCursorPosition(X, Y);
                    }
                    else if (i == 2 && j != 0)
                    {
                        Console.Write($"|{dataY[j - 1]}");
                        X += maxx;
                        X++;
                        Console.SetCursorPosition(X, Y);
                    }


                }
                Console.SetCursorPosition(X, Y);
                Console.Write("|");
                X = 0;
                Y++;
                Console.WriteLine();
            }
            WriteSeparator(width, maxx);
        }
        static double Function(double argument)
        {
            return Math.Round(Math.Cos(argument) * argument + argument, 3);
        }
        static void Main()
        {
            while(true)
            {
                Console.WriteLine("Введите 1 если хотите получить таблицу с случайными числами, 2 если хотите получить таблицу отображением из Ox в Oy.");
                int answer = NormalizeData(Console.ReadLine());
                Console.Clear();
                if (answer == 1) ReadDatazForFirst();
                else if (answer == 2) ReadDatazForSecond();
                else
                {
                    Console.WriteLine($"Число {answer} не может вызвать операцию");
                    Console.ReadKey();
                    Console.Clear();
                    Main();
                }
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Если хотите завершить программу ввдите stop, иначе нажмите любую клавишу.");
                string str = Console.ReadLine();
                if (str.Trim() == "stop") break;
                Console.Clear();
            }
                     
        }
    }
}