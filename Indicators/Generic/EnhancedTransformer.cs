using System;

namespace QuantTC.Indicators.Generic
{
    /// <inheritdoc />
    /// <summary>
    /// Enhanced Transformer: Map
    /// </summary>
    /// <typeparam name="T1">From Type T1</typeparam>
    /// <typeparam name="T2">To Type T2</typeparam>
    public class EnhancedTransformer<T1, T2> : Indicator<T2>
    {
        /// <summary>
        /// Construct a transformer
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="func">map function</param>
        public EnhancedTransformer(IIndicator<T1> source, Func<T1, int, T2> func)
        {
            Source = source;
            Func = func;
            Source.Update += Source_Update;
        }

        private void Source_Update()
        {
            Data.FillRange(Count, Source.Count, i => Func(Source[i], i));
            FollowUp();
        }

        private IIndicator<T1> Source { get; }
        private Func<T1, int, T2> Func { get; }
    }
}