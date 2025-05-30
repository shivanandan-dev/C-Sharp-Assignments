namespace Task5.Model {
    public class QueueManager<T> {
        private readonly Queue<T> _queue;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueManager{T}"/> class,
        /// optionally seeded with an initial collection of items.
        /// </summary>
        /// <param name="initial">A collection of items to initialize the queue with; or null for an empty queue.</param>
        public QueueManager(IEnumerable<T> initial = null) {
            _queue = initial != null ? new Queue<T>(initial) : new Queue<T>();
        }

        /// <summary>
        /// Gets the number of items contained in the queue.
        /// </summary>
        public int Count => _queue.Count;

        /// <summary>
        /// Adds an item to the end of the queue.
        /// </summary>
        /// <param name="item">The item to enqueue.</param>
        public void Enqueue(T item) => _queue.Enqueue(item);

        /// <summary>
        /// Removes and returns the item at the beginning of the queue.
        /// </summary>
        /// <returns>The item removed from the front of the queue.</returns>
        public T Dequeue() => _queue.Dequeue();

        /// <summary>
        /// Writes each item in the queue to the console in FIFO order.
        /// </summary>
        public void Display() {
            foreach (var it in _queue) Console.WriteLine(it);
        }
    }
}
