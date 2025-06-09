# Explanation of the issues and its respective fix

1. Unnecessary use of `MemoryStream`
    ```csharp
    byte[] buffer = Encoding.ASCII.GetBytes(data);
    memoryStream.Write(buffer, 0, buffer.Length);

    // Write from MemoryStream to file
    using (FileStream fileStream = new FileStream(path, FileMode.Create))
    {
        byte[] writeBuffer = memoryStream.ToArray();
        fileStream.Write(writeBuffer, 0, writeBuffer.Length);
    }
    ```

    - **Problem**: Writing into a `MemoryStream`, only to call `memoryStream.ToArray()` allocates an entirely new `byte[]` of identical length. So atleast two full copies of data coexist in memory briefly.
    - **Fix**: We already had the single `byte[]` from `Encoding.UTF8.GetBytes(data)`. We can write that directly to `FileStream`. By dropping `MemoryStream`, we keep only one memory at a time.

2. Unnecessary use of `.ToArray()`
    ```csharp
    byte[] writeBuffer = memoryStream.ToArray();
    fileStream.Write(writeBuffer, 0, writeBuffer.Length);
    ```

    - **Problem**: `memoryStream.ToArray()` must allocate and copy every byte from the underlying stream buffer into a brand‐new array.If data were large (e.g. megabytes or gigabytes), that temporary `ToArray()` could cause an out‐of‐memory spike.
    - **Fix**: Since we already had the original encoded byte[] (`writeBuffer`), call `fileStream.Write(writeBuffer, 0, writeBuffer.Length)` directly. This eliminates the extra “copy‐out” step.

3. Inefficient Per‐Byte Console Output
    ```csharp
    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
    {
        // Simulate memory inefficiency
        for (int i = 0; i < bytesRead; i++)
        {
            Console.Write((char)buffer[i]);
        }
        Console.WriteLine();
    }
    ```
    - **Problem**:
        - Every single `Console.Write((char)buffer[i])` is one call to the console I/O. If `bytesRead` is, say, 1024, we do 1024 separate `Console.Write(...)` calls, then a `Console.WriteLine()`. Not only is that slower (Console is comparatively high‐latency), but the per‐character loop also forces repeated conversions from `byte` → `char`.

    - **Fix**: After we read a chunk of bytes (`buffer[0..bytesRead-1]`), we do a single call to `Encoding.UTF8.GetString(buffer, 0, bytesRead)`. That allocates exactly one string for up to 4096 bytes. Then call `Console.Write(...)` once per chunk. In practice, that is far more efficient than making one console call per character.
