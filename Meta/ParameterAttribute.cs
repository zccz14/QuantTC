using System;

namespace QuantTC.Meta
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
        [Obsolete]
        public string Domain { get; set; }
        /// <summary>
        /// Priority (the less the first)
        /// </summary>
        [Obsolete]
        public int Priority { get; set; }

        public object Upper { get; set; }
        public object Lower { get; set; }
        public object Step { get; set; } = 1;
    }
}