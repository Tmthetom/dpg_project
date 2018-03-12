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
            // Settings
            int sizeOfStorage = 100;  // Number of integers in storage
            int delayProducent = 10;  // In milliseconds
            int delayConsumer = 20;  // In milliseconds

            // Create objects
            Logger logger = new Logger();
            Storage storage = new Storage(sizeOfStorage, logger);
            Producent producent = new Producent(storage, logger, delayProducent);
            Consumer consumer = new Consumer(storage, logger, delayConsumer);
            
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
