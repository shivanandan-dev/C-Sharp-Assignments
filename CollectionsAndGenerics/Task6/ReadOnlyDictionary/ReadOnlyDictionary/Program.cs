class Program {
    /// <summary>
    /// Builds a Dictionary<string,int> and returns it as an IReadOnlyDictionary
    /// to prevent callers from modifying it.
    /// </summary>
    public static IReadOnlyDictionary<string, int> GenerateDictionary() {
        var dict = new Dictionary<string, int> {
            ["Apple"] = 5,
            ["Banana"] = 10,
            ["Cherry"] = 15
        };
        return dict;
    }

    /// <summary>
    /// Prints each key/value pair in the given read-only dictionary.
    /// </summary>
    public static void PrintDictionary(IReadOnlyDictionary<string, int> dict) {
        if (dict == null) throw new ArgumentNullException(nameof(dict));

        foreach (var kvp in dict)
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
    }

    static void Main() {
        IReadOnlyDictionary<string, int> dictionary = GenerateDictionary();
        PrintDictionary(dictionary);
        Console.ReadKey();
    }
}
