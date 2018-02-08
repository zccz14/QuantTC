using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantTC.Indicators.Generic
{
    public static partial class X
    {
        public static void Print(this ITreeView root)
        {
            var remain = new List<int> {1};
            var id = new Dictionary<ITreeView, int>();
            print(root, 0, remain, id);
        }

        private static void print(this ITreeView node, int tabs, IList<int> remains, IDictionary<ITreeView, int> id)
        {
            if (id.ContainsKey(node) == false)
            {
                id.Add(node, id.Count + 1);
            }
            Console.Write(string.Concat(remains.Take(tabs).Select(i => i > 0 ? " |  " : "    ")));
            Console.Write($" +--[{id[node]}]");
            Console.WriteLine(node);
            remains[tabs]--;
            var nextTreeViews = node.GetNexts().ToArray();
            var nexts = nextTreeViews.Count();
            if (tabs + 1 < remains.Count)
                remains[tabs + 1] = nexts;
            else
            {
                remains.Add(nexts);
            }
            nextTreeViews.ForEach(f => f.print(tabs + 1, remains, id));
        }
    }
}