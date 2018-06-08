using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantTC.Experimental.PSO
{
    /// <summary>
    /// </summary>
    public class Particle
    {
        public Func<double[], double> ErrorFunc { get; }

        public PsoArguments Arguments { get; }

        public Random Random { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dimensions"></param>
        /// <param name="errorFunc"></param>
        public Particle(int dimensions, Func<double[], double> errorFunc, PsoArguments arguments)
        {
            Random = new Random();
            Dimensions = dimensions;
            Arguments = arguments;
            ErrorFunc = errorFunc;
            InformersList = new List<Particle>();
            Velocity = Enumerable.Range(0, Dimensions)
                .Select(i => Random.NextDouble(Arguments.RangeMin, Arguments.RangeMax)).ToArray();
            Position = Enumerable.Range(0, Dimensions)
                .Select(i => Random.NextDouble(Arguments.RangeMin, Arguments.RangeMax)).ToArray();
            BestPosition = new double[Dimensions];
            Position.CopyTo(BestPosition, 0);
            Objective = ErrorFunc(Position);
            BestError = Objective;
        }

        public double BestError { get; set; }

        public double[] BestPosition { get; set; }

        public int Dimensions { get; set; }

        public double Objective { get; set; }

        public List<Particle> InformersList { get; set; }

        /// <summary>
        /// The position of the particle
        /// </summary>
        public double[] Position { get; set; }

        /// <summary>
        /// The Velocity of the particle
        /// </summary>
        public double[] Velocity { get; set; }

        /// <summary>
        /// Update Velocity (1st)
        /// </summary>
        public void UpdateVelocity()
        {
            var bestLocalPosition = BestPosition;
            var bestErrorFound = BestError;
            foreach (var informerParticle in InformersList)
                if (bestErrorFound > informerParticle.BestError)
                {
                    bestErrorFound = informerParticle.BestError;
                    bestLocalPosition = informerParticle.BestPosition;
                }

            for (var i = 0; i < Dimensions; i++)
            {
                Velocity[i] = Arguments.W * Velocity[i]
                              + Arguments.C1 * Random.NextDouble() * (BestPosition[i] - Position[i])
                              + Arguments.C2 * Random.NextDouble() * (bestLocalPosition[i] - Position[i]);
            }
        }

        /// <summary>
        /// Update Position (2nd)
        /// </summary>
        public void UpdatePosition()
        {
            for (var i = 0; i < Dimensions; i++)
            {
                Position[i] += Velocity[i];
            }
        }

        /// <summary>
        /// Update Objective (3rd)
        /// </summary>
        public void UpdateObjective()
        {
            if (Position.All(pd => !(pd < Arguments.RangeMin) && !(pd > Arguments.RangeMax)))
            {
                var newError = ErrorFunc(Position);
                if (newError < BestError)
                {
                    Position.CopyTo(BestPosition, 0);
                    BestError = newError;
                }

                Objective = newError;
            }
        }
    }
}