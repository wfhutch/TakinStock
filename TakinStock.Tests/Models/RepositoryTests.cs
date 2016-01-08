using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TakinStock.Models;
using System.Collections.Generic;
using Moq;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;



namespace TakinStock.Tests.Models
{
    [TestClass]
    public class RepositoryTests
    {
        private Mock<StockContext> mock_context;
        private Mock<DbSet<Items>> mock_set;
        private Mock<DbSet<TakinStockUsers>> mock_set_users;
        private StockRepository repo;
        private ApplicationUser test_user;
        private ApplicationUser test_user2;

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

        private void ConnectMocksToDataStore(IEnumerable<TakinStockUsers> data_store)
        {
            var data_source = data_store.AsQueryable();

            mock_set_users.As<IQueryable<TakinStockUsers>>().Setup(data => data.Provider)
                .Returns(data_source.Provider);
            mock_set_users.As<IQueryable<TakinStockUsers>>().Setup(data => data.Expression)
                .Returns(data_source.Expression);
            mock_set_users.As<IQueryable<TakinStockUsers>>().Setup(data => data.ElementType)
                .Returns(data_source.ElementType);
            mock_set_users.As<IQueryable<TakinStockUsers>>().Setup(data => data.GetEnumerator())
                .Returns(data_source.GetEnumerator);

            mock_context.Setup(a => a.User).Returns(mock_set_users.Object);
        }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<StockContext>();
            mock_set = new Mock<DbSet<Items>>();
            mock_set_users = new Mock<DbSet<TakinStockUsers>>();
            repo = new StockRepository(mock_context.Object);
            test_user = new ApplicationUser { Email = "test@example.com", Id = "MyId" };
            test_user2 = new ApplicationUser { Email = "test2@example.com", Id = "MyId2" };

        }

        [TestCleanup]
        public void Cleanup()
        {
            mock_context = null;
            mock_set = null;
            mock_set_users = null;
            repo = null;
            test_user = null;
            test_user2 = null;

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
        public void RepoEnsureICanGetAllItemsForASpecificUser()
        {
            DateTime purchase_date = DateTime.Now.Date;
            List<TakinStockUsers> users_table = new List<TakinStockUsers>();
            List<Items> items_table = new List<Items>();

            Items test_item = new Items();
            
            //test_item.Owner = test_owner;
            test_item.Make = "Samsung";
            test_item.Type = "Electronics";
            test_item.Model = "HD48SM";
            test_item.SerialNumber = "A123B456";
            test_item.PurchaseDate = purchase_date;
            test_item.PurchasedFrom = "Best Buy";
            test_item.Image = "Image URL";
            test_item.LostByDamage = false;
            test_item.Stolen = false;

            items_table.Add(test_item);
            TakinStockUsers test_owner = new TakinStockUsers { RealUser = test_user, UserID = 1, Items = items_table};
            users_table.Add(test_owner);

            ConnectMocksToDataStore(users_table);
            ConnectMocksToDataStore(items_table);

            mock_set_users.Setup(i => i.Add(It.IsAny<TakinStockUsers>())).Callback((TakinStockUsers s) => users_table.Add(s));
            mock_set.Setup(i => i.Add(It.IsAny<Items>())).Callback((Items s) => items_table.Add(s));
            List<Items> expected = repo.GetUserItems(test_owner);

            Assert.AreEqual(1, expected.Count);
        }

        [TestMethod]
        public void RepoEnsureICanGetAllUsers()
        {
            var expected = new List<TakinStockUsers>
            {
                new TakinStockUsers {UserID = 1},
                new TakinStockUsers {UserID = 2},
                new TakinStockUsers {UserID = 3}
            };

            mock_set_users.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);
            var actual = repo.GetAllUsers();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RepoEnsureICanAddANewUser()
        {
            List<TakinStockUsers> emptyDB = new List<TakinStockUsers>(); //This is the empty database table
            ConnectMocksToDataStore(emptyDB);

            mock_set_users.Setup(i => i.Add(It.IsAny<TakinStockUsers>())).Callback((TakinStockUsers s) => emptyDB.Add(s));

            bool added = repo.AddNewUser(test_user);

            TakinStockUsers stock_user = repo.GetAllUsers().Where(u => u.RealUser.Id == test_user.Id).SingleOrDefault();
            Assert.IsNotNull(stock_user);
            Assert.IsTrue(added);
            Assert.AreEqual(1, repo.GetAllUsers().Count);
        }

        [TestMethod]
        public void RepoEnsureICanAddANewItem()
        {
            DateTime purchase_date = DateTime.Now.Date;
            TakinStockUsers user = new TakinStockUsers { UserID = 500 };
            
            List<Items> emptyDB = new List<Items>(); //This is the empty database
            ConnectMocksToDataStore(emptyDB);

            int item_id = 1;
            TakinStockUsers owner = user;
            string type = "Electronics";
            string make = "Samsung";
            string model = "HD48SM";
            string description = "48\" HDTV";
            string serialNumber = "A123B456";
            decimal purchasePrice = 499.99M;
            DateTime purchaseDate = purchase_date;
            string purchasedFrom = "Best Buy";
            string image = "Image URL";
            bool damaged = false;
            bool stolen = false;

            //Listen for any item trying to be added to the database. When you see one add it to emptyDB
            mock_set.Setup(i => i.Add(It.IsAny<Items>())).Callback((Items s) => emptyDB.Add(s));

            bool added = repo.AddNewItem(owner, item_id, type, make, model, description, serialNumber, purchasePrice, purchaseDate,
                purchasedFrom, image, damaged, stolen);

            Assert.IsTrue(added);
            Assert.AreEqual(1, repo.GetAllItems().Count);
        }

        [TestMethod]
        public void RepoSearchByItemType()
        {
            var expected = new List<Items>
            {
                new Items {Type = "Electronics" },
                new Items {Type = "Electronics" },
                new Items {Type = "Instruments" }
            };

            mock_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            string search_string = "Electronics";

            List<Items> expected_types = new List<Items>
            {
                new Items {Type = "Electronics" },
                new Items {Type = "Electronics" }
            };

            List<Items> actual_types = repo.SearchByType(search_string);

            Assert.AreEqual(expected_types[0].Type, actual_types[0].Type);
            Assert.AreEqual(expected_types[1].Type, actual_types[1].Type);
        }

        [TestMethod]
        public void RepoSearchByMake()
        {
            var expected = new List<Items>
            {
                new Items {Make = "Samsung" },
                new Items {Make = "Toshiba" },
                new Items {Make = "Toshiba" }
            };

            mock_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            string search_string = "Toshiba";

            List<Items> expected_types = new List<Items>
            {
                new Items {Make = "Toshiba" },
                new Items {Make = "Toshiba" }
            };

            List<Items> actual_types = repo.SearchByMake(search_string);

            Assert.AreEqual(expected_types[0].Type, actual_types[0].Type);
            Assert.AreEqual(expected_types[1].Type, actual_types[1].Type);
        }

        [TestMethod]
        public void RepoSearchByDatePurchased()
        {
            DateTime first_date = DateTime.Now.Date;
            DateTime second_date = DateTime.Now.AddYears(1);
            DateTime third_date = DateTime.Now.AddYears(1);

            var expected = new List<Items>
            {
                new Items {PurchaseDate =  first_date},
                new Items {PurchaseDate =  second_date},
                new Items {PurchaseDate =  third_date}
            };

            mock_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            DateTime search_date = third_date;

            List<Items> expected_dates = new List<Items>
            {
                new Items {PurchaseDate = third_date },
                new Items {PurchaseDate = third_date }
            };

            List<Items> actual_dates = repo.SearchByDate(search_date);

            Assert.AreEqual(expected_dates[0].Type, actual_dates[0].Type);
            Assert.AreEqual(expected_dates[1].Type, actual_dates[1].Type);
        }
    }
}

