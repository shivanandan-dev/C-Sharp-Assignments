namespace Task3 {
    internal class Program {
        public static void Main() {
            int[] numbers = { 12, 5, 42, 3, 17, 8 };

            Array.Sort(
                numbers,
                delegate (int x, int y) {
                    return x.CompareTo(y);
                });

            Console.WriteLine("Sorted numbers: " + string.Join(", ", numbers));

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
