using System;
using QuantTC.Data;

namespace QuantTC.Backtesting
{
    /// <inheritdoc />
    /// <summary>
    /// 合约无关成交记录
    /// </summary>
    public class ContractFreeTradeRecord : IContractFreeTradeRecord
    {
        /// <inheritdoc />
        public ContractFreeTradeRecord(DateTime dateTime, double price, int lots, double commission)
        {
            DateTime = dateTime;
            Price = price;
            Lots = lots;
            Commission = commission;
        }

        /// <inheritdoc />
        public double Price { get; }
        /// <inheritdoc />
        public int Lots { get; }
        /// <inheritdoc />
        public DateTime DateTime { get; }
        /// <inheritdoc />
        public double Commission { get; }
    }
}