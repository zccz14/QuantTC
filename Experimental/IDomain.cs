using System;
using System.IO;
using System.Reflection;

namespace QuantTC.Experimental
{
    public interface IDomain
    {
        string Description { get; }
        IComparable Upper { get; }
        IComparable Lower { get; }
        double SizeFactor { get; }
        bool IsValid(object obj);
    }
}