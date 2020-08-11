namespace ZideunMail.Models
{
    public partial class EmailAccount
    {
        /// <summary>
        /// Gets or sets an email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets an email display name
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets an email host
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets an email port
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets an email user name
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets an email password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value that controls whether the SmtpClient uses Secure Sockets Layer (SSL) to encrypt the connection
        /// </summary>
        public bool EnableSsl { get; set; }

        /// <summary>
        /// Gets or sets a value that controls whether the default system credentials of the application are sent with requests.
        /// </summary>
        public bool UseDefaultCredentials { get; set; }

        /// <summary>
        /// Gets a friendly email account name
        /// </summary>
        public string FriendlyName
        {
            get
            {
                if (string.IsNullOrEmpty(DisplayName))
                    return Email;

                return $"{DisplayName} ({Email})";
            }
        }

        /// <summary>
        /// Gets or sets a store default email account identifier
        /// </summary>
        public int DefaultEmailAccountId { get; set; }

        /// <summary>
        /// Gets or sets a folder where mail messages should be saved (instead of sending them).
        /// For debug and test purposes only.
        /// </summary>
        public string PickupDirectoryLocation { get; set; }

        public EmailAccount Clone()
        {
            return (EmailAccount)this.MemberwiseClone();
        }

        public EmailAddress ToEmailAddress()
        {
            return new EmailAddress(this.Email, this.DisplayName);
        }
    }
}
