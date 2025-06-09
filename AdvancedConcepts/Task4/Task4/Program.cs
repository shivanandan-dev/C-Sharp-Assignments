namespace Task4 {
    internal class Program {
        public static void Main() {
            var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var evens = numbers.Where(n => n % 2 == 0);

            var squares = evens.Select(n => {
                int result = n * n;
                return result;
            });

            Console.WriteLine("Squared evens: " + string.Join(", ", squares));

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
