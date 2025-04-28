namespace InventoryManager {
    internal class InventoryManager {
        static List<Product> products = new List<Product>();
        Validator validator = new Validator();

        /// <summary>
        /// Checks if a product with the given name or ID already exists in the list.
        /// </summary>
        /// <param name="value">The name or ID of the product to check.</param>
        /// <returns>True if the product exists; otherwise, false.</returns>
        bool IsProductAlreadyExist(string value) {
            foreach (Product productInfo in products) {
                if (productInfo.Name == value || productInfo.Id == value) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Continuously prompts the user for input until a non-existing product name or ID is entered.
        /// </summary>
        /// <param name="informationType">Type of information to request (e.g., "Name" or "Id").</param>
        /// <param name="isProductAlreadyExist">Outputs whether the product already exists.</param>
        /// <returns>The valid input string.</returns>
        string GetProductUntilNotExist(string informationType, out bool isProductAlreadyExist) {
            string input;

            do {
                isProductAlreadyExist = false;
                Console.Write($"Enter {informationType}: ");
                input = Console.ReadLine();
                isProductAlreadyExist = IsProductAlreadyExist(input);
                if (input == "")
                    Console.WriteLine($"[Error] {informationType} cannot be empty!");
                else if (isProductAlreadyExist) {
                    Console.WriteLine($"[Error] A product with this {informationType} already exists.");
                }
            } while (input == "" || isProductAlreadyExist);

            return input;
        }

        /// <summary>
        /// Validates the given input string based on its type (e.g., "Name", "Price", "Quantity").
        /// </summary>
        /// <param name="informationType">The type of information being validated.</param>
        /// <param name="input">The input string to validate.</param>
        /// <returns>An error message if validation fails; otherwise, an empty string.</returns>
        string ValidateInput(string informationType, string input) {
            return informationType switch {
                "Name" => validator.IsNameValid(input)
                    ? string.Empty
                    : "Name must contain only letters and spaces.",
                "Price" => validator.IsDecimal(input)
                    ? string.Empty
                    : "Price must be a positive decimal.",
                "Quantity" => validator.IsPositiveInteger(input)
                    ? string.Empty
                    : "Quanity must be a positive number.",
                _ => string.Empty
            };
        }

        /// <summary>
        /// Prompts the user for information and validates the input based on the given type.
        /// Optionally checks for duplicate entries in the product list.
        /// </summary>
        /// <param name="informationType">The type of information being requested (e.g., "Name", "Id").</param>
        /// <param name="checkForDuplicate">Indicates whether to check for duplicate entries.</param>
        /// <returns>The valid input string.</returns>
        string GetInformation(string informationType, bool checkForDuplicate = false) {
            string input;
            do {
                if (checkForDuplicate) {
                    input = GetProductUntilNotExist(informationType, out bool isProductAlreadyExist);
                } else {
                    Console.Write($"Enter {informationType}: ");
                    input = Console.ReadLine();
                }

                string invalidMessage = ValidateInput(informationType, input);
                if (invalidMessage == "")
                    break;

                Console.WriteLine($"[Error] {invalidMessage}");
            } while (true);

            return input;
        }

        /// <summary>
        /// Prompts the user to press any key to continue.
        /// </summary>
        void PromptForContinuation() {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Prompts the user to add a new product with details like ID, name, price, and quantity.
        /// Validates and stores the new product in the product list.
        /// </summary>
        void AddNewProduct() {
            Console.WriteLine("========== Add new Product ==========\n");
            string id = GetInformation("Id", true);
            string name = GetInformation("Name", true);
            string price = GetInformation("Price", false);
            string quantity = GetInformation("Quantity", false);

            products.Add(new Product(id, name, decimal.Parse(price), int.Parse(quantity)));
            Console.WriteLine("[Success] New product is created successfully!");
        }

        /// <summary>
        /// Displays the main menu with available options for managing products.
        /// </summary>
        void MainMenu() {
            Console.WriteLine("========== Product Manager ==========\n");
            Console.WriteLine("1. Add a New Product");
            Console.WriteLine("2. View Products");
            Console.WriteLine("3. Search a Product");
            Console.WriteLine("4. Edit a Product");
            Console.WriteLine("5. Delete a Product");
            Console.WriteLine("6. Exit");
        }

        /// <summary>
        /// Exits the application gracefully with a farewell message.
        /// </summary>
        void ExitEnvironment() {
            Console.WriteLine("\nBye Bye...");
            Console.ReadKey();
            Environment.Exit(0);
        }

        /// <summary>
        /// The main entry point of the application. Handles menu navigation and user input for managing products.
        /// </summary>
        /// <param name="args">Command-line arguments (not used).</param>
        public static void Main(string[] args) {
            InventoryManager manager = new InventoryManager();

            // NOTE: The following default data is added for testing and debugging purposes only.
            // These entries should be removed or commented out before deploying the application to production.
            products.Add(new Product("1", "Oreo Buiscuit", 20.03m, 3));
            products.Add(new Product("2", "Dairy Milk", 30m, 5));
            products.Add(new Product("3", "Kit Kat", 40.33m, 2));
            products.Add(new Product("4", "Bourbon", 50m, 5));

            var actions = new Dictionary<int, Action> {
                { 1, () => manager.AddNewProduct() },
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

                manager.PromptForContinuation();
                Console.Clear();
            } while (true);
        }
    }
}