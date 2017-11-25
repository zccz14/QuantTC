using System;
using System.Linq;
using QuantTC.Data;
using QuantTC.Indicators.Generic;

namespace QuantTC.Indicators.Collectors
{
	/// <inheritdoc />
	public class OrderAgent : Indicator<IOrder>
	{
		/// <inheritdoc />
		public OrderAgent(IIndicator<IBar> market, IIndicator<int> model)
		{
			Market = market;
			Model = model;
			Market.Update += OnMarket;
		}

		private class Datum : IOrder
		{
			public DateTime DateTime { get; set; }
			public double Price { get; set; }
			public int Lots { get; set; }
			public OrderDirection Direction { get; set; }
		}

		private void OnMarket()
		{
			if (IsEnabled)
			{
				// Trade
				if (TargetPosition > Position)
				{
					Data.Add(new Datum
					{
						DateTime = Market.Last().DateTime,
						Lots = TargetPosition - Position,
						Price = Market.Last().Open,
						Direction = OrderDirection.Buy
					});
				}
				else
				{
					Data.Add(new Datum
					{
						DateTime = Market.Last().DateTime,
						Lots = Position - TargetPosition,
						Price = Market.Last().Open,
						Direction = OrderDirection.Sell
					});
				}
				Position = TargetPosition;
				IsEnabled = false;
				FollowUp();
			}
			if (TargetPosition != Position)
			{
				IsEnabled = true; // yield 
			}
//			if (TargetPosition != Position)
//			{
//				// Trade
//				if (TargetPosition > Position)
//				{
//					Data.Add(new Datum
//					{
//						DateTime = Market.Last().DateTime,
//						Lots = TargetPosition - Position,
//						Price = Market.Last().Close,
//						Direction = OrderDirection.Buy
//					});
//				}
//				else
//				{
//					Data.Add(new Datum
//					{
//						DateTime = Market.Last().DateTime,
//						Lots = Position - TargetPosition,
//						Price = Market.Last().Close,
//						Direction = OrderDirection.Sell
//					});
//				}
//				Position = TargetPosition;
//				IsEnabled = false;
//				FollowUp();
//			}
		}

		/// <summary>
		/// Target Position: The last position that the model indicated
		/// </summary>
		public int TargetPosition => Model.LastOrDefault();
		/// <summary>
		/// Current Position: The Order Agent has ordered
		/// </summary>
		public int Position { get; private set; }

		public bool IsEnabled { get; private set; }

		private IIndicator<IBar> Market { get; }
		private IIndicator<int> Model { get; }
	}
}