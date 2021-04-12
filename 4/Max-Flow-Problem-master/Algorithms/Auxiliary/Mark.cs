namespace Algorithms.Auxiliary
{
    public class Mark // Ford-Fulkerson
    {
        public int a { get; }
        public int vertexFrom { get; }
        public Mark(int a, int vertexFrom)
        {
            this.a = a;
            this.vertexFrom = vertexFrom;
        }
        //public override string ToString() => $"[{a}, {vertexFrom}]";
    }
}
