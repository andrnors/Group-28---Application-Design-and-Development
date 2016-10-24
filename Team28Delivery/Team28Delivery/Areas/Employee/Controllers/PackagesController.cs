using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team28Delivery.Models;
using Team28Delivery.Areas.Employee.Models;

namespace Team28Delivery.Areas.Employee.Controllers
{
    public class PackagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employee/Packages
        /// <summary>
        /// GETs index page the Order Management page 
        /// </summary>
        /// <param name="fromDate"></param>
        /// <returns></returns>
        [Authorize(Roles = "Employee, Admin")]
        public ActionResult Index(DateTime? fromDate)
        {
            var packages = db.Packages.Include(p => p.Order).Include(p => p.User);

            //if (!fromDate.HasValue) fromDate = DateTime.Now.Date;  
            ViewBag.fromDate = fromDate;
            if(fromDate != null)        
            {
                packages = packages.Where(s => s.Order.ReadyForPickUpTime > fromDate);
            }
            
            return View(packages.ToList());
        }

        // GET: Employee/Packages/Details/5
        /// <summary>
        /// GETs details page about selected order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Employee, Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Packages packages = db.Packages.Find(id);
            Orders orders = db.Orders.Find(packages.OrderID);
            Models.OrderModel orderModel = new Models.OrderModel
            {
                Packages = packages,
                Orders = orders
            };

            
            if (packages == null)
            {
                return HttpNotFound();
            }
            return View(orderModel);
        }


        // GET: Employee/Packages/Edit/5
        /// <summary>
        /// GETs Edit order page.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Page with a form with all order details</returns>
        [Authorize(Roles = "Employee, Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Packages packages = db.Packages.Find(id);
            OrderDetailsViewModel model = new OrderDetailsViewModel
            {
                package = packages,
                order = packages.Order,
                pickupAddress = packages.Order.Address,
                deliveryAddress = packages.Address
            };
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "ReadyForPickUpTime", packages.OrderID);
            //ViewBag.SenderID = new SelectList(db.ApplicationUsers, "Id", "FirstName", packages.SenderID);
            return View(packages);
        }

        // POST: Employee/Packages/Edit/5
        /// <summary>
        /// POST Method for editing orders
        /// Will update the database with the form sent in when save/submit button is pressed
        /// </summary>
        /// <param name="packages"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee, Admin")]
        public ActionResult Edit(Packages packages)
        {
          
            if (ModelState.IsValid)
            {
                // Manualy updates all entries in the form. 
                // Does the same thing every field
                var package = db.Packages.Find(packages.PackageID);
                package.RecieversName = packages.RecieversName;
                package.Weight = packages.Weight;
                package.SpecialInfo = packages.SpecialInfo;
                db.Entry(package).State = EntityState.Modified;  // Saves modified state 

                var order = db.Orders.Find(packages.OrderID);
                order.WarehouseArrivalTime = packages.Order.WarehouseArrivalTime;
                order.OrderStatus = packages.Order.OrderStatus;
                order.OrderPriority = packages.Order.OrderPriority;
                order.WarehouseDepartureTime = packages.Order.WarehouseDepartureTime;
                db.Entry(order).State = EntityState.Modified;

                var pickupAddress = db.Addresses.Find((packages.Order.PickupAddressID));
                pickupAddress.StreetAddress = packages.Order.Address.StreetAddress;
                pickupAddress.Suburb = packages.Order.Address.Suburb;
                pickupAddress.PostalCode = packages.Order.Address.PostalCode;
                pickupAddress.State = packages.Order.Address.State;
                pickupAddress.Country = packages.Order.Address.Country;
                db.Entry(pickupAddress).State = EntityState.Modified;

                var deliverAddress = db.Addresses.Find(packages.RecieverAddressID);
                deliverAddress.StreetAddress = packages.Address.StreetAddress;
                deliverAddress.Suburb = packages.Address.Suburb;
                deliverAddress.PostalCode = packages.Address.PostalCode;
                deliverAddress.State = packages.Address.State;
                deliverAddress.Country = packages.Address.Country;
                db.Entry(deliverAddress).State = EntityState.Modified;


                db.SaveChanges(); // Saves chagnes
                return RedirectToAction("Index"); // returns to Index page
            }

            return View(packages);
        }

        // GET: Employee/Packages/Delete/5
        /// <summary>
        /// GETs page where you can delete an order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Employee, Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Packages packages = db.Packages.Find(id);
            if (packages == null)
            {
                return HttpNotFound();
            }
            return View(packages);
        }

        // POST: Employee/Packages/Delete/5
        /// <summary>
        /// POST method for actually deleting an order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee, Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Packages packages = db.Packages.Find(id);
            var order = db.Orders.Find(packages.OrderID);

            db.Packages.Remove(packages);
            db.Orders.Remove(order);
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
