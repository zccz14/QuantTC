using System;

namespace QuantTC.Experimental
{
    public static partial class X
    {
        public static object Activate(this IModel model) =>
            Activator.CreateInstance(model.Type, 0, null, null, null, null);

        private static object ConsoleLock = new object();
        public static void PrintDetails(this IModel model)
        {
            lock (ConsoleLock)
            {
                Console.WriteLine($"Model: {model.Name}");
                Console.WriteLine($"with {model.Parameters.Count} parameter(s):");
                model.Parameters.ForEach(p =>
                {
                    Console.WriteLine($"\t{p.Name}: {p.Type.Name} in {p.Domain.Lower}, {p.Domain.Upper} ({p.Domain.SizeFactor})");
                });
                Console.WriteLine($"max {model.Objectives.Count} objective(s)");
                model.Objectives.ForEach(o =>
                {
                    Console.WriteLine($"\t{o.Name}: {o.Type.Name}");
                });
                Console.WriteLine($"s.t. {model.Constraints.Count} constraint(s)");
                model.Constraints.ForEach(p =>
                {
                    Console.WriteLine($"\t{p.Name}: {p.Description}");
                });

            }
        }
    }
}