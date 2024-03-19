using System;

namespace booman.Models
{
    public class Account
    {
        // Data fields
        private string phone;
        private string email;
        private string password;
        private string fullName;
        private string role;

        // Constructors

        // Properties
        public string Phone
        {
            get { return this.phone; }
            set { this.phone = value; }
        }
        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }
        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }
        public string FullName
        {
            get { return this.fullName; }
            set { this.fullName = value; }
        }
        public string Role
        {
            get { return this.role; }
            set { this.role = value; }
        }

        // Methods
    }
}
