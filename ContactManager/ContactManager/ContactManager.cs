namespace ContactManager {
    internal class ContactManager {
        public static List<Contact> contacts = new List<Contact>();
        public static bool IsEditSuccessful = true;
        public static bool IsDeleteSuccessful = true;

        Validator validator = new Validator();

        /// <summary>
        /// Displays a prompt asking the user to press any key to continue and waits until a key is pressed.
        /// </summary>
        public static void PromptForContinuation() {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Collects information for a new contact, validates the inputs, and adds the contact to the contact list.
        /// </summary>
        public void AddNewContact() {
            Console.WriteLine("========== Add new contact ==========\n");
            string name = GetInformation("Name", true);
            string phoneNumber = GetInformation("Phone Number", true);
            string email = GetInformation("Email", true);
            string additionalInformation = GetInformation("Additional Information", true);

            contacts.Add(new Contact(name, phoneNumber, email, additionalInformation));
            Console.WriteLine("[Success] New contact is created successfully!");
        }

        /// <summary>
        /// Displays the list of contacts in a formatted table.
        /// </summary>
        /// <param name="contactList">List of contacts to display.</param>
        public void DisplayContacts(List<Contact> contactList) {
            Console.WriteLine("{0, -20} | {1, -15} | {2, -30} | {3, -25}", "Name", "Phone Number", "Email", "Additional Information");
            Console.WriteLine(new string('-', 100));

            foreach (Contact contactInfo in contactList) {
                Console.WriteLine("{0, -20} | {1, -15} | {2, -30} | {3, -25}",
                    contactInfo.Name,
                    contactInfo.PhoneNumber,
                    contactInfo.Email,
                    contactInfo.AdditionalInformation
                );
            }

            Console.WriteLine("\n========== Sort By ==========\n");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Phone Number");
            Console.WriteLine("3. Email");
            Console.WriteLine("4. Exit");
        }

        /// <summary>
        /// Displays all contacts in the contact list in a formatted table.
        /// </summary>
        public void ViewContacts(List<Contact> contactList) {
            while (true) {
                Console.Clear();
                Console.WriteLine("========== Contacts ==========\n");

                if (contactList.Count == 0) {
                    Console.WriteLine("[Error] Contact list is empty.");
                    return;
                }

                DisplayContacts(contactList);
                Console.WriteLine("\nPress (1-3) to Sort, (4) to Exit:");
                ConsoleKey input = Console.ReadKey().Key;

                if (input == ConsoleKey.D4 || input == ConsoleKey.NumPad4) {
                    return;
                }

                contactList = SortContacts(contactList, input);
            }
        }

        /// <summary>
        /// Displays the main menu options to allow the user to interact with the contact manager.
        /// </summary>
        public static void MainMenu() {
            Console.WriteLine("========== Contact Manager ==========\n");
            Console.WriteLine("1. Add a New Contact");
            Console.WriteLine("2. View Contacts");
            Console.WriteLine("3. Search a Contact");
            Console.WriteLine("4. Edit a Contact");
            Console.WriteLine("5. Delete a Contact");
            Console.WriteLine("6. Exit");
        }

        /// <summary>
        /// Handles the workflow for searching contacts by displaying a menu and executing the corresponding actions.
        /// </summary>
        public void SearchContact() {
            var actions = new Dictionary<int, Action> {
                { 1, () => SearchContactBy("Name")},
                { 2, () => SearchContactBy("Phone Number")},
                { 3, () => SearchContactBy("Email")},
            };

            do {
                Console.Clear();
                SearchContactMenu();
                Console.Write("\n[Menu] Enter your choice: ");

                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out int choice);

                if (isNumber && actions.ContainsKey(choice)) {
                    actions[choice].Invoke();
                } else if (choice == 4) {
                    return;
                } else {
                    Console.WriteLine("[Error] Invalid choice!");
                }

                PromptForContinuation();
            } while (true);
        }

        /// <summary>
        /// Handles the deletion of contacts based on user input.
        /// </summary>
        public void DeleteContact() {
            var actions = new Dictionary<int, Action> {
                { 1, () => DeleteContactBy("Name") },
                { 2, () => DeleteContactBy("Phone Number") },
                { 3, () => DeleteContactBy("Email") }
            };

            HandleEditOrDeleteOperation(DeleteContactMenu, actions, ref IsDeleteSuccessful);
        }

        /// <summary>
        /// Exits the application after displaying a farewell message.
        /// </summary>
        public void ExitEnvironment() {
            Console.WriteLine("\nBye Bye...");
            Console.ReadKey();
            Environment.Exit(0);
        }

        /// <summary>
        /// Handles the editing of contacts based on user input.
        /// </summary>
        public void EditContact() {
            var actions = new Dictionary<int, Action> {
                { 1, () => EditContactBy("Name") },
                { 2, () => EditContactBy("Phone Number") },
                { 3, () => EditContactBy("Email") }
            };

            HandleEditOrDeleteOperation(DisplayContactByMenu, actions, ref IsEditSuccessful);
        }

        /// <summary>
        /// Checks if a contact with the given value (name, email, or phone number) already exists in the contact list.
        /// </summary>
        /// <param name="value">The value to check (name, email, or phone number).</param>
        /// <returns>Returns true if a contact with the given value already exists; otherwise, false.</returns>
        bool IsContactAlreadyExist(string value) {
            foreach (Contact contactInfo in contacts) {
                if (contactInfo.Name == value || contactInfo.Email == value || contactInfo.PhoneNumber == value) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Prompts the user to enter information until the input does not match any existing contact.
        /// </summary>
        /// <param name="informationType">The type of information being requested (e.g., "Name", "Phone Number").</param>
        /// <param name="isContactAlreadyExist">Outputs whether a duplicate contact was found.</param>
        /// <returns>Returns the user-provided input that does not already exist.</returns>
        string GetContactUntilNotExist(string informationType, out bool isContactAlreadyExist) {
            string input;

            do {
                isContactAlreadyExist = false;
                Console.Write($"Enter {informationType}: ");
                input = Console.ReadLine();
                isContactAlreadyExist = IsContactAlreadyExist(input);
                if (input == "" && informationType != "Additional Information")
                    Console.WriteLine($"[Error] {informationType} cannot be empty!");
                else if (isContactAlreadyExist) {
                    Console.WriteLine($"[Error] A contact with this {informationType} already exists.");
                }
            } while ((input == "" && informationType != "Additional Information") || isContactAlreadyExist);

            return input;
        }

        /// <summary>
        /// Validates input based on the specified type of information.
        /// </summary>
        /// <param name="informationType">The type of information being validated (e.g., "Name", "Phone Number", "Email").</param>
        /// <param name="input">The user-provided input to validate.</param>
        /// <returns>An error message if the input is invalid; otherwise, an empty string.</returns>
        string ValidateInput(string informationType, string input) {
            return informationType switch {
                "Name" => validator.IsNameValid(input)
                    ? string.Empty
                    : "Name must contain only letters and spaces.",
                "Phone Number" => validator.IsNumberValid(input)
                    ? string.Empty
                    : "Phone Number must be exactly 10 digits or include a country code with '+'.",
                "Email" => validator.IsEmailValid(input)
                    ? string.Empty
                    : "Invalid email format.",
                _ => string.Empty
            };
        }

        /// <summary>
        /// Requests user input for a specific type of information, validates it, and checks for duplicates if required.
        /// </summary>
        /// <param name="informationType">The type of information being requested (e.g., "Name", "Phone Number", "Email").</param>
        /// <param name="checkForDuplicate">Indicates whether to check for duplicate entries in the contact list.</param>
        /// <returns>Validated string input provided by the user.</returns>
        string GetInformation(string informationType, bool checkForDuplicate = false) {
            string input;
            do {
                if (checkForDuplicate) {
                    input = GetContactUntilNotExist(informationType, out bool isContactAlreadyExist);
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
        /// Sorts the list of contacts based on the selected sorting option.
        /// </summary>
        /// <param name="contactList">The list of contacts to sort.</param>
        /// <param name="input">The sorting option entered by the user.</param>
        /// <returns>A sorted list of contacts.</returns>
        List<Contact> SortContacts(List<Contact> contactList, ConsoleKey input) {
            // Dictionary of sorting actions
            var sortingActions = new Dictionary<ConsoleKey, Func<List<Contact>, List<Contact>>>
            {
                { ConsoleKey.D1, contacts => contacts.OrderBy(contact => contact.Name).ToList() },
                { ConsoleKey.NumPad1, contacts => contacts.OrderBy(contact => contact.Name).ToList() },
                { ConsoleKey.D2, contacts => contacts.OrderBy(contact => contact.PhoneNumber).ToList() },
                { ConsoleKey.NumPad2, contacts => contacts.OrderBy(contact => contact.PhoneNumber).ToList() },
                { ConsoleKey.D3, contacts => contacts.OrderBy(contact => contact.Email).ToList() },
                { ConsoleKey.NumPad3, contacts => contacts.OrderBy(contact => contact.Email).ToList() },
            };

            if (sortingActions.ContainsKey(input)) {
                return sortingActions[input](contactList);
            }

            return contactList;
        }

        /// <summary>
        /// Displays the detailed information of a specific contact.
        /// </summary>
        /// <param name="contactInfo">The contact whose details are to be displayed.</param>
        void DisplayDetails(Contact contactInfo) {
            Console.WriteLine("\n========== Details ==========\n");
            Console.WriteLine("{0,-25}: {1}", "Name", contactInfo.Name);
            Console.WriteLine("{0,-25}: {1}", "Phone Number", contactInfo.PhoneNumber);
            Console.WriteLine("{0,-25}: {1}", "Email", contactInfo.Email);
            Console.WriteLine("{0,-25}: {1}", "Additional Information", contactInfo.AdditionalInformation);
        }

        /// <summary>
        /// Searches for a contact in the contact list based on a specified attribute (e.g., Name, Phone Number, Email).
        /// </summary>
        /// <param name="attribute">The attribute to search by (e.g., "Name", "Phone Number", "Email").</param>
        /// <param name="value">The value of the attribute to search for.</param>
        /// <returns>The contact that matches the attribute and value; otherwise, null.</returns>
        Contact FindContactByAttribute(string attribute, string value) {
            return contacts.Find(contact =>
                (attribute.Equals("Name", StringComparison.OrdinalIgnoreCase) &&
                contact.Name.Equals(value, StringComparison.OrdinalIgnoreCase)) ||
                (attribute.Equals("Phone Number", StringComparison.OrdinalIgnoreCase) &&
                contact.PhoneNumber.Equals(value, StringComparison.OrdinalIgnoreCase)) ||
                (attribute.Equals("Email", StringComparison.OrdinalIgnoreCase) &&
                contact.Email.Equals(value, StringComparison.OrdinalIgnoreCase))
            );
        }

        /// <summary>
        /// Displays the menu options for searching contacts.
        /// </summary>
        void SearchContactMenu() {
            Console.WriteLine("\n========== Search Contact ==========\n");
            Console.WriteLine("1. Search by Name");
            Console.WriteLine("2. Search by Phone Number");
            Console.WriteLine("3. Search by Email");
            Console.WriteLine("4. Main Menu");
        }

        /// <summary>
        /// Searches for a contact based on a specific information type (e.g., Name, Phone Number, Email).
        /// </summary>
        /// <param name="informationType">The type of information to search by (e.g., "Name", "Phone Number").</param>
        void SearchContactBy(string informationType) {
            string input = GetInformation(informationType);
            Contact contactInfo = FindContactByAttribute(informationType, input);
            if (contactInfo == null) {
                Console.WriteLine("[Error] No Contact Found");
            } else {
                DisplayDetails(contactInfo);
            }
        }

        /// <summary>
        /// Displays the menu options for editing specific fields of a contact.
        /// </summary>
        void DisplayEditByMenu() {
            Console.WriteLine("\n========== Edit ==========\n");
            Console.WriteLine("1. Edit Name");
            Console.WriteLine("2. Edit Phone Number");
            Console.WriteLine("3. Edit Email");
            Console.WriteLine("4. Edit Additional Information");
            Console.WriteLine("5. Edit Menu");
        }

        /// <summary>
        /// Displays the menu options for finding a contact to edit.
        /// </summary>
        void DisplayContactByMenu() {
            Console.WriteLine("\n========== Edit Contact ==========\n");
            Console.WriteLine("1. Find Contact by Name");
            Console.WriteLine("2. Find Contact by Phone number");
            Console.WriteLine("3. Find Contact by Email");
            Console.WriteLine("4. Main Menu");
        }

        /// <summary>
        /// Updates a specific field of a contact with a new value.
        /// </summary>
        /// <param name="fieldName">The name of the field to update (e.g., "Name", "Phone Number").</param>
        /// <param name="checkForDuplicate">Indicates whether to check for duplicate entries when updating the field.</param>
        /// <param name="updateAction">The action to perform for updating the field.</param>
        void UpdateContactField(string fieldName, bool checkForDuplicate, Action<string> updateAction) {
            string updatedValue = GetInformation(fieldName, checkForDuplicate);
            updateAction(updatedValue);
            Console.WriteLine($"[Success] {fieldName} updated successfully!");
            IsEditSuccessful = false;
        }

        /// <summary>
        /// Handles workflows for menu-driven operations like editing or deleting contacts.
        /// </summary>
        /// <param name="menuDisplayAction">The action to display the menu options.</param>
        /// <param name="actions">A dictionary mapping menu choices to their corresponding actions.</param>
        /// <param name="isOperationSuccessful">A reference to the success flag for the operation (e.g., IsEditSuccessful, IsDeleteSuccessful).</param>
        void HandleEditOrDeleteOperation(Action menuDisplayAction, Dictionary<int, Action> actions, ref bool isOperationSuccessful) {
            do {
                Console.Clear();
                menuDisplayAction(); // Display the menu
                Console.WriteLine("");
                Console.Write("[Menu] Enter your choice: ");
                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out int choice);

                if (isNumber && actions.ContainsKey(choice)) {
                    actions[choice].Invoke(); // Execute the selected action
                } else if (isNumber && choice == 4) { // Exit option
                    return;
                } else {
                    Console.WriteLine("[Error] Invalid choice!");
                    PromptForContinuation();
                }

            } while (isOperationSuccessful);
        }

        /// <summary>
        /// Handles the workflow for finding a contact by an attribute and performing an action on it.
        /// </summary>
        /// <param name="ContactBy">The attribute to find the contact by (e.g., "Name", "Phone Number", "Email").</param>
        /// <param name="action">The action to perform on the found contact (e.g., Edit, Delete).</param>
        /// <param name="isOperationSuccessful">A reference to the success flag for the operation (e.g., IsEditSuccessful, IsDeleteSuccessful).</param>
        void HandleEditOrDeleteContactBy(string ContactBy, Action<Contact> action, ref bool isOperationSuccessful) {
            do {
                string input = GetInformation(ContactBy);
                Contact contact = FindContactByAttribute(ContactBy, input);

                if (contact == null) {
                    Console.WriteLine("[Error] No Contact Found");
                    PromptForContinuation();
                    return;
                } else {
                    action(contact);
                }
            } while (isOperationSuccessful);
        }

        /// <summary>
        /// Handles the field-specific editing of a contact by displaying a submenu and executing actions.
        /// </summary>
        /// <param name="ContactToEdit">The contact to be edited.</param>
        void EditBy(Contact ContactToEdit) {
            var actions = new Dictionary<int, Action> {
                { 1, () => UpdateContactField("Name", true, value => ContactToEdit.Name = value)},
                { 2, () => UpdateContactField("Phone Number", true, value => ContactToEdit.PhoneNumber = value)},
                { 3, () => UpdateContactField("Email", true, value => ContactToEdit.Email = value)},
                { 4, () => UpdateContactField("Additional Information", false, value => ContactToEdit.AdditionalInformation = value)}
            };

            bool isValidChoice = false;
            Console.Clear();
            DisplayDetails(ContactToEdit);
            DisplayEditByMenu();

            do {
                Console.Write("\n[Menu] Enter your choice: ");
                string input = Console.ReadLine();
                isValidChoice = int.TryParse(input, out int choice);

                if (!isValidChoice) {
                    Console.WriteLine("[Error] Invalid input. Please enter a number.");
                    continue;
                }

                if (isValidChoice && actions.ContainsKey(choice)) {
                    actions[choice].Invoke();
                } else if (isValidChoice && choice == 5) {
                    return;
                } else {
                    Console.WriteLine("[Error] Invalid choice. Please select a valid option.");
                }
            } while (IsEditSuccessful);
        }

        /// <summary>
        /// Edits a contact by searching for it based on an attribute.
        /// </summary>
        /// <param name="ContactBy">The attribute to find the contact by (e.g., "Name", "Phone Number", "Email").</param>
        void EditContactBy(string ContactBy) {
            HandleEditOrDeleteContactBy(ContactBy, EditBy, ref IsEditSuccessful);
        }

        /// <summary>
        /// Displays the menu options for deleting contacts.
        /// </summary>
        void DeleteContactMenu() {
            Console.WriteLine("\n========== Delete Contact ==========\n");
            Console.WriteLine("1. Delete Contact by Name");
            Console.WriteLine("2. Delete Contact by Phone number");
            Console.WriteLine("3. Delete Contact by Email");
            Console.WriteLine("4. Main Menu");
        }

        /// <summary>
        /// Deletes a contact from the contact list and updates the deletion state.
        /// </summary>
        /// <param name="ContactToDelete">The contact to be deleted.</param>
        void Delete(Contact ContactToDelete) {
            contacts.Remove(ContactToDelete);
            Console.WriteLine("[Success] Contact Deleted Successfully");
            IsDeleteSuccessful = false;
        }

        /// <summary>
        /// Deletes a contact by searching for it based on an attribute.
        /// </summary>
        /// <param name="ContactBy">The attribute to find the contact by (e.g., "Name", "Phone Number", "Email").</param>
        void DeleteContactBy(string ContactBy) {
            HandleEditOrDeleteContactBy(ContactBy, Delete, ref IsDeleteSuccessful);
        }
    }
}