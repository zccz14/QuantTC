using System;
using System.Linq;
using QuantTC.Meta;

namespace QuantTC.Experimental
{
    public static partial class X
    {

        /// <summary>
        /// Evaluate the model with arguments
        /// </summary>
        /// <param name="model"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public static IEvaluationResult Evaluate
            (this IModel model, Array arguments)
        {
            // Tests Feasibilities
            var tests = model.Constraints
                .Select(c => c.Test(arguments))
                .ToArray();
            var isFeasible = tests.All(r => r);
            if (!isFeasible)
            {
                return new EvaluationResult
                {
                    IsFeasible = false,
                    Feasibilities = tests,
                    Objectives = null
                };
            }
            // Evaluates Objectives, if feasible
            var objs = model.Objectives
                .Select(o => o.Evaluate(arguments))
                .ToArray();
            return new EvaluationResult
            {
                IsFeasible = true,
                Objectives = objs,
                Feasibilities = tests
            };
        }
    }
}