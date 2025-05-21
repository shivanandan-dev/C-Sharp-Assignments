## Assignment 11 - Memory Management in C#

### Task 1: Value Types vs Reference Types

I created a `Person` class with the fields `Name` and `Age`. In the `Program` class, I made a `HandleValueUpdate` method that takes an integer and a `Person` object. Inside the method, the integer value is changed to 20 and the person's name and age are updated.  
In the `Main` method, I set up an integer and a `Person` object, displayed their values, then passed them to `HandleValueUpdate`. After the call, the integer stayed the same but the person's name and age were changed.

This shows the difference between value types and reference types:
- **Value types** like `int` are copied when passed to methods, so changes inside the method don't affect the original variable.
- **Reference types** like `Person` pass a reference to the actual object, so changes inside the method are reflected outside as well.

---

### Task 2: Stack and Heap Memory

I added two methods: `AllocateLargeArray` and `ManyLocalVariablesCalculation`.

- `AllocateLargeArray` creates a large int array (`int[]`), which is a reference type. The array is stored on the heap.  
- `ManyLocalVariablesCalculation` creates many individual integer variables, which are value types and are stored on the stack.

By running the program and watching memory usage in Visual Studio’s Diagnostic Tools:
- When calling `AllocateLargeArray`, heap memory usage increases.
- When running `ManyLocalVariablesCalculation`, stack memory is used, but this change is less visible in profiling tools.

### Task 3: Garbage Collection and Performance

I wrote a `CreateAndDestroyObjects` method in the `GarbageCollection` project. It creates a lot of large objects in a loop.  
In the `Main` method, I printed the memory usage before and after creating the objects, and then again after calling `GC.Collect()` to force garbage collection.

When running the program, memory usage jumps when creating many objects. After calling `GC.Collect()`, unused memory is released and usage drops.

### Task 4: IDisposable and the `using` Statement

In the `IDisposableDemo` project, I made a `FileWriter` class (inherited from `IDisposable`) that writes to a file and ensures the file is closed when it's disposed.  
In `Main`, I used a `using` block to create a `FileWriter`, wrote some text, and let the block automatically clean up the file. After the `using` block, I opened the file for reading. Since the file was properly released, I could open and read it without any errors.

---