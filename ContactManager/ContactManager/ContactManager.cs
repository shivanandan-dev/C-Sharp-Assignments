namespace ContactManager {
    internal class ContactManager {
        public static List<ContactDetails> contacts = new List<ContactDetails>();
        public static bool IsEditSuccessful = true;
        public static bool IsDeleteSuccessful = true;

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
            string name = GetContactInformation("Name", true);
            string phoneNumber = GetContactInformation("Phone Number", true);
            string email = GetContactInformation("Email", true);
            string additionalInformation = GetContactInformation("Additional Information", true);

            contacts.Add(new ContactDetails(name, phoneNumber, email, additionalInformation));
            Console.WriteLine("[Success] New contact is created successfully!");
        }

        /// <summary>
        /// Displays the list of contacts in a formatted table.
        /// </summary>
        /// <param name="contactList">List of contacts to display.</param>
        public void DisplayContacts(List<ContactDetails> contactList) {
            Console.WriteLine("{0, -20} | {1, -15} | {2, -30} | {3, -25}", "Name", "Phone Number", "Email", "Additional Information");
            Console.WriteLine(new string('-', 100));

            foreach (ContactDetails contactInfo in contactList) {
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
        public void ViewContacts(List<ContactDetails> contactList) {
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
        /// Handles the workflow for searching contacts by displaying a menu and executing the corresponding actions.
        /// </summary>
        public void SearchContact() {
            var searchMenuActions = new Dictionary<int, (string, Action action)> {
                { 1, ("Search by Name", () => SearchContactBy("Name"))},
                { 2, ("Search by Phone Number", () => SearchContactBy("Phone Number"))},
                { 3, ("Search by Email", () => SearchContactBy("Email"))},
                { 4, ("Main Menu", () => { })}
            };

            do {
                Console.Clear();
                DisplayMenu("Search", searchMenuActions);
                Console.Write("\n[Menu] Enter your choice: ");

                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out int choice);

                if (isNumber && searchMenuActions.ContainsKey(choice) && choice != 4) {
                    searchMenuActions[choice].action.Invoke();
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
            var deleteMenuActions = new Dictionary<int, (string, Action)> {
                { 1, ("Delete Contact by Name", () => DeleteContactBy("Name")) },
                { 2, ("Delete Contact by Phone Number", () => DeleteContactBy("Phone Number")) },
                { 3, ("Delete Contact by Email", () => DeleteContactBy("Email")) },
                { 4, ("Main Menu", () => { }) }
            };

            HandleEditOrDeleteOperation("Delete", deleteMenuActions, ref IsDeleteSuccessful);
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
            var editMenuActions = new Dictionary<int, (string, Action)> {
                { 1, ("Find Contact by Name", () => EditContactBy("Name"))},
                { 2, ("Find Contact by Phone Number", () => EditContactBy("Phone Number"))},
                { 3, ("Find Contact by Email", () => EditContactBy("Email"))},
                { 4, ("Main Menu", () => { })}
            };

            HandleEditOrDeleteOperation("Edit by", editMenuActions, ref IsEditSuccessful);
        }

        /// <summary>
        /// Display the menu options.
        /// </summary>
        /// <param name="menuTitle">Title of the menu</param>
        /// <param name="menuActions">A dictionary mapping menu choices to their corresponding actions.</param>
        public void DisplayMenu(string menuTitle, Dictionary<int, (string description, Action)> menuActions) {
            Console.WriteLine($"\n========== {menuTitle} ==========\n");
            foreach (var menuAction in menuActions) {
                Console.WriteLine($"{menuAction.Key}. {menuAction.Value.description}");
            }
        }

        /// <summary>
        /// Checks if a contact with the given value (name, email, or phone number) already exists in the contact list.
        /// </summary>
        /// <param name="value">The value to check (name, email, or phone number).</param>
        /// <returns>Returns true if a contact with the given value already exists; otherwise, false.</returns>
        bool IsContactDuplicate(string value) {
            foreach (ContactDetails contactInfo in contacts) {
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
        /// <returns>Returns the user-provided input that does not already exist.</returns>
        string GetUniqueContactInput(string informationType) {
            string input;

            do {
                Console.Write($"Enter {informationType}: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input) && informationType != "Additional Information")
                    Console.WriteLine($"[Error] {informationType} cannot be empty!");
                else if (IsContactDuplicate(input)) {
                    Console.WriteLine($"[Error] A contact with this {informationType} already exists.");
                } else {
                    break;
                }
            } while (true);

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
                "Name" => Validator.IsNameValid(input)
                    ? string.Empty
                    : "Name must contain only letters and spaces.",
                "Phone Number" => Validator.IsNumberValid(input)
                    ? string.Empty
                    : "Phone Number must be exactly 10 digits or include a country code with '+'.",
                "Email" => Validator.IsEmailValid(input)
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
        string GetContactInformation(string informationType, bool checkForDuplicate = false) {
            string input;
            do {
                if (checkForDuplicate) {
                    input = GetUniqueContactInput(informationType);
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
        List<ContactDetails> SortContacts(List<ContactDetails> contactList, ConsoleKey input) {
            // Dictionary of sorting actions
            var sortingActions = new Dictionary<ConsoleKey, Func<List<ContactDetails>, List<ContactDetails>>>
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
        void DisplayDetails(ContactDetails contactInfo) {
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
        ContactDetails FindContactByAttribute(string attribute, string value) {
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
        /// Searches for a contact based on a specific information type (e.g., Name, Phone Number, Email).
        /// </summary>
        /// <param name="informationType">The type of information to search by (e.g., "Name", "Phone Number").</param>
        void SearchContactBy(string informationType) {
            string input = GetContactInformation(informationType);
            ContactDetails contactInfo = FindContactByAttribute(informationType, input);
            if (contactInfo == null) {
                Console.WriteLine("[Error] No Contact Found");
            } else {
                DisplayDetails(contactInfo);
            }
        }


        /// <summary>
        /// Updates a specific field of a contact with a new value.
        /// </summary>
        /// <param name="fieldName">The name of the field to update (e.g., "Name", "Phone Number").</param>
        /// <param name="checkForDuplicate">Indicates whether to check for duplicate entries when updating the field.</param>
        /// <param name="updateAction">The action to perform for updating the field.</param>
        void UpdateContactField(string fieldName, bool checkForDuplicate, Action<string> updateAction) {
            string updatedValue = GetContactInformation(fieldName, checkForDuplicate);
            updateAction(updatedValue);
            Console.WriteLine($"[Success] {fieldName} updated successfully!");
            IsEditSuccessful = false;
        }

        /// <summary>
        /// Handles workflows for menu-driven operations like editing or deleting contacts.
        /// </summary>
        /// <param name="menuTitle">Title of the Menu.</param>
        /// <param name="menuActions">A dictionary mapping menu choices to their corresponding actions.</param>
        /// <param name="isOperationSuccessful">A reference to the success flag for the operation (e.g., IsEditSuccessful, IsDeleteSuccessful).</param>
        /// 
        void HandleEditOrDeleteOperation(string menuTitle, Dictionary<int, (string, Action action)> menuActions, ref bool isOperationSuccessful) {
            do {
                Console.Clear();
                DisplayMenu(menuTitle, menuActions); // Display the menu
                Console.WriteLine("");
                Console.Write("[Menu] Enter your choice: ");
                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out int choice);

                if (isNumber && menuActions.ContainsKey(choice) && choice != 4) {
                    menuActions[choice].action.Invoke(); // Execute the selected action
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
        void HandleEditOrDeleteContactBy(string ContactBy, Action<ContactDetails> action, ref bool isOperationSuccessful) {
            do {
                string input = GetContactInformation(ContactBy);
                ContactDetails contact = FindContactByAttribute(ContactBy, input);

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
        void EditBy(ContactDetails ContactToEdit) {
            var EditByMenuActions = new Dictionary<int, (string, Action action)> {
                { 1, ("Edit Name", () => UpdateContactField("Name", true, value => ContactToEdit.Name = value))},
                { 2, ("Edit Phone Number", () => UpdateContactField("Phone Number", true, value => ContactToEdit.PhoneNumber = value))},
                { 3, ("Edit Email", () => UpdateContactField("Email", true, value => ContactToEdit.Email = value))},
                { 4, ("Edit Additional Information", () => UpdateContactField("Additional Information", false, value => ContactToEdit.AdditionalInformation = value))},
                { 5, ("Main Menu", () => { IsEditSuccessful = false; })}
            };

            bool isValidChoice = false;
            Console.Clear();
            DisplayDetails(ContactToEdit);
            DisplayMenu("Edit", EditByMenuActions);

            do {
                Console.Write("\n[Menu] Enter your choice: ");
                string input = Console.ReadLine();
                isValidChoice = int.TryParse(input, out int choice);

                if (!isValidChoice) {
                    Console.WriteLine("[Error] Invalid input. Please enter a number.");
                    continue;
                }

                if (isValidChoice && EditByMenuActions.ContainsKey(choice)) {
                    EditByMenuActions[choice].action.Invoke();
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
        /// Deletes a contact from the contact list and updates the deletion state.
        /// </summary>
        /// <param name="ContactToDelete">The contact to be deleted.</param>
        void Delete(ContactDetails ContactToDelete) {
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