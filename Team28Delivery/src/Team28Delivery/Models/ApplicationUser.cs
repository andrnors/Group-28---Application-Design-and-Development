using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Team28Delivery.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Country { get; internal set; }
        public string FirtName { get; internal set; }
        public string LastName { get; internal set; }
        public string PostalCode { get; internal set; }
        public string State { get; internal set; }
        public string StreetAddress { get; internal set; }
        public string Suburb { get; internal set; }
    }
}
