using System;
using QuantTC.Meta;

namespace QuantTC.Experimental
{
    /// <summary>
    /// Meta Information of Constraint
    /// </summary>
    [Obsolete]
    public interface ITypedConstraint : IConstraint
    {
        string Description { get; }
        int Priority { get; }
        /// <summary>
        /// Test the active typedModel
        /// </summary>
        /// <param name="activeModel">Active Model</param>
        /// <returns></returns>
        bool Test(object activeModel);
    }
}