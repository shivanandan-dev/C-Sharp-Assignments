namespace Task3 {
    internal class Program {
        private static Queue<string> personQueue = new Queue<string>();

        /// <summary>
        /// Displays a formatted menu header with the specified title.
        /// </summary>
        /// <param name="menuTitle">The title to display in the menu header.</param>
        public static void DisplayMenu(string menuTitle) {
            Console.WriteLine($"\n============= {menuTitle} =============\n");
        }

        /// <summary>
        /// Writes each person in the queue to the console.
        /// </summary>
        public static void DisplayQueue() {
            foreach (var person in personQueue) {
                Console.WriteLine(person);
            }
        }

        /// <summary>
        /// Entry point of the application demonstrating queue operations.
        /// </summary>
        public static void Main() {
            personQueue.Enqueue("Sudharsan");
            personQueue.Enqueue("Arun");
            personQueue.Enqueue("Cheran");
            personQueue.Enqueue("Shivanandan");
            personQueue.Enqueue("Kamar");

            DisplayMenu("Queue");
            DisplayQueue();

            string personDequeued = personQueue.Dequeue();
            DisplayMenu($"Dequeued the first person: {personDequeued}");
            DisplayQueue();

            Console.ReadKey();
        }
    }
}
}
