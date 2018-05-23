using System.Reflection;

namespace QuantTC.Experimental
{
    /// <inheritdoc />
    /// <summary>
    /// Meta Information of Constraint
    /// </summary>
    public interface IConstraint : INamedConcept
    {
        string Description { get; }
        int Priority { get; }
        /// <summary>
        /// Test the active model
        /// </summary>
        /// <param name="activeModel">Active Model</param>
        /// <returns></returns>
        bool Test(object activeModel);
        /// <summary>
        /// Backward Reference to Model
        /// </summary>
        IModel Model { get; }
    }
}