using System;
using System.Reflection;

namespace QuantTC.Meta.Reflection
{
    public class FieldParameter : MemberParameter
    {
        public FieldParameter(IModel model, Type type, FieldInfo field) : base(model, type, field)
        {
            Field = field;
        }

        private FieldInfo Field { get; }
        public override void SetValue(object obj, object value) => Field.SetValue(obj, value);
    }
}