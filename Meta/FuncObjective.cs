using System;

namespace QuantTC.Meta
{
    public class FuncObjective : IObjective
    {
        public FuncObjective(IModel model, Func<Array, double> evalFunc)
        {
            Model = model;
            EvalFunc = evalFunc;
        }

        public string Name { get; set; }
        public double Evaluate(Array arguments) => EvalFunc(arguments);
        public IModel Model { get; }
        public Func<Array, double> EvalFunc { get; }
    }
}