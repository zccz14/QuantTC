using System;

namespace QuantTC.Meta
{
    /// <inheritdoc />
    /// <summary>
    /// Constraint is anything that can test the feasibility of arguments
    /// </summary>
    public interface IConstraint: INamedConcept
    {
        /// <summary>
        /// Tests the feasibility of arguments.
        /// </summary>
        /// <param name="arguments">Arguments to be tested.</param>
        /// <returns></returns>
        bool Test(Array arguments);
        /// <summary>
        /// Gets the referenced model
        /// </summary>
        IModel Model { get; }
    }
}