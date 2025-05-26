namespace InventoryManager {
    internal class InventoryManager {
        public static List<ProductDetails> Products = new List<ProductDetails>();
        public bool IsOperationSuccessful = false;

        readonly string _consoleId = StringFormatter.AddSpaces(nameof(ProductDetails.Id));
        readonly string _consoleName = StringFormatter.AddSpaces(nameof(ProductDetails.Name));
        readonly string _consolePrice = StringFormatter.AddSpaces(nameof(ProductDetails.Price));
        readonly string _consoleQuantity = StringFormatter.AddSpaces(nameof(ProductDetails.Quantity));

        /// <summary>
        /// Handles display and user selection for a menu, invoking the selected action.
        /// </summary>
        /// <param name="menuActions">A list of menu actions to present to the user.</param>
        public void HandleMenuActions(List<Menu> menuActions, bool isDisplayProduct = false) {
            do {
                Console.Clear();
                if (isDisplayProduct)
                    DisplayProducts();
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
            DisplayProducts();
            Console.WriteLine("========== Add new Product ==========\n");
            string id = GetProductInformation(_consoleId, true);
            string name = GetProductInformation(_consoleName, true);
            string price = GetProductInformation(_consolePrice, false);
            string quantity = GetProductInformation(_consoleQuantity, false);

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
                DisplayProducts();
                Console.WriteLine("========== Sort By ==========\n");
                Console.WriteLine("1. Id");
                Console.WriteLine("2. Name");
                Console.WriteLine("3. Price");
                Console.WriteLine("4. Quantity");
                Console.WriteLine("5. Exit");
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
                new Menu("Search Product by Id", () => SearchProductByAttribute(_consoleId)),
                new Menu("Search Product by Name", () => SearchProductByAttribute(_consoleName)),
                new Menu("Main Menu", () => { })
            };

            HandleMenuActions(searchMenuActions);
        }

        /// <summary>
        /// Displays the product edit menu and handles user selection.
        /// </summary>
        public void EditProduct() {
            DisplayProducts();
            var editMenuActions = new List<Menu>
            {
                new Menu("Edit Product by Id", () => FindAndEditProductByAttribute(_consoleId)),
                new Menu("Edit Product by Name", () => FindAndEditProductByAttribute(_consoleName)),
                new Menu("Main Menu", () => { })
            };

            HandleMenuActions(editMenuActions, true);
        }

        /// <summary>
        /// Displays the product delete menu and handles user selection.
        /// </summary>
        public void DeleteProduct() {
            var deleteMenuActions = new List<Menu>
            {
                new Menu("Delete Product by Id", () => DeleteProductByAttribute(_consoleId)),
                new Menu("Delete Product by Name", () => DeleteProductByAttribute(_consoleName)),
                new Menu("Main Menu", () => { })
            };

            HandleMenuActions(deleteMenuActions, true);
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
                "Id" => Validator.IsPositiveInteger(input)
                        ? string.Empty
                    : "Id must be a positive number.",
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
        void DisplayProducts() {
            Console.WriteLine("========== Products ==========\n");
            Console.WriteLine("{0, -20} | {1, -15} | {2, -30} | {3, -25}", _consoleId, _consoleName, _consolePrice, _consoleQuantity);
            Console.WriteLine(new string('-', 100));

            foreach (ProductDetails productInfo in Products) {
                Console.WriteLine("{0, -20} | {1, -15} | {2, -30} | {3, -25}",
                    productInfo.Id, productInfo.Name, productInfo.Price, productInfo.Quantity);
            }
            Console.WriteLine("\n");
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
                (attribute.Equals(_consoleId, StringComparison.OrdinalIgnoreCase) &&
                product.Id.Equals(value, StringComparison.OrdinalIgnoreCase)) ||
                (attribute.Equals(_consoleName, StringComparison.OrdinalIgnoreCase) &&
                product.Name.Equals(value, StringComparison.OrdinalIgnoreCase))
            );
        }

        /// <summary>
        /// Displays detailed information for a product.
        /// </summary>
        /// <param name="productInfo">The product to display.</param>
        void DisplayDetails(ProductDetails productInfo) {
            Console.WriteLine("\n========== Details ==========\n");
            Console.WriteLine("{0,-9}: {1}", _consoleId, productInfo.Id);
            Console.WriteLine("{0,-9}: {1}", _consoleName, productInfo.Name);
            Console.WriteLine("{0,-9}: {1}", _consolePrice, productInfo.Price);
            Console.WriteLine("{0,-9}: {1}", _consoleQuantity, productInfo.Quantity);
            Console.WriteLine("\n");
        }

        /// <summary>
        /// Prompts for an attribute value, searches for a product, and displays details if found.
        /// </summary>
        /// <param name="attribute">The attribute to search by.</param>
        void SearchProductByAttribute(string attribute) {
            string input = GetProductInformation(attribute);
            ProductDetails productInfo = FindProductByAttribute(attribute, input);
            if (productInfo == null)
                Console.WriteLine("[Error] No Product Found");
            else
                DisplayDetails(productInfo);
        }

        /// <summary>
        /// Finds a product by attribute and allows editing its properties.
        /// </summary>
        /// <param name="attribute">The attribute to find the product by.</param>
        void FindAndEditProductByAttribute(string attribute) {
            string input = GetProductInformation(attribute);
            ProductDetails product = FindProductByAttribute(attribute, input);

            if (product == null) {
                Console.WriteLine("[Error] No Product Found");
                PromptForContinuation();
                return;
            }
            EditProductByAttribute(product);
        }

        /// <summary>
        /// Displays edit options for a product and updates the selected property.
        /// </summary>
        /// <param name="productToEdit">The product to edit.</param>
        void EditProductByAttribute(ProductDetails productToEdit) {
            var EditProductByAttributeMenuActions = new List<Menu>
            {
                new Menu("Edit Id", () => UpdateProductField(_consoleId, true, value => productToEdit.Id = value)),
                new Menu("Edit Name", () => UpdateProductField(_consoleName, true, value => productToEdit.Name = value)),
                new Menu("Edit Price", () => UpdateProductField(_consolePrice, false, value => productToEdit.Price = decimal.Parse(value))),
                new Menu("Edit Quantity", () => UpdateProductField(_consoleQuantity, false, value => productToEdit.Quantity = int.Parse(value))),
                new Menu("Edit Menu", () => { })
            };

            IsOperationSuccessful = false;
            do {
                Console.Clear();
                DisplayDetails(productToEdit);
                DisplayMenuOptions(EditProductByAttributeMenuActions, "Edit");

                Console.Write("\n[Menu] Enter your choice: ");
                string input = Console.ReadLine();
                bool isValidChoice = int.TryParse(input, out int choice);

                if (isValidChoice && choice > 0 && choice <= EditProductByAttributeMenuActions.Count) {
                    if (choice == EditProductByAttributeMenuActions.Count)
                        return;
                    EditProductByAttributeMenuActions[choice - 1].Handler.Invoke();
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
        /// <param name="attribute">The attribute to find the product by.</param>
        void DeleteProductByAttribute(string attribute) {
            string input = GetProductInformation(attribute);
            ProductDetails product = FindProductByAttribute(attribute, input);

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
        /// <param name="productToDelete">The product to delete.</param>
        void Delete(ProductDetails productToDelete) {
            Products.Remove(productToDelete);
            Console.WriteLine("[Success] Product Deleted Successfully");
            IsOperationSuccessful = true;
        }
    }
}