using System;
using System.Reflection;

namespace QuantTC.Meta.Reflection
{
    public class MethodObjective : IObjective
    {
        public MethodObjective(TypeModel typeModel, MethodInfo method)
        {
            TypeModel = typeModel;
            Method = method;
        }

        public string Name { get; set; }

        public double Evaluate(Array arguments) => (double) Method.Invoke(TypeModel.Activate(arguments), null);

        public IModel Model => TypeModel;
        private TypeModel TypeModel { get; }
        private MethodInfo Method { get; }
    }
}