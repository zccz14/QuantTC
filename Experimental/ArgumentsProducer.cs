using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using QuantTC.Meta;

namespace QuantTC.Experimental
{
    public class ArgumentsProducer
    {
        public ArgumentsProducer(IModel model, ConcurrentQueue<object[]> pool, int threadCount)
        {
            Model = model;
            N = Model.Parameters.Count;
            Current = new int[N];
            Totals = Model.Parameters.Select(p => p.Values.Count).ToArray();
            Pool = pool;
            ThreadCount = threadCount;
            Tasks = QuantTC.X.Range(0, ThreadCount).Select(i => new Task(() => MProduce(ThreadCount, i))).ToArray();
//            Tasks = QuantTC.X.Range(0, ThreadCount).Select(i => new Task(Produce)).ToArray();
        }

        private IModel Model { get; }
        private int N { get; }
        private ConcurrentQueue<object[]> Pool { get; }
        public int ThreadCount { get; }
        public Task[] Tasks { get; }
        public int[] Totals { get; }
        public int[] Current { get; }
        public bool IsCompleted { get; private set; }

        private void Produce()
        {
            var indices = new int[N];
            while (true)
            {
                var isFound = false;
                lock (Current)
                {
                    for (var i = N - 1; i >= 0; i--)
                    {
                        indices[i] = Current[i];
                        if (!isFound)
                        {
                            if (Current[i] + 1 < Totals[i])
                            {
                                isFound = true;
                                Current[i]++; // the next
                            }
                            else
                            {
                                Current[i] = 0; // reset
                            }
                        }
                    }
                }

                if (!isFound)
                {
                    IsCompleted = true;
                    break;
                }

                Pool.Enqueue(indices.Zip(Model.Parameters, (i, p) => p.Values[i]).ToArray());
            }
        }

        private void MProduce(int mod, int remain)
        {
            var indices = Enumerable.Repeat(0, N).ToArray();
            var total = Totals[N - 1];
            while (true)
            {
                for (var j = remain; j < total; j += mod)
                {
                    indices[N - 1] = j;
                    Pool.Enqueue(indices.Zip(Model.Parameters, (i, p) => p.Values[i]).ToArray());
                }

                indices[N - 2]++;
                for (var i = N - 2; i > 0; i--)
                {
                    if (indices[i] >= Totals[i])
                    {
                        indices[i] = 0;
                        indices[i - 1]++; // Carry
                    }
                    else
                    {
                        // Hold on
                        break;
                    }
                }

                if (indices[0] >= Totals[0])
                {
                    IsCompleted = true;
                    break;
                }
            }
        }

        public void Start()
        {
            foreach (var task in Tasks)
            {
                task.Start();
            }
        }

    }
}