1. **Baseline Snapshot (Pre-optimization)**

   * Using Visual Studio’s Memory Usage tool, the unbounded-allocation version showed the **Managed Heap size** climbing continuously (e.g. from 20 MB up to 200 MB over a minute) with no significant drops after GC.
   * The **Object Count** view revealed thousands of active `int[]` instances, all rooted in the `memAlloc` list.

2. **Optimized Snapshot (Post-optimization)**

   * After switching to `ArrayPool<int>` plus a capped queue, the **Managed Heap size** stabilized (e.g. oscillating between 30 MB and 35 MB).
   * The **Object Count** now shows at most `MaxHeldBuffers` rented buffers, and GC collections actually free returned arrays, leading to visible post-GC memory drops.

3. **Why the Improvement Occurred**

   * **Bounded Retention:** By limiting the number of live buffers, you prevent runaway growth.
   * **Array Pooling:** Reusing buffers avoids repeated Large Object Heap (LOH) allocations and fragmentation, cutting both allocation churn and GC pressure.

4. **Role of VS Memory Profiler**

   * It lets you capture **heap snapshots** at key moments (before/after GC or on demand) and compare object counts and sizes side-by-side.
   * The **Allocation Timeline** view correlates your code’s allocations with GC events, making leaks and hotspots immediately visible.

5. **Key Takeaway**

   * Regularly profiling—even simple demos—reveals hidden allocation patterns and validates that your optimizations (pooling, caps, proper disposal) are actually effective in reducing memory usage and GC overhead.
