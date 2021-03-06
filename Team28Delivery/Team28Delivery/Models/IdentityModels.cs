﻿using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Activities.Statements;
using System.Linq;

namespace Team28Delivery.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
      // Table for Users
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        [ForeignKey("Address")]
        public int AddressID { get; set; }
        public string AccessLevel { get; set; }

        public virtual Addresses Address { get; set; }
        public virtual IList<Packages> Packages { get; set; }
        public virtual Employees Employee { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        // Databse tables
        public DbSet<Addresses> Addresses { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Packages> Packages { get; set; }
        public DbSet<Orders> Orders { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        


    }

    public class CustumDBContext : DbContext, IDisposable
    {
        public virtual IDbSet<Orders> Orders { get; set; }

    }

    public class Processor
    {
        private readonly CustumDBContext _dbContext;

        public Processor(CustumDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Orders getOrder(int id)
        {
            return _dbContext.Orders.FirstOrDefault(Orders => Orders.OrderID == id);
        }
    }

    public class CustumEmDBContext : DbContext, IDisposable
    {
        public virtual IDbSet<Employees> Employees { get; set; }

    }

    public class ProcessorEmployees
    {
        private readonly CustumEmDBContext _dbContext;

        public ProcessorEmployees(CustumEmDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Employees GetEmployees(String id)
        {
            return _dbContext.Employees.FirstOrDefault(e => e.EmployeeID == id);
        }
    }
}
