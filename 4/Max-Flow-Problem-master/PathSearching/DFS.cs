using System.Collections.Generic;

namespace PathSearching
{
    public class DFS : PathSearch   //used in Dinics algorithm
    {
        private readonly List<bool> _closed = new List<bool>();
        public DFS(int[,] matrix, int n, int from, int to) : base(matrix, n, from, to)
        {
            for (var i = 0; i < _N; i++)
                _closed.Add(false);
        }
        public (int, List<int>) Run()
        {
            var result = new List<int> {_from};
            var found = false;
            var cost = 0;
            DFSAlgorithm(ref result, _from, ref found, ref cost, -1);
            return (cost, result);
        }
        private void DFSAlgorithm(ref List<int> path, int current, ref bool found, ref int cost, int previous)
        {
            if (current == _to)
            {
                found = true;
                return;
            }
            var next = GetNextNodes(current);
            if (next.Count != 0)
            {
                foreach (var x in next)
                {
                    path.Add(x);
                    cost += _matrix[current, x];
                    DFSAlgorithm(ref path, x, ref found, ref cost, current);
                    if (found)
                        break;
                }
                if (!found)
                {
                    path.Remove(current);
                    cost -= _matrix[previous, current];
                }

            }
            else
            {
                path.Remove(current);
                cost -= _matrix[previous, current];
                _closed[current] = true;
            }

        }
        private List<int> GetNextNodes(int current)
        {
            var list = new List<int>();
            for (var i = 0; i < _N; i++)
            {
                if (_matrix[current, i] != 0 && !_closed[i])
                    list.Add(i);
            }
            return list;
        }
    }
}
