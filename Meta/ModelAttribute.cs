using System;

namespace QuantTC.Meta
{
    /// <summary>
    /// Model (Marker)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ModelAttribute : Attribute, INamedConcept
    {
        public string Name { get; set; }
    }
}