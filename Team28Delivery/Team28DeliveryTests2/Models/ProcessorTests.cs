﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Team28Delivery.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using Moq;

namespace Team28Delivery.Models.Tests
{
    [TestClass()]
    public class ProcessorTests
    {
        [TestMethod()]
        public void OrdersTest()
        {
            const int expectedId = 1;
            var expected = new Orders
            {
                PickupAddressID = 1,
                Timestap = new DateTime(2016, 10, 16),
                OrderPriority = "HIGH",
                ReadyForPickUpTime = new DateTime(2016, 11, 17),
                OrderStatus = "Order Recieved"

            };

            var data = new List<Orders>
           {
                expected,
                new Orders
               {
                    PickupAddressID = 2,
                    Timestap = new DateTime(2016, 11, 17),
                    OrderPriority = "HIGH",
                    ReadyForPickUpTime = new DateTime(2016, 06, 01),
                    OrderStatus = "Order Recieved"
                } ,
                new Orders
               {
                    PickupAddressID = 3,
                    Timestap = new DateTime(2016, 07, 16),
                    OrderPriority = "LOW",
                    ReadyForPickUpTime = new DateTime(2016, 11, 17),
                    OrderStatus = "Order Recieved"
                } ,
                new Orders
                {
                    PickupAddressID = 4,
                    Timestap = new DateTime(2016, 10, 16),
                    OrderPriority = "LOW",
                    ReadyForPickUpTime = new DateTime(2016, 11, 17),
                    OrderStatus = "Order Recieved"
                }}.AsQueryable();

            var dbSetMock = new Mock<IDbSet<Orders>>();


            dbSetMock.Setup(m => m.Provider).Returns(data.Provider);
            dbSetMock.Setup(m => m.Expression).Returns(data.Expression);
            dbSetMock.Setup(m => m.ElementType).Returns(data.ElementType);
            dbSetMock.Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var customDbContextMock = new Mock<CustumDBContext>();

            customDbContextMock.Setup(x => x.Orders).Returns(dbSetMock.Object);

            var classUnderTest = new Processor(customDbContextMock.Object);

            var right = classUnderTest.getOrder(0) == expected;


            Assert.IsNotNull(dbSetMock);
            Assert.IsTrue(right);

        }

    }
}