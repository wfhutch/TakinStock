using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TakinStock.Models;
using System.Collections.Generic;

namespace TakinStock.Tests.Models
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void UserEnsureICanCreateAClassInstance()
        {
            TakinStockUsers user = new TakinStockUsers();
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void UserEnsureICanCreateEmail()
        {
            TakinStockUsers user = new TakinStockUsers();
            user.Email = "me@me.com";
            Assert.AreEqual("me@me.com", user.Email);
        } 

        [TestMethod]
        public void UserEnsureCanSeeAllItems()
        {
            List<Items> items_list = new List<Items>
            {
                new Items {Make = "Fender" },
                new Items {Make = "Gretsch" },
                new Items {Make = "Yamaha" }
            };
            TakinStockUsers user = new TakinStockUsers();
            user.Items = items_list;
            List<Items> actual = user.Items;
            CollectionAssert.AreEqual(actual, items_list);
        }
    }
}
