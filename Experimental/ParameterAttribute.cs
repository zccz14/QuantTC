using System;

namespace QuantTC.Experimental
{
    /// <inheritdoc cref="Attribute"/>
    /// <inheritdoc cref="INamedConcept"/>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ParameterAttribute : Attribute, INamedConcept
    {
        /// <inheritdoc />
        public string Name { get; set; }
        /// <summary>
        /// Serialized Domain
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// Priority (the less the first)
        /// </summary>
        public int Priority { get; set; }

        public object Upper { get; set; }
        public object Lower { get; set; }
        public object Step { get; set; } = 1;
    }
}