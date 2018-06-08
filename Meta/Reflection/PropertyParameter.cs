using System;
using System.Reflection;

namespace QuantTC.Meta.Reflection
{
    public class PropertyParameter : MemberParameter
    {
        public PropertyParameter(IModel model, Type type, PropertyInfo property) : base(model, type, property)
        {
            Property = property;
        }

        private PropertyInfo Property { get; }
        public override void SetValue(object obj, object value) => Property.SetValue(obj, value);
    }
}