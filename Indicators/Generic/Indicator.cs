using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace QuantTC.Indicators.Generic
{
	/// <inheritdoc />
	public abstract class Indicator: IIndicator , ITitle, ITreeView
	{
		/// <inheritdoc />
		public virtual event Action Update;

		/// <summary>
		/// Inform Update Event
		/// </summary>
		protected virtual void FollowUp() => Update?.Invoke();

		/// <summary>
		/// Get All Followers (Listener)
		/// </summary>
		/// <remarks>Some of followers may not be an Indicator.</remarks>
		public IEnumerable<object> Followers => Update?.GetInvocationList().Select(inv => inv.Target) ?? Enumerable.Empty<object>();

		public string Title { get; set; }

	    public IEnumerable<ITreeView> GetNexts() =>
	        Update?.GetInvocationList().Select(x => x.Target).OfType<ITreeView>() ?? Enumerable.Empty<ITreeView>();

	    public override string ToString()
		{
			return Title ?? $"[Anonymous]{base.ToString()}";
		}
	}

	/// <inheritdoc cref="IIndicator{T}" />
	/// <summary>
	/// Generic Implemented Indicator
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Indicator<T> : Indicator, IIndicator<T>
	{
		/// <inheritdoc />
		public IEnumerator<T> GetEnumerator() => Data.GetEnumerator();

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		/// <inheritdoc />
		public int Count => Data.Count;

		/// <inheritdoc />
		public T this[int index] => Data[index];
		/// <summary>
		/// Data Container
		/// </summary>
		protected List<T> Data { get; } = new List<T>();
	}
	/// <summary>
	/// Indicator Extension
	/// </summary>
	public static partial class IndicatorX
	{
		/// <summary>
		/// Get Following Indicators
		/// </summary>
		/// <param name="This"></param>
		/// <returns></returns>
		public static IEnumerable<Indicator> FollowingIndicators(this Indicator This) => This.Followers.OfType<Indicator>();
		/// <summary>
		/// Indicate the impaction of this indicator
		/// </summary>
		/// <param name="This"></param>
		/// <returns>the count of the following invocations</returns>
		public static int Impaction(this Indicator This) => This.FollowingIndicators().Sum(i => i.Impaction()) + This.Followers.Count(v => !(v is Indicator)) + 1;
	}
}