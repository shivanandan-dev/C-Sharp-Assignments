namespace InventoryManager {
    public class Product {
        /// <summary>
        /// Gets or sets the unique identifier for the object.
        /// </summary>
        /// <param name="Id">The unique identifier.</param>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the object.
        /// </summary>
        /// <param name="Name">The name of the object.</param>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price of the object.
        /// </summary>
        /// <param name="Price">The price value.</param>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the object.
        /// </summary>
        /// <param name="Quantity">The quantity value.</param>
        public int Quantity { get; set; }

        /// <summary>
        /// Initializes a new instance of the Product class with the specified details.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <param name="name">The name of the product.</param>
        /// <param name="price">The price of the product.</param>
        /// <param name="quantity">The quantity of the product.</param>
        public Product(string id, string name, decimal price, int quantity) {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }
}