using System;
using System.Collections.Generic;
using QuantTC.Meta;

namespace QuantTC.Experimental
{
    /// <summary>
    /// Meta Information of Model
    /// </summary>
    public interface ITypedModel : INamedConcept, IModel
    {
        /// <summary>
        /// Type of the referenced typedModel
        /// </summary>
        Type Type { get; }

        IReadOnlyList<ITypedParameter> TypedParameters { get; } 
    }
}