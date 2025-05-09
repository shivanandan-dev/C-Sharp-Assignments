namespace ContactManager {
    internal class Program {
        /// <summary>
        /// The entry point of the program. Displays the main menu and handles user interactions with the contact manager.
        /// </summary>
        /// <param name="args">Command-line arguments (not used in this program).</param>
        static void Main(string[] args) {
            ContactManager manager = new ContactManager();

            // NOTE: The following default data is added for testing and debugging purposes only.
            // These entries should be removed or commented out before deploying the application to production.
            ContactManager.contacts.Add(new ContactDetails("John Doe", "+11234567890", "john.doe@example.com", "Friend from college"));
            ContactManager.contacts.Add(new ContactDetails("Jane Smith", "+441234567890", "jane.smith@example.co.uk", "Colleague"));
            ContactManager.contacts.Add(new ContactDetails("Alice Johnson", "+918765432109", "alice.johnson@example.in", "Family friend"));
            ContactManager.contacts.Add(new ContactDetails("Bob Brown", "+61234567890", "bob.brown@example.au", "Neighbor"));

            var mainMenuActions = new Dictionary<int, (string, Action action)> {
                { 1, ("Add New Contact", () => manager.AddNewContact()) },
                { 2, ("View Contacts", () => manager.ViewContacts()) },
                { 3, ("Search Contact", () => manager.SearchContact()) },
                { 4, ("Edit Contact", () => manager.EditContact()) },
                { 5, ("Delete Contact", () => manager.DeleteContact()) },
                { 6, ("Exit", () => manager.ExitEnvironment()) }
            };

            do {
                Console.Clear();
                manager.DisplayMenu("Main Menu", mainMenuActions);
                Console.Write("\n[Menu] Enter your choice: ");

                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out int choice);


                if (isNumber && mainMenuActions.ContainsKey(choice)) {
                    Console.Clear();
                    mainMenuActions[choice].action.Invoke();
                } else {
                    Console.WriteLine("[Error] Invalid choice!");
                }

                ContactManager.IsOperationSuccessful = false;
                ContactManager.PromptForContinuation();
                Console.Clear();
            } while (true);
        }
    }
}
