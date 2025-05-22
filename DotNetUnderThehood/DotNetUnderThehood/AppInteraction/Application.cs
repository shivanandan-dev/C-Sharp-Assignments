using DotNetUnderThehood.IOManager;
using DotNetUnderThehood.Model;

namespace DotNetUnderThehood.AppInteraction {
    public static class Application {
        /// <summary>
        /// Handles the core application logic for arithmetic operations.
        /// Prompts the user to input two numbers, then performs addition, subtraction,
        /// multiplication, and division using those numbers.
        /// </summary>
        public static void HandleApplication() {
            try {
                Console.Write("Enter first number: ");
                decimal firstNumber = InputManager.GetDecimalValue();
                Console.Write("Enter second number: ");
                decimal secondNumber = InputManager.GetDecimalValue();
                Console.WriteLine($"Addition of {firstNumber} and {secondNumber} is {MathUtils.Add(firstNumber, secondNumber)}");
                Console.WriteLine($"Subtraction of {firstNumber} and {secondNumber} is {MathUtils.Sub(firstNumber, secondNumber)}");
                Console.WriteLine($"Multiplication of {firstNumber} and {secondNumber} is {MathUtils.Mul(firstNumber, secondNumber)}");
                Console.WriteLine($"Division of {firstNumber} and {secondNumber} is {MathUtils.Div(firstNumber, secondNumber)}");
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}
