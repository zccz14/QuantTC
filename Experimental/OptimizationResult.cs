using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QuantTC.Experimental
{
    public class OptimizationResult
    {
        public double BestScore { get; set; } = double.NaN;
        public IReadOnlyCollection<OptimizationSolution> Solutions { get; set; }
        public IModel Model { get; set; }

        

        public void Report(TextWriter stream)
        {
            lock (stream)
            {
                stream.WriteLine($"Optimize {Model}: Best Score {BestScore}");
                Solutions.Take(100).ForEach(solution =>
                    stream.WriteLine(
                        $"{Model.Name}({string.Join(",", solution.Arguments)}) = [{string.Join(",", solution.Objectives)}] => ({solution.Score})"));
            }
        }
    }
}