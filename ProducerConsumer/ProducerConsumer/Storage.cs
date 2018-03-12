using System;
using System.Collections.Generic;

namespace ProducerConsumer
{
    class Storage
    {
        private Object monitor = new object();  // Monitor object
        private List<int> list = new List<int>();  // Storage
        private int size;

        /// <summary>
        /// Create storage for producent and consumer
        /// </summary>
        /// <param name="size">Maximum size of storage</param>
        public Storage(int size)
        {
            this.size = size;
        }

        /// <summary>
        /// Add new item into storage
        /// </summary>
        /// <param name="number">Number to be added into storage</param>
        public void Write(int number)
        {
            lock (monitor)  // Thread safe operation
            {
                if (list.Count >= GetSize()) return;  // When storage full, return

                list.Add(number);  // Add into storage
                ConsoleWriteLine("Produced [" + number + "]", ConsoleColor.Green);  // Log
            }
        }

        /// <summary>
        /// Remove last item from storage
        /// </summary>
        public void Read()
        {
            lock (monitor)  // Thread safe operation
            {
                if (list.Count <= 0) return;  // When list empty, return

                int number = list[0];  // Read from storage
                list.RemoveAt(0);  // Remove from storage
                ConsoleWriteLine("Consumed [" + number + "]", ConsoleColor.Red);  // Log
            }
        }

        /// <summary>
        /// Write line into console with storage info and colored message
        /// </summary>
        /// <param name="message">Message to be colored and printed into console</param>
        /// <param name="textColor">Color of text</param>
        private void ConsoleWriteLine(string message, ConsoleColor textColor)
        {
            Console.ForegroundColor = textColor;
            Console.Write("[" + DateTime.Now + "] " + message + " ");
            Console.ResetColor();
            Console.WriteLine("\t" + GetStorageVisualization());
        }

        /// <summary>
        /// Get visualization of storage capacity
        /// </summary>
        /// <returns>Visualized capacity of storage</returns>
        private string GetStorageVisualization()
        {
            char fullSector = '■';  // Every full 10%
            char emptySector = '_';  // Every empty 10%
            string visualization = "";  // Visualization string

            // Get percentage of capacity taken
            int capacity = 0;
            if (GetSizeTaken() > 0)  // Size taken is greater then 0
            {
                float onePercentage = (float)(GetSize()) / 100;  // Get one percentage of capacity
                int numberOfPercentage = (int)(GetSizeTaken() / onePercentage);  // Get current percentage of capacity
                int tenTimes = numberOfPercentage / 10;  // How many times there is number 10
                int toWholeTen = (tenTimes + 1) * 10 - numberOfPercentage;  // How many we need to whole tens (from 8 to 10 we need 2)
                capacity = numberOfPercentage + toWholeTen;  // Count capacity rounded to tens
            }

            // Visialize full sectors
            for (int i = 0; i < (capacity / 10); i++)
            {
                visualization += fullSector;  // Add full sector character
            }

            // Visialize empty sectors
            for (int i = 0; i < ((100 - capacity) / 10); i++)
            {
                visualization += emptySector;  // Add empty sector character
            }

            //visualization = capacity + "%";

            return "Storage [" + visualization + "] " + GetSizeTaken() + "/" + GetSize();
        }

        /// <summary>
        /// Get maximum size of storage
        /// </summary>
        /// <returns>Maximum size</returns>
        private int GetSize()
        {
            return size;
        }

        /// <summary>
        /// Get used capacity of storages
        /// </summary>
        /// <returns>Size taken</returns>
        private int GetSizeTaken()
        {
            return list.Count;
        }

        public int isFull()
        {

        }

        public int is
    }
}
