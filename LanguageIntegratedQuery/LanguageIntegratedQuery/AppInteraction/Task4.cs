using LanguageIntegratedQuery.IOManager;
using LanguageIntegratedQuery.Model;

namespace LanguageIntegratedQuery.AppInteraction {
    internal class Task4 {
        /// <summary>
        /// Displays all products under the "Books" category,
        /// sorted by price in descending order, in a tabular format.
        /// </summary>
        public static void HandleTask() {
            Console.Clear();
            // Question:
            // One that selects all products under the category "Books" and sorts them by price. 
            var books = ProductList.products
                .Where(product => product.Category == Category.Books)
                .OrderByDescending(product => product.Price);

            Console.WriteLine("========== Books (Price - Descending Order) ==========\n");
            Console.WriteLine("{0,-10} | {1,-30} | {2,-10}", "ProductId", "ProductName", "Price");
            Console.WriteLine(new string('-', 55));

            foreach (var book in books) {
                Console.WriteLine("{0,-10} | {1,-30} | {2,-10}", book.ProductId, book.ProductName, "$ " + book.Price);
            }

            InputManager.PromptForContinuation();
        }
    }
}
