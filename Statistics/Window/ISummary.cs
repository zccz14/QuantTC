namespace QuantTC.Statistics.Window
{
    /// <inheritdoc />
    public interface ISummary : INamedConcept
    {
        /// <summary>
        /// Get into summary
        /// </summary>
        /// <param name="objects"></param>
        /// <returns>result</returns>
        object GetResult(params object[] objects);
    }
}