using System;
using System.Reflection;

namespace QuantTC.Experimental
{
    /// <inheritdoc />
    public class MethodObjective : IObjective
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

        public static MethodObjective Create(IModel model, MemberInfo member)
        {
            switch (member)
            {
                case MethodInfo method:
                    return Create(model, method);
                case PropertyInfo property:
                    return Create(model, property);
                default:
                    return null;
            }
        }

        public static MethodObjective Create(IModel model, MethodInfo methodInfo)
        {
            var attr = methodInfo.GetCustomAttribute<ObjectiveAttribute>();
            var name = attr.Name ?? methodInfo.Name;
            var type = methodInfo.ReturnType;
            if (type != typeof(double))
            {
                Console.WriteLine($"Analyzer Warning: type of Constraint {name} must be double. (in {model})");
                return null;
            }
            return new MethodObjective
            {
                Name = name,
                Type = type,
                Method = methodInfo,
                Priority = attr.Priority,
                Model = model
            };
        }
        public static MethodObjective Create(IModel model, PropertyInfo property)
        {
            var attr = property.GetCustomAttribute<ObjectiveAttribute>();
            var name = attr.Name ?? property.Name;
            var type = property.PropertyType;
            if (type != typeof(double))
            {
                Console.WriteLine($"Analyzer Warning: type of Constraint {name} must be double. (in {model})");
                return null;
            }
            return new MethodObjective
            {
                Name = name,
                Type = type,
                Priority = attr.Priority,
                Model = model,
                Method = property.GetMethod
            };
        }
    }
    /// <inheritdoc />
    public class PropertyObjective : IObjective
    {
        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public Type Type { get; set; }

        /// <inheritdoc />
        public int Priority { get; set; }

        /// <inheritdoc />
        public IModel Model { get; set; }

        private PropertyInfo Property { get; set; }

        /// <inheritdoc />
        public double Eval(object obj) => Convert.ToDouble(Property.GetValue(obj));

        public static PropertyObjective Create(IModel model, MemberInfo member)
        {
            switch (member)
            {
                case PropertyInfo property:
                    return Create(model, property);
                default:
                    return null;
            }
        }

        public static PropertyObjective Create(IModel model, PropertyInfo property)
        {
            var attr = property.GetCustomAttribute<ObjectiveAttribute>();
            var name = attr.Name ?? property.Name;
            var type = property.PropertyType;
            if (type != typeof(double))
            {
                Console.WriteLine($"Analyzer Warning: type of Constraint {name} must be double. (in {model})");
                return null;
            }
            return new PropertyObjective
            {
                Name = name,
                Type = type,
                Priority = attr.Priority,
                Model = model
            };
        }
    }
}