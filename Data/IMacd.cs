using System;
using System.Collections.Generic;
using System.Text;

namespace QuantTC.Data
{
    /// <summary>
    /// MACD Datum Interface
    /// </summary>
    public interface IMacd
    {
        /// <summary>
        /// DIFF
        /// </summary>
        double Diff { get; }

        /// <summary>
        /// DEA
        /// </summary>
        double Dea { get; }

        /// <summary>
        /// MACD
        /// </summary>
        double Macd { get; }
    }
}