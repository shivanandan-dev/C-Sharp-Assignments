namespace InventoryManager {
    public class Program {
        /// <summary>
        /// The main entry point of the application. Handles menu navigation and user input for managing products.
        /// </summary>
        /// <param name="args">Command-line arguments (not used).</param>
        public static void Main(string[] args) {
            InventoryManager manager = new InventoryManager();

            // NOTE: The following default data is added for testing and debugging purposes only.
            // These entries should be removed or commented out before deploying the application to production.
            InventoryManager.Products.Add(new Product("1", "Oreo Buiscuit", 20.03m, 3));
            InventoryManager.Products.Add(new Product("2", "Dairy Milk", 30m, 5));
            InventoryManager.Products.Add(new Product("3", "Kit Kat", 40.33m, 2));
            InventoryManager.Products.Add(new Product("4", "Bourbon", 50m, 5));

            var actions = new Dictionary<int, Action> {
                { 1, () => manager.AddNewProduct() },
                { 2, () => manager.ViewProducts() },
                { 3, () => manager.SearchProduct() },
                { 4, () => manager.EditProduct() },
                { 5, () => manager.DeleteProduct() },
                { 6, () => manager.ExitEnvironment()}
            };

            do {
                Console.Clear();
                manager.MainMenu();
                Console.Write("\n[Menu] Enter your choice: ");

                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out int choice);


                if (isNumber && actions.ContainsKey(choice)) {
                    Console.Clear();
                    actions[choice].Invoke();
                } else {
                    Console.WriteLine("[Error] Invalid choice!");
                }

                manager.IsDeleteSuccessful = true;
                manager.IsEditSuccessful = true;
                manager.PromptForContinuation();
                Console.Clear();
            } while (true);
        }
    }
}
