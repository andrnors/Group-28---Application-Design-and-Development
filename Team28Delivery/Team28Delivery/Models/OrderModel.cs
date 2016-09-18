using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Team28Delivery.Models
{
    public class OrderModel
    {
        public DeliveryAddress Delivery { get; set; }
        public PickupAddress Pickup { get; set; }
        public PackageDetails PackageDetails { get; set; }
    }

    public class DeliveryAddress
    {
        [Required]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Required]
        [Display(Name = "Suburb")]
        public string Suburb { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Required]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }
    }

    public class PickupAddress
    {
        [Required]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Required]
        [Display(Name = "Suburb")]
        public string Suburb { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Required]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }
    }

    public class PackageDetails
    {
        
        [Display(Name = "Special info")]
        public string SpecialInfo { get; set; }

        [Required]
        [Display(Name = "Weight")]
        public double Weight { get; set; }

        [Required]
        [Display(Name = "Priority")]
        public string Priority { get; set; }

        [Required]
        [Display(Name = "Reciever Name")]
        public string RecieverName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Pickup Time")]
        public string PickupTime { get; set; }
         
    }
}