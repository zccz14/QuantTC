using System.Collections.Generic;

namespace QuantTC.Experimental
{
    public class OptimizationResult
    {
        public double BestScore { get; set; } = double.NaN;
        public IReadOnlyCollection<OptimizationSolution> Solutions { get; set; }
    }
}