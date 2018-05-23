using System;
using System.Collections;
using System.Collections.Generic;
using QuantTC.Backtesting;
using QuantTC.Data;

namespace QuantTC.Audit
{
    /// <inheritdoc />
    /// <summary>
    /// 合约无关交易记录审计
    /// </summary>
    [Obsolete]
    public class ContractFreeTradeRecordsAuditor: IReadOnlyList<IContractFreeTradeRecord>
    {
        /// <summary>
        /// 添加交易记录
        /// </summary>
        /// <param name="record"></param>
        public void Add(IContractFreeTradeRecord record) => Records.Add(record);
        /// <summary>
        /// 批量添加交易记录
        /// </summary>
        /// <param name="records"></param>
        public void AddRange(IEnumerable<IContractFreeTradeRecord> records) => Records.AddRange(records);

        private List<IContractFreeTradeRecord> Records { get; } = new List<IContractFreeTradeRecord>();

        /// <inheritdoc />
        public IEnumerator<IContractFreeTradeRecord> GetEnumerator() => Records.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public int Count => Records.Count;

        /// <inheritdoc />
        public IContractFreeTradeRecord this[int index] => Records[index];
    }
}
