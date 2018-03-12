using System;
using System.Threading;

namespace ProducerConsumer
{
    class Consumer
    {
        private Storage storage;
        private int delay;
        private Random random = new Random();

        public Consumer(Storage storage, int delay)
        {
            this.storage = storage;
            this.delay = delay;
        }

        public void Consume()
        {
            while (true)
            {
                storage.Read();
                Thread.Sleep(delay);
            }
        }  
    }
}
