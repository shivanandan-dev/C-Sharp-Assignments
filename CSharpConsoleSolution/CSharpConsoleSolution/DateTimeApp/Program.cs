using UtilityApp;

namespace DateTimeApp {
    public class Program {
        public static void Main() {
            string now = Helper.FormatTimestamp(DateTime.Now);
            Console.WriteLine(Helper.Beautify($"Current Time: {now}"));
        }
    }
}
