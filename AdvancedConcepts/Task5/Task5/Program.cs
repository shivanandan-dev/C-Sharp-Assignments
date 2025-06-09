using Task5.Model;

namespace Task5 {
    /// <summary>
    /// Defines a method that compares two <see cref="Product"/> instances and returns 
    /// a value indicating their relative order.
    /// </summary>
    /// <param name="x">The first <see cref="Product"/> to compare.</param>
    /// <param name="y">The second <see cref="Product"/> to compare.</param>
    public delegate int SortDelegate(Product x, Product y);

    internal class Program {
        /// <summary>
        /// Application entry point.
        /// </summary>
        public static void Main() {
            var products = new List<Product>
            {
                new("Laptop", "Electronics", 999.99m),
                new("Banana", "Groceries", 0.59m),
                new("T-Shirt", "Apparel", 19.99m),
                new("Headphones", "Electronics", 49.99m),
                new("Apples", "Groceries", 1.29m),
                new("Jeans", "Apparel", 39.99m),
            };

            SortDelegate byName = SortByName;
            SortDelegate byCategory = SortByCategory;
            SortDelegate byPrice = SortByPrice;

            Console.WriteLine("=== Sort by Name ===");
            SortAndDisplay(byName, new List<Product>(products));

            Console.WriteLine("\n=== Sort by Category ===");
            SortAndDisplay(byCategory, new List<Product>(products));

            Console.WriteLine("\n=== Sort by Price ===");
            SortAndDisplay(byPrice, new List<Product>(products));

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// Sorts the provided list of products using the given sorter delegate, then writes each product to the console.
        /// </summary>
        /// <param name="sorter">The delegate that defines the comparison logic between two <see cref="Product"/> objects.</param>
        /// <param name="items">The list of <see cref="Product"/> objects to sort and display.</param>
        private static void SortAndDisplay(SortDelegate sorter, List<Product> items) {
            items.Sort((x, y) => sorter(x, y));

            foreach (var p in items) {
                Console.WriteLine(p);
            }
        }

        /// <summary>
        /// Compares two products by their <see cref="Product.Name"/> (alphabetical order, case-insensitive).
        /// </summary>
        /// <param name="a">The first <see cref="Product"/> to compare.</param>
        /// <param name="b">The second <see cref="Product"/> to compare.</param>
        /// <returns>
        /// A negative number if <paramref name="a"/> precedes <paramref name="b"/>,
        /// zero if they are equal,
        /// or a positive number if <paramref name="a"/> follows <paramref name="b"/>.
        /// </returns>
        private static int SortByName(Product a, Product b) => string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Compares two products by their <see cref="Product.Category"/> (alphabetical order, case-insensitive).
        /// </summary>
        /// <param name="a">The first <see cref="Product"/> to compare.</param>
        /// <param name="b">The second <see cref="Product"/> to compare.</param>
        /// <returns>
        /// A negative number if <paramref name="a"/> precedes <paramref name="b"/>,
        /// zero if they are equal,
        /// or a positive number if <paramref name="a"/> follows <paramref name="b"/>.
        /// </returns>
        private static int SortByCategory(Product a, Product b) => string.Compare(a.Category, b.Category, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Compares two products by their <see cref="Product.Price"/> (ascending numeric order).
        /// </summary>
        /// <param name="a">The first <see cref="Product"/> to compare.</param>
        /// <param name="b">The second <see cref="Product"/> to compare.</param>
        /// <returns>
        /// A negative number if <paramref name="a"/> is cheaper than <paramref name="b"/>,
        /// zero if they have the same price,
        /// or a positive number if <paramref name="a"/> is more expensive than <paramref name="b"/>.
        /// </returns>
        private static int SortByPrice(Product a, Product b) => a.Price.CompareTo(b.Price);
    }
}
