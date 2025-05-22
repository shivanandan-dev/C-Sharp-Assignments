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

            var menuActions = new Dictionary<int, Action> {
                { 1, () => inventoryManager.AddNewProduct() },
                { 2, () => inventoryManager.ViewProducts() },
                { 3, () => inventoryManager.SearchProduct() },
                { 4, () => inventoryManager.EditProduct() },
                { 5, () => inventoryManager.DeleteProduct() },
                { 6, () => inventoryManager.ExitEnvironment()}
            };

            do {
                Console.Clear();
                inventoryManager.MainMenu();
                Console.Write("\n[Menu] Enter your choice: ");

                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out int choice);


                if (isNumber && menuActions.ContainsKey(choice)) {
                    Console.Clear();
                    menuActions[choice].Invoke();
                } else {
                    Console.WriteLine("[Error] Invalid choice!");
                }

                inventoryManager.IsOperationSuccessful = true;
                inventoryManager.PromptForContinuation();
                Console.Clear();
            } while (true);
        }
    }
}
