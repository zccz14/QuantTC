using System;

namespace QuantTC.Statistics.Window
{
    /// <inheritdoc />
    public class ClosingCondition : IClosingCondition
    {
        /// <inheritdoc />
        public ClosingCondition(string title, Func<int, int, bool> predicate)
        {
            Name = title;
            Predicate = predicate;
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public bool Query(int start, int end) => Predicate(start, end);

        /// <inheritdoc />
        public override string ToString() => Name;

        private Func<int, int, bool> Predicate { get; }
    }
}