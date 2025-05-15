using ExceptionHandling.Exceptions;
using ExceptionHandling.IOManager;

namespace ExceptionHandling.AppInteraction {
    internal class DivisionApplication {
        /// <summary>
        /// Handles division operation by taking dividend and divisor as inputs.
        /// </summary>
        public static void HandleDivision() {
            try {
                Console.Write("Enter Dividend Value: ");
                decimal dividend = InputManager.GetDecimal();

                Console.Write("Enter Divisor Value: ");
                decimal divisor = InputManager.GetDecimal();

                Console.WriteLine($"{dividend} divided by {divisor} is {dividend / divisor}");
            } catch (DivideByZeroException) {
                Console.WriteLine("[Error] Divisor cannot be zero!");
            } catch (InvalidUserInputException e) {
                Console.WriteLine(e.Message);
            } catch (Exception e) {
                Console.WriteLine($"[Error] An unexpected error occured: {e.Message}");
            } finally {
                InputManager.PromptForContinuation();
            }
        }
    }
}