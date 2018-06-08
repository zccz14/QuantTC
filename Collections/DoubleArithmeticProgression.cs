using System;
using System.Collections;
using System.Collections.Generic;

namespace QuantTC.Collections
{
    /// <inheritdoc cref="IReadOnlyList{T}" />
    /// <summary>
    /// Arithmetic Progression with double strong-typed
    /// </summary>
    public class DoubleArithmeticProgression : IReadOnlyList<double>, IReadOnlyList<object>
    {
        /// <summary>
        /// Constructs Arithmetic Progression with double strong-typed
        /// </summary>
        /// <param name="lower">Lower Bound (Inclusive)</param>
        /// <param name="upper">Upper Bound (Exclusive)</param>
        /// <param name="step">Step Length</param>
        public DoubleArithmeticProgression(double lower, double upper, double step)
        {
            Lower = lower;
            Upper = upper;
            Step = step;
            Count = Convert.ToInt32(Math.Ceiling((Upper - Lower) / Step));
        }

        /// <summary>
        /// Lower Bound (Inclusive)
        /// </summary>
        public double Lower { get; }

        /// <summary>
        /// Upper Bound (Exclusive)
        /// </summary>
        public double Upper { get; }

        /// <summary>
        /// Step Length
        /// </summary>
        public double Step { get; }

        /// <inheritdoc />
        public IEnumerator<double> GetEnumerator()
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
        public double this[int index] => Lower + index * Step;

        object IReadOnlyList<object>.this[int index] => Lower + index * Step;
    }
}