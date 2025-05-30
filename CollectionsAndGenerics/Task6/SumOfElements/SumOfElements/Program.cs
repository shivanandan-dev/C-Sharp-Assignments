namespace SumOfElements {
    class Program {
        /// <summary>
        /// Computes and returns the sum of all integers in the provided sequence.
        /// Works with any IEnumerable<int> (List, Array, Queue, etc.).
        /// </summary>
        public static int SumOfElements(IEnumerable<int> source) {
            if (source == null) throw new ArgumentNullException(nameof(source));

            int sum = 0;
            foreach (var n in source)
                sum += n;
            return sum;
        }

        static void Main() {
            var list = new List<int> { 1, 2, 3 };
            var array = new int[] { 4, 5, 6 };
            var queue = new Queue<int>(new[] { 7, 8, 9 });

            Console.WriteLine($"Sum of [{string.Join(", ", list)}] = {SumOfElements(list)}");
            Console.WriteLine($"Sum of [{string.Join(", ", array)}] = {SumOfElements(array)}");
            Console.WriteLine($"Sum of [{string.Join(", ", queue)}] = {SumOfElements(queue)}");
            Console.ReadKey();
        }
    }

}
