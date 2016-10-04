﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team28Delivery.Models;

namespace Team28Delivery.Areas.Employee.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employee/Orders
        [Authorize(Roles = "Employee, Admin")]
        public ActionResult Index()
        {
            var packages = db.Packages.Include(o => o.Order);
            return View(packages.ToList());
        }

        // GET: Employee/Orders/Details/5
        [Authorize(Roles = "Employee, Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // GET: Employee/Orders/Create
        [Authorize(Roles = "Employee, Admin")]
        public ActionResult Create()
        {
            ViewBag.PickupAddressID = new SelectList(db.Addresses, "AddressID", "StreetAddress");
            return View();
        }

        // POST: Employee/Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee, Admin")]
        public ActionResult Create([Bind(Include = "OrderID,Timestap,PickupAddressID,ReadyForPickUpTime,WarehouseArrivalTime,OrderStatus,OrderPriority,WarehouseDepartureTime")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(orders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PickupAddressID = new SelectList(db.Addresses, "AddressID", "StreetAddress", orders.PickupAddressID);
            return View(orders);
        }

        // GET: Employee/Orders/Edit/5
        [Authorize(Roles = "Employee, Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            ViewBag.PickupAddressID = new SelectList(db.Addresses, "AddressID", "StreetAddress", orders.PickupAddressID);
            return View(orders);
        }

        // POST: Employee/Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Employee, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,Timestap,PickupAddressID,ReadyForPickUpTime,WarehouseArrivalTime,OrderStatus,OrderPriority,WarehouseDepartureTime")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PickupAddressID = new SelectList(db.Addresses, "AddressID", "StreetAddress", orders.PickupAddressID);
            return View(orders);
        }

        // GET: Employee/Orders/Delete/5
        [Authorize(Roles = "Employee, Admin")]

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // POST: Employee/Orders/Delete/5
        [Authorize(Roles = "Employee, Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orders orders = db.Orders.Find(id);
            db.Orders.Remove(orders);
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
