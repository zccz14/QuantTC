using System;
using System.Collections.Generic;

namespace QuantTC.Experimental
{
    public interface IModel : INamedConcept
    {
        Type Type { get; }
        IReadOnlyList<IParameter> Parameters { get; }
        IReadOnlyList<IConstraint> Constraints { get; }
        IReadOnlyList<IObjective> Objectives { get; }
        double Precision { get; }
    }
}