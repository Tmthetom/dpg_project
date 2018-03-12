using System;
using System.Collections.Generic;

namespace ProducerConsumer
{
    class Storage
    {
        private Object monitor = new object();  // Monitor object
        private Logger logger;  // Logger for console output
        private List<int> list = new List<int>();  // Storage
        private int size;  // Maximum size of storage

        // Getters as properties
        public bool IsFull { get; private set; } = false;
        public bool IsEmpty { get; private set; } = true;

        /// <summary>
        /// Create storage for producent and consumer
        /// </summary>
        /// <param name="size">Maximum size of storage</param>
        public Storage(int size, Logger logger)
        {
            this.size = size;
            this.logger = logger;
        }

        /// <summary>
        /// Add new item into storage
        /// </summary>
        /// <param name="number">Number to be added into storage</param>
        public void Write(int number)
        {
            lock (monitor)  // Thread safe operation
            {
                // When storage full, return
                if (list.Count >= GetSize()) return;

                // Add
                list.Add(number);  // Add into storage
                logger.ConsoleWriteLine("Produced [" + number + "]", GetStorageVisualization(), ConsoleColor.Green);  // Log

                // Check fullness
                if (list.Count >= GetSize()) IsFull = true;
                else IsFull = false;
            }
        }

        /// <summary>
        /// Remove last item from storage
        /// </summary>
        public void Read()
        {
            lock (monitor)  // Thread safe operation
            {
                // When list empty, return
                if (list.Count <= 0) return;

                // Read and remove
                int number = list[0];  // Read from storage
                list.RemoveAt(0);  // Remove from storage
                logger.ConsoleWriteLine("Consumed [" + number + "]", GetStorageVisualization(), ConsoleColor.Red);  // Log

                // Check emptyness 
                if (list.Count <= 0) IsEmpty = true;
                else IsEmpty = false;
            }
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

            // Get percentage of taken capacity
            int capacity = 0;
            if (GetSizeTaken() > 0)  // If size taken is greater then 0
            {
                float onePercentage = (float)(GetSize()) / 100;  // Get one percentage of capacity
                int numberOfPercentage = (int)(GetSizeTaken() / onePercentage);  // Get current percentage of capacity
                int tenTimes = numberOfPercentage / 10;  // How many times there is number 10
                int toWholeTen = (tenTimes + 1) * 10 - numberOfPercentage;  // How many we need to whole tens (from 8 to 10 we need 2)
                capacity = numberOfPercentage + toWholeTen;  // Count capacity rounded to tens
            }

            // Visualize full sectors
            for (int i = 0; i < (capacity / 10); i++)
            {
                visualization += fullSector;  // Add full sector character
            }

            // Visualize empty sectors
            for (int i = 0; i < ((100 - capacity) / 10); i++)
            {
                visualization += emptySector;  // Add empty sector character
            }

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
    }
}
