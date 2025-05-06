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
            ContactManager.contacts.Add(new Contact("John Doe", "+11234567890", "john.doe@example.com", "Friend from college"));
            ContactManager.contacts.Add(new Contact("Jane Smith", "+441234567890", "jane.smith@example.co.uk", "Colleague"));
            ContactManager.contacts.Add(new Contact("Alice Johnson", "+918765432109", "alice.johnson@example.in", "Family friend"));
            ContactManager.contacts.Add(new Contact("Bob Brown", "+61234567890", "bob.brown@example.au", "Neighbor"));

            var mainMenuActions = new Dictionary<int, Action> {
                { 1, () => manager.AddNewContact() },
                { 2, () => manager.ViewContacts(ContactManager.contacts) },
                { 3, () => manager.SearchContact() },
                { 4, () => manager.EditContact() },
                { 5, () => manager.DeleteContact() },
                { 6, () => manager.ExitEnvironment()}
            };

            do {
                Console.Clear();
                ContactManager.MainMenu();
                Console.Write("\n[Menu] Enter your choice: ");

                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out int choice);


                if (isNumber && mainMenuActions.ContainsKey(choice)) {
                    Console.Clear();
                    mainMenuActions[choice].Invoke();
                } else {
                    Console.WriteLine("[Error] Invalid choice!");
                }

                ContactManager.IsDeleteSuccessful = true;
                ContactManager.IsEditSuccessful = true;
                ContactManager.PromptForContinuation();
                Console.Clear();
            } while (true);
        }
    }
}
