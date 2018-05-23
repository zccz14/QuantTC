namespace QuantTC.Backtesting
{
    /// <summary>
    /// 成交记录 Trade Record
    /// </summary>
    public interface ITradeRecord
    {
        /// <summary>
        /// 成交价格
        /// </summary>
        double Price { get; }
        /// <summary>
        /// 成交手数 (signed: positive for buy, negative for sell)
        /// </summary>
        int Lots { get; }
    }
}