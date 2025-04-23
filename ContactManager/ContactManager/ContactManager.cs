namespace ContactManager {
    internal class ContactManager {
        static List<Contact> Contacts = new List<Contact>();
        Validator validator = new Validator();

        /// <summary>
        /// Checks if a contact with the given value (name, email, or phone number) already exists in the contact list.
        /// </summary>
        /// <param name="value">The value to check (name, email, or phone number).</param>
        /// <returns>Returns true if a contact with the given value already exists; otherwise, false.</returns>
        bool IsContactAlreadyExist(string value) {
            foreach (Contact ContactInfo in Contacts) {
                if (ContactInfo.Name == value || ContactInfo.Email == value || ContactInfo.PhoneNumber == value) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Prompts the user to input information of a specific type (e.g., Name, Phone Number, Email).
        /// Validates the input and ensures it meets the required conditions.
        /// </summary>
        /// <param name="informationType">The type of information being requested (e.g., "Name", "Phone Number").</param>
        /// <returns>Returns a validated string input provided by the user.</returns>
        string GetInformation(string informationType) {
            string input;
            do {
                bool isContactAlreadyExist;
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

                string invalidMessage = informationType switch {
                    "Name" => validator.IsNameValid(input) ?
                        string.Empty :
                        "Name must contain only letters and spaces.",
                    "Phone Number" => validator.IsNumberValid(input) ?
                        string.Empty :
                        "Phone Number must be exactly 10 digits or include a country code with '+'.",
                    "Email" => validator.IsEmailValid(input) ?
                        string.Empty :
                        "Invalid email format.",
                    _ => ""
                };

                if (invalidMessage == "")
                    break;

                Console.WriteLine($"[Error] {invalidMessage}");
            } while (true);

            return input;
        }

        /// <summary>
        /// Collects information for a new contact, validates the input, and adds the contact to the contact list.
        /// </summary>
        void AddNewContact() {
            Console.WriteLine("========== Add new contact ==========\n");
            string name = GetInformation("Name");
            string phoneNumber = GetInformation("Phone Number");
            string email = GetInformation("Email");
            string additionalInformation = GetInformation("Additional Information");

            Contacts.Add(new Contact(name, phoneNumber, email, additionalInformation));
            Console.WriteLine("[Success] New contact is created successfully!");
        }

        /// <summary>
        /// Displays all the contacts in a formatted table with columns for Name, Phone Number, Email, 
        /// and Additional Information.
        /// </summary>
        void ViewContacts() {
            Console.WriteLine("========== Contacts ==========\n");

            if (Contacts.Count == 0) {
                Console.WriteLine("[Error] Contact list is empty.");
                return;
            }

            Console.WriteLine("{0, -20} | {1, -15} | {2, -30} | {3, -25}", "Name", "Phone Number", "Email", "Additional Information");
            Console.WriteLine(new string('-', 100));
            foreach (Contact ContactInformation in Contacts) {
                Console.WriteLine("{0, -20} | {1, -15} | {2, -30} | {3, -25}",
                    ContactInformation.Name,
                    ContactInformation.PhoneNumber,
                    ContactInformation.Email,
                    ContactInformation.AdditionalInformation
                );
            }
        }

        /// <summary>
        /// The entry point of the program. Displays a menu and allows the user to interact with the contact manager.
        /// </summary>
        /// <param name="args">Command-line arguments (not used in this program).</param>
        public static void Main(string[] args) {
            ContactManager manager = new ContactManager();

            // NOTE: The following default data is added for testing and debugging purposes only.
            // These entries should be removed or commented out before deploying the application to production.
            Contacts.Add(new Contact("John Doe", "+11234567890", "john.doe@example.com", "Friend from college"));
            Contacts.Add(new Contact("Jane Smith", "+441234567890", "jane.smith@example.co.uk", "Colleague"));
            Contacts.Add(new Contact("Alice Johnson", "+918765432109", "alice.johnson@example.in", "Family friend"));
            Contacts.Add(new Contact("Bob Brown", "+61234567890", "bob.brown@example.au", "Neighbor"));

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
                bool isString = int.TryParse(input, out int choice);
                Console.Clear();
                switch (choice) {
                    case 1:
                        manager.AddNewContact();
                        break;
                    case 2:
                        manager.ViewContacts();
                        break;
                    default:
                        Console.WriteLine("[Error] Invalid choice!");
                        break;
                }
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            } while (true);
        }
    }
}
