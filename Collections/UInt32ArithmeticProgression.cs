using System;
using System.Collections;
using System.Collections.Generic;

namespace QuantTC.Collections
{
    /// <inheritdoc cref="IReadOnlyList{T}" />
    /// <summary>
    /// Arithmetic Progression with UInt32 strong-typed.
    /// </summary>
    public class UInt32ArithmeticProgression : IReadOnlyList<uint>, IReadOnlyList<object>
    {
        /// <summary>
        /// Constructs Arithmetic Progression with int strong-typed
        /// </summary>
        /// <param name="lower">Lower Bound (Inclusive)</param>
        /// <param name="upper">Upper Bound (Exclusive)</param>
        /// <param name="step">Step Length</param>
        public UInt32ArithmeticProgression(uint lower, uint upper, uint step)
        {
            Lower = lower;
            Upper = upper;
            Step = step;
            Count = Convert.ToInt32((Upper - Lower - 1) / Step + 1);
        }

        /// <summary>
        /// Lower Bound (Inclusive)
        /// </summary>
        public uint Lower { get; }

        /// <summary>
        /// Upper Bound (Exclusive)
        /// </summary>
        public uint Upper { get; }

        /// <summary>
        /// Step Length
        /// </summary>
        public uint Step { get; }

        IEnumerator<object> IEnumerable<object>.GetEnumerator()
        {
            for (var i = Lower; i < Upper; i += Step) yield return i;
        }

        /// <inheritdoc />
        public IEnumerator<uint> GetEnumerator()
        {
            for (var i = Lower; i < Upper; i += Step) yield return i;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc cref="IReadOnlyList{T}" />
        public int Count { get; }

        /// <inheritdoc />
        public uint this[int index] => Convert.ToUInt32(Lower + index * Step);

        object IReadOnlyList<object>.this[int index] => Lower + index * Step;
    }
}