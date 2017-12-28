using System.Collections.Generic;

namespace QuantTC.Indicators.Generic
{
    /// <inheritdoc />
    /// <summary>
    /// The Dumb Data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Dumb<T> : Indicator<T>
    {
        /// <summary>
        /// Same as List&lt;T&gt;.Add(T item)
        /// </summary>
        /// <param name="item">item to add</param>
        public void Add(T item)
        {
            Data.Add(item);
        }

        /// <summary>
        /// Add Range to dump
        /// </summary>
        /// <param name="items"></param>
        public void AddRange(IEnumerable<T> items)
        {
            Data.AddRange(items);
        }

        /// <summary>
        /// Refresh Manually
        /// </summary>
        public void Refresh() => FollowUp();
    }
}