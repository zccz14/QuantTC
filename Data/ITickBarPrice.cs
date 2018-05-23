using System.Collections.Generic;
using System.Text;

namespace QuantTC.Data
{
    /// <inheritdoc />
    /// <summary>
    /// Tick 数据
    /// </summary>
    public interface ITickBarPrice: IPriceMarket
    {
        /// <summary>
        /// 结算价
        /// </summary>
        double SettlePrice { get; }

        /// <summary>
        /// 卖量
        /// </summary>
        IReadOnlyList<double> BidPrice { get; }

        /// <summary>
        /// 买量
        /// </summary>
        IReadOnlyList<int> BidVolume { get; }

        /// <summary>
        /// 卖价
        /// </summary>
        IReadOnlyList<double> AskPrice { get; }

        /// <summary>
        /// 卖量
        /// </summary>
        IReadOnlyList<int> AskVolume { get; }
    }
}
