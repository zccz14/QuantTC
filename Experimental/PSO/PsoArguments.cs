using System;

namespace QuantTC.Experimental.PSO
{
    /// <summary>
    ///     PSO Attributes (Arguments)
    /// </summary>
    public class PsoArguments
    {
        /// <summary>
        ///     Inertia Weight (惯性权重)
        /// </summary>
        public double W { get; set; }

        /// <summary>
        ///     Acceleration Constant 1: Global Best
        /// </summary>
        public double C1 { get; set; }

        /// <summary>
        ///     Acceleration Constant 2: Local Best
        /// </summary>
        public double C2 { get; set; }

        /// <summary>
        /// TODO: Remove it
        /// </summary>
        public double RangeMin { get; set; }
        /// <summary>
        /// TODO: Remove it
        /// </summary>
        public double RangeMax { get; set; }

        /// <summary>
        ///     The maximum iteration times
        /// </summary>
        public int MaxGeneration { get; set; }

        /// <summary>
        ///     The maximum iteration times that didn't improve best solution
        /// </summary>
        public int MaxStaticGeneration { get; set; }

        /// <summary>
        ///     The Size of Particle Swarm
        /// </summary>
        public int SwarmSize { get; set; }

        public int MaxInformers { get; set; }
        
        public Func<Array, double> AggregateFunc { get; set; }
    }
}