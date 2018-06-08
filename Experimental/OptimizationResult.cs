using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QuantTC.Experimental
{
    
    public class OptimizationResult
    {
        public double BestScore { get; set; } = double.NaN;
        public IReadOnlyCollection<OptimizationSolution> Solutions { get; set; }
        public ITypedModel TypedModel { get; set; }

        

        public void Report(TextWriter stream)
        {
            lock (stream)
            {
                stream.WriteLine($"Result of {TypedModel.Name}:");
                stream.WriteLine($"\tBest Score: {BestScore}");
                stream.WriteLine($"\tBest Solutions: ");
                Solutions.Take(100).ForEach(solution =>
                    stream.WriteLine(
                        $"\t\t{TypedModel.Name}({string.Join(", ", solution.Arguments)}) = [{string.Join(", ", solution.Objectives)}] => ({solution.Score})"));
            }
        }
    }
}