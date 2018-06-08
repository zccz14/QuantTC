using System;
using System.Collections;
using System.Collections.Generic;

namespace QuantTC.Collections
{
    /// <inheritdoc cref="IReadOnlyList{T}" />
    /// <summary>
    /// Arithmetic Progression with Int64 strong-typed.
    /// </summary>
    public class Int64ArithmeticProgression : IReadOnlyList<long>, IReadOnlyList<object>
    {
        /// <summary>
        /// Constructs Arithmetic Progression with long strong-typed
        /// </summary>
        /// <param name="lower">Lower Bound (Inclusive)</param>
        /// <param name="upper">Upper Bound (Exclusive)</param>
        /// <param name="step">Step Length</param>
        public Int64ArithmeticProgression(long lower, long upper, long step)
        {
            Lower = lower;
            Upper = upper;
            Step = step;
            Count = Convert.ToInt32((Upper - Lower - 1L) / Step + 1L);
        }

        /// <summary>
        /// Lower Bound (Inclusive)
        /// </summary>
        public long Lower { get; }

        /// <summary>
        /// Upper Bound (Exclusive)
        /// </summary>
        public long Upper { get; }

        /// <summary>
        /// Step Length
        /// </summary>
        public long Step { get; }

        IEnumerator<object> IEnumerable<object>.GetEnumerator()
        {
            for (var i = Lower; i < Upper; i += Step) yield return i;
        }

        /// <inheritdoc />
        public IEnumerator<long> GetEnumerator()
        {
            for (var i = Lower; i < Upper; i += Step) yield return i;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc cref="IReadOnlyList{T}" />
        public int Count { get; }

        /// <inheritdoc />
        public long this[int index] => Lower + index * Step;

        object IReadOnlyList<object>.this[int index] => Lower + index * Step;
    }
}