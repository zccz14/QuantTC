using System;

namespace QuantTC.Experimental
{
    public interface IParameter : INamedConcept
    {
        Type Type { get; }
        IDomain Domain { get; }
        IIteratorList Values { get; }
        int Priority { get; }
        object GetValue(object obj);
        void SetValue(object obj, object value);

        IModel Model { get; }
    }
}