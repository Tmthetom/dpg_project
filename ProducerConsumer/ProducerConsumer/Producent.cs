using System;
using System.Threading;

namespace ProducerConsumer
{
    class Producent
    {
        private Storage storage;
        private Logger logger;
        private int delay;
        private Random random = new Random();

        public Producent(Storage storage, Logger logger, int delay)
        {
            this.storage = storage;
            this.logger = logger;
            this.delay = delay;
        }

        public void Produce()
        {
            while (true)
            {
                lock (Program.@lock)  // Thread safe operation
                {
                    // Check fullness of storage
                    if (storage.IsFull)
                    {
                        logger.ConsoleWriteLine("Producent falling to sleep [storage full]", ConsoleColor.Green);
                        Monitor.PulseAll(Program.@lock);
                        Monitor.Wait(Program.@lock);
                    }

                    // Produce
                    else
                    {
                        //bool wasEmptyBefore = storage.IsEmpty;
                        storage.Write(random.Next(0, 10000));
                        //if (wasEmptyBefore) Monitor.PulseAll(Program.@lock);
                        Thread.Sleep(delay);
                    }
                }
            }
        }
    }
}
