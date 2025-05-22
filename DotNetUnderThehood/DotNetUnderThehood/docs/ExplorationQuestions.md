# **Exploration Questions**

### 1. Explain what the .NET platform is and its primary purpose. 
.NET is a framework which was developed in the year 2002, to abstract away many low-level programming details and provide tools, librarires and runtime environment (like Common Language Runtime or CLR) to simplify development, enhance productivity, ensure security and reliability across different types of applications and operating systems.

**Key Features**
- Cross Platform Support
- Multiple Language Support (F#, C#, Visual Basic)
- Large standard library for common programming tasks
- Managed code and memory management through CLR (Garbage Collector)

---

### 2. What are the key components of the .NET platform? 

The key components in .NET platforms:
- **Roslyn Compiler (C# Compiler)** - This is the compiler which is responsible for compiling the `*.cs` files. Unlike C compilers this doesn't convert the `*.cs` directly into a machine code or binary code. Instead it converts the `*.cs` files to Common Intermediate Language (CIL), also known as Intermediate Language (IL). CIL is a programming language like any, perhaps not the prettiest ever, but still human-readable unlike the binary code. The compiled code is stored in assemblies with `.dll` file extension.

- **Common Language Runtime (CLR)** - It is a runtime environment that manages the execution of .NET application. It is responsible for converting the Common Intermediate Language (CIL) files to Machine Code using **Just-In-Time (JIT)** compiler. And also it is responsible for low-level task such as memory management, error handling and dealing with threads.

- **Just-In-Time Compiler (JIT)** - It is part of the Common Language Runtime (CLR) which is responsible for converting the Intermediate Language to Executable File `*.exe`.

- **Framework Class Library (FCL)** - It contains various interfaces, classes and different datatypes to develope different methods and develope different types of applications such as web application, etc.,

---

### 3. Differentiate between the Common Language Runtime (CLR) and the Common Type System (CTS) in .NET. 

- **Common Language Runtime (CLR)** - It is a runtime environment that manages the execution of .NET application. It is responsible for converting the Common Intermediate Language (CIL) files to Machine Code using Just-In-Time (JIT) compiler. And also it is responsible for low-level task such as memory management, error handling and dealing with threads.

- **Common Type System (CTS)** - It standardizes the data types of all programming languages using .NET under the umbrella of .NET to a common data type for easy and smooth communication among these .NET languages.

---

### 4. What is the role of the Global Assembly Cache (GAC) in .NET? 

Global Assembly Cache allows multiple .NET applications on the same machine to share assemblies (DLLs) instead of each app having its own copy. This way it reduces dupliation and saves disk spaces. It acts as a global store for assemblies, ensuring secure, versioned and shared use of libraries across multiple .NET applications on a single Windows machine.

---

### 5. Explain the difference between value types and reference types in C#. 

| Value Types      |  Reference Types|
|------------|-----|
| If a variable is a value type, the only way to modify the value of a variable is through the variable itself.      |  If a variable is a reference type, the variable of this type store an identity (not a value) of a object, called a reference. |
| Value store in the variable are independent.       |  Two variables can hold a reference to the same object, so they are not independent. | 
| All struct instances are value types    |  All class instances are reference types |
| Simple built-in types (`int`, `decimal`, `Datetime`, etc.,) | Built-in classes (`list<T>`, `object`, `array`) |
| `int a = 5` </br> `a` stores the value. | `var listA = new List<int>()` </br>`ListA` stores the reference. |

---

### 6. Describe the concept of garbage collection on .NET and its advantages. 

- Each object in our programs has its lifecycle.
- Once an objects is no longer used, it should be `removed` from the computer’s memory so that new objects can occupy it.
- In .NET, the heap’s memory management mechanism is called the `Garbage Collector (GC)`.
- It is part of the `Common Language Runtime (CLR)`
- Garbage Collector will not clean the memory immediately.
- We can’t deterministically say when exactly that will happen.
- Runs in background on its own, separate thread.
- It may stop all other threads when working.
- After the Garbage Collector frees the memory occupied by unused objects, it performs memory defragmentation.
- The process of moving the objects in memory to create a bigger block of free memory is called defragmentation.

**Triggers of Garbage Collector**

- When the operating system informs the CLR that it has `little free memory left`.
- When the amount of memory occupied by objects on the heap surpasses a given threshold.
- When the `GC.Collect()` method is called. `GC.Collect()` method is not used in the production code, it is predominantly used for debugging purposes.

---

### 7. What is the purpose of the Globalization and Localization features in .NET? 

- **Globalization** is the process of designing and developing an application that functions for multiple cultures or regions. It involves separating the application’s core logic from the culture-specific information (such as date formats, number formats, and string resources).

- **Localization** is the process of customizing your application for a specific culture or locale. This typically means providing translations and adjusting formats for dates, numbers, and currencies.

---

### 8.  Explain the role of the Common Intermediate Language (CIL) and Just-In-Time (JIT) compilation in the .NET framework. 

- **Common Language Runtime (CLR)** - It is a runtime environment that manages the execution of .NET application. It is responsible for converting the Common Intermediate Language (CIL) files to Machine Code using **Just-In-Time (JIT)** compiler. And also it is responsible for low-level task such as memory management, error handling and dealing with threads.

- **Just-In-Time Compiler (JIT)** - It is part of the Common Language Runtime (CLR) which is responsible for converting the Intermediate Language to Executable File `*.exe`.

---