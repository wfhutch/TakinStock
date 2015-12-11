using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TakinStock.Models;

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
    }
}
