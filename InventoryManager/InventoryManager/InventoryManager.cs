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
        /// Displays a formatted list of products and provides sorting options to the user.
        /// </summary>
        /// <param name="productList">A list of Product objects to be displayed.</param>
        void DisplayProducts(List<Product> productList) {
            Console.WriteLine("{0, -20} | {1, -15} | {2, -30} | {3, -25}", "Id", "Name", "Price", "Quantity");
            Console.WriteLine(new string('-', 100));

            foreach (Product productInfo in productList) {
                Console.WriteLine("{0, -20} | {1, -15} | {2, -30} | {3, -25}",
                    productInfo.Id,
                    productInfo.Name,
                    productInfo.Price,
                    productInfo.Quantity
                );
            }

            Console.WriteLine("\n\n========== Sort By ==========\n");
            Console.WriteLine("1. Id");
            Console.WriteLine("2. Name");
            Console.WriteLine("3. Price");
            Console.WriteLine("4. Quantity");
            Console.WriteLine("5. Exit");
        }

        /// <summary>
        /// Sorts the product list based on the user's input.
        /// </summary>
        /// <param name="productList">A list of Product objects to be sorted.</param>
        /// <param name="input">The user's console key input indicating the sorting criterion.</param>
        /// <returns>A sorted list of Product objects.</returns>
        List<Product> SortProducts(List<Product> productList, ConsoleKey input) {
            // Dictionary of sorting actions
            var sortingActions = new Dictionary<ConsoleKey, Func<List<Product>, List<Product>>>
            {
                { ConsoleKey.D1, products => products.OrderBy(product => product.Id).ToList() },
                { ConsoleKey.NumPad1, products => products.OrderBy(product => product.Id).ToList() },
                { ConsoleKey.D2, products => products.OrderBy(product => product.Name).ToList() },
                { ConsoleKey.NumPad2, products => products.OrderBy(product => product.Name).ToList() },
                { ConsoleKey.D3, products => products.OrderBy(product => product.Price).ToList() },
                { ConsoleKey.NumPad3, products => products.OrderBy(product => product.Price).ToList() },
                { ConsoleKey.D4, products => products.OrderBy(product => product.Quantity).ToList() },
                { ConsoleKey.NumPad4, products => products.OrderBy(product => product.Quantity).ToList() },
            };

            if (sortingActions.ContainsKey(input)) {
                return sortingActions[input](productList);
            }

            return productList;
        }

        /// <summary>
        /// Provides a user interface for viewing and sorting a list of products.
        /// </summary>
        /// <param name="productList">A list of Product objects to be displayed and sorted.</param>
        void ViewProducts(List<Product> productList) {
            while (true) {
                Console.Clear();
                if (productList.Count == 0) {
                    Console.WriteLine("[Error] Product list is empty.");
                    return;
                }
                Console.WriteLine("========== Products ==========\n");
                DisplayProducts(productList);
                Console.WriteLine("\nPress (1-4) to Sort, (5) to Exit:");
                ConsoleKey input = Console.ReadKey().Key;

                if (input == ConsoleKey.D5 || input == ConsoleKey.NumPad5) {
                    return;
                }

                productList = SortProducts(productList, input);
            }
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
                { 2, () => manager.ViewProducts(products) },
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