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
                // Check emptyness of storage
                if (storage.IsEmpty)
                {
                    logger.ConsoleWriteLine("Consumer falling to sleep [storage empty]", ConsoleColor.Red);
                    Thread.Sleep(Timeout.Infinite);
                }

                // Consume
                storage.Read();
                Thread.Sleep(delay);
            }
        }
    }
}
