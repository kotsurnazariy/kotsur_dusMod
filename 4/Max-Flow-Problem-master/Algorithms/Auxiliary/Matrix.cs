namespace Algorithms.Auxiliary
{
    public class Matrix // both
    {
        private readonly Edge[,] _edges;
        private readonly int _N;
        public Edge this[int i, int j]
        {
            get
            {
                return _edges[i, j];
            }
            set
            {
                _edges[i, j] = value;
            }
        }
        public Matrix(int[,] matrix, int N) 
        {
            _N = N;
            _edges = new Edge[_N, _N];
            Fill();
            for(var i = 0; i < _N; i++)
            {
                for(var j = 0; j < _N; j++)
                {
                    if(matrix[i, j] != 0)
                    {
                        _edges[i, j].Flow = matrix[i, j];
                        if (j != _N - 1)
                        {
                            _edges[j, i].Flow = matrix[i, j];
                            _edges[j, i].CurrentUsage = matrix[i, j];
                        }
                    }
                }
            }
            
        }
        private void Fill()
        {
            for (var i = 0; i < _N; i++)
                for (var j = 0; j < _N; j++)
                    _edges[i, j] = new Edge(0);
        }
        public int[,] FlowToIntMatrix()
        {
            var m = new int[_N, _N];
            for(var i = 0; i < _N; i++)
                for (var j = 0; j < _N; j++)
                    m[i, j] = this[i, j].CurrentUsage;
            return m;
        }
        
    }
}
