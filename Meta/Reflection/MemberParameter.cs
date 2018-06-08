using System;
using System.Reflection;

namespace QuantTC.Meta.Reflection
{
    public abstract class MemberParameter : Parameter
    {
        public MemberParameter(IModel model, Type type, MemberInfo member) : base(model, type)
        {
            Member = member;
        }

        public abstract void SetValue(object obj, object value);
        private MemberInfo Member { get; }
    }
}