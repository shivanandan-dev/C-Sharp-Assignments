## Learnings and Memory Leak Optimization

---

## 1. What Is a Memory Leak?

A **memory leak** occurs when an application allocates memory but never releases it when it’s no longer needed. Over time, these unreleased allocations accumulate, causing the application’s memory usage to grow uncontrollably—leading to degraded performance or even crashes.

---

## 2. How to Diagnose a Memory Leak

### 2.1 Monitoring Process Memory

1. **Run the application.**
2. **Open your diagnostics tool** (e.g., Visual Studio Diagnostic Tools or Windows Performance Monitor).
3. **Observe the “Process Memory (MB)”** counter:

   * A steady upward trend—even when idle—indicates a leak.
4. **Limitation:** You’ll know *that* there’s extra memory retained, but not *where* it’s leaking.

### 2.2 Using `dotnet-counters`

1. **Install the tool:**

   ```bash
   dotnet tool install --global dotnet-counters
   ```
2. **List running .NET processes:**

   ```bash
   dotnet-counters ps
   ```
3. **Monitor your target process:**

   ```bash
   dotnet-counters monitor -p <process-id>
   ```
4. **Watch the “GC Heap Size (MB)”** metric:

   * If it climbs steadily without the usual post-GC drops, you’ve got a leak.

---

## 3. Finding the Root Cause with `dotnet-dump`

1. **Install the dump tool:**

   ```bash
   dotnet tool install --global dotnet-dump
   ```
2. **List .NET processes:**

   ```bash
   dotnet-dump ps
   ```
3. **Collect a memory dump:**

   ```bash
   dotnet-dump collect -p <process-id>
   ```
4. **Analyze the dump:**

   ```bash
   dotnet-dump analyze <path-to-dmp-file>
   ```
5. **Within the interactive analyzer:**

   * `dumpheap -stat` → show object types, counts, and sizes.
   * Identify the suspicious type (e.g., `int[]`) and its MethodTable (MT).
   * `dumpheap -mt <MethodTable>` → list all instances.
   * `gcroot -all <ObjectAddress>` → reveals the reference chain preventing collection.

---

## 4. Baseline Snapshot (Pre-optimization)

```csharp
namespace Task1 {
    public class MemoryEater {
        private readonly List<int[]> memAlloc = new List<int[]>();

        public void Allocate() {
            while (true) {
                memAlloc.Add(new int[1000]);
                Thread.Sleep(10);
            }
        }
    }

    class Program {
        static void Main(string[] args) {
            var me = new MemoryEater();
            me.Allocate();
        }
    }
}
```

* **Managed Heap Growth:**
  Continuous allocations of 1 000‐element arrays drive the heap from \~20 MB to \~200 MB in one minute, with no significant drops after GC.
* **Object Count:**
  Thousands of `int[]` instances remain rooted in `memAlloc`, so Live Object Count climbs without bound.

---

## 5. Optimized Snapshot (Post-optimization)

```csharp
namespace Task2 {
    class Program {
        static void Main(string[] args) {
            Console.Write("Enter the size of each array (e.g. 1000): ");
            int bufferSize = int.Parse(Console.ReadLine()!);

            Console.Write("Enter how many arrays to keep in memory (e.g. 100): ");
            int maxBuffers = int.Parse(Console.ReadLine()!);

            var memAlloc = new List<int[]>();

            Console.WriteLine("\nPress any key to start allocating...");
            Console.ReadKey();

            while (true) {
                memAlloc.Add(new int[bufferSize]);

                if (memAlloc.Count > maxBuffers) {
                    memAlloc.RemoveAt(0);
                }
                Thread.Sleep(10);
            }
        }
    }
}
```

* **Stable Heap Footprint:**
  Capping `memAlloc` at `maxBuffers` keeps the heap around 30–35 MB (depending on `bufferSize`), oscillating only as old buffers are dropped.
* **Capped Buffer Count:**
  Live `int[]` instances never exceed `maxBuffers`; post-GC drops are visible in memory graphs.

---

## 6. Why the Improvement Occurred

* **Bounded Retention:**
  Removing the oldest buffer when the list exceeds its cap prevents unbounded growth and caps worst-case memory usage.
* **Reduced LOH Pressure:**
  By limiting simultaneous large-array allocations on the Large Object Heap, you reduce fragmentation and GC overhead.

---

## 7. Role of the VS Memory Profiler

* **Heap Snapshots:**
  Capture and compare snapshots before and after optimization to see differences in total heap size and object counts.
* **Allocation Timeline:**
  Correlate allocation bursts with GC events—Task1 shows constant ramps, whereas Task2 shows steady-state oscillations.

---

## 8. Observations

* During Task1 monitoring, the process’s private memory counter climbed continuously—even between GC cycles—confirming no buffers were ever released.
* `dotnet-counters` revealed the GC Heap Size ramping upward without corresponding post‐GC drops.
* A memory dump exposed thousands of `int[1000]` instances rooted in `memAlloc`, pinpointing `MemoryEater.Allocate()` as the leak source.

---
