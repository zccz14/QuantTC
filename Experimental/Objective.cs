using System;
using System.Reflection;

namespace QuantTC.Experimental
{
    /// <inheritdoc />
    public class Objective : IObjective
    {
        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public Type Type { get; set; }

        /// <inheritdoc />
        public int Priority { get; set; }

        /// <inheritdoc />
        public IModel Model { get; set; }

        private MethodInfo Method { get; set; }

        /// <inheritdoc />
        public double Eval(object obj)
        {
            return Convert.ToDouble(Method.Invoke(obj, null));
        }

        public static Objective Create(IModel model, MemberInfo member)
        {
            switch (member)
            {
                case MethodInfo method:
                    return Create(model, method);
                default:
                    return null;
            }
        }

        public static Objective Create(IModel model, MethodInfo methodInfo)
        {
            var attr = methodInfo.GetCustomAttribute<ObjectiveAttribute>();
            var name = attr.Name ?? methodInfo.Name;
            var type = methodInfo.ReturnType;
            if (type != typeof(double))
            {
                Console.WriteLine($"Analyzer Warning: type of Constraint {name} must be double. (in {model})");
                return null;
            }
            return new Objective
            {
                Name = name,
                Type = type,
                Method = methodInfo,
                Priority = attr.Priority,
                Model = model
            };
        }
    }
}