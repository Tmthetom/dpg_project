using System;
using System.Threading;

namespace ProducerConsumer
{
    class Consumer
    {
        private Storage storage;
        private Logger logger;
        private Random random = new Random();
        private int delay;
        
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
                while (storage.IsEmpty)
                {
                    logger.ConsoleWriteLine("Consumer falling to sleep [storage empty]", ConsoleColor.DarkYellow);
                    lock (Program.@lock)  // Thread safe operation
                    {
                        Monitor.Wait(Program.@lock);
                    }
                    logger.ConsoleWriteLine("Consumer awake from sleep", ConsoleColor.DarkYellow);
                }

                // Consume
                Thread.Sleep(delay + random.Next(0, delay)*5);
                storage.Read();
                lock (Program.@lock)  // Thread safe operation
                {
                    Monitor.PulseAll(Program.@lock);
                }
            }
        }
    }
}
