using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    class Program
    {
        private Object monitor = new Object();

        static void Main(string[] args)
        {
            // Create objects
            Storage storage = new Storage(10);
            Producent producent = new Producent(storage, 100);
            Consumer consumer = new Consumer(storage, 200);
            
            // Create threads
            Thread producentThread = new Thread(new ThreadStart(producent.Produce));
            Thread consumerThread = new Thread(new ThreadStart(consumer.Consume));

            // Start threads
            producentThread.Start();
            consumerThread.Start();
            producentThread.Join();
            consumerThread.Join();

            // Prevent console from closing
            Console.ReadKey();
        }
    }
}
