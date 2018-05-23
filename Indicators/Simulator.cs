using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QuantTC.Data;
using QuantTC.Indicators.Generic;

namespace QuantTC.Indicators
{
    /// <inheritdoc />
    public class Simulator : IIndicator<IBarPrice>, ITreeView
    {
        /// <inheritdoc />
        public Simulator(IIndicator<IBarPrice> source)
        {
            Source = source;
            Source.Update += Resume;
        }

        /// <inheritdoc />
        public event Action Update;

        /// <inheritdoc />
        public IEnumerator<IBarPrice> GetEnumerator() => Source.Take(Count).GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public int Count { get; private set; }

        /// <summary>
        /// The Last (Current) Index = Count - 1
        /// </summary>
        public int Index => Count - 1;

        /// <inheritdoc />
        public IBarPrice this[int index] => Source[index];

        private IIndicator<IBarPrice> Source { get; }

        /// <summary>
        /// Replay Source
        /// </summary>
        public void Replay()
        {
            Count = 0;
            Resume();
        }

        /// <summary>
        /// Resume (Continue when source updated)
        /// </summary>
        public void Resume()
        {
            QuantTC.X.Range(Count, Source.Count).ForEach(i =>
            {
                Count = i + 1;
                Update?.Invoke();
            });
        }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public IEnumerable<ITreeView> GetNexts() =>
            Update?.GetInvocationList().Select(x => x.Target).OfType<ITreeView>() ?? Enumerable.Empty<ITreeView>();
    }
}