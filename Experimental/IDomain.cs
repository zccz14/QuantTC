using System;

namespace QuantTC.Experimental
{
    [Obsolete]
    public interface IDomain
    {
        string Description { get; }
        IComparable Upper { get; }
        IComparable Lower { get; }
        double SizeFactor { get; }
        bool IsValid(object obj);
    }
}