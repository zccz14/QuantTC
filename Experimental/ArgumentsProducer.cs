using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantTC.Experimental
{
    public class ArgumentsProducer
    {
        public ArgumentsProducer(IModel model, ConcurrentQueue<object[]> pool)
        {
            Model = model;
            N = Model.Parameters.Count;
            Current = new int[N];
            Totals = Model.Parameters.Select(p => p.Values.Count).ToArray();
            Pool = pool;
        }

        private IModel Model { get; }
        private int N { get; }
        private ConcurrentQueue<object[]> Pool { get; }
        public int ThreadCount { get; set; }
        public List<Task> Tasks { get; } = new List<Task>();
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

        public void Update()
        {
            while (Tasks.Count < ThreadCount)
            {
                Tasks.Add(new Task(Produce));
            }

            foreach (var task in Tasks)
            {
                if (!task.IsCompleted)
                {
                    task.Start();
                }
            }
        }

        public void Start()
        {
            foreach (var task in Tasks.Take(ThreadCount))
            {
                task.Start();
            }
        }

    }
}