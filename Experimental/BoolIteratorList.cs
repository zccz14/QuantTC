using System.Collections;
using System.Collections.Generic;

namespace QuantTC.Experimental
{
    public class BoolIteratorList : IReadOnlyList<bool>, IIteratorList
    {
        private List<bool> Values { get; } = new List<bool>(2);

        public BoolIteratorList(bool enableFalse, bool enableTrue)
        {
            if (enableFalse) Values.Add(false);
            if (enableTrue) Values.Add(true);
        }

        /// <inheritdoc />
        public IEnumerator<bool> GetEnumerator() => Values.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public int Count => Values.Count;

        /// <inheritdoc />
        public bool this[int index] => Values[index];

        /// <inheritdoc />
        public long Size => Values.Count;

        /// <inheritdoc />
        public object GetValue(int index) => Values[index];
    }
}