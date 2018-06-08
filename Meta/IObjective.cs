using System;

namespace QuantTC.Meta
{
    /// <inheritdoc />
    /// <summary>
    /// Meta information of Objective
    /// </summary>
    public interface IObjective : INamedConcept
    {
        /// <summary>
        /// Evaluates the arguments
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        double Evaluate(Array arguments);

        /// <summary>
        /// Gets the referenced model
        /// </summary>
        IModel Model { get; }
    }
}