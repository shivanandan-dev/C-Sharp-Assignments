namespace LanguageIntegratedQuery.Model {
    public class Menu {
        /// <summary>
        /// Gets or sets the description of the menu item.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the action handler associated with the menu item.
        /// This delegate will be invoked when the menu item is selected.
        /// </summary>
        public Action Handler { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Menu"/> class with the specified description and handler.
        /// </summary>
        /// <param name="description">The description to display for the menu item.</param>
        /// <param name="handler">The action to execute when the menu item is selected.</param>
        public Menu(string description, Action handler) {
            Description = description;
            Handler = handler;
        }
    }
}
