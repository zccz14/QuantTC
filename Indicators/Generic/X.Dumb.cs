using System.Collections.Generic;
using System.IO;
using System.Linq;
using QuantTC.Data;

namespace QuantTC.Indicators.Generic
{
    public static partial class X
    {
        public static Dumb<T> ToIndicator<T>(this IEnumerable<T> source)
        {
            var ret = new Dumb<T>();
            source.ForEach(t => ret.Add(t));
            return ret;
        }

        /// <summary>
        /// Loading CSV without Refresh
        /// </summary>
        /// <param name="d"></param>
        /// <param name="path"></param>
        /// <param name="header"></param>
        public static void LoadCsv(this Dumb<IPrice> d, string path, bool header = true) =>
            d.AddRange(File.ReadAllLines(path).Skip(header ? 1 : 0).Select(Price.FromString));

        /// <summary>
        /// Live Loading CSV with Refresh
        /// </summary>
        /// <param name="d"></param>
        /// <param name="path"></param>
        /// <param name="header"></param>
        public static void LiveLoadCsv(this Dumb<IPrice> d, string path, bool header = true)
        {
            File.ReadAllLines(path).Skip(header ? 1 : 0).Select(Price.FromString).ForEach(bar =>
            {
                d.Add(bar);
                d.Refresh();
            });
        }

        public static Simulator Simulator(this IIndicator<IPrice> d) => new Simulator(d);
    }
}