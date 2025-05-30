namespace InventoryManager {
    public class Menu {
        /// <summary>
        /// Gets or sets the description of the menu item.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the action to be invoked when the menu item is selected.
        /// </summary>
        public Action Handler { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Menu"/> class with the specified description and handler.
        /// </summary>
        /// <param name="description">The description of the menu item.</param>
        /// <param name="handler">The action to invoke when the menu item is selected.</param>
        public Menu(string description, Action handler) {
            Description = description;
            Handler = handler;
        }
    }
}
