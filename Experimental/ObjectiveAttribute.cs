using System;

namespace QuantTC.Experimental
{
    /// <inheritdoc cref="Attribute" />
    /// <inheritdoc cref="INamedConcept" />
    /// <summary>
    /// Mark Objective Function:
    /// the more the better
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ObjectiveAttribute : Attribute, INamedConcept
    {
        /// <inheritdoc />
        public string Name { get; set; }
        public int Priority { get; }
    }
}