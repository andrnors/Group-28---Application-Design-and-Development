using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team28Delivery.Models
{
    public class CustomerUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string StreetAddress { get; set; }
        public string Suburb { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string UserType { get; set; }
        public string Email { get; set; }
    }
}