using System.Text.RegularExpressions;
using LanguageIntegratedQuery.IOManager;
using LanguageIntegratedQuery.Model;

namespace LanguageIntegratedQuery.AppInteraction {
    internal class Task2 {
        /// <summary>
        /// Handles the execution of Task 2, which includes grouping products by category 
        /// and performing an inner join between products and suppliers.
        /// </summary>
        public static void HandleTask() {
            Console.Clear();
            GroupByCategory();
            InnerJoinProductSupplier();
            InputManager.PromptForContinuation();
        }

        /// <summary>
        /// Groups products by category, displays the count of products in each category, 
        /// and displays the most expensive product in each category as a formatted table.
        /// </summary>
        private static void GroupByCategory() {
            // Question:
            // Group products by category and count the products in each category. 
            // Each group should also have the most expensive product in that category. 
            var groupedByCategory = ProductList.products
                .GroupBy(p => p.Category)
                .Select(g => new {
                    Category = g.Key,
                    ProductCount = g.Count(),
                    MostExpensiveProduct = g.OrderByDescending(p => p.Price).First()
                });

            Console.WriteLine("========== Group by Category ==========\n");
            Console.WriteLine("{0,-20} | {1,-13} | {2,-25} | {3,-10}", "Category", "Product Count", "Most Expensive Product", "Price");
            Console.WriteLine(new string('-', 75));

            foreach (var group in groupedByCategory) {
                Console.WriteLine(
                    "{0,-20} | {1,-13} | {2,-25} | {3,-10}",
                    group.Category,
                    group.ProductCount,
                    group.MostExpensiveProduct.ProductName,
                    "$ " + group.MostExpensiveProduct.Price
                );
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Performs an inner join between products and suppliers based on ProductId, 
        /// displaying matched product and supplier information as a formatted table.
        /// </summary>
        private static void InnerJoinProductSupplier() {
            // Question:
            // Perform an inner join with a List<Supplier>, where Supplier is a class with
            // properties SupplierId, SupplierName, and ProductId, to match products with their suppliers. 
            var joined = ProductList.products
                .Join(
                    SupplierList.suppliers,
                    product => product.ProductId,
                    supplier => supplier.ProductId,
                    (product, supplier) => new {
                        product.ProductId,
                        product.ProductName,
                        product.Category,
                        product.Price,
                        supplier.SupplierId,
                        supplier.SupplierName
                    });

            Console.WriteLine("========== Inner Join product with Supplier ==========\n");
            Console.WriteLine("{0,-5} | {1,-25} | {2,-15} | {3,10} | {4,-5} | {5,-20}",
                "ID", "Product Name", "Category", "Price", "SId", "Supplier Name");
            Console.WriteLine(new string('-', 90));

            foreach (var item in joined) {
                Console.WriteLine(
                    "{0,-5} | {1,-25} | {2,-15} | {3,-10} | {4,-5} | {5,-20}",
                    item.ProductId,
                    item.ProductName,
                    item.Category,
                    "$ " + item.Price,
                    item.SupplierId,
                    item.SupplierName
                );
            }
            Console.WriteLine();
        }
    }
}
