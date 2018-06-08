using System;
using System.Collections;
using System.Collections.Generic;

namespace QuantTC.Experimental
{
    [Obsolete]
    public class BoolIteratorList : IReadOnlyList<bool>, IReadOnlyList<object>
    {
        private List<bool> Values { get; } = new List<bool>(2);

        public BoolIteratorList(bool enableFalse, bool enableTrue)
        {
            if (enableFalse) Values.Add(false);
            if (enableTrue) Values.Add(true);
        }

        IEnumerator<object> IEnumerable<object>.GetEnumerator()
        {
            foreach (var value in Values)
            {
                yield return value;
            }
        }

        /// <inheritdoc />
        public IEnumerator<bool> GetEnumerator() => Values.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc cref="IReadOnlyList{T}" />
        public int Count => Values.Count;

        /// <inheritdoc />
        public bool this[int index] => Values[index];

        /// <inheritdoc />
        public int Size => Values.Count;

        /// <inheritdoc />
        public object GetValue(int index) => Values[index];

        object IReadOnlyList<object>.this[int index] => Values[index];
    }
}