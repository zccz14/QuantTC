using System;

namespace QuantTC.Experimental
{
    /// <inheritdoc />
    /// <summary>
    /// Meta Information of Parameter
    /// </summary>
    public interface IParameter : INamedConcept
    {
        /// <summary>
        /// Type of Parameter
        /// </summary>
        Type Type { get; }
        IDomain Domain { get; }
        /// <summary>
        /// Values in the domain
        /// </summary>
        IIteratorList Values { get; }

        int Priority { get; }

        object GetValue(object obj);

        void SetValue(object obj, object value);
        /// <summary>
        /// Backwards Reference
        /// </summary>
        IModel Model { get; }
    }
}