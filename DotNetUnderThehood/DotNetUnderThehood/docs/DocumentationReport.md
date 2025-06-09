## Project Overview

This project is a simple console application designed to perform basic arithmetic operations (addition, subtraction, multiplication, and division) on two user-supplied integers. It demonstrates modular design, clear exception handling, and straightforward console I/O.

## Approach

1. **Modularity**

   * Responsibilities are separated using different namespaces and static classes:

     * `DotNetUnderThehood.Model`: Contains `MathUtils` with int-based arithmetic methods.
     * `DotNetUnderThehood.IOManager`: (Assumed) Handles input/output operations.
     * `DotNetUnderThehood.AppInteraction`: Contains `Application` class that manages user interaction and calculation logic.

2. **Concrete Types**

   * All arithmetic methods operate on only `int`. This aligns directly with the requirement to work with integers and simplifies the implementation.

3. **Exception Handling**

   * Division by zero is explicitly checked: if the divisor is zero, a `DivideByZeroException` is thrown with a clear message.
   * Input validation (e.g., ensuring the user enters valid integers) is presumed to be handled by `InputManager.GetIntValue()`. If invalid input occurs, the application prompts the user again.

4. **User Experience**

   * Clear console prompts guide the user through entering two integers and selecting an operation.
   * Results are displayed with contextual messages, and errors (such as “cannot divide by zero”) are shown directly.

## Concepts Applied

* **Static Classes & Methods**:
  Promotes organization by grouping related utility methods (e.g., `MathUtils`) in one place.

* **Concrete Type Usage**:
  By using `int` for all operations, the code is simpler and there is no overhead from unnecessary generic constraints.

* **Exception Handling**:

  * Explicit check for zero divisor in `MathUtils.Div(int a, int b)` to throw `DivideByZeroException`.
  * Catches invalid input and reprompts via the input manager.

* **Console I/O**:
  Demonstrates reading from and writing to the console, including loops for retrying invalid entries.

## Challenges and Resolutions

* **Division by Zero**

  * *Description*: Preventing the application from crashing when the user attempts to divide by zero.
  * *Resolution*: In `MathUtils.Div(int a, int b)`, perform a check `if (b == 0)` and throw `new DivideByZeroException("Cannot divide by zero.")`. This ensures callers receive a clear, specific exception and can display an appropriate error message.

* **User Input Validation**

  * *Description*: Ensuring that the user enters valid integers.
  * *Resolution*: Presumed that `InputManager.GetIntValue()` handles parsing and reprompts until the user enters a valid integer. If not already implemented, one would wrap `int.TryParse` in a loop to re-ask when parsing fails.

* **Removing Unnecessary Generics**

  * *Description*: The original generic implementation (`INumber<T>`) was more complex than needed since only integers are required.
  * *Resolution*: Simplified all methods to accept and return `int`. This reduces code complexity and avoids the need for .NET 7+ generic constraints.

## File List

* `Program.cs` - Entry point for the application; invokes the `Application` class to orchestrate console interaction.

* `MathUtils.cs` - Contains int-based arithmetic utility methods (`Add`, `Sub`, `Mul`, `Div`).

* `Application.cs` - Handles user interaction (prompts, reading two integers, selecting the operation) and invokes `MathUtils` methods.

* `ExplorationQuestions.md` - Answers to exploration questions related to the project (if still relevant).

* `DocumentationReport.md` - This documentation report.

