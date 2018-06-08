using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using QuantTC.Backtesting;
using QuantTC.Meta;

namespace QuantTC.Experimental
{
    public interface ITradingModel : IModel
    {
        void OnInitialize();

        void OnTestBegan();

        void OnTestEnded();

        void OnSetArguments(Array arguments);

        void OnSimulate();

        void OnEvaluateBegan();

        void OnEvaluateEnded();

        IReadOnlyList<ITradeRecord> TradeRecords { get; }
    }

    public interface ITradeBroker
    {

    }

    public static partial class X
    {
        public static IEvaluationResult Evaluate(this ITradingModel model, Array arguments)
        {
            model.OnInitialize();
            var tests = model.Constraints.Select(c => c.Test(arguments)).ToArray();
            var isFeasible = tests.All(t => t);
            if (!isFeasible)
            {
                return new EvaluationResult
                {
                    IsFeasible = false,
                    Feasibilities = tests,
                    Objectives = null
                };
            }
            model.OnSetArguments(arguments);
            model.OnSimulate();
            model.OnEvaluateEnded();
            var objectives = model.Objectives.Select(o => o.Evaluate(arguments)).ToArray();
            return new EvaluationResult
            {
                IsFeasible = true,
                Feasibilities = tests,
                Objectives = objectives
            };
        }
    }
}
