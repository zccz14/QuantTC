using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace QuantTC
{
	public static class Functions
	{
		/// <summary>
		/// Take the range of [left, right)
		/// </summary>
		public static IEnumerable<int> Range(int left, int right)
		{
			for (var i = left; i < right; i++)
			{
				yield return i;
			}
		}

		/// <summary>
		/// Take the range of [left, right) from the right-end
		/// </summary>
		public static IEnumerable<int> RangeRight(int left, int right)
		{
			for (var i = right - 1; i >= left; i--)
			{
				yield return i;
			}
		}

		public static IEnumerable<Tuple<T, int>> Range<T>(this IReadOnlyList<T> source, int left, int right) =>
			Range(left, right).Select(i => Tuple.Create(source[i], i));

		/// <summary>
		/// Judge if a value tuple become greater than another
		/// </summary>
//		public static bool IsUpX(this (double, double) subject, (double, double) @object) =>
//			subject.Item1 <= @object.Item1 && subject.Item2 > @object.Item2;

		/// <summary>
		/// Judge if a value tuple become greater than another
		/// </summary>
		public static bool IsUpX(this Tuple<double, double> subject, Tuple<double, double> @object) =>
			subject.Item1 <= @object.Item1 && subject.Item2 > @object.Item2;

		/// <summary>
		/// Judge if a value tuple become greater than a contain value
		/// </summary>
//		public static bool IsUpX(this (double, double) subject, double value) =>
//			subject.Item1 <= value && subject.Item2 > value;

		/// <summary>
		/// Judge if a value tuple become greater than a contain value
		/// </summary>
		public static bool IsUpX(this Tuple<double, double> subject, double value) =>
			subject.Item1 <= value && subject.Item2 > value;

		/// <summary>
		/// Judge if a value tuple become less than another
		/// </summary>
//		public static bool IsDownX(this (double, double) subject, (double, double) @object) =>
//			subject.Item1 >= @object.Item1 && subject.Item2 < @object.Item2;

		/// <summary>
		/// Judge if a value tuple become less than another
		/// </summary>
		public static bool IsDownX(this Tuple<double, double> subject, Tuple<double, double> @object) =>
			subject.Item1 >= @object.Item1 && subject.Item2 < @object.Item2;

		/// <summary>
		/// Judge if a value tuple become less than a contain value
		/// </summary>
//		public static bool IsDownX(this (double, double) subject, double value) =>
//			subject.Item1 >= value && subject.Item2 < value;

		/// <summary>
		/// Judge if a value tuple become less than a contain value
		/// </summary>
		public static bool IsDownX(this Tuple<double, double> subject, double value) =>
			subject.Item1 >= value && subject.Item2 < value;

		/// <summary>
		/// Judge if a value is same signed with another
		/// </summary>
		public static bool IsSameSigned(this double subject, double @object) =>
			subject >= 0 && @object >= 0 || subject < 0 && @object < 0;

		/// <summary>
		/// Judge if a value is different signed with another
		/// </summary>
		public static bool IsDiffSigned(this double subject, double @object) =>
			subject >= 0 && @object < 0 || subject < 0 && @object >= 0;

		/// <summary>
		/// Action To Func
		/// </summary>
		/// <typeparam name="T">class or ref type</typeparam>
		/// <returns>The Function returning the origin object</returns>
		public static Func<T, T> ToFunc<T>(this Action<T> action) => t =>
		{
			action(t);
			return t;
		};
		public static Tuple<T1, T2> VT<T1, T2>(T1 t1, T2 t2) => Tuple.Create(t1, t2);

		public static double Abs(this double v) => Math.Abs(v);
		/// <summary>
		/// Judge if a value is between l and u : [l, u]
		/// </summary>
		/// <param name="v"></param>
		/// <param name="l">Lower Bound</param>
		/// <param name="u">Upper Bound</param>
		/// <returns></returns>
		public static bool IsBetween(this double v, double l, double u) => l <= v && v <= u;
	}
}