using LanguageIntegratedQuery.IOManager;
using LanguageIntegratedQuery.Model;

namespace LanguageIntegratedQuery.AppInteraction {
    internal class Task5 {
        /// <summary>
        /// Handles the execution of Task 5, which displays a table of product name, supplier name, and price
        /// for products with a price greater than 100, sorted by price, and joined with their suppliers.
        /// </summary>
        public static void HandleTask() {
            Console.Clear();
            var queryBuilder = new QueryBuilder<Product>(ProductList.products);

            var result = queryBuilder
                .Filter(p => p.Price > 100)
                .SortBy(p => p.Price)
                .Join(
                    SupplierList.suppliers,
                    product => product.ProductId,
                    supplier => supplier.ProductId,
                    (product, supplier) => new {
                        product.ProductName,
                        product.Price,
                        supplier.SupplierName
                    }
                );
            Console.WriteLine("=========== Custom Query Builder ==========\n");

            Console.WriteLine("{0,-25} | {1,-25} | {2,-10}", "Product Name", "Supplier Name", "Price");
            Console.WriteLine(new string('-', 65));

            foreach (var item in result) {
                dynamic row = item;
                Console.WriteLine("{0,-25} | {1,-25} | {2,-10}", row.ProductName, row.SupplierName, "$ " + row.Price);
            }

            InputManager.PromptForContinuation();
        }
    }
}
