using System;

namespace QuantTC.Statistics.Window
{
    /// <inheritdoc />
    public class Summary : ISummary
    {
        /// <inheritdoc />
        public Summary(string title, Func<object[], object> func)
        {
            Name = title;
            Func = func;
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public override string ToString() => Name;

        /// <inheritdoc />
        public object GetResult(params object[] objects) => Func(objects);

        private Func<object[], object> Func { get; }
    }
}