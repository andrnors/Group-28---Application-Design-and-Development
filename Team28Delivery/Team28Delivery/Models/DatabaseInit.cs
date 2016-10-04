using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Team28Delivery.Models
{
    // Drops databse every time 
    // This is to ensure that the roleManager is doing it job
    // adn because we don't want the database to fill up with crap while debuging
    public class DatabaseInit : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            roleManager.Create(new IdentityRole("Customer"));  // Customer role
            roleManager.Create(new IdentityRole("Employee")); // Employee role
            roleManager.Create(new IdentityRole("Admin")); // Admin role

            var EmployeeAddress = new Addresses
            {
                Suburb = "West End",
                State = "Queensland",
                PostalCode = "4121",
                StreetAddress = "113 Hardgrave Rd"
            };

            context.Addresses.Add(EmployeeAddress);

            var EmployeeUser = new ApplicationUser
            {
                UserName = "employee@28delivery.au",
                Email = "employee@28delivery.au",
                FirstName = "Graham",
                LastName = "Payne",
                AddressID = EmployeeAddress.AddressID,
                Phone = "987654321",
                AccessLevel = "Employee"

            };

            var Employee = new Employees
            {
                EmployeeID = EmployeeUser.Id,
                BankAccount = "1206387898",
                CarRegister = "#YOLO"
            };

            context.Employees.Add(Employee);

            userManager.Create(EmployeeUser, "#Password1");
            userManager.AddToRole(EmployeeUser.Id, "Employee");

            var AdminAddress = new Addresses
            {
                Suburb = "South Brisbane",
                State = "Queensland",
                PostalCode = "4101",
                StreetAddress = "36 Vulture Street"
            };

            context.Addresses.Add(AdminAddress);

            var Admin= new ApplicationUser
            {
                UserName = "admin@28delivery.au",
                Email = "admin@28delivery.au",
                FirstName = "Andy",
                LastName = "Admin",
                AddressID = AdminAddress.AddressID,
                Phone = "45412984",
                AccessLevel = "Admin"

            };

            userManager.Create(Admin, "#Password1");
            userManager.AddToRole(Admin.Id, "Admin");

            context.SaveChanges();
            base.Seed(context);
        }


    }
}
