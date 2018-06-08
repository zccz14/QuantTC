using System;
using System.Reflection;
using QuantTC.Meta;

namespace QuantTC.Experimental
{
    /// <inheritdoc />
    /// 
    [Obsolete]
    public class MethodTypedObjective : ITypedObjective
    {
        /// <inheritdoc />
        public string Name { get; set; }

        public double Evaluate(Array arguments)
        {
            var obj = TypedModel.Activate();
            TypedModel.TypedParameters.ForEach((p, i) => p.SetValue(obj, arguments.GetValue(i)));
            return Eval(obj);
        }

        public IModel Model => TypedModel;

        /// <inheritdoc />
        public Type Type { get; set; }

        /// <inheritdoc />
        public int Priority { get; set; }

        /// <inheritdoc />
        public ITypedModel TypedModel { get; set; }

        private MethodInfo Method { get; set; }

        /// <inheritdoc />
        public double Eval(object obj)
        {
            return Convert.ToDouble(Method.Invoke(obj, null));
        }

        public static MethodTypedObjective Create(ITypedModel typedModel, MemberInfo member)
        {
            switch (member)
            {
                case MethodInfo method:
                    return Create(typedModel, method);
                case PropertyInfo property:
                    return Create(typedModel, property);
                default:
                    return null;
            }
        }

        public static MethodTypedObjective Create(ITypedModel typedModel, MethodInfo methodInfo)
        {
            var attr = methodInfo.GetCustomAttribute<ObjectiveAttribute>();
            var name = attr.Name ?? methodInfo.Name;
            var type = methodInfo.ReturnType;
            if (type != typeof(double))
            {
                Console.WriteLine($"Analyzer Warning: type of Constraint {name} must be double. (in {typedModel})");
                return null;
            }
            return new MethodTypedObjective
            {
                Name = name,
                Type = type,
                Method = methodInfo,
                Priority = attr.Priority,
                TypedModel = typedModel
            };
        }
        public static MethodTypedObjective Create(ITypedModel typedModel, PropertyInfo property)
        {
            var attr = property.GetCustomAttribute<ObjectiveAttribute>();
            var name = attr.Name ?? property.Name;
            var type = property.PropertyType;
            if (type != typeof(double))
            {
                Console.WriteLine($"Analyzer Warning: type of Constraint {name} must be double. (in {typedModel})");
                return null;
            }
            return new MethodTypedObjective
            {
                Name = name,
                Type = type,
                Priority = attr.Priority,
                TypedModel = typedModel,
                Method = property.GetMethod
            };
        }
    }
    /// <inheritdoc />
    public class PropertyTypedObjective : ITypedObjective
    {
        /// <inheritdoc />
        public string Name { get; set; }

        public double Evaluate(Array arguments)
        {
            throw new NotImplementedException();
        }

        public IModel Model => TypedModel;

        /// <inheritdoc />
        public Type Type { get; set; }

        /// <inheritdoc />
        public int Priority { get; set; }

        /// <inheritdoc />
        public ITypedModel TypedModel { get; set; }

        private PropertyInfo Property { get; set; }

        /// <inheritdoc />
        public double Eval(object obj) => Convert.ToDouble(Property.GetValue(obj));

        public static PropertyTypedObjective Create(ITypedModel typedModel, MemberInfo member)
        {
            switch (member)
            {
                case PropertyInfo property:
                    return Create(typedModel, property);
                default:
                    return null;
            }
        }

        public static PropertyTypedObjective Create(ITypedModel typedModel, PropertyInfo property)
        {
            var attr = property.GetCustomAttribute<ObjectiveAttribute>();
            var name = attr.Name ?? property.Name;
            var type = property.PropertyType;
            if (type != typeof(double))
            {
                Console.WriteLine($"Analyzer Warning: type of Constraint {name} must be double. (in {typedModel})");
                return null;
            }
            return new PropertyTypedObjective
            {
                Name = name,
                Type = type,
                Priority = attr.Priority,
                TypedModel = typedModel
            };
        }
    }
}