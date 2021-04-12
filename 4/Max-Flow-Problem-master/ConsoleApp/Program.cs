using PathSearching;
using System;
using System.Collections.Generic;
using System.IO;
using ConsoleApp.Experiment;
using Algorithms.Solvers;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            IOConsole console = new IOConsole();
            while (true)
            {
                try
                {

                    Console.Clear();
                    if (console.CMatrix == null)
                        Console.WriteLine("*** Немає матрицi ***\n");
                    else
                        Console.WriteLine("*** Матриця готова ***\n");
                    Console.WriteLine("1. Зчитати матрицю з файлу");
            
                    Console.WriteLine("2. Вивести матрицю ");
                    Console.WriteLine("3. Вирiшення методом Форда-Фалкерсона");
                  
                    Console.WriteLine("4. Вийти");
                    var key = Console.ReadLine();
                    Console.Clear();
                    switch (key)
                    {
                        case "1":
                            if (console.CMatrix != null)
                            {
                                Console.WriteLine("Матриця вже є! " +
                                  "Перезаписати її? [y/n]");
                                char k;
                                do
                                {
                                    k = Console.ReadKey().KeyChar;
                                } while (k != 'n' && k != 'y');
                                if (k == 'n')
                                    break;
                            }

                            Console.WriteLine("\nВвести назву файлу: ");
                            var filename = Console.ReadLine();
                            if (!File.Exists(filename))
                            {
                                Console.WriteLine("Файл з назвою  \"" + filename + "\" не iснує");
                                Console.ReadKey();
                                continue;
                            }
                            console = new IOConsole(filename);
                            console.ReadMatrix();
                            console.WriteMatrix(console.CMatrix);
                            Console.ReadKey();
                            break;
                        
                        case "2":
                            if (console.CMatrix == null)
                                Console.WriteLine("Немає матрицi");
                            else
                                console.WriteMatrix(console.CMatrix);
                            Console.ReadKey();
                            break;
                        case "3":
                            if (console.CMatrix == null)
                            {
                                Console.WriteLine("Немає матрицi");
                                Console.ReadKey();
                                continue;
                            }

                            Console.WriteLine("Вирiшення методом Форда-Фалкерсона:");
                            Solver solve = new Solver(console.From - 1, console.To - 1, console.N, console.CMatrix);
                            console.WriteFlowMatrixToConsole(console.CMatrix, console.GetNullMatrix());
                            solve.FordFulkerson();

                            Console.WriteLine($"Результат: Max F = {solve.F}");
                            console.WriteFlowMatrixToConsole(console.CMatrix, solve.FlowMatrix.FlowToIntMatrix());
                            Console.ReadKey();
                            break;
                        
                        case "4":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Неправильний вибiр");
                            Console.ReadKey();
                            break;
                    }
                }catch(StackOverflowException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }
            }
        }
    }
}
