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
    public class DatabaseInit : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            roleManager.Create(new IdentityRole("Customer"));  // Customer role
            base.Seed(context);
        }


    }
}
