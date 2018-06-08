using System;

namespace QuantTC.Meta.Constraints
{
    /// <inheritdoc />
    /// <summary>
    /// Tests if the length of Parameters equals to the length of arguments
    /// </summary>
    public class ParameterLengthConstraint : IConstraint
    {
        /// <summary>
        /// Tests if the length of Parameters equals to the length of arguments
        /// </summary>
        /// <param name="model"></param>
        public ParameterLengthConstraint(IModel model)
        {
            Model = model;
        }

        /// <inheritdoc />
        public string Name => "Parameter Length Constraint";

        /// <inheritdoc />
        public bool Test(Array arguments) => Model.Parameters.Count == arguments.Length;

        /// <inheritdoc />
        public IModel Model { get; }
    }

}