using Task5.Model;

namespace Task5 {
    internal class Program {
        private static ListManager<string> _books = new ListManager<string>(new[] {
            "The beginning after the end",
            "Wind breaker"
        });
        private static StackManager<char> _stack = new StackManager<char>();
        private static QueueManager<string> _personQueue = new QueueManager<string>(new[] {
            "Sudharsan", "Arun", "Cheran", "Shivanandan", "Kamar"
        });
        private static RecordManager<string, int> _studentMarks = new RecordManager<string, int>();

        static Program() {
            _studentMarks.Add("Shivanandan", 40);
            _studentMarks.Add("Sudharsan", 50);
        }

        static void PrintHeading(string heading) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"=========== {heading} ===========\n");
            Console.ResetColor();
        }

        static void PrintSubheading(string subHeading) {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"=========== {subHeading} ===========\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Application entry point: displays the main menu and processes user choices.
        /// </summary>
        static void Main() {
            while (true) {
                Console.Clear();
                PrintHeading("Main Menu");
                Console.WriteLine("1. Demo Books");
                Console.WriteLine("2. Demo Palindrome Checker");
                Console.WriteLine("3. Demo Person Queue");
                Console.WriteLine("4. Demo Student Marks");
                Console.WriteLine("5. Exit");
                Console.Write("\nSelect an option: ");
                var choice = Console.ReadLine();
                switch (choice) {
                    case "1": BooksDemo(); break;
                    case "2": PalindromeDemo(); break;
                    case "3": PersonQueueDemo(); break;
                    case "4": StudentMarksDemo(); break;
                    case "5": return;
                    default: break;
                }
            }
        }

        /// <summary>
        /// Demonstrates list operations: display, add, remove, and contains on the book list.
        /// </summary>
        private static void BooksDemo() {
            Console.Clear();
            PrintHeading("Book Demo");
            PrintSubheading("Before adding books");
            _books.Display();
            Console.WriteLine();

            _books.Add("Death note");
            _books.Add("Teenage Mercenary");
            _books.Add("The baskerville's blood hound");
            _books.Add("Maze Runner");
            _books.Add("Kagurabachi");

            PrintSubheading("After adding books");
            _books.Display();
            Console.WriteLine();

            _books.Remove("The beginning after the end");
            PrintSubheading("After removing 'The beginning after the end'");
            _books.Display();
            Console.WriteLine();

            PrintSubheading("Checking if 'Wind breaker' exists");
            Console.WriteLine(_books.Contains("Wind breaker")
                ? "Wind breaker is there in the book list!"
                : "Wind breaker is not there in the book list!");
            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        /// <summary>
        /// Demonstrates palindrome checking using a stack: pushes characters and compares reversed string.
        /// </summary>
        private static void PalindromeDemo() {
            Console.Clear();
            PrintHeading("Palindrome Checker Demo");
            Console.Write("Enter a word: ");
            var input = Console.ReadLine() ?? string.Empty;

            _stack.Clear();
            foreach (var c in input) _stack.Push(c);

            var rev = string.Empty;
            while (_stack.Count > 0) rev += _stack.Pop();

            Console.WriteLine();
            Console.WriteLine(input.Equals(rev)
                ? $"{input} is a palindrome."
                : $"{input} is not a palindrome.");
            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        /// <summary>
        /// Demonstrates queue operations: display initial queue and dequeue one element.
        /// </summary>
        private static void PersonQueueDemo() {
            Console.Clear();
            PrintHeading("Person Queue Demo");
            PrintSubheading("Initial queue:");
            _personQueue.Display();
            Console.WriteLine();

            var dequeued = _personQueue.Dequeue();
            Console.WriteLine($"Dequeued: {dequeued}\n");

            PrintSubheading("Queue after dequeue:");
            _personQueue.Display();
            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        /// <summary>
        /// Demonstrates dictionary operations: display records, add and remove entries from student marks.
        /// </summary>
        private static void StudentMarksDemo() {
            Console.Clear();
            PrintHeading("Student Marks Demo");
            PrintSubheading("Before adding a student");
            _studentMarks.Display();
            Console.WriteLine();

            _studentMarks.Add("Arun", 50);
            PrintSubheading("After adding 'Arun'");
            _studentMarks.Display();
            Console.WriteLine();

            _studentMarks.Remove("Sudharsan");
            PrintSubheading("After removing 'Sudharsan'");
            _studentMarks.Display();
            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }
    }
}
