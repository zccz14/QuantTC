using System;
using System.Reflection;
using QuantTC.Meta;

namespace QuantTC.Experimental
{
    [Obsolete]
    public class MemberTypedConstraint : ITypedConstraint
    {
        private ITypedModel _model;
        public string Name { get; set; }
        public bool Test(Array arguments)
        {
            throw new NotImplementedException();
        }

        public Type Type { get; set; }
        public MethodInfo Method { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }

        public bool Test(object activeModel) => (bool)Method.Invoke(activeModel, null);
        public IModel Model { get; set; }

        public static MemberTypedConstraint Create(ITypedModel typedModel, MemberInfo member)
        {
            switch (member)
            {
                case MethodInfo method:
                    return Create(typedModel, method);
                default:
                    return null;
            }
        }

        public static MemberTypedConstraint Create(ITypedModel typedModel, MethodInfo method)
        {
            var attr = method.GetCustomAttribute<ConstraintAttribute>();
            if (method.ReturnType != typeof(bool)) return null;
            var name = attr.Name ?? method.Name;
            return new MemberTypedConstraint
            {
                Name = name,
                Type = method.ReturnType,
                Method = method,
                Priority = attr.Priority,
                Description = attr.Description,
                Model = typedModel
            };
        }
    }
}