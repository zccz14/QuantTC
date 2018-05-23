using System;
using System.Reflection;

namespace QuantTC.Experimental
{
    /// <inheritdoc />
    /// <summary>
    /// Meta Information of Objective function
    /// </summary>
    public interface IObjective : INamedConcept
    {
        /// <summary>
        /// Type of Objective Function
        /// </summary>
        Type Type { get; }
//        MethodInfo Method { get; }
        int Priority { get; }
        double Eval(object obj);

        /// <summary>
        /// Backward Reference of Model
        /// </summary>
        IModel Model { get; }
    }
}