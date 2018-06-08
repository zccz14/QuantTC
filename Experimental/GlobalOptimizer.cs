using System;

namespace QuantTC.Experimental
{
    [Obsolete]
    public class GlobalOptimizer
    {
        public GlobalOptimizer(ITypedModel typedModel)
        {
            TypedModel = typedModel;
        }

        private ITypedModel TypedModel { get; }
    }
}