namespace InventoryManager {
    internal class InventoryManager {
        public static List<ProductDetails> Products = new List<ProductDetails>();
        public bool IsOperationSuccessful = false;

        /// <summary>
        /// Handles display and user selection for a menu, invoking the selected action.
        /// </summary>
        /// <param name="menuActions">A list of menu actions to present to the user.</param>
        public void HandleMenuActions(List<Menu> menuActions) {
            do {
                Console.Clear();
                DisplayMenuOptions(menuActions);
                Console.Write("\n[Menu] Enter your choice: ");

                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out int choice);

                if (isNumber && choice > 0 && choice <= menuActions.Count) {
                    if (choice == menuActions.Count)
                        return;
                    menuActions[choice - 1].Handler.Invoke();
                } else {
                    Console.WriteLine("[Error] Invalid choice!");
                }

                PromptForContinuation();
            } while (true);
        }

        /// <summary>
        /// Displays the given menu options with an optional title.
        /// </summary>
        /// <param name="menuActions">The menu actions to display.</param>
        /// <param name="menuTitle">The title of the menu.</param>
        public void DisplayMenuOptions(List<Menu> menuActions, string menuTitle = "Menu") {
            Console.WriteLine($"========== {menuTitle} ==========\n");
            for (int index = 0; index < menuActions.Count; index++) {
                Console.WriteLine($"{index + 1}. {menuActions[index].Description}");
            }
        }

        /// <summary>
        /// Prompts the user for product details and adds a new product to the list.
        /// </summary>
        public void AddNewProduct() {
            Console.WriteLine("========== Add new Product ==========\n");
            string id = GetProductInformation("Id", true);
            string name = GetProductInformation("Name", true);
            string price = GetProductInformation("Price", false);
            string quantity = GetProductInformation("Quantity", false);

            Products.Add(new ProductDetails(id, name, decimal.Parse(price), int.Parse(quantity)));
            Console.WriteLine("[Success] New product is created successfully!");
        }

        /// <summary>
        /// Exits the application.
        /// </summary>
        public void ExitEnvironment() {
            Console.WriteLine("\nBye Bye...");
            Console.ReadKey();
            Environment.Exit(0);
        }

        /// <summary>
        /// Displays all products and allows the user to sort them.
        /// </summary>
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

                if (input == ConsoleKey.D5 || input == ConsoleKey.NumPad5)
                    return;

                Products = SortProducts(Products, input);
            }
        }

        /// <summary>
        /// Displays the product search menu and handles user selection.
        /// </summary>
        public void SearchProduct() {
            var searchMenuActions = new List<Menu>
            {
                new Menu("Search Product by Id", () => SearchProductBy("Id")),
                new Menu("Search Product by Name", () => SearchProductBy("Name")),
                new Menu("Main Menu", () => { })
            };

            HandleMenuActions(searchMenuActions);
        }

        /// <summary>
        /// Displays the product edit menu and handles user selection.
        /// </summary>
        public void EditProduct() {
            var editMenuActions = new List<Menu>
            {
                new Menu("Edit Product by Id", () => EditProductBy("Id")),
                new Menu("Edit Product by Name", () => EditProductBy("Name")),
                new Menu("Main Menu", () => { })
            };

            HandleMenuActions(editMenuActions);
        }

        /// <summary>
        /// Displays the product delete menu and handles user selection.
        /// </summary>
        public void DeleteProduct() {
            var deleteMenuActions = new List<Menu>
            {
                new Menu("Delete Product by Id", () => DeleteProductBy("Id")),
                new Menu("Delete Product by Name", () => DeleteProductBy("Name")),
                new Menu("Main Menu", () => { })
            };

            HandleMenuActions(deleteMenuActions);
        }

        /// <summary>
        /// Prompts the user to press any key to continue.
        /// </summary>
        public void PromptForContinuation() {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Checks if a product with the given value already exists.
        /// </summary>
        /// <param name="value">The value to check (Id or Name).</param>
        /// <returns>True if a product with the value exists, otherwise false.</returns>
        bool IsProductDuplicate(string value) {
            foreach (ProductDetails productInfo in Products) {
                if (productInfo.Name == value || productInfo.Id == value) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Prompts for input until a unique value is entered for a product property.
        /// </summary>
        /// <param name="informationType">The property type (Id or Name).</param>
        /// <returns>The unique input value.</returns>
        string GetUniqueProductInput(string informationType) {
            string input;

            do {
                Console.Write($"Enter {informationType}: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                    Console.WriteLine($"[Error] {informationType} cannot be empty!");
                else if (IsProductDuplicate(input))
                    Console.WriteLine($"[Error] A product with this {informationType} already exists.");
                else
                    break;
            } while (true);

            return input;
        }

        /// <summary>
        /// Validates user input for a product field.
        /// </summary>
        /// <param name="informationType">The field type (Name, Price, Quantity).</param>
        /// <param name="input">The input value to validate.</param>
        /// <returns>An error message if validation fails, otherwise empty string.</returns>
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
        /// Prompts for and validates input for a product property.
        /// </summary>
        /// <param name="informationType">The property to request.</param>
        /// <param name="checkForDuplicate">Whether to check for duplicates.</param>
        /// <returns>The validated input value.</returns>
        string GetProductInformation(string informationType, bool checkForDuplicate = false) {
            string input;
            do {
                if (checkForDuplicate)
                    input = GetUniqueProductInput(informationType);
                else {
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
        /// Displays a formatted table of products.
        /// </summary>
        /// <param name="productList">The list of products to display.</param>
        void DisplayProducts(List<ProductDetails> productList) {
            Console.WriteLine("{0, -20} | {1, -15} | {2, -30} | {3, -25}", "Id", "Name", "Price", "Quantity");
            Console.WriteLine(new string('-', 100));

            foreach (ProductDetails productInfo in productList) {
                Console.WriteLine("{0, -20} | {1, -15} | {2, -30} | {3, -25}",
                    productInfo.Id, productInfo.Name, productInfo.Price, productInfo.Quantity);
            }

            Console.WriteLine("\n\n========== Sort By ==========\n");
            Console.WriteLine("1. Id");
            Console.WriteLine("2. Name");
            Console.WriteLine("3. Price");
            Console.WriteLine("4. Quantity");
            Console.WriteLine("5. Exit");
        }

        /// <summary>
        /// Sorts a list of products by the selected column.
        /// </summary>
        /// <param name="productList">The list of products to sort.</param>
        /// <param name="input">The ConsoleKey indicating the sort column.</param>
        /// <returns>The sorted product list.</returns>
        List<ProductDetails> SortProducts(List<ProductDetails> productList, ConsoleKey input) {
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

            if (sortingActions.ContainsKey(input))
                return sortingActions[input](productList);

            return productList;
        }

        /// <summary>
        /// Finds the first product with the given attribute and value.
        /// </summary>
        /// <param name="attribute">The attribute to search by (Id or Name).</param>
        /// <param name="value">The value to match.</param>
        /// <returns>The matching ProductDetails, or null if not found.</returns>
        ProductDetails FindProductByAttribute(string attribute, string value) {
            return Products.Find(product =>
                (attribute.Equals("Id", StringComparison.OrdinalIgnoreCase) &&
                product.Id.Equals(value, StringComparison.OrdinalIgnoreCase)) ||
                (attribute.Equals("Name", StringComparison.OrdinalIgnoreCase) &&
                product.Name.Equals(value, StringComparison.OrdinalIgnoreCase))
            );
        }

        /// <summary>
        /// Displays detailed information for a product.
        /// </summary>
        /// <param name="productInfo">The product to display.</param>
        void DisplayDetails(ProductDetails productInfo) {
            Console.WriteLine("\n========== Details ==========\n");
            Console.WriteLine("{0,-9}: {1}", "Id", productInfo.Id);
            Console.WriteLine("{0,-9}: {1}", "Name", productInfo.Name);
            Console.WriteLine("{0,-9}: {1}", "Price", productInfo.Price);
            Console.WriteLine("{0,-9}: {1}", "Quantity", productInfo.Quantity);
        }

        /// <summary>
        /// Prompts for an attribute value, searches for a product, and displays details if found.
        /// </summary>
        /// <param name="informationType">The attribute to search by.</param>
        void SearchProductBy(string informationType) {
            string input = GetProductInformation(informationType);
            ProductDetails productInfo = FindProductByAttribute(informationType, input);
            if (productInfo == null)
                Console.WriteLine("[Error] No Product Found");
            else
                DisplayDetails(productInfo);
        }

        /// <summary>
        /// Finds a product by attribute and allows editing its properties.
        /// </summary>
        /// <param name="ProductBy">The attribute to find the product by.</param>
        void EditProductBy(string ProductBy) {
            string input = GetProductInformation(ProductBy);
            ProductDetails product = FindProductByAttribute(ProductBy, input);

            if (product == null) {
                Console.WriteLine("[Error] No Product Found");
                PromptForContinuation();
                return;
            }
            EditBy(product);
        }

        /// <summary>
        /// Displays edit options for a product and updates the selected property.
        /// </summary>
        /// <param name="ProductToEdit">The product to edit.</param>
        void EditBy(ProductDetails ProductToEdit) {
            var EditByMenuActions = new List<Menu>
            {
                new Menu("Edit Id", () => UpdateProductField("Id", true, value => ProductToEdit.Id = value)),
                new Menu("Edit Name", () => UpdateProductField("Name", true, value => ProductToEdit.Name = value)),
                new Menu("Edit Price", () => UpdateProductField("Price", false, value => ProductToEdit.Price = decimal.Parse(value))),
                new Menu("Edit Quantity", () => UpdateProductField("Quantity", false, value => ProductToEdit.Quantity = int.Parse(value))),
                new Menu("Edit Menu", () => { })
            };

            IsOperationSuccessful = false;
            do {
                Console.Clear();
                DisplayDetails(ProductToEdit);
                DisplayMenuOptions(EditByMenuActions, "Edit");

                Console.Write("\n[Menu] Enter your choice: ");
                string input = Console.ReadLine();
                bool isValidChoice = int.TryParse(input, out int choice);

                if (isValidChoice && choice > 0 && choice <= EditByMenuActions.Count) {
                    if (choice == EditByMenuActions.Count)
                        return;
                    EditByMenuActions[choice - 1].Handler.Invoke();
                } else {
                    Console.WriteLine("[Error] Invalid choice. Please select a valid option.");
                }
            } while (!IsOperationSuccessful);
        }

        /// <summary>
        /// Updates a product property with a new value from the user.
        /// </summary>
        /// <param name="fieldName">The property to update.</param>
        /// <param name="checkForDuplicate">Whether to check for duplicates.</param>
        /// <param name="updateAction">The update action to apply.</param>
        void UpdateProductField(string fieldName, bool checkForDuplicate, Action<string> updateAction) {
            string updatedValue = GetProductInformation(fieldName, checkForDuplicate);
            updateAction(updatedValue);
            Console.WriteLine($"[Success] {fieldName} updated successfully!");
            IsOperationSuccessful = true;
        }

        /// <summary>
        /// Finds a product by attribute and deletes it from the list.
        /// </summary>
        /// <param name="ProductBy">The attribute to find the product by.</param>
        void DeleteProductBy(string ProductBy) {
            string input = GetProductInformation(ProductBy);
            ProductDetails product = FindProductByAttribute(ProductBy, input);

            if (product == null) {
                Console.WriteLine("[Error] No Product Found");
                PromptForContinuation();
                return;
            }
            Delete(product);
        }

        /// <summary>
        /// Removes a product from the product list.
        /// </summary>
        /// <param name="ProductToDelete">The product to delete.</param>
        void Delete(ProductDetails ProductToDelete) {
            Products.Remove(ProductToDelete);
            Console.WriteLine("[Success] Product Deleted Successfully");
            IsOperationSuccessful = true;
        }
    }
}