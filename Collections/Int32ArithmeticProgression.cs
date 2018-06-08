using System.Collections;
using System.Collections.Generic;

namespace QuantTC.Collections
{
    /// <inheritdoc cref="IReadOnlyList{T}" />
    /// <summary>
    /// Arithmetic Progression with Int32 strong-typed.
    /// </summary>
    public class Int32ArithmeticProgression : IReadOnlyList<int>, IReadOnlyList<object>
    {
        /// <summary>
        /// Constructs Arithmetic Progression with int strong-typed
        /// </summary>
        /// <param name="lower">Lower Bound (Inclusive)</param>
        /// <param name="upper">Upper Bound (Exclusive)</param>
        /// <param name="step">Step Length</param>
        public Int32ArithmeticProgression(int lower, int upper, int step)
        {
            Lower = lower;
            Upper = upper;
            Step = step;
            Count = (Upper - Lower - 1) / Step + 1;
        }

        /// <summary>
        /// Lower Bound (Inclusive)
        /// </summary>
        public int Lower { get; }

        /// <summary>
        /// Upper Bound (Exclusive)
        /// </summary>
        public int Upper { get; }

        /// <summary>
        /// Step Length
        /// </summary>
        public int Step { get; }

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

        /// <inheritdoc cref="IReadOnlyList{T}" />
        public int Count { get; }

        /// <inheritdoc />
        public int this[int index] => Lower + index * Step;

        object IReadOnlyList<object>.this[int index] => Lower + index * Step;
    }
}