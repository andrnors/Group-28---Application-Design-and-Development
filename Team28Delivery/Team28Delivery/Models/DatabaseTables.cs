using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

// All tables for the database are defined here, EXCEPT Users table, that is in IdentityModels
namespace Team28Delivery.Models
{
    // Table for orders
    [Table("Orders")]
    public class Orders
    {
        [Key]
        public int OrderID { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Timestap { get; set; }
        [ForeignKey("Address")]
        public int PickupAddressID { get; set; }
        public DateTime ReadyForPickUpTime { get; set; }
        public DateTime? WarehouseArrivalTime { get; set; }
        public string OrderStatus { get; set; }
        public string OrderPriority { get; set; }
        public DateTime? WarehouseDepartureTime { get; set; }

        public virtual Addresses Address { get; set; }
        public virtual IList<Packages> Package { get; set; }
    }
    // Table for addresses
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

    //Table for employees
    [Table("Employees")]
    public class Employees
    {
        [Key,ForeignKey("User")]
        public string EmployeeID { get; set; }
        public string BankAccount { get; set; }
        public string CarRegister { get; set; }
        public string AccessLevel { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
    // Table for packages
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
        [ForeignKey("Address")]
        public int? RecieverAddressID { get; set; }
        public double Cost { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Orders Order { get; set; }
        public virtual Addresses Address { get; set; }

    }

    // Status class for tracking
    public enum Status
    {
        Recieved, Shipped, Completed
    }
    // Priority class for Priority of packages
    public enum Priority
    {
        Normal, High
    }
}
