using ExceptionHandling.Exceptions;
using ExceptionHandling.IOManager;

namespace ExceptionHandling.AppInteraction {
    internal class ArrayApplication {
        private static readonly int[] numbers = [2, 3, 5, 7, 11, 13, 17, 19];

        /// <summary>
        /// Prompts the user for an array index and displays the element at that index.
        /// </summary>
        public static void HandleArrayOutOfBoundsDemo() {
            try {
                OutputManager.DisplayArray(numbers);
                Console.Write("\nEnter index: ");
                int index = InputManager.GetInteger();
                DisplayElementAtIndex(index);
            } catch (IndexOutOfRangeException e) {
                Console.WriteLine(e.Message);
            } catch (InvalidUserInputException e) {
                Console.WriteLine(e.Message);
            } catch (Exception e) {
                Console.WriteLine($"[Error] An unexpected error occured: {e.Message}");
            } finally {
                InputManager.PromptForContinuation();
            }
        }

        /// <summary>
        /// Displays the element at the specified index of the array.
        /// </summary>
        private static void DisplayElementAtIndex(int position) {
            try {
                int index = position - 1;
                Console.WriteLine($"Element at {position} is {numbers[index]}");
            } catch (IndexOutOfRangeException) {
                throw new IndexOutOfRangeException($"[Error] Invalid index. The index value should be in the range (1 - {numbers.Length})");
            }
        }
    }
}
