namespace Task1 {
    internal class Program {
        private static List<string> _books = new List<string>() {
            "The beginning after the end",
            "Wind breaker",
        };

        /// <summary>
        /// Displays a menu header with the specified title.
        /// </summary>
        /// <param name="menuTitle">The title to display in the menu header.</param>
        private static void DisplayMenu(string menuTitle) {
            Console.WriteLine($"\n========== {menuTitle} ==========\n");
        }

        /// <summary>
        /// Writes each item in the provided list to the console.
        /// </summary>
        /// <param name="list">The list of strings to display.</param>
        private static void DisplayList(List<string> list) {
            foreach (string item in list) Console.WriteLine(item);
        }

        /// <summary>
        /// The entry point of the program that demonstrates adding, removing, and checking books in the list.
        /// </summary>
        public static void Main() {
            DisplayMenu("Before adding a book to the list");
            DisplayList(_books);

            _books.Add("Death note");
            _books.Add("Teenage Mercenary");
            _books.Add("The baskerville's blood hound");
            _books.Add("Maze Runner");
            _books.Add("Kagurabachi");

            DisplayMenu("After adding 5 books to the list");
            DisplayList(_books);

            _books.Remove("The beginning after the end");

            DisplayMenu("After removing: The beggining after the end book");
            DisplayList(_books);

            DisplayMenu("Checking if wind breaker book is there in the book list");
            if (_books.Contains("Wind breaker")) {
                Console.WriteLine("Wind breaker is there in the book list!");
            } else {
                Console.WriteLine("Wind breaker book is not there in the book list!");
            }


            Console.ReadKey();
        }
    }
}
