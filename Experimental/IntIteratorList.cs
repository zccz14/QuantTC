using System;
using System.Collections;
using System.Collections.Generic;

namespace QuantTC.Experimental
{
    public class IntIteratorList : IReadOnlyList<int>, IIteratorList
    {
        public IntIteratorList(int lower, int upper, int step)
        {
            Lower = lower;
            Upper = upper;
            Step = step;
        }

        public int Lower { get; }
        public int Upper { get; }
        public int Step { get; }

        public IEnumerator<int> GetEnumerator()
        {
            for (var i = Lower; i < Upper; i += Step) yield return i;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count => (Upper - Lower - 1) / Step + 1;

        public long Size => (-1L + Upper - Lower) / Step + 1;
        public object GetValue(int index) => Lower + index * Step;

        public int this[int index] => Lower + index * Step;
    }
}