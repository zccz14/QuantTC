using System;

namespace QuantTC.Experimental
{
    /// <summary>
    /// Mark Objective Function:
    /// the more the better
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ObjectiveFunctionAttribute : Attribute, INamedConcept
    {
        public string Name { get; set; }
        public int Priority { get; }
    }
}