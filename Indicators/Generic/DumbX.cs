using System.Collections.Generic;
using System.IO;
using System.Linq;
using QuantTC.Data;

namespace QuantTC.Indicators.Generic
{
	public static class DumbX
	{
		public static Dumb<T> ToIndicator<T>(this IEnumerable<T> source)
		{
			var ret = new Dumb<T>();
			source.ForEach(t => ret.Add(t));
			return ret;
		}
        public static void LoadCsv(this Dumb<IBar> d, string path, bool header = true) => d.AddRange(File.ReadAllLines(path).Skip(header ? 1 : 0).Select(Bar.FromString));
    }
}