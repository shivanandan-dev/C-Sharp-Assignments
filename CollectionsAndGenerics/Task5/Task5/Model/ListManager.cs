namespace Task5.Model {
    public class ListManager<T> {
        private readonly List<T> _items;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListManager{T}"/> class,
        /// optionally seeded with an initial collection of items.
        /// </summary>
        /// <param name="initial">A collection of items to initialize the list with; or null for an empty list.</param>
        public ListManager(IEnumerable<T> initial = null) {
            _items = initial != null ? new List<T>(initial) : new List<T>();
        }

        /// <summary>
        /// Adds an item to the end of the list.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(T item) => _items.Add(item);

        /// <summary>
        /// Removes the first occurrence of the specified item from the list.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>
        /// True if the item was found and removed; otherwise, false.
        /// </returns>
        public bool Remove(T item) => _items.Remove(item);

        /// <summary>
        /// Determines whether the list contains a specific value.
        /// </summary>
        /// <param name="item">The item to locate in the list.</param>
        /// <returns>
        /// True if the list contains the specified item; otherwise, false.
        /// </returns>
        public bool Contains(T item) => _items.Contains(item);

        /// <summary>
        /// Writes each item in the list to the console, one per line.
        /// </summary>
        public void Display() {
            foreach (var it in _items) {
                Console.WriteLine(it);
            }
        }
    }
}
