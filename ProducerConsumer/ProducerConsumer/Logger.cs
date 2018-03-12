using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    class Logger
    {
        /// <summary>
        /// Write line into console with storage info and colored message
        /// </summary>
        /// <param name="message">Message to be colored and printed into console</param>
        /// <param name="textColor">Color of text</param>
        public void ConsoleWriteLine(string message, ConsoleColor textColor)
        {
            lock (Program.@lock)  // Thread safe operation
            {
                Console.ForegroundColor = textColor;
                Console.WriteLine("[" + DateTime.Now + "] " + message + " ");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Write line into console with storage info and colored message
        /// </summary>
        /// <param name="message">Message to be colored and printed into console</param>
        /// <param name="visualization">Visualized capacity of storage</param>
        /// <param name="textColor">Color of text</param>
        public void ConsoleWriteLine(string message, string visualization, ConsoleColor textColor)
        {
            lock (Program.@lock)  // Thread safe operation
            {
                Console.ForegroundColor = textColor;
                Console.Write("[" + DateTime.Now + "] " + message + " ");
                Console.ResetColor();
                Console.WriteLine("\t" + visualization);
            }
        }
    }
}
