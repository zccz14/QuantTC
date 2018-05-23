namespace QuantTC.Statistics.Window
{
    /// <inheritdoc />
    public interface IOpeningCondition : INamedConcept
    {
        /// <summary>
        /// Should Open Window at [index]?
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        bool Query(int index);
    }
}