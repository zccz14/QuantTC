using System;
using System.Reflection;

namespace QuantTC.Experimental
{
    public interface IConstraint : INamedConcept
    {
        string Description { get; }
        int Priority { get; }
//        Type Type { get; }
//        MethodInfo Method { get; }
        bool Test(object obj);

        IModel Model { get; }
    }

    class PredicateConstraint : IConstraint
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public Func<object, bool> Predicate { get; set; }

        public bool Test(object obj) => Predicate(obj);
        public IModel Model { get; set; }
    }
}