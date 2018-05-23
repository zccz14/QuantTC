using System;
using System.Reflection;

namespace QuantTC.Experimental
{
    public interface IObjective : INamedConcept
    {
        Type Type { get; }
        MethodInfo Method { get; }
        int Priority { get; }
        double Eval(object obj);
    }
}