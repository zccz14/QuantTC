using System;

namespace QuantTC.Indicators.Generic
{
    /// <inheritdoc />
    /// <summary>
    /// Apply a binary operation to (double, double) and get results
    /// </summary>
    public class BinaryOperation<T1, T2, T3> : Indicator<T3>
    {
        /// <inheritdoc />
        public BinaryOperation(IIndicator<T1> source1, IIndicator<T2> source2, Func<T1, T2, T3> @operator)
        {
            Source1 = source1;
            Source2 = source2;
            Operator = @operator;
            Source1.Update += Calc;
            Source2.Update += Calc;
            Title = $"Op({source1}, {source2})";
        }

        private void Calc()
        {
            Data.FillRange(Count, Math.Min(Source1.Count, Source2.Count), i => Operator(Source1[i], Source2[i]));
            FollowUp();
        }

        public IIndicator<T1> Source1 { get; }
        public IIndicator<T2> Source2 { get; }
        public Func<T1, T2, T3> Operator { get; }
    }
}