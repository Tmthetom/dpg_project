using System;
using System.Threading;

namespace ProducerConsumer
{
    class Consumer
    {
        private Storage storage;
        private Logger logger;
        private int delay;
        private Random random = new Random();

        public Consumer(Storage storage, Logger logger, int delay)
        {
            this.storage = storage;
            this.logger = logger;
            this.delay = delay;
        }

        public void Consume()
        {
            while (true)
            {
                lock (Program.@lock)  // Thread safe operation
                {
                    // Check emptyness of storage
                    if (storage.IsEmpty)
                    {
                        logger.ConsoleWriteLine("Consumer falling to sleep [storage empty]", ConsoleColor.Yellow);
                        Monitor.PulseAll(Program.@lock);
                        Monitor.Wait(Program.@lock);
                    }

                    // Consume
                    else
                    {
                        //bool wasFullBefore = storage.IsFull;
                        storage.Read();
                        //if (wasFullBefore) Monitor.PulseAll(Program.@lock);
                        Thread.Sleep(delay);
                    }
                }
            }
        }
    }
}
