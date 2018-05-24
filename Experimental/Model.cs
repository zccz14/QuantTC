using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QuantTC.Experimental
{
    public class Model: IModel
    {
        private readonly List<IObjective> _objectives = new List<IObjective>();
        private readonly List<IConstraint> _constraints = new List<IConstraint>();
        private readonly List<IParameter> _parameters = new List<IParameter>();

        public string Name { get; set; }

        public override string ToString()
        {
            return $"Model: {Name}";
        }

        /// <summary>
        /// Try analyze a type as a Model
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>a Model instance if success, or null</returns>
        public static Model Analyze(Type type)
        {
            var tAttr = type.GetCustomAttribute<ModelAttribute>();
            if (tAttr == null) return null;
            if (tAttr.Ignore) return null;
            var name = tAttr.Name ?? type.Name;
            var model = new Model
            {
                Type = type,
                Name = name
            };
            var members = type.GetMembers();
            foreach (var member in members)
            {
                if (member.GetCustomAttribute<ParameterAttribute>() != null)
                {
                    var p = Parameter.Create(model, member);
                    if (p != null)
                    {
                        model._parameters.Add(p);
                    }
                }

                if (member.GetCustomAttribute<ConstraintAttribute>() != null)
                {
                    var c = MemberConstraint.Create(model, member);
                    if (c != null)
                    {
                        model._constraints.Add(c);
                    }
                }

                if (member.GetCustomAttribute<ObjectiveAttribute>() != null)
                {
                    var o = MethodObjective.Create(model, member);
                    if (o != null)
                    {
                        model._objectives.Add(o);
                    }
                }
            }
            model._parameters.Sort((a, b) => a.Priority.CompareTo(b.Priority));
            model._constraints.Sort((a, b) => a.Priority.CompareTo(b.Priority));
            model._objectives.Sort((a, b) => a.Priority.CompareTo(b.Priority));
            return model;
        }


        public object Activate()
        {
            var obj = Activator.CreateInstance(Type, 0, null, null, null);
            foreach (var member in Parameters)
            {
                switch (member)
                {
                    case FieldInfo field:
                        field.SetValue(obj, 0);
                        break;
                    case PropertyInfo property:
                        property.SetValue(obj, 0);
                        break;
                }
            }
            return obj;
        }



        /// <inheritdoc />
        public Type Type { get; set; }

        /// <inheritdoc />
        public IReadOnlyList<IParameter> Parameters => _parameters;

        /// <inheritdoc />
        public IReadOnlyList<IConstraint> Constraints => _constraints;

        /// <inheritdoc />
        public IReadOnlyList<IObjective> Objectives => _objectives;

    }
}