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
        /// GETs user details for user with that id
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
        /// This function takes you to the page where Admins are allowed to make users Employees
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
        /// Make a normal 'user' to an Employee. USER UPGRADE
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
        /// <summary>
        /// GETs user with that ID and shows all details in a form
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Admin/ApplicationUsers/Edit/5
        [Authorize(Roles = "Employee, Admin")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            Addresses userAddress = new Addresses()
            {
                AddressID = applicationUser.AddressID,
                StreetAddress = applicationUser.Address.StreetAddress,
                Country = applicationUser.Address.Country,
                PostalCode = applicationUser.Address.State,
                Suburb = applicationUser.Address.Suburb
            };

            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "StreetAddress", applicationUser.AddressID);
            ViewBag.Id = new SelectList(db.Employees, "EmployeeID", "BankAccount", applicationUser.Id);
            return View(applicationUser);
        }
        /// <summary>
        /// Changes userdetails for the current user
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <returns></returns>
        // POST: Admin/ApplicationUsers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee, Admin")]
        public ActionResult Edit(ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                // this part is all good 
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var user = manager.FindByIdAsync(User.Identity.GetUserId()).Result;
                user.FirstName = applicationUser.FirstName;
                user.LastName = applicationUser.LastName;
                user.Phone = applicationUser.Phone;
                user.Email = applicationUser.Email;
                user.UserName = applicationUser.UserName;
                db.Entry(applicationUser).State = EntityState.Modified;


                // This does not work
                // have to figure out why. Updates userdeails but not anything else..

                //user.Employee.BankAccount = applicationUser.Employee.BankAccount;
                //user.Employee.CarRegister = applicationUser.Employee.CarRegister;

                //var address = db.Addresses.Find(applicationUser.Address.AddressID);
                //address.StreetAddress = applicationUser.Address.StreetAddress;
                //address.Suburb = applicationUser.Address.Suburb;
                //address.PostalCode = applicationUser.Address.PostalCode;
                //address.State = applicationUser.Address.State;
                //address.Country = applicationUser.Address.Country;
                //db.Entry(address).State = EntityState.Modified;

                // save changes to database 
                db.SaveChanges();
                // return to index view
                return RedirectToAction("Index");

            }
            return View(applicationUser);
        }

        // GET: Admin/ApplicationUsers/Delete/5
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
