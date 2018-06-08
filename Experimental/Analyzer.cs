using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using QuantTC.Meta;
using QuantTC.Meta.Reflection;

namespace QuantTC.Experimental
{
    public class Analyzer : INamedConcept
    {
        public string Name { get; set; }

        public List<Assembly> Assemblies { get; } = new List<Assembly>();

        public List<Type> Types { get; } = new List<Type>();

        public List<IModel> Models { get; } = new List<IModel>();

        public List<IParameter> Parameters { get; } = new List<IParameter>();

        public List<IConstraint> Constraints { get; } = new List<IConstraint>();

        public List<IObjective> Objectives { get; } = new List<IObjective>();

        public void AnalyzeType(Type type)
        {
            Types.Add(type);
            AnalyzeTypeModel(type);
            // TODO: Analyze Model Factory
        }

        public IModel AnalyzeTypeModel(Type type)
        {
            var tAttr = type.GetCustomAttribute<ModelAttribute>();
            if (tAttr == null) return null;
            var model = new TypeModel
            {
                Type = type,
                Name = tAttr.Name ?? type.Name
            };
            var members = type.GetMembers();
            foreach (var member in members)
                switch (member)
                {
                    case FieldInfo field:
                        AnalyzeField(field, model);
                        break;
                    case MethodInfo method:
                        AnalyzeMethod(method, model);
                        break;
                    case PropertyInfo property:
                        AnalyzeProperty(property, model);
                        break;
                }

            // sort by name
            model.ParameterList.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));
            model.ConstraintList.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));
            model.ObjectiveList.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));
            Models.Add(model);
            return model;
        }

        private void AnalyzeProperty(PropertyInfo property, TypeModel model)
        {
            var pAttr = property.GetCustomAttribute<ParameterAttribute>();
            if (pAttr != null)
            {
                if (property.CanRead && property.CanWrite)
                    model.ParameterList.Add(new PropertyParameter(model, property.PropertyType, property)
                    {
                        Name = pAttr.Name ?? property.Name
                    });

                return;
            }

            var cAttr = property.GetCustomAttribute<ConstraintAttribute>();
            if (cAttr != null)
            {
                if (property.CanRead && property.PropertyType == typeof(bool))
                    model.ConstraintList.Add(new PropertyConstraint(model, property)
                    {
                        Name = cAttr.Name ?? property.Name
                    });
                return;
            }

            var oAttr = property.GetCustomAttribute<ObjectiveAttribute>();
            if (oAttr != null)
                if (property.CanRead && property.PropertyType == typeof(double))
                    model.ObjectiveList.Add(new PropertyObjective(model, property)
                    {
                        Name = oAttr.Name ?? property.Name
                    });
        }

        private void AnalyzeMethod(MethodInfo method, TypeModel model)
        {
            var cAttr = method.GetCustomAttribute<ConstraintAttribute>();
            if (cAttr != null)
            {
                if (method.ReturnType == typeof(bool))
                    model.ConstraintList.Add(new MethodConstraint(model, method)
                    {
                        Name = cAttr.Name ?? method.Name
                    });

                return;
            }

            var oAttr = method.GetCustomAttribute<ObjectiveAttribute>();
            if (oAttr != null)
            {
                if (method.ReturnType == typeof(double))
                    model.ObjectiveList.Add(new MethodObjective(model, method)
                    {
                        Name = oAttr.Name ?? method.Name
                    });
            }
        }

        private void AnalyzeField(FieldInfo field, TypeModel model)
        {
            var pAttr = field.GetCustomAttribute<ParameterAttribute>();
            if (pAttr != null)
                model.ParameterList.Add(new FieldParameter(model, field.FieldType, field)
                {
                    Name = pAttr.Name ?? field.Name
                });
        }

        public void AnalyzeAssembly(Assembly assembly)
        {
            Assemblies.Add(assembly);
            assembly.GetTypes().ForEach(AnalyzeType);
        }

        public void AnalyzeFile(string filename)
        {
            AnalyzeAssembly(Assembly.LoadFrom(filename));
        }

        public void AnalyzeDirectory(string directory)
        {
            var files = Directory.GetFiles(directory);
            foreach (var file in files)
                try
                {
                    AnalyzeFile(file);
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine($"[ERROR][Analyzer {Name}] {e.Message}");
                }
        }
    }
}