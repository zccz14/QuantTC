using System;

namespace QuantTC.Backtesting
{
    public interface IOrder
    {
        DateTime DateTime { get; }
        double Price { get; }
        int Lots { get; }
        OrderDirection Direction { get; }
    }

    public enum OrderDirection
    {
        Buy,
        Sell
    }
}