using System;
using System.Collections.Generic;
using System.Text;

namespace QuantTC.Data
{
	/// <inheritdoc />
	public class Boll : IBoll
	{
		/// <inheritdoc />
		public double Upper { get; set; }

		/// <inheritdoc />
		public double Middle { get; set; }

		/// <inheritdoc />
		public double Lower { get; set; }

		/// <inheritdoc />
		public double Width => Upper - Lower;

		/// <inheritdoc />
		public double Ratio => Width / Middle;

		/// <inheritdoc />
		public double Std => Width / 4;
	}
}
