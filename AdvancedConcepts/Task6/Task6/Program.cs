namespace Task6 {
    /// <summary>
    /// Represents a book with a title, author, and ISBN.
    /// Immutable by design.
    /// </summary>
    public record Book(string Title, string Author, string ISBN) { }

    internal class Program {
        static void Main() {
            var book1 = new Book("1984", "George Orwell", "1234567890");
            var book2 = new Book("Brave New World", "Aldous Huxley", "0987654321");
            var book3 = new Book("1984", "George Orwell", "1234567890");

            Console.WriteLine("--- Book Details ---");
            DisplayBook(book1);
            DisplayBook(book2);

            Console.WriteLine($"\nbook1 == book3: {book1 == book3}");

            // Show immutability (the following line would now be a compile-time error)
            // book1.Title = "Animal Farm";

            var modifiedBook = book1 with { Title = "Animal Farm" };

            Console.WriteLine("\n--- After 'with' Modification ---");
            DisplayBook(book1);
            DisplayBook(modifiedBook);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// Prints the properties of a Book record using deconstruction.
        /// </summary>
        /// <param name="book">The Book record to display.</param>
        private static void DisplayBook(Book book) {
            var (title, author, isbn) = book;
            Console.WriteLine($"Title: {title}, Author: {author}, ISBN: {isbn}");
        }
    }
}
