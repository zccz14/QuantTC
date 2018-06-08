using System;
using System.Collections;
using System.Collections.Generic;

namespace QuantTC.Collections
{
    /// <inheritdoc cref="IReadOnlyList{T}" />
    /// <summary>
    /// Arithmetic Progression with UInt16 strong-typed.
    /// </summary>
    public class UInt16ArithmeticProgression : IReadOnlyList<ushort>, IReadOnlyList<object>
    {
        /// <summary>
        /// Constructs Arithmetic Progression with UInt16 strong-typed
        /// </summary>
        /// <param name="lower">Lower Bound (Inclusive)</param>
        /// <param name="upper">Upper Bound (Exclusive)</param>
        /// <param name="step">Step Length</param>
        public UInt16ArithmeticProgression(ushort lower, ushort upper, ushort step)
        {
            Lower = lower;
            Upper = upper;
            Step = step;
            Count = (Upper - Lower - 1) / Step + 1;
        }

        /// <summary>
        /// Lower Bound (Inclusive)
        /// </summary>
        public ushort Lower { get; }

        /// <summary>
        /// Upper Bound (Exclusive)
        /// </summary>
        public ushort Upper { get; }

        /// <summary>
        /// Step Length
        /// </summary>
        public ushort Step { get; }

        IEnumerator<object> IEnumerable<object>.GetEnumerator()
        {
            for (var i = Lower; i < Upper; i += Step) yield return i;
        }

        /// <inheritdoc />
        public IEnumerator<ushort> GetEnumerator()
        {
            for (var i = Lower; i < Upper; i += Step) yield return i;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc cref="IReadOnlyList{T}" />
        public int Count { get; }

        /// <inheritdoc />
        public ushort this[int index] => Convert.ToUInt16(Lower + index * Step);

        object IReadOnlyList<object>.this[int index] => Lower + index * Step;
    }
}