namespace QuantTC.Data
{
    /// <summary>
    /// Bollinger Data
    /// </summary>
    public interface IBoll
    {
        /// <summary>
        /// Lower Line
        /// </summary>
        double Lower { get; }

        /// <summary>
        /// Middle Line
        /// </summary>
        double Middle { get; }

        /// <summary>
        /// Upper Line
        /// </summary>
        double Upper { get; }
    }

    /// <summary>
    /// Extension for IBoll Datum
    /// </summary>
    public static class BollX
    {
        /// <summary>
        /// Width = Upper - Lower
        /// </summary>
        /// <param name="datum">IBoll Datum</param>
        /// <returns>Width</returns>
        public static double Width(this IBoll datum) => datum.Upper - datum.Lower;

        /// <summary>
        /// Ratio = Width / Middle
        /// </summary>
        /// <param name="datum">IBoll Datum</param>
        /// <returns>Ratio</returns>
        public static double Ratio(this IBoll datum) => datum.Width() / datum.Middle;

        /// <summary>
        /// Std: Width / (4 * Deviation)
        /// </summary>
        /// <param name="datum"></param>
        /// <param name="deviation">default set to 2</param>
        /// <returns></returns>
        public static double Std(this IBoll datum, double deviation = 2) => datum.Width() / 4 / deviation;

        /// <summary>
        /// Mid + x * Std
        /// </summary>
        /// <param name="datum"></param>
        /// <param name="x"></param>
        /// <param name="deviation"></param>
        /// <returns></returns>
        public static double MidDevStd(this IBoll datum, double x, double deviation = 2) =>
            datum.Middle + x * datum.Std(deviation);
    }
}