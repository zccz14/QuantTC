using QuantTC.Data;
using QuantTC.Indicators.Generic;
using QuantTC.Meta;

namespace QuantTC.Backtesting
{
    /// <summary>
    /// 单合约 策略回测容器
    /// </summary>
    public class Container
    {
        public Container(IBackTestingStrategy strategy, IIndicator<IBarPrice> source)
        {
            Strategy = strategy;
            Source = source;
            Source.Update += Source_Update;
        }

        private void Source_Update()
        {
            var price = Source.Last();
            // pretend trading at open (back testing)
            if (Position != Strategy.Position)
            {
                Records.Add(new ContractFreeTradeRecord(price.DateTime, price.Open, Strategy.Position - Position, 0));
                Position = Strategy.Position;
            }
            Strategy.OnPriceClose(price);
        }

        private IIndicator<IBarPrice> Source { get; }
        private IBackTestingStrategy Strategy { get; }
        public Dumb<ITradeRecord> Records { get; } = new Dumb<ITradeRecord>();
        private int Position { get; set; }
    }
}
