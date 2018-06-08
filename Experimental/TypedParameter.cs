using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using QuantTC.Collections;
using QuantTC.Meta;

namespace QuantTC.Experimental
{
    [Obsolete]
    public class TypedParameter : ITypedParameter
    {
        /// <inheritdoc />
        public string Name { get; set; }
        /// <inheritdoc />
        public Type Type { get; set; }
        /// <inheritdoc />
        public IDomain Domain { get; set; }
        /// <inheritdoc />
        public IReadOnlyList<object> Values { get; set; }

        public IModel Model => TypedModel;

        /// <inheritdoc />
        public int Priority { get; set; }

        private Func<object, object> Getter { get; set; }
        private Action<object, object> Setter { get; set; }

        /// <inheritdoc />
        public object GetValue(object obj) => Getter(obj);
        /// <inheritdoc />
        public void SetValue(object obj, object value) => Setter(obj, value);
        /// <inheritdoc />
        public ITypedModel TypedModel { get; set; }

        /// <summary>
        /// Create a parameter from MemberInfo (Field / Property)
        /// </summary>
        /// <param name="member"></param>
        /// <returns>TypedParameter if member is valid Field / Property, otherwise null</returns>
        public static TypedParameter Create(MemberInfo member)
        {
            switch (member)
            {
                case PropertyInfo property:
                    return Create(property);
                case FieldInfo field:
                    return Create(field);
                default:
                    return null;
            }
        }        
        /// <summary>
        /// Create a parameter from MemberInfo (Field / Property)
        /// </summary>
        /// <param name="member"></param>
        /// <returns>TypedParameter if member is valid Field / Property, otherwise null</returns>
        public static TypedParameter Create(ITypedModel typedModel, MemberInfo member)
        {
            var p = Create(member);
            if (p != null)
            {
                p.TypedModel = typedModel;
            }
            return p;
        }

        /// <summary>
        /// Create a parameter from FieldInfo (Field)
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static TypedParameter Create(FieldInfo field)
        {
            var attr = field.GetCustomAttribute<ParameterAttribute>();
            var name = attr.Name ?? field.Name;
            var type = field.FieldType;
            var (lower, upper, factor) = Parse(attr, type);

            IReadOnlyList<object> values;

            if (type == typeof(int))
            {
                values = new Int32ArithmeticProgression(Convert.ToInt32(attr.Lower), Convert.ToInt32(attr.Upper),
                    Convert.ToInt32(attr.Step));
            }
            else if (type == typeof(double))
            {
                if (attr.Lower == null)
                {
                    attr.Lower = short.MinValue;
                }

                if (attr.Upper == null)
                {
                    attr.Upper = short.MaxValue;
                }

                if (attr.Step == null)
                {
                    attr.Step = 1;
                }

                values = new DoubleArithmeticProgression(Convert.ToDouble(attr.Lower), Convert.ToDouble(attr.Upper), Convert.ToDouble(attr.Step));
            }
            else
            {
                values = null;
            }

            var domain = new Domain
            {
                Upper = upper,
                Lower = lower,
                SizeFactor = factor
            };
            return new TypedParameter
            {
                Name = name,
                Type = type,
                Domain = domain,
                Getter = field.GetValue,
                Setter = field.SetValue,
                Values = values
            };
        }

        private static (IComparable lower, IComparable upper, double factor) Parse(ParameterAttribute attr, Type type)
        {
            var match = attr.Domain != null ? Regex.Match(attr.Domain, @"^[\(\[](.+),(.+)[\)\]]$") : null;
            if (type == typeof(int))
            {
                var lower = attr.Lower != null ? Convert.ToInt32(attr.Lower) : int.MinValue;
                var upper = attr.Upper != null ? Convert.ToInt32(attr.Upper) : int.MaxValue;
                return (lower, upper, Math.Max(0, (1.0 + upper - lower) / (int)attr.Step));
            }

            if (type == typeof(double))
            {
                var lower = attr.Lower != null ? Convert.ToDouble(attr.Lower) : double.MinValue;
                var upper = attr.Upper != null ? Convert.ToDouble(attr.Upper) : double.MaxValue;
                var step = Convert.ToDouble(attr.Step);
                var factor = (int) Math.Min(
                    (upper - lower + 1) / step,
                    (upper / step - lower / step + 1 / step)
                );
                return (lower, upper, Math.Max(0, factor));
            }

            if (type == typeof(bool))
            {
                var lower = (bool?) attr.Lower ?? false;
                var upper = (bool?) attr.Upper ?? true;
                return (lower, upper, lower == upper ? 1: (lower ? 0: 2));
            }

            return (null, null, 0);
        }

        /// <summary>
        /// Create a parameter from PropertyInfo (Property)
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static TypedParameter Create(PropertyInfo property)
        {
            var attr = property.GetCustomAttribute<ParameterAttribute>();
            if (!property.CanWrite || !property.CanRead) return null;
            var name = attr.Name ?? property.Name;
            var type = property.PropertyType;

            var (lower, upper, factor) = Parse(attr, type);

            IReadOnlyList<object> values;

            if (type == typeof(int))
            {
                values = new Int32ArithmeticProgression(Convert.ToInt32(attr.Lower), Convert.ToInt32(attr.Upper),
                    Convert.ToInt32(attr.Step));
            }
            else if (type == typeof(double))
            {
                values = new DoubleArithmeticProgression(Convert.ToDouble(attr.Lower), Convert.ToDouble(attr.Upper), Convert.ToDouble(attr.Step));
            }
            else
            {
                values = null;
            }


            var domain = new Domain()
            {
                Description = attr.Domain,
                Lower = lower,
                Upper = upper,
                SizeFactor = factor
            };
            return new TypedParameter
            {
                Name = name,
                Type = type,
                Domain = domain,
                Priority = attr.Priority,
                Getter = property.GetValue,
                Setter = property.SetValue,
                Values = values
            };
        }
    }
}