using System;
using System.Collections;
using System.Collections.Generic;

namespace QuantTC.Collections
{
    /// <inheritdoc cref="IReadOnlyList{T}" />
    /// <summary>
    /// Arithmetic Progression with float strong-typed
    /// </summary>
    public class FloatArithmeticProgression : IReadOnlyList<float>, IReadOnlyList<object>
    {
        /// <summary>
        /// Constructs Arithmetic Progression with float strong-typed
        /// </summary>
        /// <param name="lower">Lower Bound (Inclusive)</param>
        /// <param name="upper">Upper Bound (Exclusive)</param>
        /// <param name="step">Step Length</param>
        public FloatArithmeticProgression(float lower, float upper, float step)
        {
            Lower = lower;
            Upper = upper;
            Step = step;
            Count = Convert.ToInt32(Math.Ceiling((Upper - Lower) / Step));
        }

        /// <summary>
        /// Lower Bound (Inclusive)
        /// </summary>
        public float Lower { get; }

        /// <summary>
        /// Upper Bound (Exclusive)
        /// </summary>
        public float Upper { get; }

        /// <summary>
        /// Step Length
        /// </summary>
        public float Step { get; }

        /// <inheritdoc />
        public IEnumerator<float> GetEnumerator()
        {
            for (var i = Lower; i < Upper; i += Step) yield return i;
        }

        IEnumerator<object> IEnumerable<object>.GetEnumerator()
        {
            for (var i = Lower; i < Upper; i += Step) yield return i;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc cref="IReadOnlyList{T}" />
        public int Count { get; }

        /// <inheritdoc />
        public float this[int index] => Lower + index * Step;

        object IReadOnlyList<object>.this[int index] => Lower + index * Step;
    }
}