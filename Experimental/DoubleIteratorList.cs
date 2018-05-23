using System;
using System.Collections;
using System.Collections.Generic;

namespace QuantTC.Experimental
{
    public class DoubleIteratorList : IReadOnlyList<double>, IReadOnlyList<object>, IIteratorList
    {
        private int _size;

        public DoubleIteratorList(double lower, double upper, double step)
        {
            Lower = lower;
            Upper = upper;
            Step = step;
            _size = Convert.ToInt32(Math.Ceiling((Upper - Lower) / Step));
        }

        public double Lower { get; }
        public double Upper { get; }
        public double Step { get; }

        IEnumerator<object> IEnumerable<object>.GetEnumerator()
        {
            for (var i = Lower; i < Upper; i += Step) yield return i;
        }

        public IEnumerator<double> GetEnumerator()
        {
            for (var i = Lower; i < Upper; i += Step) yield return i;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count => _size;

        public int Size => _size;

        public object GetValue(int index) => Lower + index * Step;

        public double this[int index] => Lower + index * Step;
        object IReadOnlyList<object>.this[int index] => Lower + index * Step;
    }

}