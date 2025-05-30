namespace Task5.Model {
    public class StackManager<T> {
        private readonly Stack<T> _stack = new Stack<T>();

        /// <summary>
        /// Gets the number of items contained in the stack.
        /// </summary>
        public int Count => _stack.Count;

        /// <summary>
        /// Pushes an item onto the top of the stack.
        /// </summary>
        /// <param name="item">The item to push onto the stack.</param>
        public void Push(T item) => _stack.Push(item);

        /// <summary>
        /// Removes and returns the item at the top of the stack.
        /// </summary>
        /// <returns>The item removed from the top of the stack.</returns>
        public T Pop() => _stack.Pop();

        /// <summary>
        /// Removes all items from the stack.
        /// </summary>
        public void Clear() => _stack.Clear();
    }
}
