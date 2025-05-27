namespace LanguageIntegratedQuery.Model {
    internal class Product {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the category to which the product belongs.
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class with the specified id, name, price, and category.
        /// </summary>
        /// <param name="productId">The unique identifier for the product.</param>
        /// <param name="productName">The name of the product.</param>
        /// <param name="price">The price of the product.</param>
        /// <param name="category">The category to which the product belongs.</param>
        public Product(int productId, string productName, decimal price, Category category) {
            ProductId = productId;
            ProductName = productName;
            Price = price;
            Category = category;
        }
    }
}
