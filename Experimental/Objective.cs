using System;
using System.Reflection;

namespace QuantTC.Experimental
{
    public class Objective : IObjective
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public MethodInfo Method { get; set; }
        public int Priority { get; set; }

        public double Eval(object obj)
        {
            return Convert.ToDouble(Method.Invoke(obj, null));
        }

        public static Objective Create(MemberInfo member)
        {
            switch (member)
            {
                case MethodInfo method:
                    return Create(method);
                default:
                    return null;
            }
        }

        public static Objective Create(MethodInfo methodInfo)
        {
            var attr = methodInfo.GetCustomAttribute<ObjectiveFunctionAttribute>();
            return new Objective
            {
                Name = attr.Name,
                Type = methodInfo.ReturnType,
                Method = methodInfo,
                Priority = attr.Priority
            };
        }
    }
}