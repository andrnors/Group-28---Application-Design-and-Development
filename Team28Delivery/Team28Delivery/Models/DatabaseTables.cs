﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Team28Delivery.Models
{
    [Table("Orders")]
    public class Orders
    {
        [Key]
        public int OrderID { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Timestap { get; set; }
        [ForeignKey("Address")]
        public int PickupAddressID { get; set; }
        public string ReadyForPickUpTime { get; set; }
        public string WarehouseArrivalTime { get; set; }
        public string OrderStatus { get; set; }
        public string OrderPriority { get; set; }
        public string WarehouseDepartureTime { get; set; }
        
        public virtual Addresses Address { get; set; }
        public virtual IList<Packages> Package { get; set; }
    }
    [Table("Addresses")]
    public class Addresses
    {
        [Key]
        public int AddressID { get; set; }
        public string StreetAddress { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public virtual IList<ApplicationUser> User { get; set; }
        public virtual IList<Orders> Order { get; set; }
    }

    [Table("Employees")]
    public class Employees
    {
        [Key,ForeignKey("User")]
        public string EmployeeID { get; set; }
        public string BankAccount { get; set; }
        public string CarRegister { get; set; }

        public virtual ApplicationUser User { get; set; }
    }

    [Table("Packages")]
    public class Packages
    {
        [Key]
        public int PackageID { get; set; }
        [ForeignKey("User")]
        public string SenderID { get; set; }
        [ForeignKey("Order")]
        public int OrderID { get; set; }
        public string RecieversName { get; set; }
        public double Weight { get; set; }
        public string SpecialInfo { get; set; }
        public int RecieverAddressID { get; set; }
        public double Cost { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Orders Order { get; set; }
        
    }

    public enum Status
    {
        Recieved, Shipped, Completed
    }
    public enum Priority
    {
        Normal, High
    }
}