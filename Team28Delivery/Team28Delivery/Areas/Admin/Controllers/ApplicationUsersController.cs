using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Team28Delivery.Models;

namespace Team28Delivery.Areas.Admin.Controllers
{
    public class ApplicationUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ApplicationUsers
         /// <summary>
         ///  GETs Users in the database, and show basic userdetails in table
         /// Searchstring is searchable value for search bar on the page.
         /// Searches after emails.
         /// </summary>
         /// <param name="searchstring"></param>
         /// <returns></returns>
        [Authorize(Roles = "Employee, Admin")]
        public ActionResult Index(string searchstring)
        { 
            var applicationUsers = db.Users.Include(a => a.Address).Include(a => a.Employee);
            if (!String.IsNullOrEmpty(searchstring))
            {
                applicationUsers = applicationUsers.Where(au => au.Email.Contains(searchstring));
            }

            return View(applicationUsers.ToList());
        }

        // GET: Admin/ApplicationUsers/Details/5
        /// <summary>
        /// GETs user details for the user with that id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Employee, Admin")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // GET: Admin/ApplicationUsers/Create
        /// <summary>
        // This function takes you to the page where Admins are allowed to make users Employees
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public ActionResult Create(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "StreetAddress", applicationUser.AddressID);
            ViewBag.Id = new SelectList(db.Employees, "EmployeeID", "BankAccount", applicationUser.Id);
            return View(applicationUser);
        }

        // POST: Admin/ApplicationUsers/Create
        /// <summary>
        /// Make a normal user to an Employee. USER UPGADE
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Phone,AddressID,AccessLevel,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var employee = new Employees()
                {
                    EmployeeID = applicationUser.Id,
                    AccessLevel = "Employee"
                    
                };
                db.Entry(applicationUser).State = EntityState.Modified;
                db.Employees.Add(employee);
                db.SaveChanges();
                userManager.AddToRole(applicationUser.Id, "Employee");
                return RedirectToAction("Index");
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "StreetAddress", applicationUser.AddressID);
            ViewBag.Id = new SelectList(db.Employees, "EmployeeID", "BankAccount", applicationUser.Id);
            return View(applicationUser);
        }

        // GET: Admin/ApplicationUsers/Edit/5
        /// <summary>
        /// GETs user with that Id and shows all details in a form
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Employee, Admin")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "StreetAddress", applicationUser.AddressID);
            ViewBag.Id = new SelectList(db.Employees, "EmployeeID", "BankAccount", applicationUser.Id);
            return View(applicationUser);
        }

        // POST: Admin/ApplicationUsers/Edit/5
        /// <summary>
        /// POST method for updating user profiles.
        /// Edit details on profile selected
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee, Admin")]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Phone,AddressID,AccessLevel,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "StreetAddress", applicationUser.AddressID);
            ViewBag.Id = new SelectList(db.Employees, "EmployeeID", "BankAccount", applicationUser.Id);
            return View(applicationUser);
        }

        // GET: Admin/ApplicationUsers/Delete/5
        /// <summary>
        /// GETs Delete user page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Employee, Admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: Admin/ApplicationUsers/Delete/5
        /// <summary>
        /// Actually deletes a user from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee, Admin")]
        public ActionResult DeleteConfirmed(string id)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
            var employee = db.Employees.Find(applicationUser.Id);
            if (userManager.IsInRole(applicationUser.Id, "Employee"))
            {
                db.Employees.Remove(employee);
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
