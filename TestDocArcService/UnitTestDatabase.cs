using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DocArcService.Provider;

namespace TestDocArcService
{
    [TestClass]
    public class UnitTestDatabase
    {
        [TestMethod]
        public void Mocked_TestDbConnection()
        {
            ProviderFactory.IsMocked = true;

            var dbProvider = ProviderFactory.CreateDatabaseProvider();
            Assert.IsTrue(dbProvider.DatabaseIsReachable());
        }

        [TestMethod]
        public void TestDbConnection()
        {
            ProviderFactory.IsMocked = false;

            var dbProvider = ProviderFactory.CreateDatabaseProvider();
            Assert.IsTrue(dbProvider.DatabaseIsReachable());
        }
    }
}
