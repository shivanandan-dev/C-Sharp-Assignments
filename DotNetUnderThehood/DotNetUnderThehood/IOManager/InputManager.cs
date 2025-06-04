namespace DotNetUnderThehood.IOManager {
    internal class InputManager {
        /// <summary>
        /// Reads and parses an decimal input from the user.
        /// </summary>
        /// <returns>The parsed decimal value.</returns>
        /// <exception cref="InvalidUserInputException">Thrown when the input is not a valid decimal.</exception>
        public static int GetIntegerValue() {
            string input = Console.ReadLine();
            bool isInteger = int.TryParse(input, out int number);

            if (!isInteger) {
                throw new Exception("[Error] Invalid input. Please enter a valid decimal.");
            }

            return number;
        }
    }
}
