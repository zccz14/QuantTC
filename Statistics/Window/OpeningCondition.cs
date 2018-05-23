using System;

namespace QuantTC.Statistics.Window
{
    /// <inheritdoc />
    public class OpeningCondition : IOpeningCondition
    {
        /// <inheritdoc />
        public OpeningCondition(string title, Func<int, bool> predicate)
        {
            Name = title;
            Predicate = predicate;
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public bool Query(int index) => Predicate(index);

        /// <inheritdoc />
        public override string ToString() => Name;

        private Func<int, bool> Predicate { get; }
    }
}