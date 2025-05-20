# Documentation Report

## Project Overview

This project is a simple console application designed to perform basic arithmetic operations (addition, subtraction, multiplication, and division) on two user-supplied numbers. It demonstrates the use of generic programming in C# with the `INumber<T>` interface, modular design, and exception handling.

## Approach

1. **Modularity**  
   - The project separates concerns using different namespaces and static classes:
     - `DotNetUnderThehood.Model`: Contains `MathUtils` with generic arithmetic methods.
     - `DotNetUnderThehood.IOManager`: (Assumed) Handles input/output operations.
     - `DotNetUnderThehood.AppInteraction`: Contains `Application` class that helps with user interaction and calculation logic.

2. **Generics and Modern C#**  
   - Used generic methods with constraints (`where T : INumber<T>`) to allow arithmetic operations on multiple numeric types, not just `decimal` or `int`.
   - This approach leverages .NET 7+ features for more flexible and reusable code.

3. **Exception Handling**  
   - Implemented try-catch blocks to gracefully handle invalid user input and arithmetic errors (like division by zero).

4. **User Experience**  
   - Clear prompts guide the user through input.
   - Results are displayed with contextual messages.

## Concepts Applied

- **Generics (`INumber<T>`)**: Allows arithmetic operations on any numeric type, making the code extensible and reusable.
- **Namespaces & Static Classes**: Promotes organization and modularity.
- **Exception Handling**: Provides robust error handling for both input and computation.
- **Console I/O**: Demonstrates user interaction in a console environment.

## Challenges and Resolutions

- **Division by Zero**
  - *Description*: Needed to handle division by zero for different numeric types.
  - *Resolution*: Caught `DivideByZeroException` in `MathUtils.Div<T>` and rethrew as a generic exception with a message. This keeps the error handling generic and consistent for all numeric types.

- **User Input Validation**
  - *Description*: Ensuring the user enters valid numbers.
  - *Resolution*: Presumed that `InputManager.GetDecimalValue()` includes logic for input validation. Otherwise, would recommend implementing loops to reprompt on invalid input.

- **Applying Generics to Arithmetic**
  - *Description*: Arithmetic operators (`+`, `-`, `*`, `/`) are not directly supported for generics in older C# versions.
  - *Resolution*: Used the `INumber<T>` interface, a .NET 7+ feature, which enables operator overloading for generic types.

## File List

- `Program.cs` - Entry point for the application.
- `MathUtils.cs` - Contains generic arithmetic utility methods.
- `Application.cs` - Handles user interaction and ties together I/O and computation.
- `ExplorationQuestions.md` - Answers to exploration questions.
- `DocumentationReport.md` - This documentation report.


