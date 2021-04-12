namespace PathSearching
{
    public abstract class PathSearch
    {
        protected int _N;
        protected int[,] _matrix;
        protected int _from;
        protected int _to;

        public PathSearch(int[,] matrix, int n, int from, int to)
        {
            _N = n;
            _matrix = CopyMatrix(matrix);
            _from = from;
            _to = to;
        }
        private int[,] CopyMatrix(int[,] matrix)
        {
            var m = new int[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                    m[i, j] = matrix[i, j];
            }
            return m;
        }
    }
}
