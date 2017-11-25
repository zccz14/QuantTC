using System;
using QuantTC.Data;
using QuantTC.Indicators.Generic;

namespace QuantTC.Indicators.Collectors
{
	/// <inheritdoc />
	public class WinningProbability : Indicator<double>
	{
		/// <inheritdoc />
		public WinningProbability(IIndicator<double> netValue, IIndicator<int> winningCounter, IIndicator<int> operationCounter)
		{
			NetValue = netValue;
			WinningCounter = winningCounter;
			OperationCounter = operationCounter;
			NetValue.Update += NetValueOnUpdate;
		}

		private void NetValueOnUpdate()
		{
			Data.FillRange(Count, NetValue.Count, i => (double) WinningCounter[i] / OperationCounter[i]);
			FollowUp();
		}

		private IIndicator<double> NetValue { get; }
		private IIndicator<int> WinningCounter { get; }
		private IIndicator<int> OperationCounter { get; }
	}

	/// <inheritdoc />
	public class WinningCounter : Indicator<int>
	{
		/// <inheritdoc />
		public WinningCounter(IIndicator<double> netValue)
		{
			NetValue = netValue;
			NetValue.Update += NetValueOnUpdate;
		}

		private void NetValueOnUpdate()
		{
			Data.FillRange(Count, NetValue.Count, i =>
			{
				if (i == 0) return 0;
				return NetValue[i] > NetValue[i - 1] ? Data[i - 1] + 1 : Data[i - 1];
			});
			FollowUp();
		}

		private IIndicator<double> NetValue { get; }
	}

	/// <inheritdoc />
	public class OperationCounter : Indicator<int>
	{
		/// <inheritdoc />
		public OperationCounter(IIndicator<double> netValue)
		{
			NetValue = netValue;
			NetValue.Update += NetValueOnUpdate;
		}

		private void NetValueOnUpdate()
		{
			// ReSharper disable once CompareOfFloatsByEqualityOperator
			Data.FillRange(Count, NetValue.Count, i => i == 0 ? 0 : (NetValue[i] != NetValue[i - 1] ? Data[i - 1] + 1 : Data[i - 1]));
			FollowUp();
		}

		private IIndicator<double> NetValue { get; }
	}


	/// <inheritdoc />
	public class NetValue : Indicator<double>
	{
		/// <inheritdoc />
		public NetValue(IIndicator<IOrder> orders, IIndicator<int> netPosition, IIndicator<double> cash)
		{
			Orders = orders;
			NetPosition = netPosition;
			Cash = cash;
			Orders.Update += OrdersOnUpdate;
		}

		private void OrdersOnUpdate()
		{
			Data.FillRange(Count, Orders.Count, i =>
			{
				if (i == 0) return 0;
				if (NetPosition[i].IsSameSigned(NetPosition[i - 1]))
				{
					if (NetPosition[i].Abs() > NetPosition[i - 1].Abs())
					{
						return Data[i - 1];
					}
					return Cash[i];
				}
				else
				{
					return Cash[i] + NetPosition[i] * Orders[i].Price;
				}
			});
			FollowUp();
		}

		private IIndicator<IOrder> Orders { get; }
		private IIndicator<int> NetPosition { get; }
		private IIndicator<double> Cash { get; }
	}
}
