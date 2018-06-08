using System;
using System.Collections.Generic;
using QuantTC.Experimental;

namespace QuantTC.Meta
{
    public interface IParameter : INamedConcept
    {
        Type Type { get; }
        IReadOnlyList<object> Values { get; }
        IModel Model { get; }
    }
}