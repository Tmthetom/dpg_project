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
        public static Object @lock = new Object();

        static void Main(string[] args)
        {
            // Settings
            int sizeOfStorage = 100;  // Number of integers in storage
            int delayProducent = 200;  // In milliseconds (real delay would be around this value)
            int delayConsumer = 200;  // In milliseconds (real delay would be around this value)

            // Create objects
            Logger logger = new Logger();
            Storage storage = new Storage(sizeOfStorage, logger);
            Producent producent = new Producent(storage, logger, delayProducent);
            Consumer consumer = new Consumer(storage, logger, delayConsumer);

            // Create threads
            Thread producentThread = new Thread(new ThreadStart(producent.Produce));
            Thread consumerThread = new Thread(new ThreadStart(consumer.Consume));

            // Set threads as background
            producentThread.IsBackground = true;
            consumerThread.IsBackground = true;

            // Start threads
            producentThread.Start();
            consumerThread.Start();

            // Prevent console from closing
            Console.ReadKey();
        }
    }
}
