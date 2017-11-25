using QuantTC.Data;
using QuantTC.Indicators.Generic;

namespace QuantTC.Indicators.Collectors
{
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
				if (NetPosition[i - 1] == 0 || NetPosition[i].IsSameSigned(NetPosition[i - 1]) && NetPosition[i].Abs() > NetPosition[i - 1].Abs())
				{
					return Data[i - 1]; // yield 
				}
				return Cash[i];
			});
		}

		private IIndicator<IOrder> Orders { get; }
		private IIndicator<int> NetPosition { get; }
		private IIndicator<double> Cash { get; }
	}
}
