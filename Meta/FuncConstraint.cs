using System;

namespace QuantTC.Meta
{
    public class FuncConstraint : IConstraint
    {
        public FuncConstraint(IModel model, Func<Array, bool> testFunc)
        {
            Model = model;
            TestFunc = testFunc;
        }

        public string Name { get; set; }
        public bool Test(Array arguments) => TestFunc(arguments);
        public IModel Model { get; }
        public Func<Array, bool> TestFunc { get; }
    }
}