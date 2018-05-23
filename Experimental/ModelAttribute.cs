using System;

namespace QuantTC.Experimental
{
    /// <summary>
    /// Model (Marker)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ModelAttribute : Attribute, INamedConcept
    {
        public string Name { get; set; }

        public bool Ignore { get; set; }
    }
}