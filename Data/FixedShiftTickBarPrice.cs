using System;
using System.Collections.Generic;

namespace QuantTC.Data
{
    /// <inheritdoc />
    /// <summary>
    /// 固定挡位 Tick 数据
    /// </summary>
    public class FixedShiftTickBarPrice : ITickBarPrice
    {
        /// <inheritdoc />
        public FixedShiftTickBarPrice(int shiftCount, params (double, int, double, int)[] infos)
        {
            ShiftCount = shiftCount;
            BidPrice = new double[ShiftCount];
            BidVolume = new int[ShiftCount];
            AskPrice = new double[ShiftCount];
            AskVolume = new int[ShiftCount];
        }

        /// <summary>
        /// Tick 挡位数
        /// </summary>
        public int ShiftCount { get; }
        /// <inheritdoc />
        public double Open => Price;
        /// <inheritdoc />
        public double High => Price;
        /// <inheritdoc />
        public double Low => Price;
        /// <inheritdoc />
        public double Close => Price;
        /// <inheritdoc />
        public DateTime DateTime { get; set; }
        /// <inheritdoc />
        public int Volume { get; set; }
        /// <inheritdoc />
        public int OpenInterest { get; set; }
        /// <inheritdoc />
        public double Price { get; set; }
        /// <inheritdoc />
        public double SettlePrice { get; set; }
        /// <inheritdoc />
        public IReadOnlyList<double> BidPrice { get; }
        /// <inheritdoc />
        public IReadOnlyList<int> BidVolume { get; }
        /// <inheritdoc />
        public IReadOnlyList<double> AskPrice { get; }
        /// <inheritdoc />
        public IReadOnlyList<int> AskVolume { get; }
    }
}