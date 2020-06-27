using System.Collections.Generic;

namespace Acme.Orders.Domain.Entities
{
    public class Customer
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Username { get; private set; }
        public string EmailAddress { get; private set; }
        public IEnumerable<CustomerAddress> Addresses { get; private set; }
        public IEnumerable<Order> Orders { get; private set; }
  
        private Customer() { }
        public Customer(string firstname, string lastname, string username,
                    string emailAddress)
        {
            FirstName = firstname;
            LastName = lastname;
            Username = username;
            EmailAddress = emailAddress;
        }
    }
}
