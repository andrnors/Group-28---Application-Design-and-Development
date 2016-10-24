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
        /// <summary>
        /// GETs homepage
        /// </summary>
        /// <returns>HomePage</returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// GETs About page
        /// </summary>
        /// <returns>About page</returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        /// <summary>
        /// GETs contact page
        /// </summary>
        /// <returns>Contact Page</returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// GETs order page
        /// </summary>
        /// <returns>Order page</returns>
        [Authorize(Roles = "Customer,Employee, Admin")]
        public ActionResult Order()
        {
            return View();
        }

        /// <summary>
        /// POST method for placing an order.
        /// Order details in the form provided will be sent to the database.
        /// </summary>
        /// <param name="orderModel"></param>
        /// <returns></returns>
        [Authorize(Roles = "Customer, Employee, Admin")]
        [HttpPost]
        public ActionResult Order(OrderModel orderModel)
        {

            var message = "Something went really wrong. To bad, try again."; // this message will show in the url if the form is filed out wrong or something has gone wrong

            if (ModelState.IsValid)
            {
              // create a pickup address
                var pickup = new Addresses
                {
                    StreetAddress = orderModel.Pickup.StreetAddress,
                    Suburb = orderModel.Pickup.Suburb,
                    PostalCode = orderModel.Pickup.PostalCode,
                    State = orderModel.Pickup.State,
                    Country = orderModel.Pickup.Country
                };

                // if the address is in the datbase already it will not and it another time
                // this is just to keep the database clean
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
                // creates new delivery address
                var delivery = new Addresses
                {
                    StreetAddress = orderModel.Delivery.StreetAddress,
                    Suburb = orderModel.Delivery.Suburb,
                    PostalCode = orderModel.Delivery.PostalCode,
                    State = orderModel.Delivery.State,
                    Country = orderModel.Delivery.Country
                };
                // checks if the delivery address is in the database
                // this is just to keep the database clean
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

                //creates new order to the orders table
                var order = new Orders
                {
                    PickupAddressID = pickup.AddressID,
                    Timestap = DateTime.Now,
                    OrderPriority = orderModel.PackageDetails.Priority,
                    ReadyForPickUpTime = orderModel.PackageDetails.PickupTime,
                    OrderStatus = "Order Recieved"

                };

                // adds changes and save the database
                db.Orders.Add(order);
                db.SaveChanges();

                // creates a user manager, so I can access the users database
                // this is so i can set the senderID
                // SenderID will be set to the person who creates the order, which means you will have to be loged in to create an order
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                ApplicationUser user = manager.FindByIdAsync(User.Identity.GetUserId()).Result;

                // creates new package to the databse
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
                // adds and changes the table
                db.Packages.Add(package);
                db.SaveChanges();

                message = "Your order is recievd, THANK YOU!";  // if the order is all good, this message will be displayed in the url

            }
            // redriects back to home
            return RedirectToAction("Index", "Home", new { message = message });

        }


        /// <summary>
        // This method is used to check if an address is in the Addresses table allready
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
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
