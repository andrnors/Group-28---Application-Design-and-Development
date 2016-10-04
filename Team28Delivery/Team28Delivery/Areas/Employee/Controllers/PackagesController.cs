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
        public ActionResult Index()
        {
            var packages = db.Packages.Include(p => p.Order).Include(p => p.User);
            
            return View(packages.ToList());
        }

        // GET: Employee/Packages/Details/5
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
            return View(model);
        }

        // POST: Employee/Packages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Packages packages)
        {
            var order = db.Orders.Find(packages.OrderID);
            var package = db.Packages.Find(packages.PackageID);
            var pickupAddress = db.Addresses.Find((packages.Order.PickupAddressID));
            var deliverAddress = db.Addresses.Find(packages.RecieverAddressID);

            order.Address = packages.Order.Address;
            order.Timestap = packages.Order.Timestap;
            order.PickupAddressID = packages.Order.PickupAddressID;
            order.ReadyForPickUpTime = packages.Order.ReadyForPickUpTime;
            order.WarehouseArrivalTime = packages.Order.WarehouseArrivalTime;
            order.OrderStatus = packages.Order.OrderStatus;
            order.OrderPriority = packages.Order.OrderPriority;
            order.WarehouseDepartureTime = packages.Order.WarehouseDepartureTime;


            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(packages);
        }

        // GET: Employee/Packages/Delete/5
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Packages packages = db.Packages.Find(id);
            db.Packages.Remove(packages);
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
