namespace ContactManager {
    internal class ContactManager {
        static List<Contact> contacts = new List<Contact>();
        Validator validator = new Validator();

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
        /// Validates the input based on the type of information requested.
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
        /// Prompts the user to input information of a specific type (e.g., Name, Phone Number, Email).
        /// Validates the input and ensures it meets the required conditions.
        /// </summary>
        /// <param name="informationType">The type of information being requested (e.g., "Name", "Phone Number").</param>
        /// <param name="checkForDuplicate">Indicates whether to check for duplicate entries in the contact list.</param>
        /// <returns>Returns a validated string input provided by the user.</returns>
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
        /// Displays a prompt asking the user to press any key to continue and pauses execution until a key is pressed.
        /// </summary>
        void PromptForContinuation() {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Prompts the user to input details for a new contact and adds it to the contact list after validation.
        /// </summary>
        void AddNewContact() {
            Console.WriteLine("========== Add new contact ==========\n");
            string name = GetInformation("Name", true);
            string phoneNumber = GetInformation("Phone Number", true);
            string email = GetInformation("Email", true);
            string additionalInformation = GetInformation("Additional Information", true);

            contacts.Add(new Contact(name, phoneNumber, email, additionalInformation));
            Console.WriteLine("[Success] New contact is created successfully!");
        }

        /// <summary>
        /// Displays all contacts in the contact list in a formatted table.
        /// </summary>
        void ViewContacts() {
            Console.WriteLine("========== Contacts ==========\n");

            if (contacts.Count == 0) {
                Console.WriteLine("[Error] Contact list is empty.");
                return;
            }

            Console.WriteLine("{0, -20} | {1, -15} | {2, -30} | {3, -25}", "Name", "Phone Number", "Email", "Additional Information");
            Console.WriteLine(new string('-', 100));
            foreach (Contact contactInfo in contacts) {
                Console.WriteLine("{0, -20} | {1, -15} | {2, -30} | {3, -25}",
                    contactInfo.Name,
                    contactInfo.PhoneNumber,
                    contactInfo.Email,
                    contactInfo.AdditionalInformation
                );
            }
        }

        /// <summary>
        /// Displays detailed information about a specific contact.
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
        /// Finds a contact in the contact list based on a specific attribute (e.g., Name, Phone Number, Email).
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
        /// Displays the search contact menu options to the user.
        /// </summary>
        void SearchContactMenu() {
            Console.WriteLine("\n========== Search Contact ==========\n");
            Console.WriteLine("1. Search by Name");
            Console.WriteLine("2. Search by Phone Number");
            Console.WriteLine("3. Search by Email");
            Console.WriteLine("4. Main Menu");
        }

        /// <summary>
        /// Searches for a contact based on a specific type of information (e.g., Name, Phone Number, Email).
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
        /// Handles the search contact workflow by displaying a menu and executing the corresponding actions.
        /// </summary>
        void SearchContact() {
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
        /// The entry point of the program. Displays a menu and allows the user to interact with the contact manager.
        /// </summary>
        /// <param name="args">Command-line arguments (not used in this program).</param>
        public static void Main(string[] args) {
            ContactManager manager = new ContactManager();

            // NOTE: The following default data is added for testing and debugging purposes only.
            // These entries should be removed or commented out before deploying the application to production.
            contacts.Add(new Contact("John Doe", "+11234567890", "john.doe@example.com", "Friend from college"));
            contacts.Add(new Contact("Jane Smith", "+441234567890", "jane.smith@example.co.uk", "Colleague"));
            contacts.Add(new Contact("Alice Johnson", "+918765432109", "alice.johnson@example.in", "Family friend"));
            contacts.Add(new Contact("Bob Brown", "+61234567890", "bob.brown@example.au", "Neighbor"));

            do {
                Console.WriteLine("========== Contact Manager ==========\n");
                Console.WriteLine("1. Add a New Contact");
                Console.WriteLine("2. View Contacts");
                Console.WriteLine("3. Search a Contact");
                Console.WriteLine("4. Edit a Contact");
                Console.WriteLine("5. Delete a Contact");
                Console.WriteLine("6. Exit");
                Console.Write("\n[Menu] Enter your choice: ");

                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out int choice);
                Console.Clear();
                switch (choice) {
                    case 1:
                        manager.AddNewContact();
                        break;
                    case 2:
                        manager.ViewContacts();
                        break;
                    case 3:
                        manager.SearchContact();
                        break;
                    default:
                        Console.WriteLine("[Error] Invalid choice!");
                        break;
                }
                manager.PromptForContinuation();
                Console.Clear();
            } while (true);
        }
    }
}