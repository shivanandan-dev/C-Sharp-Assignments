namespace Task5.Model {
    public class RecordManager<TKey, TValue> {
        private readonly Dictionary<TKey, TValue> _dict = new Dictionary<TKey, TValue>();

        /// <summary>
        /// Adds a new record with the specified key and value.
        /// </summary>
        /// <param name="key">The key of the record to add.</param>
        /// <param name="value">The value associated with the key.</param>
        public void Add(TKey key, TValue value) => _dict.Add(key, value);

        /// <summary>
        /// Removes the record with the specified key.
        /// </summary>
        /// <param name="key">The key of the record to remove.</param>
        /// <returns>True if the record was found and removed; otherwise, false.</returns>
        public bool Remove(TKey key) => _dict.Remove(key);

        /// <summary>
        /// Writes all key-value records to the console in "key | value" format.
        /// </summary>
        public void Display() {
            Console.WriteLine("{0,-15} | {1}", "Key", "Value");
            Console.WriteLine(new string('-', 30));
            foreach (var kv in _dict) {
                Console.WriteLine("{0,-15} | {1}", kv.Key, kv.Value);
            }
        }
    }
}
