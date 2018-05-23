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
}