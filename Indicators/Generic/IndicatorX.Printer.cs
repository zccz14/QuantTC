using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantTC.Indicators.Generic
{
	public static partial class IndicatorX
	{
		public static void Print(this Indicator root)
		{
			var remain = new List<int> {1};
			var id = new Dictionary<Indicator, int>();
			print(root, 0, remain, id);
		}

		private static void print(this Indicator node, int tabs, List<int> remains, Dictionary<Indicator, int> id)
		{
			if (id.ContainsKey(node) == false)
			{
				id.Add(node, id.Count + 1);
			}
			Console.Write(string.Concat(remains.Take(tabs).Select(i => i > 0? " |  ": "    ")));
			Console.Write($" +--[{id[node]}]");
			Console.WriteLine(node);
			remains[tabs]--;
			var nexts = node.FollowingIndicators().Count();
			if (tabs + 1 < remains.Count)
				remains[tabs + 1] = nexts;
			else
			{
				remains.Add(nexts);
			}
			node.FollowingIndicators().ForEach(f => f.print(tabs + 1, remains, id));
		}
	}
}