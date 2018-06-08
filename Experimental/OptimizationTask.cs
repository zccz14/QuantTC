using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using QuantTC.Meta;

namespace QuantTC.Experimental
{
    /// <summary>
    /// The base class of optimizer
    /// </summary>
    public class OptimizationTask
    {
        public OptimizationTask(IModel model)
        {
            Model = model;
        }

        private IModel Model { get; }
        private ICollection<OptimizationSolution> Solutions { get; }
        private Func<Array, double> AggregateFunc { get; }
        public double BestScore { get; private set; }
        public OptimizationSolution BestSolution { get; private set; }

        public void Optimize()
        {
            var point = new int[] {1, 2};
            var args = Model.GetArguments(point);
            var res = Model.Evaluate(args);
            if (res.IsFeasible)
            {
                var score = AggregateFunc(res.Objectives);
                if (BestScore < score)
                {
                    BestScore = score;
                    BestSolution = new OptimizationSolution
                    {
                        Arguments = args,
                        Objectives = res.Objectives,
                        Score = score
                    };
                }
            }
        }

        public void Start()
        {

        }

        public void Stop()
        {

        }

        public void Pause()
        {

        }

        public void Resume()
        {

        }
    }
}
