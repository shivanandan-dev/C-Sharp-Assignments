namespace Task1 {
    public class Notifier {
        /// <summary>
        /// Delegate defining the signature for notification handlers.
        /// </summary>
        /// <param name="message">The notification message.</param>
        public delegate void Notify(string message);

        public event Notify? OnAction;

        /// <summary>
        /// Performs an action and notifies all subscribers.
        /// </summary>
        /// <param name="message">The message to send to subscribers.</param>
        public void DoAction(string message) {
            OnAction?.Invoke(message);
        }
    }

    public static class OutputManager {
        /// <summary>
        /// Writes the notification message to the console.
        /// </summary>
        /// <param name="message">The notification message to display.</param>
        public static void HandleNotification(string message) {
            Console.WriteLine($"[Notification] {message}");
        }
    }

    internal class Program {
        /// <summary>
        /// Application entry point.
        /// </summary>
        public static void Main() {
            Notifier notifier = new Notifier();

            notifier.OnAction += OutputManager.HandleNotification;
            notifier.DoAction("Action performed successfully!");

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
