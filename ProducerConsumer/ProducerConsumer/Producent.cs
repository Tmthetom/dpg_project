using System;
using System.Threading;

namespace ProducerConsumer
{
    class Producent
    {
        private Storage storage;
        private Logger logger;
        private Random random = new Random();
        private int delay;

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
                // Check fullness of storage
                while (storage.IsFull)
                {
                    logger.ConsoleWriteLine("Producent falling to sleep [storage full]", ConsoleColor.DarkGreen);
                    lock (Program.@lock)  // Thread safe operation
                    {
                        
                        Monitor.Wait(Program.@lock);
                    }
                    logger.ConsoleWriteLine("Producent awake from sleep", ConsoleColor.DarkGreen);
                }

                // Produce
                Thread.Sleep(delay + random.Next(0, delay)*5);
                storage.Write(random.Next(0, 10000));
                lock (Program.@lock)  // Thread safe operation
                {
                    Monitor.PulseAll(Program.@lock);
                }
            }
        }
    }
}
