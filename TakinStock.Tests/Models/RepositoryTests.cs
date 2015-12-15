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
        public void RepoEnsureICanGetAllItems()
        {
            var expected = new List<Items>
            {
                new Items {Make = "Samsung" },
                new Items {Make = "Onkyo" },
                new Items {Make = "Apple" }
            };

            Mock<StockContext> mock_context = new Mock<StockContext>();
            Mock<DbSet<Items>> mock_set = new Mock<DbSet<Items>>();

            mock_set.Object.AddRange(expected);
            var data_source = expected.AsQueryable();
            mock_set.As<IQueryable<Items>>().Setup(data => data.Provider)
                .Returns(data_source.Provider);
            mock_set.As<IQueryable<Items>>().Setup(data => data.Expression)
                .Returns(data_source.Expression);
            mock_set.As<IQueryable<Items>>().Setup(data => data.ElementType)
                .Returns(data_source.ElementType);
            mock_set.As<IQueryable<Items>>().Setup(data => data.GetEnumerator())
                .Returns(data_source.GetEnumerator);

            mock_context.Setup(a => a.Items).Returns(mock_set.Object);
            StockRepository repo = new StockRepository(mock_context.Object);
            var actual = repo.GetAllItems();

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
