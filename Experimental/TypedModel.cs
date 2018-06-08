using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using QuantTC.Meta;

namespace QuantTC.Experimental
{
    [Obsolete]
    public class TypedModel: ITypedModel
    {
        private readonly List<ITypedObjective> _objectives = new List<ITypedObjective>();
        private readonly List<ITypedConstraint> _constraints = new List<ITypedConstraint>();
        private readonly List<ITypedParameter> _parameters = new List<ITypedParameter>();

        public string Name { get; set; }

        /// <summary>
        /// Try analyze a type as a Model
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>a Model instance if success, or null</returns>
        public static TypedModel Analyze(Type type)
        {
            var tAttr = type.GetCustomAttribute<ModelAttribute>();
            if (tAttr == null) return null;
            var name = tAttr.Name ?? type.Name;
            var model = new TypedModel
            {
                Type = type,
                Name = name
            };
            var members = type.GetMembers();
            foreach (var member in members)
            {
                if (member.GetCustomAttribute<ParameterAttribute>() != null)
                {
                    var p = TypedParameter.Create(model, member);
                    if (p != null)
                    {
                        model._parameters.Add(p);
                    }
                }

                if (member.GetCustomAttribute<ConstraintAttribute>() != null)
                {
                    var c = MemberTypedConstraint.Create(model, member);
                    if (c != null)
                    {
                        model._constraints.Add(c);
                    }
                }

                if (member.GetCustomAttribute<ObjectiveAttribute>() != null)
                {
                    var o = MethodTypedObjective.Create(model, member);
                    if (o != null)
                    {
                        model._objectives.Add(o);
                    }
                }
            }
            model._parameters.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));
            model._constraints.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));
            model._objectives.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));
            return model;
        }


        public object Activate()
        {
            var obj = Activator.CreateInstance(Type, 0, null, null, null);
            foreach (var member in TypedParameters)
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
        public IReadOnlyList<ITypedParameter> TypedParameters => _parameters;

        /// <inheritdoc />
        public IReadOnlyList<IConstraint> Constraints => _constraints;

        /// <inheritdoc />
        public IReadOnlyList<IObjective> Objectives => _objectives;

        /// <inheritdoc />
        public IReadOnlyList<IParameter> Parameters => _parameters;
    }
}