namespace QuantTC.Experimental
{
    public class GlobalOptimizer
    {
        public GlobalOptimizer(IModel model)
        {
            Model = model;
        }

        private IModel Model { get; }
    }
}