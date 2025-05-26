namespace Task2 {
    class Program {
        static void Main(string[] args) {
            Console.Write("Enter the size of each array (e.g. 1000): ");
            int bufferSize = int.Parse(Console.ReadLine()!);

            Console.Write("Enter how many arrays to keep in memory (e.g. 100): ");
            int maxBuffers = int.Parse(Console.ReadLine()!);

            var memAlloc = new List<int[]>();

            Console.WriteLine("\nPress any key to start allocating...");
            Console.ReadKey();

            while (true) {
                memAlloc.Add(new int[bufferSize]);

                if (memAlloc.Count > maxBuffers) {
                    memAlloc.RemoveAt(0);
                }
                Thread.Sleep(10);
            }
        }
    }
}
