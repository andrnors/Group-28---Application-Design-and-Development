using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team28Delivery.Models;

namespace Team28Delivery.Areas.Employee.Models
{
    public class OrderModel
    {
        public Packages Packages { get; set; }
        public Orders Orders { get; set; }
    }
}