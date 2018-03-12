using System;
using System.Threading;

namespace ProducerConsumer
{
    class Producent
    {
        private Storage storage;
        private int delay;
        private Random random = new Random();

        public Producent(Storage storage, int delay)
        {
            this.storage = storage;
            this.delay = delay;
        }

        public void Produce()
        {
            while (true)
            {
                storage.Write(random.Next(0, 10000));
                Thread.Sleep(delay);
            }
        }
    }
}
