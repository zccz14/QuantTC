using System;
using System.Collections.Generic;
using QuantTC.Experimental;

namespace QuantTC.Meta
{
    public class Parameter : IParameter
    {
        public Parameter(IModel model, Type type)
        {
            Model = model;
            Type = type;
        }

        public string Name { get; set; }
        public Type Type { get; }
        public IReadOnlyList<object> Values { get; set; }
        public IModel Model { get; }
    }
}