using System;

namespace QuantTC.Experimental
{
    public class Domain : IDomain
    {
        public string Description { get; set; }
        public IComparable Upper { get; set; }
        public IComparable Lower { get; set; }
        public double SizeFactor { get; set; }

        public bool IsValid(object obj) => Lower.CompareTo(obj) <= 0 && Upper.CompareTo(obj) >= 0;
    }
}