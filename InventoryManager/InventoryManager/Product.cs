namespace InventoryManager {
    public class Product {
        string _id;
        string _name;
        decimal _price;
        int _quantity;

        public string Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public decimal price { get => _price; set => _price = value; }
        public int quantity { get => _quantity; set => _quantity = value; }

        /// <summary>
        /// Initializes a new instance of the Product class with the specified details.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <param name="name">The name of the product.</param>
        /// <param name="price">The price of the product.</param>
        /// <param name="quantity">The quantity of the product.</param>
        public Product(string id, string name, decimal price, int quantity) {
            _id = id;
            _name = name;
            _price = price;
            _quantity = quantity;
        }
    }
}