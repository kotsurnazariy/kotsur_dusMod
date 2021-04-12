namespace Algorithms.Auxiliary
{
    public class Edge // both
    {
        private int _flow;
        private int _currentUsage;
        public int CurrentUsage { get => _currentUsage; set => _currentUsage = value; }
        public int Flow { get => _flow; set => _flow = value; }

        public Edge(int flow)
        {
            Flow = flow;
            _currentUsage = 0;
        }
        public int Difference()
        {
            return Flow - _currentUsage;
        }
    }
}
