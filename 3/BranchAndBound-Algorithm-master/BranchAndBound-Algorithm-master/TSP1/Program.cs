using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"1. Отримати матрицю з файлу");
           
            var decision = Convert.ToInt32(Console.ReadLine());
            var loader = new Loader();
            switch (decision)
            {
                case 1:
                    Console.WriteLine($"Введи назву файлу без розширення :");
                    var name = Console.ReadLine();
                    var data = loader.ReadFromFile($"{name}.txt");
                    if (data.Cities == 0)
                    {
                        Console.ReadKey();
                        return;
                    }
                    var solver = new BBAlgorithm(data);
                    var sw = new Stopwatch();
                    sw.Start();
                    solver.Solve();
                    sw.Stop();
                    Console.WriteLine(sw.Elapsed);
                    break;

               

                default:
                    Console.WriteLine($"Натисни 1");
                    break;
            }
            
            Console.ReadKey();

        }
    }
}
