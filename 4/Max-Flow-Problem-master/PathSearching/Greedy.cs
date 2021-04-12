using System.Collections.Generic;

namespace PathSearching
{
    public class Greedy : PathSearch //used in Ford-Fulkerson algorithm
    {
        public Greedy(int[,] matrix, int n, int from, int to) : base(matrix, n, from, to) { }
        public (int cost, List<int> way) GreedyAlgorithm()
        {
            int i = _from;
            int[] d = new int[_N];
            List<int>[] p = new List<int>[_N];
            for (int j = 0; j < _N; j++)
            {
                p[j] = new List<int>();
            }
            List<int> u = new List<int>();
            d[i] = 0;
            for (int j = 0; j < _N; j++)
            {
                if (j != i)
                {
                    d[j] = int.MinValue;
                }
            }
            while (i != _to)
            {
                u.Add(i);
                int nextVertex = -1;
                List<int> S = new List<int>();
                for (int j = 0; j < _N; j++)
                {
                    if (_matrix[i, j] > 0)
                    {
                        S.Add(j);
                    }
                }
                bool thereIsWay = false;
                if (S.Count > 0)
                {
                    for (int j = 0; j < S.Count; j++)
                    {
                        if (d[S[j]] < d[i] + _matrix[i, S[j]])
                        {
                            thereIsWay = true;
                            d[S[j]] = d[i] + _matrix[i, S[j]];
                            p[S[j]].Clear();
                            foreach (int vertex in p[i])
                            {
                                p[S[j]].Add(vertex);
                            }
                            p[S[j]].Add(i);
                            if (u.Contains(S[j]))
                            {
                                u.Remove(S[j]);
                            }
                        }
                    }
                    if (thereIsWay)
                    {
                        int max = int.MinValue;
                        for (int j = 0; j < S.Count; j++)
                        {
                            if (d[S[j]] > max && !u.Contains(S[j]))
                            {
                                max = d[S[j]];
                                nextVertex = S[j];
                            }
                        }
                    }
                }
                if (!thereIsWay)
                {
                    int max = int.MinValue;
                    for (int k = 0; k < _N; k++)
                    {
                        if (d[k] > max && !u.Contains(k))
                        {
                            max = d[k];
                            nextVertex = k;
                        }
                    }
                }
                if (nextVertex == -1)
                {
                    d[_to] = 0;
                    p[_to] = null;
                    break;
                }
                i = nextVertex;
            }
            p[_to].Add(_to);
            return (d[_to], p[_to]);
        }
    }
}
