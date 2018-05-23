using System.Linq;

namespace QuantTC.Experimental
{
    public static partial class X
    {
        /// <summary>
        /// Evaluate the model with arguments
        /// </summary>
        /// <param name="model"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public static (bool, double[], bool[]) Eval(this IModel model, params object[] arguments)
        {
            // object initialization
            var obj = model.Activate();
            arguments.ForEach((arg, i) => model.Parameters[i].SetValue(obj, arg));
            // test feasible
            var tests = model.Constraints.Select(c => c.Test(obj)).ToArray();
            if (!tests.All(r => r)) return (false, null, tests); // not feasible
            // Evaluate Objectives
            return (true, model.Objectives.Select(o => o.Eval(obj)).ToArray(), null);
        }
    }
}