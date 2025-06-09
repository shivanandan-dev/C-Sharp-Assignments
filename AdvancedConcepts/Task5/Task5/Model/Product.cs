namespace Task5.Model {
    public class Product {
        /// <summary>
        /// Gets the name of the product.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the category of the product.
        /// </summary>
        public string Category { get; }

        /// <summary>
        /// Gets the price of the product.
        /// </summary>
        public decimal Price { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class with the specified values.
        /// </summary>
        /// <param name="name">The name of the product.</param>
        /// <param name="category">The category to which the product belongs.</param>
        /// <param name="price">The price of the product.</param>
        public Product(string name, string category, decimal price) {
            Name = name;
            Category = category;
            Price = price;
        }

        /// <summary>
        /// Returns a string representation of the product.
        /// </summary>
        /// <returns>A formatted string containing the product's name, category, and price.</returns>
        public override string ToString() => $"{Name} — {Category} — ${Price:F2}";
    }
}
