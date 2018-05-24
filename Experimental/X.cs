using System;
using System.IO;
using System.Linq;

namespace QuantTC.Experimental
{
    public static partial class X
    {
        public static object Activate(this IModel model) =>
            Activator.CreateInstance(model.Type, 0, null, null, null, null);

        public static void PrintDetails(this IModel model, TextWriter stream)
        {
            lock (stream)
            {
                stream.WriteLine($"[Model] {model.Name}({string.Join(", ", model.Parameters.Select(p => p.Name))})");
                stream.WriteLine($"where {model.Parameters.Count} parameter(s):");
                model.Parameters.ForEach(p =>
                {
                    stream.WriteLine($"\t{p.Name}: {p.Type.Name} in {p.Values.First()}, {p.Values.Last()}, (x{p.Values.Count})");
                });
                stream.WriteLine($"max [{string.Join(", ", model.Objectives.Select(o => o.Name))}]");
                if (model.Constraints.Count == 0)
                {
                    stream.WriteLine("with no constraint");
                }
                else
                {
                    stream.WriteLine($"s.t. {model.Constraints.Count} constraint(s): ");
                    model.Constraints.ForEach(p =>
                    {
                        stream.WriteLine($"\t{p.Name}: {p.Description}");
                    });
                }
            }
        }
    }
}