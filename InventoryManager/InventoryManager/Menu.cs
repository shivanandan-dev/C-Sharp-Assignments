namespace InventoryManager {
    public class Menu {
        public string Description { get; set; }
        public Action Handler { get; set; }

        public Menu(string description, Action handler) {
            Description = description;
            Handler = handler;
        }
    }
}
