using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QuantTC.Meta.Reflection
{
    public class PropertyObjective : IObjective
    {
        public PropertyObjective(TypeModel typeModel, PropertyInfo property)
        {
            TypeModel = typeModel;
            Property = property;
        }

        public string Name { get; set; }
        public double Evaluate(Array arguments)
        {
            return (double) Property.GetValue(TypeModel.Activate(arguments));
        }

        public IModel Model => TypeModel;
        private TypeModel TypeModel { get; }
        private PropertyInfo Property { get; }
    }
}