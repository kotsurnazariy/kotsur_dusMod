using Algorithms.Auxiliary;
using System.Collections.Generic;

namespace Algorithms.Solvers
{
    public class Solver : Algorithm
    {
        private readonly Mark[] _marks;
        public Solver(int A, int B, int N, int[,] matrix) : base(A, B, N, matrix) 
        {
            _marks = new Mark[_N];
        }
        public void FordFulkerson()
        {
            List<int> deletedVertex = new List<int>();

            int i = _A;
            _marks[i] = new Mark(int.MaxValue, -1);
            do
            {
                List<int> S = new List<int>();
                for (int j = 0; j < _N; j++)
                {
                    if (!deletedVertex.Contains(j) && _matrix[i, j].Difference() > 0 && _marks[j] == null)
                    {
                        S.Add(j);
                    }
                }
                if (S.Count > 0)
                {
                    int max, nextVertex;
                    max = int.MinValue;
                    nextVertex = -1;
                    for (int j = 0; j < S.Count; j++)
                    {
                        if (_matrix[i, S[j]].Difference() > max)
                        {
                            max = _matrix[i, S[j]].Difference();
                            nextVertex = S[j];
                        }
                    }
                    _marks[nextVertex] = new Mark(max, i);
                    i = nextVertex;
                    if (i == _B)
                    {
                        int min = int.MaxValue;
                        for (int j = 0; j < _N; j++)
                        {
                            if (_marks[j] != null)
                            {
                                if (_marks[j].a < min)
                                {
                                    min = _marks[j].a;
                                }
                            }
                        }
                        _F += min;

                        var stack = new Stack<int>();

                        var k = _B;
                        while (k != -1)
                        {
                            stack.Push(k);
                            k = _marks[k].vertexFrom;
                        }

                        PathOutput(stack, _B, min);

                        for (int j = 0; j < _N; j++)
                        {
                            if (_marks[j] != null && j != _A)
                            {
                                _matrix[_marks[j].vertexFrom, j].CurrentUsage += min;
                                _matrix[j, _marks[j].vertexFrom].CurrentUsage -= min;
                            }
                        }

                        for (int j = 1; j < _N; j++)
                        {
                            _marks[j] = null;
                        }
                        deletedVertex = new List<int>();
                        i = _A;
                    }
                }
                else
                {
                    if (i != _A)
                    {
                        deletedVertex.Add(i);
                        i = _marks[i].vertexFrom;

                        _marks[deletedVertex[deletedVertex.Count - 1]] = null;
                    }
                    else
                    {
                        break;
                    }
                }
            } while (true);       
        }
    }
}
