using System;
using System.Collections.Generic;

namespace QuantTC.Meta.Reflection
{
    public class TypeModel : IModel
    {
        public Type Type { get; set; }
        public object Activate()
        {
            return Activator.CreateInstance(Type, 0, null, null, null, null);
        }

        public object Activate(Array arguments)
        {
            var obj = Activate();
            ParameterList.ForEach((p, i) => p.SetValue(obj, arguments.GetValue(i)));
            return obj;
        }

        public List<MemberParameter> ParameterList { get; } = new List<MemberParameter>();
        public List<IConstraint> ConstraintList { get; } = new List<IConstraint>();
        public List<IObjective> ObjectiveList { get; } = new List<IObjective>();
        public string Name { get; set; }
        public IReadOnlyList<IParameter> Parameters => ParameterList;
        public IReadOnlyList<IConstraint> Constraints => ConstraintList;
        public IReadOnlyList<IObjective> Objectives => ObjectiveList;
    }
}