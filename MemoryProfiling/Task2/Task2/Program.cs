namespace Task2 {
    class Program {
        private const int BufferSize = 1000;
        private const int MaxBuffers = 100;

        static void Main(string[] args) {
            var memAlloc = new List<int[]>();

            try {
                Console.WriteLine("Buffer Size: " + BufferSize);
                Console.WriteLine("Max Buffer: " + MaxBuffers);
                Console.WriteLine("\nStarted Allocating (Press \"ESC\" to exit..)");
                while (true) {
                    if (Console.KeyAvailable) {
                        var key = Console.ReadKey(intercept: true);
                        if (key.Key == ConsoleKey.Escape) {
                            Console.WriteLine("Escape pressed; exiting loop...");
                            break;
                        }
                    }

                    memAlloc.Add(new int[BufferSize]);

                    if (memAlloc.Count > MaxBuffers) {
                        memAlloc.RemoveAt(0);
                    }
                    Thread.Sleep(10);
                }
            } finally {
                memAlloc.Clear();
                Console.WriteLine("All buffers cleared. Exiting.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
