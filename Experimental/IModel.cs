using System;
using System.Collections.Generic;

namespace QuantTC.Experimental
{
    /// <inheritdoc />
    /// <summary>
    /// Meta Information of Model
    /// </summary>
    public interface IModel : INamedConcept
    {
        /// <summary>
        /// Type of the referenced model
        /// </summary>
        Type Type { get; }
        /// <summary>
        /// List of Meta of Parameters
        /// </summary>
        IReadOnlyList<IParameter> Parameters { get; }
        /// <summary>
        /// List of Meta of Constraints
        /// </summary>
        IReadOnlyList<IConstraint> Constraints { get; }
        /// <summary>
        /// List of Meta of Objectives
        /// </summary>
        IReadOnlyList<IObjective> Objectives { get; }
    }
}