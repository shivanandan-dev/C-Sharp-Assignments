using ExceptionHandling.AppInteraction;

namespace ExceptionHandling {
    internal class Program {
        /// <summary>
        /// Entry point of the application.
        /// </summary>
        public static void Main() {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);
            Application.HandleMainMenu();
        }

        /// <summary>
        /// Handles unhandled exceptions that occur in the application.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">Contains information about the unhandled exception, including the exception object.</param>
        private static void MyHandler(object sender, UnhandledExceptionEventArgs args) {
            Exception e = (Exception)args.ExceptionObject;
            Console.WriteLine("Unhandled exception caught: " + e.StackTrace);
        }
    }
}