using System;

namespace QuantTC.Experimental
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
        public string Description { get; set; }

        public int Priority { get; set; }
    }
}
