using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TakinStock.Models;
using System.Collections.Generic;
using Moq;
using System.Data.Entity;
using System.Linq;

namespace TakinStock.Tests.Models
{
    [TestClass]
    public class RepositoryTests
    {
        private Mock<StockContext> mock_context;
        private Mock<DbSet<Items>> mock_set;
        private Mock<DbSet<Users>> mock_set_users;
        private StockRepository repo;

        private void ConnectMocksToDataStore(IEnumerable<Items> data_store)
        {
            var data_source = data_store.AsQueryable();

            mock_set.As<IQueryable<Items>>().Setup(data => data.Provider)
                .Returns(data_source.Provider);
            mock_set.As<IQueryable<Items>>().Setup(data => data.Expression)
                .Returns(data_source.Expression);
            mock_set.As<IQueryable<Items>>().Setup(data => data.ElementType)
                .Returns(data_source.ElementType);
            mock_set.As<IQueryable<Items>>().Setup(data => data.GetEnumerator())
                .Returns(data_source.GetEnumerator);

            mock_context.Setup(a => a.Items).Returns(mock_set.Object);
        }

        private void ConnectMocksToDataStore(IEnumerable<Users> data_store)
        {
            var data_source = data_store.AsQueryable();

            mock_set_users.As<IQueryable<Users>>().Setup(data => data.Provider)
                .Returns(data_source.Provider);
            mock_set_users.As<IQueryable<Users>>().Setup(data => data.Expression)
                .Returns(data_source.Expression);
            mock_set_users.As<IQueryable<Users>>().Setup(data => data.ElementType)
                .Returns(data_source.ElementType);
            mock_set_users.As<IQueryable<Users>>().Setup(data => data.GetEnumerator())
                .Returns(data_source.GetEnumerator);

            mock_context.Setup(a => a.User).Returns(mock_set_users.Object);
        }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<StockContext>();
            mock_set = new Mock<DbSet<Items>>();
            mock_set_users = new Mock<DbSet<Users>>();
            repo = new StockRepository(mock_context.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            mock_context = null;
            mock_set = null;
            mock_set_users = null;
            repo = null;
        }

        [TestMethod]
        public void RepoEnsureICanCreateInstanceOfRepository()
        {
            StockRepository repo = new StockRepository();
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void RepoEnsureICanCreateInstanceOfContext()
        {
            StockContext context = new StockContext();
            Assert.IsNotNull(context);
        }

        [TestMethod]
        public void RepoEnsureIHaveAContext()
        {
            StockRepository repo = new StockRepository();
            var actual = repo.Context;

            Assert.IsInstanceOfType(actual, typeof(StockContext));
        }

        [TestMethod]
        public void RepoEnsureICanGetAllItems()
        {
            var expected = new List<Items>
            {
                new Items {Make = "Samsung" },
                new Items {Make = "Onkyo" },
                new Items {Make = "Apple" }
            };

            mock_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);
            var actual = repo.GetAllItems();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RepoEnsureICanGetAllUsers()
        {
            var expected = new List<Users>
            {
                new Users {UserID = 1},
                new Users {UserID = 2},
                new Users {UserID = 3}
            };

            mock_set_users.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);
            var actual = repo.GetAllUsers();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RepoEnsureICanAddANewItem()
        {
            DateTime purchase_date = DateTime.Now.Date;
            Users test_user = new Users { UserID = 1 };
            List<Items> emptyDB = new List<Items>(); //This is the empty database
            ConnectMocksToDataStore(emptyDB);

            string type = "Electronics";
            string make = "Samsung";
            string model = "HD48SM";
            string serialNumber = "A123B456";
            DateTime purchaseDate = purchase_date;
            string purchasedFrom = "Best Buy";
            string image = "Image URL";
            bool damaged = false;
            bool stolen = false;

            //Listen for any item trying to be added to the database. When you see on add it to 'expected'
            mock_set.Setup(i => i.Add(It.IsAny<Items>())).Callback((Items s) => emptyDB.Add(s));

            bool added = repo.AddNewItem(test_user, type, make, model, serialNumber, purchaseDate, purchasedFrom, image, damaged, stolen);

            Assert.IsTrue(added);
            Assert.AreEqual(1, repo.GetAllItems().Count);
        }


    }
}
