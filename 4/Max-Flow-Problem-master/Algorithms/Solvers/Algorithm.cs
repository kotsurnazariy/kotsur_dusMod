using Algorithms.Auxiliary;
using System;
using System.Collections.Generic;

namespace Algorithms.Solvers
{
    public abstract class Algorithm
    {
        protected int _N;
        protected int _A;
        protected int _B;
        protected int _F;
        public int F { get => _F; }
        protected Matrix _matrix;
        public Matrix FlowMatrix { get => _matrix; }
        public Algorithm(int A, int B, int N, int[,] matrix)
        {
            _N = N;
            _A = A;
            _B = B;
            _F = 0;
            _matrix = new Matrix(matrix, _N);
        }
        protected void PathOutput(IEnumerable<int> path, int B, int f)
        {
            foreach (var x in path)
            {
                if (x == B)
                    Console.Write((x + 1).ToString());
                else
                    Console.Write((x + 1).ToString() + " --> ");
            }
            Console.Write("| f = " + f.ToString());
            Console.WriteLine();
        }
    }
}
