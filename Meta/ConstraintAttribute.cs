using System;

namespace QuantTC.Meta
{
    /// <inheritdoc cref="Attribute" />
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ConstraintAttribute: Attribute, INamedConcept
    {
        /// <inheritdoc />
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Obsolete]
        public string Description { get; set; }
        [Obsolete]
        public int Priority { get; set; }
    }
}
