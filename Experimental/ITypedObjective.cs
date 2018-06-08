using System;
using QuantTC.Meta;

namespace QuantTC.Experimental
{
    /// <inheritdoc />
    /// <summary>
    /// Meta Information of Objective function
    /// </summary>
    [Obsolete]
    public interface ITypedObjective : IObjective
    {
        /// <summary>
        /// Type of Objective Function
        /// </summary>
        Type Type { get; }
//        MethodInfo Method { get; }
        int Priority { get; }
        double Eval(object obj);
    }
}