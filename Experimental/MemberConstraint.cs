using System;
using System.Reflection;

namespace QuantTC.Experimental
{
    public class MemberConstraint : IConstraint
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public MethodInfo Method { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }

        public bool Test(object obj) => (bool)Method.Invoke(obj, null);
        public IModel Model { get; set; }

        public static MemberConstraint Create(MemberInfo member)
        {
            switch (member)
            {
                case MethodInfo method:
                    return Create(method);
                default:
                    return null;
            }
        }

        public static MemberConstraint Create(MethodInfo method)
        {
            var attr = method.GetCustomAttribute<ConstraintAttribute>();
            if (method.ReturnType != typeof(bool)) return null;
            return new MemberConstraint
            {
                Name = attr.Name,
                Type = method.ReturnType,
                Method = method,
                Priority = attr.Priority,
                Description = attr.Description
            };
        }

        internal static MemberConstraint Create(IModel model, MemberInfo member)
        {
            var c = Create(member);
            if (c == null) return null;
            c.Model = model;
            return c;
        }
    }
}