using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using QuantTC.Meta;
using QuantTC.Meta.Reflection;

namespace QuantTC.Experimental
{
    public static partial class X
    {
        [Obsolete]
        public static object Activate(this ITypedModel typedModel) =>
            Activator.CreateInstance(typedModel.Type, 0, null, null, null, null);

        public static void PrintDetails(this IModel typedModel, TextWriter stream)
        {
            lock (stream)
            {
                stream.WriteLine(
                    $"[Model] {typedModel.Name}({string.Join(", ", typedModel.Parameters.Select(p => p.Name))})");
                stream.WriteLine($"where {typedModel.Parameters.Count} parameter(s):");
                typedModel.Parameters.ForEach(p =>
                {
                    stream.WriteLine(
                        $"\t{p.Name}: {p.Type.Name} in {p.Values.First()}, {p.Values.Last()}, (x{p.Values.Count})");
                });
                stream.WriteLine($"max [{string.Join(", ", typedModel.Objectives.Select(o => o.Name))}]");
                if (typedModel.Constraints.Count == 0)
                {
                    stream.WriteLine("with no constraint");
                }
                else
                {
                    stream.WriteLine($"s.t. {typedModel.Constraints.Count} constraint(s): ");
                    typedModel.Constraints.ForEach(p => { stream.WriteLine($"\t{p.Name}"); });
                }
            }
        }

        /// <summary>
        /// Applies parameters' value mapping to the ordering point
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="point">The point to calucate</param>
        /// <returns>The array of actual arguments</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static Array GetArguments(this IModel model, IEnumerable<int> point) =>
            model.Parameters.Zip(point, (parameter, i) => parameter.Values[i]).ToArray();

        public static int MapsTo(this IParameter p, double x) => (int) (x % p.Values.Count);

        public static OptimizationTask CreateOptimizationTask(this IModel model) => new OptimizationTask(model);


        public static FuncConstraint CreateFuncConstraint(this IModel model, Func<Array, bool> testFunc) => new FuncConstraint(model, testFunc);

        public static FuncObjective CreateFuncObjective(this IModel model, Func<Array, double> evalFunc) => new FuncObjective(model, evalFunc);

        public static void AddFuncConstraint(this Model model, Func<Array, bool> testFunc) =>
            model.ConstraintList.Add(model.CreateFuncConstraint(testFunc));

        public static void AddFuncObjective(this Model model, Func<Array, double> evalFunc) =>
            model.ObjectiveList.Add(model.CreateFuncObjective(evalFunc));


    }
}