using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TakinStock.Models;

namespace TakinStock.Tests.Models
{
    [TestClass]
    public class ItemsTests
    {
        [TestMethod]
        public void ItemsEnsureICanCreateAClassInstance()
        {
            Items item = new Items();
            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void ItemsEnsureICanCreateAnItem()
        {
            Items item = new Items();
            DateTime expected_date = DateTime.Now.Date;
            item.ItemsID = 1234;
            item.Type = "Electronics";
            item.Make = "Samsung";
            item.Model = "46HD1234";
            item.Description = "46 HDTV";
            item.SerialNumber = "AB123CD456";
            item.PurchaseDate = expected_date;
            item.PurchasePrice = "499.99";
            item.PurchasedFrom = "Best Buy";
            item.Image = "Some S3 URL";

            Assert.AreEqual(item.ItemsID, 1234);
            Assert.AreEqual(item.Type, "Electronics");
            Assert.AreEqual(item.Make, "Samsung");
            Assert.AreEqual(item.Model, "46HD1234");
            Assert.AreEqual(item.Description, "46 HDTV");
            Assert.AreEqual(item.SerialNumber, "AB123CD456");
            Assert.AreEqual(item.PurchaseDate, expected_date);
            Assert.AreEqual(item.PurchasePrice, "499.99");
            Assert.AreEqual(item.PurchasedFrom, "Best Buy");
            Assert.AreEqual(item.Image, "Some S3 URL");
            Assert.IsFalse(item.LostByDamage);
            Assert.IsFalse(item.Stolen);
        }
    }
}
