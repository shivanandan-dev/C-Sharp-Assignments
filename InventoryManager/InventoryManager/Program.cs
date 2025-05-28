namespace InventoryManager {
    public class Program {
        /// <summary>
        /// The main entry point of the application. Handles menu navigation and user input for managing products.
        /// </summary>
        public static void Main() {
            InventoryManager inventoryManager = new InventoryManager();

            // NOTE: The following default data is added for testing and debugging purposes only.
            // These entries should be removed or commented out before deploying the application to production.
            InventoryManager.Products.Add(new ProductDetails("1", "Oreo Buiscuit", 20.03m, 3));
            InventoryManager.Products.Add(new ProductDetails("2", "Dairy Milk", 30m, 5));
            InventoryManager.Products.Add(new ProductDetails("3", "Kit Kat", 40.33m, 2));
            InventoryManager.Products.Add(new ProductDetails("4", "Bourbon", 50m, 5));

            var mainMenuActions = new List<Menu> {
                new Menu ( "Add New Product", inventoryManager.AddNewProduct ),
                new Menu ( "View Products", inventoryManager.ViewProducts ),
                new Menu ( "Search Product",  inventoryManager.SearchProduct ),
                new Menu ( "Edit Product",  inventoryManager.EditProduct ),
                new Menu ( "Delete Product", inventoryManager.DeleteProduct ),
                new Menu ( "Exit", () => inventoryManager.ExitEnvironment())
            };

            inventoryManager.HandleMenuActions(mainMenuActions, false, null, true);
        }
    }
}
