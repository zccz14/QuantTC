using System.Collections.Generic;

namespace QuantTC.Meta
{
    public class Model : IModel
    {
        public string Name { get; set; }
        public IReadOnlyList<IParameter> Parameters => ParameterList;
        public IReadOnlyList<IConstraint> Constraints => ConstraintList;
        public IReadOnlyList<IObjective> Objectives => ObjectiveList;

        public List<IParameter> ParameterList { get; } = new List<IParameter>();
        public List<IConstraint> ConstraintList { get; } = new List<IConstraint>();
        public List<IObjective> ObjectiveList { get; } = new List<IObjective>();
    }
}