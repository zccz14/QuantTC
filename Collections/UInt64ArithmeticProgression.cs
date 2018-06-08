using System;
using System.Collections;
using System.Collections.Generic;

namespace QuantTC.Collections
{
    /// <inheritdoc cref="IReadOnlyList{T}" />
    /// <summary>
    /// Arithmetic Progression with UInt64 strong-typed.
    /// </summary>
    public class UInt64ArithmeticProgression : IReadOnlyList<ulong>, IReadOnlyList<object>
    {
        /// <summary>
        /// Constructs Arithmetic Progression with ulong strong-typed
        /// </summary>
        /// <param name="lower">Lower Bound (Inclusive)</param>
        /// <param name="upper">Upper Bound (Exclusive)</param>
        /// <param name="step">Step Length</param>
        public UInt64ArithmeticProgression(ulong lower, ulong upper, ulong step)
        {
            Lower = lower;
            Upper = upper;
            Step = step;
            Count = Convert.ToInt32((Upper - Lower - 1L) / Step + 1L);
        }

        /// <summary>
        /// Lower Bound (Inclusive)
        /// </summary>
        public ulong Lower { get; }

        /// <summary>
        /// Upper Bound (Exclusive)
        /// </summary>
        public ulong Upper { get; }

        /// <summary>
        /// Step Length
        /// </summary>
        public ulong Step { get; }

        IEnumerator<object> IEnumerable<object>.GetEnumerator()
        {
            for (var i = Lower; i < Upper; i += Step) yield return i;
        }

        /// <inheritdoc />
        public IEnumerator<ulong> GetEnumerator()
        {
            for (var i = Lower; i < Upper; i += Step) yield return i;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc cref="IReadOnlyList{T}" />
        public int Count { get; }

        /// <inheritdoc />
        public ulong this[int index] => Lower + (ulong) index * Step;

        object IReadOnlyList<object>.this[int index] => Lower + (ulong) index * Step;
    }
}