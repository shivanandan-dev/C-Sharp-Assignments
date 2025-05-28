## Reflection

In this assignment, I learned how memory works in C# and why it is important to manage it well. A memory leak happens when the program keeps using more and more memory but never frees it. Over time, this can slow the program down or make it crash.

The most challenging part was finding where the leak happened. Watching the “Process Memory” counter showed me that memory kept growing, but it did not tell me which code caused the problem. Learning to use `dotnet-counters` and `dotnet-dump` to get heap snapshots was hard at first. I had to install the tools, list running processes, collect a dump, and then explore the heap with commands like `dumpheap -stat` and `gcroot`. It took practice to understand what each command did and how to read the results.

Another challenge was comparing the unbounded allocation in Task 1 with the capped list in Task 2. Writing code that removes old arrays when the list grows too large was simple, but seeing the real impact in the Memory Profiler made the difference clear. I learned to take heap snapshots before and after optimization and to use the Allocation Timeline to watch GC events.

**Key takeaways**:

* Always watch for memory leaks by monitoring heap size and object counts.
* Use profiling tools (`dotnet-counters`, `dotnet-dump`, Visual Studio Memory Profiler) to pinpoint leaks.
* Limit or reuse large buffers to prevent runaway memory growth.
* Small changes in code can have a big effect on performance and stability.## Reflection

In this assignment, I learned how memory works in C# and why it is important to manage it well. A memory leak happens when the program keeps using more and more memory but never frees it. Over time, this can slow the program down or make it crash.

The most challenging part was finding where the leak happened. Watching the “Process Memory” counter showed me that memory kept growing, but it did not tell me which code caused the problem. Learning to use `dotnet-counters` and `dotnet-dump` to get heap snapshots was hard at first. I had to install the tools, list running processes, collect a dump, and then explore the heap with commands like `dumpheap -stat` and `gcroot`. It took practice to understand what each command did and how to read the results.

Another challenge was comparing the unbounded allocation in Task 1 with the capped list in Task 2. Writing code that removes old arrays when the list grows too large was simple, but seeing the real impact in the Memory Profiler made the difference clear. I learned to take heap snapshots before and after optimization and to use the Allocation Timeline to watch GC events.

**Key takeaways**:

* Always watch for memory leaks by monitoring heap size and object counts.
* Use profiling tools (`dotnet-counters`, `dotnet-dump`, Visual Studio Memory Profiler) to pinpoint leaks.
* Limit or reuse large buffers to prevent runaway memory growth.
* Small changes in code can have a big effect on performance and stability.

Overall, this hands-on work gave me a deeper understanding of C# memory management and practical skills to diagnose and fix real memory issues.

