using LanguageIntegratedQuery.IOManager;

namespace LanguageIntegratedQuery.AppInteraction {
    internal class Task3 {
        private static readonly int[] numbers = [20, 30, 10, 40, 50, 60, 70, 100, 90, 80];

        /// <summary>
        /// Handles the main flow of the task, including displaying the array, 
        /// finding the second largest number, finding unique pairs that sum to a target,
        /// and prompting the user for continuation.
        /// </summary>
        public static void HandleTask() {
            Console.Clear();
            Console.WriteLine("Array: " + string.Join(", ", numbers));
            SubTask1();
            SubTask2();
            InputManager.PromptForContinuation();
        }

        /// <summary>
        /// Finds and displays the second largest unique number in the 'numbers' array.
        /// </summary>
        private static void SubTask1() {
            // Question:
            // Second highest number in the array. 
            var secondLargestNumber = numbers
                .Distinct()
                .OrderByDescending(number => number)
                .ToList()[1];

            Console.WriteLine("\nSecond Largest Number: " + secondLargestNumber);
        }

        /// <summary>
        /// Finds and displays all unique pairs of numbers from 'numbers' whose sum equals 'target'.
        /// </summary>
        private static void SubTask2() {
            // Question:
            // All unique pairs of numbers in the array that add up to a specified target. 
            int target = 100;

            var uniqueSumPairs = numbers
                .Distinct()
                .Where(number => numbers.Contains(target - number))
                .Select(number => new { FirstNumber = number, SecondNumber = target - number });

            Console.WriteLine("\n========== Unique Sum Pairs ==========\n");

            Console.WriteLine("Target Sum: " + target);

            foreach (var pair in uniqueSumPairs) {
                Console.WriteLine($"({pair.FirstNumber} {pair.SecondNumber})");
            }
        }
    }
}
