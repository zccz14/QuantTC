using System;

namespace QuantTC.Statistics.Window
{
    /// <inheritdoc />
    public class Subject<T> : ISubject
    {
        /// <inheritdoc />
        public Subject(string title, Func<Tuple<int, int>, T> selector)
        {
            Title = title;
            Selector = selector;
        }

        /// <inheritdoc />
        public string Title { get; }

        /// <inheritdoc />
        public object GetResult(Tuple<int, int> window)
        {
            return Selector(window);
        }

        /// <inheritdoc />
        public override string ToString() => Title;

        private Func<Tuple<int, int>, T> Selector { get; }
    }
}