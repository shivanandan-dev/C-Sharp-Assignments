namespace GarbageCollection {
    class LargeObject {
        public byte[] Data = new byte[1024 * 1024];
    }

    class Program {
        /// <summary>
        /// Creates and destroys a large number of objects to demonstrate garbage collection.
        /// </summary>
        public static void CreateAndDestroyObjects(int count) {
            for (int i = 0; i < count; i++) {
                LargeObject obj = new LargeObject();
            }
        }

        static void Main() {
            Console.WriteLine("Starting memory usage: " + GC.GetTotalMemory(false) / (1024 * 1024) + " MB");

            Console.WriteLine("\nCreating and destroying large objects...");
            CreateAndDestroyObjects(1000);

            Console.WriteLine("Before GC.Collect, memory usage: " + GC.GetTotalMemory(false) / (1024 * 1024) + " MB");

            Console.WriteLine("\nForcing garbage collection...");
            GC.Collect();

            Console.WriteLine("After GC.Collect, memory usage: " + GC.GetTotalMemory(false) / (1024 * 1024) + " MB");

            Console.ReadKey();
        }
    }
}