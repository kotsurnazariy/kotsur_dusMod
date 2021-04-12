using ConsoleApp.Experiment.Auxiliary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp
{
    public class IOConsole
    {
        private readonly string _filename;
        private int _N;
        private int _A;
        private int _B;
        private int[,] _matrix;
        public int N { get => _N; }
        public int From { get => _A; }
        public int To { get => _B; }
        public int[,] CMatrix { get => _matrix; }
        public IOConsole()
        {
            _filename = null;
            _matrix = null;
        }
        public IOConsole(string filename)
        {
            _filename = filename;
            _matrix = null;
        }
        public void ConsoleInputMatrix()
        {
            Console.WriteLine("Enter quantity of nodes (N): ");
            var n = Convert.ToInt32(Console.ReadLine());
            if (n <= 0)
                throw new Exception("Not positive quantity");
            Console.WriteLine("Enter the start point: ");
            var a = Convert.ToInt32(Console.ReadLine());
            if (a <= 0 || a > n)
                throw new Exception("Start point is out of range");
            Console.WriteLine("Enter the final point: ");
            var b = Convert.ToInt32(Console.ReadLine());
            if (b <= 0 || b > n)
                throw new Exception("Final point is out of range");

            _N = n;
            _A = a;
            _B = b;
            Console.WriteLine("\nCreating a flow matrix");
            _matrix = new int[n, n];
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    if (i == _B - 1 || i >= j)
                    {
                        _matrix[i, j] = 0;
                    }
                    else
                    {
                        Console.Write("[{0}, {1}] = ", i + 1, j + 1);
                        _matrix[i, j] = Convert.ToInt32(Console.ReadLine());
                    }
                }
            }
            Console.WriteLine("Created!");
            char k;
            Console.WriteLine("Save data to the file? [y/n]");
            Console.WriteLine();
            do
            {
                k = Console.ReadKey().KeyChar;
            } while (k != 'y' && k != 'n');
            if (k == 'y')
                SaveFile();
        }
        private void SaveFile()
        {
            Console.WriteLine("Enter a filename: (with .txt)");
            var filename = Console.ReadLine();
            if (!filename.Contains(".txt"))
                throw new Exception("Invalid filename");
            using (StreamWriter fileWriter = new StreamWriter(filename))
            {
                fileWriter.WriteLine(_N.ToString());
                fileWriter.WriteLine(_A.ToString() + " " + _B.ToString());
                for (var i = 0; i < _N; i++)
                {
                    for (var j = 0; j < _N; j++)
                        fileWriter.Write(_matrix[i, j].ToString() + " ");
                    fileWriter.WriteLine();
                }
            }
        }
        public int RandomInput()
        {
            Console.WriteLine("Enter quantity of nodes (N): ");
            var n = Convert.ToInt32(Console.ReadLine());
            if (n <= 0)
                throw new Exception("Not positive quantity");
            Console.WriteLine("Enter the start point: ");
            var a = Convert.ToInt32(Console.ReadLine());
            if (a <= 0 || a > n)
                throw new Exception("Start point is out of range");
            Console.WriteLine("Enter the final point: ");
            var b = Convert.ToInt32(Console.ReadLine());
            if (b <= 0 || b > n)
                throw new Exception("Final point is out of range");


            Console.WriteLine("A max value of flow: ");
            var c = Convert.ToInt32(Console.ReadLine());
            if (c <= 0)
                throw new Exception("Not positive value");
            _N = n;
            _A = a;
            _B = b;
            _matrix = new int[n, n];
            return c;
        }
        public void ResearchInput(int n, int a, int b)
        {
            if (n <= 0)
                throw new Exception("Not positive quantity");
            if (a <= 0 || a > n)
                throw new Exception("Start point is out of range");
            if (b <= 0 || b > n)
                throw new Exception("Final point is out of range");

            _N = n;
            _A = a;
            _B = b;
            _matrix = new int[n, n];
        }
        public void RandomGenerate(int c)
        {
            var rand = new Random();
            for (var i = 0; i < _N; i++)
            {
                for (var j = 0; j < _N; j++)
                {
                    if (i == _B - 1 || i >= j)
                    {
                        _matrix[i, j] = 0;
                    }
                    else
                        _matrix[i, j] = rand.Next(0, c);
                    _matrix[0, _N - 1] = 0;
                }
            }
            Console.WriteLine("Created!");
        }
        public void ReadMatrix()
        {
            StreamReader streamReader = new StreamReader(_filename, Encoding.UTF8);
            _N = Convert.ToInt32(streamReader.ReadLine());
            var destinationLine = streamReader.ReadLine();
            var _from_to = new List<int>();
            foreach (var x in destinationLine.Trim().Split(' '))
                _from_to.Add(Convert.ToInt32(x));
            _A = _from_to[0];
            _B = _from_to[1];

            var buffer = streamReader.ReadToEnd();
            _matrix = new int[_N, _N];
            Fill();
            var i = 0;
            foreach (var row in buffer.Split('\n'))
            {
                var j = 0;
                foreach (var col in row.Trim().Split(' '))
                {
                    _matrix[i, j] = Convert.ToInt32(col.Trim());
                    j++;
                }
                i++;
            }
            streamReader.Close();
        }
        public void WriteMatrix(int[,] A)
        {
            for (var i = 0; i < _N; i++)
            {
                for (var j = 0; j < _N; j++)
                    Console.Write(A[i, j].ToString() + " ");
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public void WriteFlowMatrixToConsole(int[,] A, int[,] B)
        {
            for (var i = 0; i < _N; i++)
            {
                for (var j = 0; j < _N; j++)
                {
                    if (A[i, j] == 0)
                        Console.Write("(---) ");
                    else
                        Console.Write("(" + B[i, j].ToString() + "/" + A[i, j].ToString() + ") ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public void WriteFlowMatrixToFile(int[,] A, int[,] B, StreamWriter fileWriter)
        {
            for (var i = 0; i < _N; i++)
            {
                for (var j = 0; j < _N; j++)
                {
                    if (A[i, j] == 0)
                        fileWriter.Write("(---) ");
                    else
                        fileWriter.Write("(" + B[i, j].ToString() + "/" + A[i, j].ToString() + ") ");
                }
                fileWriter.WriteLine();
            }
            fileWriter.WriteLine();

        }
        public void WriteListToConsole(List<int> list, int cost)
        {
            foreach (var x in list)
                Console.Write((x + 1).ToString() + " ");
            Console.WriteLine("\n Cost = " + cost.ToString());
        }
        public void WriteListToFile(List<int> list, int cost, StreamWriter fileWriter)
        {
            foreach (var x in list)
                fileWriter.Write((x + 1).ToString() + " ");
            fileWriter.WriteLine("\n Cost = " + cost.ToString());
        }
        public StreamWriter GetFileStream()
        {
            Console.WriteLine("\nEnter a filename: (with .txt)");
            var filename = Console.ReadLine();
            if (!filename.Contains(".txt"))
                throw new Exception("Invalid filename");
            StreamWriter fileWriter = new StreamWriter(filename);
            return fileWriter;

        }
        public void Fill()
        {
            for (var i = 0; i < _N; i++)
                for (var j = 0; j < _N; j++)
                    _matrix[i, j] = 0;
        }
        public int[,] GetNullMatrix()
        {
            var m = new int[_N, _N];
            for (int i = 0; i < _N; i++)
            {
                for (int j = 0; j < _N; j++)
                    m[i, j] = 0;
            }
            return m;
        }
        public void WriteTimeToFile(List<double> time, string filename)
        {
            filename = $"{filename}_{DateTime.Now.Day}{DateTime.Now.Month}{DateTime.Now.Year}_{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}.txt";
            using (StreamWriter fileWriter = new StreamWriter(filename))
            {
                for (var i = 0; i < time.Count; i++)
                {
                    fileWriter.WriteLine(time[i].ToString());
                }
            }
        }
        public void WriteResearch(List<Average> time)
        {
            string filename = $"Research_{DateTime.Now.Day}{DateTime.Now.Month}{DateTime.Now.Year}_{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}.txt";
            using (StreamWriter fileWriter = new StreamWriter(filename))
            {
                for (var i = 0; i < time.Count; i++)
                {
                    fileWriter.WriteLine($"{time[i].Size} {time[i].Time[0]} {time[i].Time[1]} {time[i].Time[2]} {time[i].Time[3]}");
                }
            }
        }
    }
}
