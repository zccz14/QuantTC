using System;

namespace QuantTC.Statistics.Window
{
    /// <inheritdoc />
    public interface ISubject : INamedConcept
    {
        /// <summary>
        /// Get window into an object
        /// </summary>
        /// <param name="window"></param>
        /// <returns>result</returns>
        object GetResult(Tuple<int, int> window);
    }
}