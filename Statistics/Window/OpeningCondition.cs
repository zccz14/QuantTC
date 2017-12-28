using System;

namespace QuantTC.Statistics.Window
{
    /// <inheritdoc />
    public class OpeningCondition : IOpeningCondition
    {
        /// <inheritdoc />
        public OpeningCondition(string title, Func<int, bool> predicate)
        {
            Title = title;
            Predicate = predicate;
        }

        /// <inheritdoc />
        public string Title { get; }

        /// <inheritdoc />
        public bool Query(int index) => Predicate(index);

        /// <inheritdoc />
        public override string ToString() => Title;

        private Func<int, bool> Predicate { get; }
    }
}