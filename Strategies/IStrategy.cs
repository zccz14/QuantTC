using System;
using System.Collections.Generic;
using System.Text;
using QuantTC.Data;
using QuantTC.Indicators.Generic;

namespace QuantTC.Experimental
{
    /// <summary>
    /// 策略
    /// </summary>
    public interface IStrategy
    {

    }
    /// <summary>
    /// 仓位输出策略
    /// </summary>
    public interface IPositionStrategy: IStrategy
    {
        int ContractCount { get; }
        /// <summary>
        /// 当前仓位
        /// </summary>
        int Position { get; }
    }

    /// <summary>
    /// 在收盘时决策的策略
    /// </summary>
    public interface IStrategyPriceClose: IStrategy
    {
        int ContractCount { get; }
        /// <summary>
        /// 收盘决策
        /// </summary>
        void OnPriceClose(IBarPrice barPrice);
    }
    /// <summary>
    /// 可回测的策略
    /// </summary>
    public interface IBackTestingStrategy : IPositionStrategy, IStrategyPriceClose
    {

    }

    
}
