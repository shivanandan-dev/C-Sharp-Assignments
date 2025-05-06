namespace ContactManager {
    internal class Contact {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string AdditionalInformation { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Contact"/> class with the specified details.
        /// </summary>
        /// <param name="name">The name of the contact.</param>
        /// <param name="phoneNumber">The phone number of the contact.</param>
        /// <param name="email">The email address of the contact.</param>
        /// <param name="additionalInformation">Additional information about the contact.</param>
        public Contact(string name, string phoneNumber, string email, string additionalInformation) {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            AdditionalInformation = additionalInformation;
        }
    }
}
