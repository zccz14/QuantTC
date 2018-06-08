using System;
using System.Collections.Generic;

namespace QuantTC.Experimental
{
    public class OptimizationSolution
    {
        public Array Arguments { get; set; }
        public IReadOnlyList<double> Objectives { get; set; }
        public double Score { get; set; } = double.NaN;
    }
}