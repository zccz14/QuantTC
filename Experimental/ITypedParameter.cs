using System;
using QuantTC.Meta;

namespace QuantTC.Experimental
{
    /// <inheritdoc />
    /// <summary>
    /// Meta Information of TypedParameter
    /// </summary>
    [Obsolete]
    public interface ITypedParameter : IParameter
    {
        IDomain Domain { get; }
        int Priority { get; }

        object GetValue(object obj);

        void SetValue(object obj, object value);
        /// <summary>
        /// Backwards Reference
        /// </summary>
        ITypedModel TypedModel { get; }
    }
}