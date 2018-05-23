using System;
using System.Collections;
using System.Collections.Generic;

namespace QuantTC.Experimental
{
    public class DoubleIteratorList : IReadOnlyList<double>, IIteratorList
    {
        public DoubleIteratorList(double lower, double upper, double step)
        {
            Lower = lower;
            Upper = upper;
            Step = step;
        }

        public double Lower { get; }
        public double Upper { get; }
        public double Step { get; }

        public IEnumerator<double> GetEnumerator()
        {
            for (var i = Lower; i < Upper; i += Step) yield return i;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count => Convert.ToInt32(Math.Ceiling((Upper - Lower) / Step));

        public long Size => Convert.ToInt64(Math.Ceiling((Upper - Lower) / Step));
        public object GetValue(int index) => Lower + index * Step;

        public double this[int index] => Lower + index * Step;
    }
}