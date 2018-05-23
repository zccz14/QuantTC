using System;
using QuantTC.Data;

namespace QuantTC.Backtesting
{
    /// <inheritdoc />
    /// <summary>
    /// 合约无关成交记录
    /// </summary>
    public interface IContractFreeTradeRecord: ITradeRecord
    {
        /// <summary>
        /// 成交时间
        /// </summary>
        DateTime DateTime { get; }
        /// <summary>
        /// 手续费
        /// </summary>
        double Commission { get; }
    }
}