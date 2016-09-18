using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team28Delivery.Models;

namespace Team28Delivery.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext(); 
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Order()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Order(OrderModel orderModel)
        {

            var message = "Something went really wrong. To bad, try again.";

            if (ModelState.IsValid)
            {
                var pickup = new Addresses
                {
                    StreetAddress = orderModel.Pickup.StreetAddress,
                    Suburb = orderModel.Pickup.Suburb,
                    PostalCode = orderModel.Pickup.PostalCode,
                    State = orderModel.Pickup.State,
                    Country = orderModel.Pickup.Country
                };

                var checkPickup = isAddressInTable(pickup);
                if (checkPickup == null)
                {
                    db.Addresses.Add(pickup);
                    db.SaveChanges();
                }
                else
                {
                    pickup = checkPickup;
                }

                var delivery = new Addresses
                {
                    StreetAddress = orderModel.Delivery.StreetAddress,
                    Suburb = orderModel.Delivery.Suburb,
                    PostalCode = orderModel.Delivery.PostalCode,
                    State = orderModel.Delivery.State,
                    Country = orderModel.Delivery.Country
                };

                var checkDelivery = isAddressInTable(delivery);
                if(checkDelivery == null)
                {
                    db.Addresses.Add(delivery);
                    db.SaveChanges();

                }
                else
                {
                    delivery = checkDelivery;
                }

                var order = new Orders
                {
                    PickupAddressID = pickup.AddressID,
                    Timestap = DateTime.Now,
                    OrderPriority = orderModel.PackageDetails.Priority,
                    ReadyForPickUpTime = orderModel.PackageDetails.PickupTime,
                    OrderStatus = "Recieved",
                };

                db.Orders.Add(order);
                db.SaveChanges();

                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                ApplicationUser user = manager.FindByIdAsync(User.Identity.GetUserId()).Result;

                var package = new Packages
                {
                    SenderID = user.Id,
                    OrderID = order.OrderID,
                    RecieversName = orderModel.PackageDetails.RecieverName,
                    Weight = orderModel.PackageDetails.Weight,
                    SpecialInfo = orderModel.PackageDetails.SpecialInfo,
                    RecieverAddressID = delivery.AddressID,
                    Cost = 666.0
                };

                db.Packages.Add(package);
                db.SaveChanges();

                message = "Your order is recievd, THANK YOU!";

            }
            return RedirectToAction("Index", "Home", new { message = message });

        } 




        public Addresses isAddressInTable(Addresses address)
        {
            var Addresses = db.Set<Addresses>();
            
            foreach (var adr in Addresses)
            {
                if (adr.StreetAddress == address.StreetAddress && adr.PostalCode == address.PostalCode)
                {
                    return adr;
                }
            }
            return null;

        }

    }
}