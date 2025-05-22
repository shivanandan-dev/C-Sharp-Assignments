namespace InventoryManager {
    internal class InventoryManager {
        public static List<ProductDetails> Products = new List<ProductDetails>();
        public bool IsOperationSuccessful = false;

        /// <summary>
        /// Prompts the user to add a new product with details like ID, name, price, and quantity.
        /// Validates and stores the new product in the product list.
        /// </summary>
        public void AddNewProduct() {
            Console.WriteLine("========== Add new Product ==========\n");
            string id = GetInformation("Id", true);
            string name = GetInformation("Name", true);
            string price = GetInformation("Price", false);
            string quantity = GetInformation("Quantity", false);

            Products.Add(new ProductDetails(id, name, decimal.Parse(price), int.Parse(quantity)));
            Console.WriteLine("[Success] New product is created successfully!");
        }

        /// <summary>
        /// Displays the main menu with available options for managing products.
        /// </summary>
        public void MainMenu() {
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
        public void ExitEnvironment() {
            Console.WriteLine("\nBye Bye...");
            Console.ReadKey();
            Environment.Exit(0);
        }

        /// <summary>
        /// Provides a user interface for viewing and sorting a list of products.
        /// </summary>
        /// <param name="productList">A list of Product objects to be displayed and sorted.</param>
        public void ViewProducts() {
            while (true) {
                Console.Clear();
                if (Products.Count == 0) {
                    Console.WriteLine("[Error] Product list is empty.");
                    return;
                }
                Console.WriteLine("========== Products ==========\n");
                DisplayProducts(Products);
                Console.WriteLine("\nPress (1-4) to Sort, (5) to Exit:");
                ConsoleKey input = Console.ReadKey().Key;

                if (input == ConsoleKey.D5 || input == ConsoleKey.NumPad5) {
                    return;
                }

                Products = SortProducts(Products, input);
            }
        }

        /// <summary>
        /// Facilitates the product search process by displaying a menu and handling user input.
        /// </summary>
        public void SearchProduct() {
            var searchMenuActions = new Dictionary<int, Action> {
                { 1, () => SearchProductBy("Id")},
                { 2, () => SearchProductBy("Name")},
            };

            do {
                Console.Clear();
                SearchProductMenu();
                Console.Write("\n[Menu] Enter your choice: ");

                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out int choice);

                if (isNumber && searchMenuActions.ContainsKey(choice)) {
                    searchMenuActions[choice].Invoke();
                } else if (choice == 3) {
                    return;
                } else {
                    Console.WriteLine("[Error] Invalid choice!");
                }

                PromptForContinuation();
            } while (true);
        }

        /// <summary>
        /// Facilitates the product editing process by displaying a menu and handling user input.
        /// </summary>
        public void EditProduct() {
            var editMenuActions = new Dictionary<int, Action> {
                { 1, () => EditProductBy("Id") },
                { 2, () => EditProductBy("Name") },
            };

            HandleEditOrDeleteOperation(DisplayProductByMenu, editMenuActions);
        }

        /// <summary>
        /// Displays the menu options for deleting a product.
        /// </summary>
        public void DeleteProductMenu() {
            Console.WriteLine("\n========== Delete Product ==========\n");
            Console.WriteLine("1. Delete Product by Id");
            Console.WriteLine("2. Delete Product by Name");
            Console.WriteLine("3. Main Menu");
        }

        /// <summary>
        /// Facilitates the product deletion process by displaying a menu and handling user input.
        /// </summary>
        public void DeleteProduct() {
            var deleteMenuActions = new Dictionary<int, Action> {
                { 1, () => DeleteProductBy("Id") },
                { 2, () => DeleteProductBy("Name") },
            };

            HandleEditOrDeleteOperation(DeleteProductMenu, deleteMenuActions);
        }

        /// <summary>
        /// Prompts the user to press any key to continue.
        /// </summary>
        public void PromptForContinuation() {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Checks if a product with the given name or ID already exists in the list.
        /// </summary>
        /// <param name="value">The name or ID of the product to check.</param>
        /// <returns>True if the product exists; otherwise, false.</returns>
        bool IsProductAlreadyExist(string value) {
            foreach (ProductDetails productInfo in Products) {
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
                "Name" => Validator.IsNameValid(input)
                    ? string.Empty
                    : "Name must contain only letters and spaces.",
                "Price" => Validator.IsDecimal(input)
                    ? string.Empty
                    : "Price must be a positive decimal.",
                "Quantity" => Validator.IsPositiveInteger(input)
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
        /// Displays a formatted list of products and provides sorting options to the user.
        /// </summary>
        /// <param name="productList">A list of Product objects to be displayed.</param>
        void DisplayProducts(List<ProductDetails> productList) {
            Console.WriteLine("{0, -20} | {1, -15} | {2, -30} | {3, -25}", "Id", "Name", "Price", "Quantity");
            Console.WriteLine(new string('-', 100));

            foreach (ProductDetails productInfo in productList) {
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
        List<ProductDetails> SortProducts(List<ProductDetails> productList, ConsoleKey input) {
            // Dictionary of sorting actions
            var sortingActions = new Dictionary<ConsoleKey, Func<List<ProductDetails>, List<ProductDetails>>>
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
        /// Finds a product by a specific attribute and value.
        /// </summary>
        /// <param name="attribute">The attribute to search by (e.g., "Id" or "Name").</param>
        /// <param name="value">The value of the attribute to match.</param>
        /// <returns>The Product object if found; otherwise, null.</returns>
        ProductDetails FindProductByAttribute(string attribute, string value) {
            return Products.Find(product =>
                (attribute.Equals("Id", StringComparison.OrdinalIgnoreCase) &&
                product.Id.Equals(value, StringComparison.OrdinalIgnoreCase)) ||
                (attribute.Equals("Name", StringComparison.OrdinalIgnoreCase) &&
                product.Name.Equals(value, StringComparison.OrdinalIgnoreCase))
            );
        }

        /// <summary>
        /// Displays the search menu for the product search feature.
        /// </summary>
        void SearchProductMenu() {
            Console.WriteLine("\n========== Search Product ==========\n");
            Console.WriteLine("1. Search by Id");
            Console.WriteLine("2. Search by Name");
            Console.WriteLine("3. Main Menu");
        }

        /// <summary>
        /// Displays the details of a product in a formatted manner.
        /// </summary>
        /// <param name="productInfo">The Product object containing the details to display.</param>
        void DisplayDetails(ProductDetails productInfo) {
            Console.WriteLine("\n========== Details ==========\n");
            Console.WriteLine("{0,-9}: {1}", "Id", productInfo.Id);
            Console.WriteLine("{0,-9}: {1}", "Name", productInfo.Name);
            Console.WriteLine("{0,-9}: {1}", "Price", productInfo.Price);
            Console.WriteLine("{0,-9}: {1}", "Quantity", productInfo.Quantity);
        }

        /// <summary>
        /// Searches for a product based on the specified attribute and displays the result.
        /// </summary>
        /// <param name="informationType">The type of information to search by (e.g., "Id" or "Name").</param>
        void SearchProductBy(string informationType) {
            string input = GetInformation(informationType);
            ProductDetails productInfo = FindProductByAttribute(informationType, input);
            if (productInfo == null) {
                Console.WriteLine("[Error] No Product Found");
            } else {
                DisplayDetails(productInfo);
            }
        }

        /// <summary>
        /// Displays the menu options for editing fields of a product.
        /// </summary>
        void DisplayEditByMenu() {
            Console.WriteLine("\n========== Edit ==========\n");
            Console.WriteLine("1. Edit Id");
            Console.WriteLine("2. Edit Name");
            Console.WriteLine("3. Edit Price");
            Console.WriteLine("4. Edit Quantity");
            Console.WriteLine("5. Edit Menu");
        }

        /// <summary>
        /// Displays the menu options for finding a product by specific attributes for editing.
        /// </summary>
        void DisplayProductByMenu() {
            Console.WriteLine("\n========== Edit Product ==========\n");
            Console.WriteLine("1. Find Product by Id");
            Console.WriteLine("2. Find Product by Name");
            Console.WriteLine("3. Main Menu");
        }

        /// <summary>
        /// Updates a specific field of the product and validates for duplicates if required.
        /// </summary>
        /// <param name="fieldName">The name of the field to update (e.g., "Id", "Name").</param>
        /// <param name="checkForDuplicate">Indicates whether to check for duplicate values.</param>
        /// <param name="updateAction">The action to update the specified field.</param>
        void UpdateProductField(string fieldName, bool checkForDuplicate, Action<string> updateAction) {
            string updatedValue = GetInformation(fieldName, checkForDuplicate);
            updateAction(updatedValue);
            Console.WriteLine($"[Success] {fieldName} updated successfully!");
            IsOperationSuccessful = true;
        }

        /// <summary>
        /// Handles the edit or delete operation by displaying a menu and executing the selected action.
        /// </summary>
        /// <param name="menuDisplayAction">The action to display the appropriate menu.</param>
        /// <param name="menuActions">A dictionary of actions corresponding to menu choices.</param>
        /// <param name="isOperationSuccessful">A reference to the flag indicating whether the operation was successful.</param>
        void HandleEditOrDeleteOperation(Action menuDisplayAction, Dictionary<int, Action> menuActions) {
            do {
                Console.Clear();
                menuDisplayAction(); // Display the menu
                Console.WriteLine("");
                Console.Write("[Menu] Enter your choice: ");
                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out int choice);

                if (isNumber && menuActions.ContainsKey(choice)) {
                    menuActions[choice].Invoke(); // Execute the selected action
                } else if (isNumber && choice == 3) { // Exit option
                    return;
                } else {
                    Console.WriteLine("[Error] Invalid choice!");
                    PromptForContinuation();
                }

            } while (!IsOperationSuccessful);
        }

        /// <summary>
        /// Handles the edit or delete operation for a specific product by finding it using a given attribute.
        /// </summary>
        /// <param name="ProductBy">The attribute to find the product by (e.g., "Id", "Name").</param>
        /// <param name="action">The action to perform on the found product.</param>
        /// <param name="isOperationSuccessful">A reference to the flag indicating whether the operation was successful.</param>
        void HandleEditOrDeleteProductBy(string ProductBy, Action<ProductDetails> action) {
            do {
                string input = GetInformation(ProductBy);
                ProductDetails product = FindProductByAttribute(ProductBy, input);

                if (product == null) {
                    Console.WriteLine("[Error] No Product Found");
                    PromptForContinuation();
                    return;
                } else {
                    action(product);
                }
            } while (!IsOperationSuccessful);
        }

        /// <summary>
        /// Edits a specific product by allowing the user to update its fields.
        /// </summary>
        /// <param name="ProductToEdit">The product to edit.</param>
        void EditBy(ProductDetails ProductToEdit) {
            var EditByMenuActions = new Dictionary<int, Action> {
                { 1, () => UpdateProductField("Id", true, value => ProductToEdit.Id = value)},
                { 2, () => UpdateProductField("Name", true, value => ProductToEdit.Name = value)},
                { 3, () => UpdateProductField("Price", false, value => ProductToEdit.Price = decimal.Parse(value))},
                { 4, () => UpdateProductField("Quantity", false, value => ProductToEdit.Quantity = int.Parse(value))},
            };

            bool isValidChoice = false;
            Console.Clear();
            DisplayDetails(ProductToEdit);
            DisplayEditByMenu();

            do {
                Console.Write("\n[Menu] Enter your choice: ");
                string input = Console.ReadLine();
                isValidChoice = int.TryParse(input, out int choice);

                if (!isValidChoice) {
                    Console.WriteLine("[Error] Invalid input. Please enter a number.");
                    continue;
                }

                if (isValidChoice && EditByMenuActions.ContainsKey(choice)) {
                    EditByMenuActions[choice].Invoke();
                } else if (isValidChoice && choice == 5) {
                    return;
                } else {
                    Console.WriteLine("[Error] Invalid choice. Please select a valid option.");
                }
            } while (!IsOperationSuccessful);
        }

        /// <summary>
        /// Edits a product by finding it using a specified attribute.
        /// </summary>
        /// <param name="ProductBy">The attribute to find the product by (e.g., "Id", "Name").</param>
        void EditProductBy(string ProductBy) {
            HandleEditOrDeleteProductBy(ProductBy, EditBy);
        }

        /// <summary>
        /// Deletes a specific product from the product list.
        /// </summary>
        /// <param name="ProductToDelete">The product to delete.</param>
        void Delete(ProductDetails ProductToDelete) {
            Products.Remove(ProductToDelete);
            Console.WriteLine("[Success] Product Deleted Successfully");
            IsOperationSuccessful = true;
        }

        /// <summary>
        /// Deletes a product by finding it using a specified attribute.
        /// </summary>
        /// <param name="ProductBy">The attribute to find the product by (e.g., "Id", "Name").</param>
        void DeleteProductBy(string ProductBy) {
            HandleEditOrDeleteProductBy(ProductBy, Delete);
        }
    }
}