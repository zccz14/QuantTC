using System;
using System.Collections.Generic;
using System.Linq;
using static QuantTC.X;

namespace QuantTC.Statistics.Window
{
    /// <summary>
    /// 区间统计项目
    /// </summary>
    public partial class Project
    {
        private void TestOpening()
        {
            OResults = Os.Select(condition =>
                Tuple.Create(
                    condition,
                    Range(0, N).Where(condition.Query).ToArray()
                )).ToDictionary(t => t.Item1, t => t.Item2);
        }

        private void TestClosing()
        {
            OCResults = OResults.Select(kvp =>
                Tuple.Create(
                    kvp.Key,
                    OToCs[kvp.Key].Select(cc => // closing condition
                        Tuple.Create(
                            cc,
                            kvp.Value.Select(oi =>
                                Tuple.Create(
                                    oi,
                                    Range(oi + 1, N).FirstOrDefault(i => cc.Query(oi, i))
                                )).Where(t => t.Item1 < t.Item2).ToArray()
                        )).ToDictionary(t => t.Item1, t => t.Item2)
                )).ToDictionary(t => t.Item1, t => t.Item2);
        }

        private void TestSubjects()
        {
            OCSResults = OCToSs.GroupBy(kvp => kvp.Key.Item1)
                .Select(gO => Tuple.Create(
                    gO.Key, // open cond
                    gO.Select(kvp => Tuple.Create(
                        kvp.Key.Item2, // close cond
                        kvp.Value.Select(subject => Tuple.Create(
                            subject, // subject
                            OCResults[gO.Key][kvp.Key.Item2].Select(subject.GetResult)
                                .ToArray() // calc window's subject
                        )).ToDictionary(t => t.Item1, t => t.Item2)
                    )).ToDictionary(t => t.Item1, t => t.Item2)
                )).ToDictionary(t => t.Item1, t => t.Item2);
        }

        private void TestSummary()
        {
            OCSSResults = OCSToSs.GroupBy(kvp => kvp.Key.Item1).Select(gO => Tuple.Create(
                gO.Key, // open cond
                gO.GroupBy(kvp => kvp.Key.Item2).Select(gC => Tuple.Create(
                    gC.Key, // close cond
                    gC.Select(kvp => Tuple.Create(
                        kvp.Key.Item3, // subject
                        kvp.Value.Select(summary => Tuple.Create(
                            summary, // summary
                            summary.GetResult(OCSResults[gO.Key][gC.Key][kvp.Key.Item3])
                        )).ToDictionary(t => t.Item1, t => t.Item2)
                    )).ToDictionary(t => t.Item1, t => t.Item2)
                )).ToDictionary(t => t.Item1, t => t.Item2)
            )).ToDictionary(t => t.Item1, t => t.Item2);
        }

        /// <summary>
        /// Run Statistic Tasks
        /// </summary>
        public void Run()
        {
            TestOpening();
            TestClosing();
            TestSubjects();
            TestSummary();
        }


        public int N { get; set; }
    }
}