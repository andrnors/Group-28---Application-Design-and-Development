using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team28Delivery.Models
{
    public class OrderViewModel
    {
        public Dictionary<Packages, Orders> OrderDictionaryMap { get; set; }
    }

    public class OrderDetailsViewModel
    {
        public Addresses pickupAddress { get; set; }
        public Addresses deliveryAddress { get; set; }


        public Orders order { get; set; }
        public Packages package { get; set; }
    }     
}