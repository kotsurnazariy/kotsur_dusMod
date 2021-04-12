namespace ConsoleApp.Experiment.Auxiliary
{
    public class Average
    {
        public int Size { get; }
        public double[] Time { get; }
        public Average(int size, double[] time)
        {
            Size = size;
            Time = time;
        }
    }
}
