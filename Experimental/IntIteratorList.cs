using System;
using System.Collections;
using System.Collections.Generic;

namespace QuantTC.Experimental
{
    public class IntIteratorList : IReadOnlyList<int>, IReadOnlyList<object>, IIteratorList
    {
        private readonly int _size;

        public IntIteratorList(int lower, int upper, int step)
        {
            Lower = lower;
            Upper = upper;
            Step = step;
            _size = (Upper - Lower - 1) / Step + 1;
        }

        private int Lower { get; }
        private int Upper { get; }
        private int Step { get; }

        IEnumerator<object> IEnumerable<object>.GetEnumerator()
        {
            for (var i = Lower; i < Upper; i += Step) yield return i;
        }

        /// <inheritdoc />
        public IEnumerator<int> GetEnumerator()
        {
            for (var i = Lower; i < Upper; i += Step) yield return i;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public int Count => _size;

        /// <inheritdoc />
        public int Size => _size;

        /// <inheritdoc />
        public object GetValue(int index) => Lower + index * Step;

        /// <inheritdoc />
        public int this[int index] => Lower + index * Step;

        object IReadOnlyList<object>.this[int index] => Lower + index * Step;
    }
}