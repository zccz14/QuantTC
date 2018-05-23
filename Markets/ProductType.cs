namespace QuantTC.Markets
{
    /// <summary>
    /// 产品类型
    /// </summary>
    public enum ProductType
    {
        /// <summary>
        /// 期货
        /// </summary>
        Futures,
        /// <summary>
        /// 期权
        /// </summary>
        Options,
        /// <summary>
        /// 现货
        /// </summary>
        Spot,
        /// <summary>
        /// 现货期权
        /// </summary>
        SpotOption,
        /// <summary>
        /// 指数
        /// </summary>
        Index,
        /// <summary>
        /// 股票
        /// </summary>
        Stocks
    }
}