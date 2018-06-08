using System;
using System.Reflection;

namespace QuantTC.Meta.Reflection
{
    public class PropertyConstraint : IConstraint
    {
        public PropertyConstraint(TypeModel typeModel, PropertyInfo property)
        {
            Property = property;
            TypeModel = typeModel;
        }

        public string Name { get; set; }
        public bool Test(Array arguments) => (bool) Property.GetValue(TypeModel.Activate(arguments));

        public IModel Model => TypeModel;
        private TypeModel TypeModel { get; }
        private PropertyInfo Property { get; }
    }
}