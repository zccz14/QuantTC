using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantTC.Experimental.PSO
{
    /// <summary>
    ///     PSO Optimizer
    /// </summary>
    public class PsoOptimizer
    {
        public PsoOptimizer(double bestGlobalError, double[] bestGlobalPosition, Particle[] swarm,
            PsoArguments psoArguments)
        {
            Arguments = psoArguments;
            BestGlobalError = bestGlobalError;
            Swarm = swarm;
            BestGlobalPosition = bestGlobalPosition;
        }

        public PsoArguments Arguments { get; }
        public double BestGlobalError { get; private set; }
        public double[] BestGlobalPosition { get; }
        public Particle[] Swarm { get; }

        public double[] Optimize()
        {
            var epoch = 0;
            var staticEpochs = 0;
            while (epoch < Arguments.MaxGeneration && staticEpochs < Arguments.MaxStaticGeneration)
            {
                var isErrorImproved = false;
                foreach (var particle in Swarm)
                {
                    particle.UpdateVelocity();
                    particle.UpdatePosition();
                    particle.UpdateObjective();
                    var error = particle.BestError;

                    if (error < BestGlobalError)
                    {
                        particle.Position.CopyTo(BestGlobalPosition, 0);
                        BestGlobalError = error;
                        isErrorImproved = true;
                        staticEpochs = 0;
                    }
                }

                if (!isErrorImproved) staticEpochs++;

                epoch++;
            }

            return BestGlobalPosition;
        }




        /// <summary>
        /// Init PSO
        /// </summary>
        /// <returns></returns>
        public static PsoOptimizer Build(int dimensions,
            Func<double[], double> errorFunc,
            PsoArguments psoArguments)
        {
            var swarm = Enumerable.Range(0, psoArguments.SwarmSize)
                .Select(i => new Particle(dimensions, errorFunc, psoArguments))
                .ToArray();
            var bestGlobalError = swarm[0].Objective;
            var bestGlobalPosition = new double[dimensions];
            swarm[0].Position.CopyTo(bestGlobalPosition, 0);


            var particleIndex = Enumerable.Range(0, swarm.Length).ToArray();


            // Update Ring Inline (Define which particle Learning from)
            for (var i = 0; i < particleIndex.Length; i++)
            {
                var informers = new List<Particle>();
                var numberOfinformers = psoArguments.MaxInformers / 2;

                for (var n = 1; n <= numberOfinformers; n++)
                {
                    var p = i - n;
                    while (p < 0) p = swarm.Length + p;
                    informers.Add(swarm[particleIndex[p]]);
                }

                numberOfinformers += psoArguments.MaxInformers % 2;
                for (var n = 1; n <= numberOfinformers; n++)
                {
                    var p = i + n;
                    while (p >= swarm.Length) p = p - swarm.Length;
                    informers.Add(swarm[particleIndex[p]]);
                }

                swarm[particleIndex[i]].InformersList = informers;
            }

            return new PsoOptimizer(bestGlobalError, bestGlobalPosition, swarm, psoArguments);
        }

    }
}