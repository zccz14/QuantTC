namespace QuantTC.Indicators.Generic
{
    /// <inheritdoc />
    /// <summary>
    /// Cell is a wrapped state
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// The state inside a cell can be changed by outside.
    /// It will be saved in a list when the timer updates.
    /// If type T is a value type, a value-copy will occur.
    /// If type T is a reference type, it will only copy its reference, which may introduce 'future function'.
    /// </remarks>
    public class Cell<T> : Indicator<T>
    {
        /// <inheritdoc />
        public Cell(IIndicator timer, T initValue = default(T))
        {
            Timer = timer;
            Value = initValue;
            Timer.Update += TimerOnUpdate;
        }

        private void TimerOnUpdate()
        {
            Data.Add(Value);
            FollowUp();
        }

        private IIndicator Timer { get; }
        /// <summary>
        /// the value of the cell
        /// </summary>
        public T Value { get; set; }
    }
}