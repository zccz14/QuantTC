using System;
using QuantTC.Meta;

namespace QuantTC.Experimental
{
    [Obsolete]
    public class PredicateTypedConstraint : ITypedConstraint
    {
        public string Name { get; set; }
        public bool Test(Array arguments)
        {
            throw new NotImplementedException();
        }

        public string Description { get; set; }
        public int Priority { get; set; }
        public Func<object, bool> Predicate { get; set; }

        public bool Test(object activeModel) => Predicate(activeModel);
        public IModel Model { get; set; }
    }
}