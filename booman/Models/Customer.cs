using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace booman.Models
{
    public class Customer
    {
        // Data fields
        private string id;
        private string name;
        private string phone;
        private string email;
        private string address;

        // Constructors
        public Customer() { }
        public Customer(string id, string name, string phone, string email, string address)
        {
            this.id = id;
            this.name = name;
            this.phone = phone;
            this.email = email;
            this.address = address;
        }

        // Properties
        public string ID
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
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
        public string Address
        {
            get { return this.address; }
            set { this.address = value; }
        }
    }
}
