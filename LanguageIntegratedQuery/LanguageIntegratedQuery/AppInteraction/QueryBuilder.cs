namespace LanguageIntegratedQuery.AppInteraction {
    public class QueryBuilder<T> {
        private IEnumerable<T> _data;
        private Func<T, bool> _filter;
        private Func<T, object> _sortSelector;
        private bool _sortDescending;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryBuilder{T}"/> class.
        /// </summary>
        /// <param name="data">The source data to be queried.</param>
        public QueryBuilder(IEnumerable<T> data) {
            _data = data;
        }

        /// <summary>
        /// Adds a filter condition to the query.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>The current <see cref="QueryBuilder{T}"/> instance.</returns>
        public QueryBuilder<T> Filter(Func<T, bool> predicate) {
            _filter = predicate;
            return this;
        }

        /// <summary>
        /// Adds a sorting condition to the query.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by the sort selector.</typeparam>
        /// <param name="keySelector">A function to extract the sort key from each element.</param>
        /// <param name="descending">Whether to sort in descending order. Default is false (ascending).</param>
        /// <returns>The current <see cref="QueryBuilder{T}"/> instance.</returns>
        public QueryBuilder<T> SortBy<TKey>(Func<T, TKey> keySelector, bool descending = false) {
            _sortSelector = x => keySelector(x);
            _sortDescending = descending;
            return this;
        }

        /// <summary>
        /// Executes the built query and returns the result.
        /// </summary>
        /// <returns>The filtered and sorted collection.</returns>
        public IEnumerable<T> Execute() {
            IEnumerable<T> result = _data;

            if (_filter != null)
                result = result.Where(_filter);

            if (_sortSelector != null)
                result = _sortDescending
                    ? result.OrderByDescending(_sortSelector)
                    : result.OrderBy(_sortSelector);

            return result;
        }

        /// <summary>
        /// Adds a join operation to the query.
        /// </summary>
        /// <typeparam name="TOther">The type of the elements in the other collection.</typeparam>
        /// <typeparam name="TKey">The type of the key used for joining.</typeparam>
        /// <typeparam name="TResult">The type of the result elements.</typeparam>
        /// <param name="other">The collection to join with.</param>
        /// <param name="thisKeySelector">A function to extract the join key from elements of the source collection.</param>
        /// <param name="otherKeySelector">A function to extract the join key from elements of the other collection.</param>
        /// <param name="resultSelector">A function to create a result element from two matching elements.</param>
        /// <returns>A collection that contains joined elements.</returns>
        public IEnumerable<TResult> Join<TOther, TKey, TResult>(
            IEnumerable<TOther> other,
            Func<T, TKey> thisKeySelector,
            Func<TOther, TKey> otherKeySelector,
            Func<T, TOther, TResult> resultSelector) {
            var filtered = _filter != null ? _data.Where(_filter) : _data;

            return filtered.Join(other, thisKeySelector, otherKeySelector, resultSelector);
        }
    }
}
