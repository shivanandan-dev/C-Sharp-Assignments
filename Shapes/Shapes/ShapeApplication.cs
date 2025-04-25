namespace ShapeApplication {
    internal class ShapeApplication {

        /// <summary>
        /// Displays the main menu options to the user.
        /// </summary>
        void DisplayMenu() {
            Console.WriteLine("\nShape Area Calculator");
            Console.WriteLine("1. Create Rectangle");
            Console.WriteLine("2. Create Circle");
            Console.WriteLine("3. Exit");
            Console.Write("\n[Menu] Enter your choice: ");
        }

        /// <summary>
        /// Prompts the user to press any key to continue.
        /// </summary>
        void PromptForContinuation() {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Prompts the user for a double input and validates it.
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <returns>A valid double value or -1 if input is invalid.</returns>
        double GetValidatedDouble(string prompt) {
            Console.Write(prompt);
            string input = Console.ReadLine();
            bool isValid = double.TryParse(input, out double value);

            if (!isValid) {
                Console.WriteLine("[Error] Invalid input. Try again!");
                return -1;
            }

            return value;
        }

        /// <summary>
        /// Handles the process of creating a rectangle by taking user input and displaying details.
        /// </summary>
        void CreateRectangle() {
            Console.Write("Enter the color of the rectangle: ");
            string color = Console.ReadLine();

            double length = GetValidatedDouble("Enter length: ");
            if (length < 0) return;

            double width = GetValidatedDouble("Enter width: ");
            if (width < 0) return;

            RectangleShape rectangle = new RectangleShape(color, length, width);
            Console.Write("[Info] ");
            rectangle.PrintDetails();
        }

        /// <summary>
        /// Handles the process of creating a circle by taking user input and displaying details.
        /// </summary>
        void CreateCircle() {
            Console.Write("Enter the color of the rectangle: ");
            string color = Console.ReadLine();

            double radius = GetValidatedDouble("Enter length: ");
            if (radius < 0) return;

            CircleShape rectangle = new CircleShape(color, radius);
            Console.Write("[Info] ");
            rectangle.PrintDetails();
        }

        /// <summary>
        /// Entry point of the program. Displays menu and handles user interaction in a loop.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        static void Main(string[] args) {
            ShapeApplication shape = new ShapeApplication();

            Dictionary<int, Action> action = new Dictionary<int, Action>() {
                { 1, shape.CreateRectangle },
                { 2, shape.CreateCircle },
                { 3, () => Environment.Exit(0) }
            };

            while (true) {
                Console.Clear();
                shape.DisplayMenu();
                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out int choice);

                if (isNumber && action.ContainsKey(choice)) {
                    action[choice]();
                }
                else {
                    Console.WriteLine("[Error] Invalid input!");
                }
                shape.PromptForContinuation();
            }
        }
    }
}
