namespace QuantTC.Experimental
{
    public class OptimizationSolution
    {
        public object[] Arguments { get; set; }
        public double[] Objectives { get; set; }
        public double Score { get; set; } = double.NaN;
    }
}