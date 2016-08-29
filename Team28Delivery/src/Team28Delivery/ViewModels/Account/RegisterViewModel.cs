using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Team28Delivery.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
        

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        public String PhoneNumber { get; set;  }

        [Required]
        [Display(Name= "Street address")]
        public String StreetAddress { get; set; }
        
        [Required]
        [Display(Name = "Suburb")]
        public String Suburb { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }

        [Required]
        [Display(Name = "State")]
        public String State { get; set; }

        [Required]
        [Display(Name = "Country")]
        public String Country { get; set; }

    }
}
