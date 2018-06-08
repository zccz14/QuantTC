using System;

namespace QuantTC.Meta.Objectives
{
    /// <inheritdoc />
    /// <summary>
    /// Rosenbrock Function
    /// </summary>
    public class RosenbrockFunc : IObjective
    {
        /// <inheritdoc />
        public IModel Model => null;

        /// <inheritdoc />
        public string Name => "Rosenbrock Function";

        /// <inheritdoc />
        public double Evaluate(Array arguments)
        {
            var x = (double) arguments.GetValue(0);
            var y = (double) arguments.GetValue(1);
            return Math.Pow(1 - x, 2) + 100 * Math.Pow(y - x * x, 2);
        }
    }
}
