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
                stream.WriteLine($"Result of {Model.Name}:");
                stream.WriteLine($"\tBest Score: {BestScore}");
                stream.WriteLine($"\tBest Solutions: ");
                Solutions.Take(100).ForEach(solution =>
                    stream.WriteLine(
                        $"\t\t{Model.Name}({string.Join(", ", solution.Arguments)}) = [{string.Join(", ", solution.Objectives)}] => ({solution.Score})"));
            }
        }
    }
}