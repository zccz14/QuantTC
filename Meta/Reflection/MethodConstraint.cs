using System;
using System.Reflection;

namespace QuantTC.Meta.Reflection
{
    public class MethodConstraint : IConstraint
    {
        public MethodConstraint(TypeModel typeModel, MethodInfo method)
        {
            TypeModel = typeModel;
            Method = method;
        }

        public string Name { get; set; }
        public bool Test(Array arguments) => (bool) Method.Invoke(TypeModel.Activate(arguments), null);

        public IModel Model => TypeModel;
        private TypeModel TypeModel { get; }
        private MethodInfo Method { get; }
    }
}