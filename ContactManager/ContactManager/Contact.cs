namespace ContactManager {
    internal class Contact {
        string _name;
        string _phoneNumber;
        string _email;
        string _additionalInformation;

        public string Name { get => _name; set => _name = value; }
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public string Email { get => _email; set => _email = value; }
        public string AdditionalInformation { get => _additionalInformation; set => _additionalInformation = value; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Contact"/> class with the specified details.
        /// </summary>
        /// <param name="name">The name of the contact.</param>
        /// <param name="phoneNumber">The phone number of the contact.</param>
        /// <param name="email">The email address of the contact.</param>
        /// <param name="additionalInformation">Additional information about the contact.</param>
        public Contact(string name, string phoneNumber, string email, string additionalInformation) {
            _name = name;
            PhoneNumber = phoneNumber;
            _email = email;
            _additionalInformation = additionalInformation;
        }
    }
}
