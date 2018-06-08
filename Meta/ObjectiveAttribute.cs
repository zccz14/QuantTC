using System;

namespace QuantTC.Meta
{
    /// <inheritdoc cref="Attribute" />
    /// <inheritdoc cref="INamedConcept" />
    /// <summary>
    /// Mark Objective Function:
    /// the more the better
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public class ObjectiveAttribute : Attribute, INamedConcept
    {
        /// <inheritdoc />
        public string Name { get; set; }
        [Obsolete]
        public int Priority { get; }
    }
}