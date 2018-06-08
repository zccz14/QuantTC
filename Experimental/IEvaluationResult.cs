using System.Collections.Generic;

namespace QuantTC.Experimental
{
    /// <summary>
    /// Evaluation Result
    /// </summary>
    public interface IEvaluationResult
    {
        bool IsFeasible { get; }
        double[] Objectives { get; }
        bool[] Feasibilities { get; }
        void Deconstruct(out bool isfeasible, out IReadOnlyList<double> objectives, out IReadOnlyList<bool> feasibilities);
    }

    /// <inheritdoc />
    public class EvaluationResult : IEvaluationResult
    {
        /// <inheritdoc />
        public bool IsFeasible { get; set; }
        /// <inheritdoc />
        public double[] Objectives { get; set; }
        /// <inheritdoc />
        public bool[] Feasibilities { get; set; }

        public void Deconstruct(out bool isfeasible, out IReadOnlyList<double> objectives, out IReadOnlyList<bool> feasibilities)
        {
            isfeasible = IsFeasible;
            objectives = Objectives;
            feasibilities = Feasibilities;
        }
    }
}