using System;
using System.Collections;
using System.Collections.Generic;

namespace QuantTC.Collections
{
    /// <inheritdoc cref="IReadOnlyList{T}" />
    /// <summary>
    /// Arithmetic Progression with short strong-typed.
    /// </summary>
    public class Int16ArithmeticProgression : IReadOnlyList<short>, IReadOnlyList<object>
    {
        /// <summary>
        /// Constructs Arithmetic Progression with Int16 strong-typed
        /// </summary>
        /// <param name="lower">Lower Bound (Inclusive)</param>
        /// <param name="upper">Upper Bound (Exclusive)</param>
        /// <param name="step">Step Length</param>
        public Int16ArithmeticProgression(short lower, short upper, short step)
        {
            Lower = lower;
            Upper = upper;
            Step = step;
            Count = (Upper - Lower - 1) / Step + 1;
        }

        /// <summary>
        /// Lower Bound (Inclusive)
        /// </summary>
        public short Lower { get; }

        /// <summary>
        /// Upper Bound (Exclusive)
        /// </summary>
        public short Upper { get; }

        /// <summary>
        /// Step Length
        /// </summary>
        public short Step { get; }

        IEnumerator<object> IEnumerable<object>.GetEnumerator()
        {
            for (var i = Lower; i < Upper; i += Step) yield return i;
        }

        /// <inheritdoc />
        public IEnumerator<short> GetEnumerator()
        {
            for (var i = Lower; i < Upper; i += Step) yield return i;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc cref="IReadOnlyList{T}" />
        public int Count { get; }

        /// <inheritdoc />
        public short this[int index] => Convert.ToInt16(Lower + index * Step);

        object IReadOnlyList<object>.this[int index] => Lower + index * Step;
    }
}