namespace Algorithms.Auxiliary
{
    public class Node // Dinics
    {
        private int _level;
        private bool _closed;
        public int Level { get => _level; set => _level = value; }
        public bool Closed { get => _closed; set => _closed = value; }

        public Node()
        {
            _level = 999;
            Closed = false;
        }
    }
}
