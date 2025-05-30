namespace Task2 {
    internal class Program {
        private static Stack<char> _stack = new Stack<char>();

        /// <summary>
        /// Program that checks if an input string is a palindrome using a stack.
        /// </summary>
        public static void Main() {
            Console.WriteLine("========== Palindrome Checker ==========");
            Console.Write("\nEnter a Word: ");
            string input = Console.ReadLine();

            foreach (char ch in input) {
                _stack.Push(ch);
            }

            string reverseString = string.Empty;

            while (_stack.Count > 0) {
                char c = _stack.Pop();
                reverseString += c;
            }

            if (input.Equals(reverseString)) {
                Console.WriteLine($"{input} is a Palindrome");
            } else {
                Console.WriteLine($"{input} is not a Palindrome");
            }

            Console.ReadKey();
        }
    }
}
