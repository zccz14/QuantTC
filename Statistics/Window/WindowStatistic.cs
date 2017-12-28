using System;
using System.Collections.Generic;
using System.Linq;
using static QuantTC.X;

namespace QuantTC.Statistics.Window
{
	/// <summary>
	/// 
	/// </summary>
	public static class WindowStatistic
    {
        public static T Stat<T>(int n, Func<int, bool> open, Func<int, int, bool> close, Func<int, int, T> map, Func<T, T, T> reduce)
        {
            return Windows(n, open, close).Select(t => map(t.Item1, t.Item2))
                .Aggregate(reduce);
        }
        /// <summary>
        /// For range[0, n), open several windows and each contains a start-end tuple: [i, j]
        /// </summary>
        /// <param name="open">open(i): should open a window at 'i'?</param>
        /// <param name="close">close(i, j): should the window started from 'i' be close at 'j'?</param>
        public static IEnumerable<Tuple<int, int>> Windows(int n, Func<int, bool> open, Func<int, int, bool> close)
        {
            return Range(0, n)
                .Where(open)
                .Select(i => Tuple.Create(i, Range(i + 1, n).FirstOrDefault(j => close(i, j))))
                .Where(t => t.Item1 < t.Item2);
        }
        /// <summary>
        /// Reducer: Counter
        /// </summary>
        public static int Counter(int pre, int cur) => pre + 1;

    }
}
