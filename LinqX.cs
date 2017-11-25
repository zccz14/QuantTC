using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantTC
{
	using static Functions;

	public static class LinqX
	{
		/// <summary>
		/// An alias of foreach
		/// </summary>
		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			foreach (var obj in source)
			{
				action(obj);
			}
		}

		/// <summary>
		/// tuple (element, counter), counter started from 0
		/// </summary>
		public static IEnumerable<Tuple<T, int>> WithCounter<T>(this IEnumerable<T> source)
		{
			var counter = 0;
			foreach (var obj in source)
			{
				yield return Tuple.Create(obj, counter);
				counter++;
			}
		}

		/// <summary>
		/// Filter with counter
		/// </summary>
		public static IEnumerable<Tuple<T, int>> KVWhere<T>(this IEnumerable<T> source, Func<Tuple<T, int>, bool> predicate) =>
			source.WithCounter().Where(predicate);

		/// <summary>
		/// Similar to Where but output its index (counter/position)
		/// </summary>
		public static IEnumerable<int> Find<T>(this IEnumerable<T> source, Func<Tuple<T, int>, bool> predicate) =>
			source.KVWhere(predicate).Select(t => t.Item2);

		/// <summary>
		/// get near pairs tuple (prev, next)
		/// </summary>
		/// <remarks>source.Count() == source.NearPairs() + 1</remarks>
		public static IEnumerable<Tuple<T, T>> NearPairs<T>(this IEnumerable<T> source) => source.MovingPairs().Skip(1);

		private static IEnumerable<Tuple<T, T>> MovingPairs<T>(this IEnumerable<T> source)
		{
			var last = default(T);
			foreach (var obj in source)
			{
				yield return Tuple.Create(last, obj);
				last = obj;
			}
		}

		/// <summary>
		/// Left-end range [0, source.Count)
		/// </summary>
		public static IEnumerable<int> FromLeftEnd<T>(this IReadOnlyList<T> source) => Range(0, source.Count);

		/// <summary>
		/// Right-end range [0, source.Count)
		/// </summary>
		public static IEnumerable<int> FromRightEnd<T>(this IReadOnlyList<T> source) => RangeRight(0, source.Count);

		/// <summary>
		/// Fill the range [left, right) in List
		/// </summary>
		/// <typeparam name="T">List Element Type</typeparam>
		/// <param name="source">List Container</param>
		/// <param name="left">Left Index</param>
		/// <param name="right">Right Index</param>
		/// <param name="elementFunc">For each index, returning an instance of type T</param>
		public static void FillRange<T>(this List<T> source, int left, int right, Func<int, T> elementFunc)
		{
			Range(left, right).ForEach(i =>
			{
				if (i < source.Count)
				{
					source[i] = elementFunc(i);
				}
				else
				{
					source.Add(elementFunc(i));
				}
			});
		}

	}
}