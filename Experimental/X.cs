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
                stream.WriteLine($"Model: {model.Name}");
                stream.WriteLine($"with {model.Parameters.Count} parameter(s):");
                model.Parameters.ForEach(p =>
                {
                    stream.WriteLine($"\t{p.Name}: {p.Type.Name} in {p.Values.First()}, {p.Values.Last()}, ({p.Values.Size})");
                });
                stream.WriteLine($"max {model.Objectives.Count} objective(s)");
                model.Objectives.ForEach(o =>
                {
                    stream.WriteLine($"\t{o.Name}: {o.Type.Name}");
                });
                stream.WriteLine($"s.t. {model.Constraints.Count} constraint(s)");
                model.Constraints.ForEach(p =>
                {
                    stream.WriteLine($"\t{p.Name}: {p.Description}");
                });
            }
        }
    }
}