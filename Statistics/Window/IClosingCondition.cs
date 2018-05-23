namespace QuantTC.Statistics.Window
{
    /// <inheritdoc />
    public interface IClosingCondition : INamedConcept
    {
        /// <summary>
        /// Should Close Window started from [start] at [end]?
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        bool Query(int start, int end);
    }
}