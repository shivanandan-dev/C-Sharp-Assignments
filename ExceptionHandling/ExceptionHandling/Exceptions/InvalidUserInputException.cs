namespace ExceptionHandling.Exceptions {
    public class InvalidUserInputException : Exception {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidUserInputException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public InvalidUserInputException(string message) : base(message) { }
    }
}
