using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DocArcService.Provider;
using DocArcService.Models;

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

        [TestMethod]
        public void Mocked_TestDbReadWrite()
        {
            ProviderFactory.IsMocked = true;

            var dbProvider = ProviderFactory.CreateDatabaseProvider();

            Users testUser = new Users();
            testUser.UserId = "123456789";
            testUser.ProviderUserName = "7701";
            testUser.Email = "test@user.com";
            testUser.Container = "123-456-789";

            dbProvider.DeleteUserById(testUser.UserId);

            dbProvider.InsertUser(testUser);

            Assert.IsTrue(dbProvider.GetUserById(testUser.UserId) != null);

            Assert.IsTrue(dbProvider.GetUserByProviderUserName(testUser.ProviderUserName) != null);

            Assert.IsTrue(dbProvider.DeleteUserById(testUser.UserId));
        }

        [TestMethod]
        public void TestDbReadWrite()
        {
            ProviderFactory.IsMocked = false;

            var dbProvider = ProviderFactory.CreateDatabaseProvider();

            Users testUser = new Users();
            testUser.UserId = "123456789";
            testUser.ProviderUserName = "7701";
            testUser.Email = "test@user.com";
            testUser.Container = "123-456-789";

            dbProvider.DeleteUserById(testUser.UserId);

            dbProvider.InsertUser(testUser);

            Assert.IsTrue(dbProvider.GetUserById(testUser.UserId) != null);

            Assert.IsTrue(dbProvider.GetUserByProviderUserName(testUser.ProviderUserName) != null);

            Assert.IsTrue(dbProvider.GetContainerId(testUser.ProviderUserName) == testUser.Container);

            Assert.IsTrue(dbProvider.DeleteUserById(testUser.UserId));
        }
    }
}
