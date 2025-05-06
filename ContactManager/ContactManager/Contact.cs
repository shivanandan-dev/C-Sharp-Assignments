namespace ContactManager {
    internal class Contact {
        /// <summary>
        /// Gets or sets the name of the contact.
        /// </summary>
        /// <value>The name of the contact as a string.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the contact.
        /// </summary>
        /// <value>The phone number as a string. It should follow a valid phone number format.</value>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the email address of the contact.
        /// </summary>
        /// <value>The email address as a string. It should follow a valid email format.</value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets additional information about the contact.
        /// </summary>
        /// <value>Any additional notes or details about the contact as a string.</value>
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
