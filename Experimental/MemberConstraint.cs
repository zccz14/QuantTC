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

        public bool Test(object activeModel) => (bool)Method.Invoke(activeModel, null);
        public IModel Model { get; set; }

        public static MemberConstraint Create(IModel model, MemberInfo member)
        {
            switch (member)
            {
                case MethodInfo method:
                    return Create(model, method);
                default:
                    return null;
            }
        }

        public static MemberConstraint Create(IModel model, MethodInfo method)
        {
            var attr = method.GetCustomAttribute<ConstraintAttribute>();
            if (method.ReturnType != typeof(bool)) return null;
            var name = attr.Name ?? method.Name;
            return new MemberConstraint
            {
                Name = name,
                Type = method.ReturnType,
                Method = method,
                Priority = attr.Priority,
                Description = attr.Description,
                Model = model
            };
        }
    }
}