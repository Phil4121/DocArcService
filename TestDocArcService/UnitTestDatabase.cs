using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DocArcService.Provider;
using DocArcService.Models;
using System.Threading.Tasks;

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
        public async Task Mocked_TestUserReadWrite()
        {
            ProviderFactory.IsMocked = true;

            var dbProvider = ProviderFactory.CreateDatabaseProvider();

            Users testUser = new Users();
            testUser.UserId = "123456789";
            testUser.ProviderUserName = "7701";
            testUser.Email = "test@user.com";
            testUser.Container = "123-456-789";

            await dbProvider.DeleteUserByIdAsync(testUser.UserId);

            Assert.IsTrue(dbProvider.InsertUser(testUser));

            Assert.IsTrue(dbProvider.GetUserById(testUser.UserId) != null);

            Assert.IsTrue(dbProvider.GetUserByProviderUserName(testUser.ProviderUserName) != null);

            Assert.IsTrue(dbProvider.DeleteUserByIdAsync(testUser.UserId).Result);
        }

        [TestMethod]
        public async Task TestUserReadWrite()
        {
            ProviderFactory.IsMocked = false;

            var dbProvider = ProviderFactory.CreateDatabaseProvider();

            Users testUser = new Users();
            testUser.UserId = "123456789";
            testUser.ProviderUserName = "7701";
            testUser.Email = "testUserReadWrite@user.com";
            testUser.Container = Guid.NewGuid().ToString();

            await dbProvider.DeleteAllFilesFromUserAsync(testUser.UserId);
            await dbProvider.DeleteUserByProviderNameAsync(testUser.ProviderUserName);

            Assert.IsTrue(dbProvider.InsertUser(testUser));

            Assert.IsTrue(dbProvider.GetUserById(testUser.UserId) != null);

            Assert.IsTrue(dbProvider.GetUserByProviderUserName(testUser.ProviderUserName) != null);

            Assert.IsTrue(dbProvider.GetContainerId(testUser.ProviderUserName) == testUser.Container);

            Assert.IsTrue(await dbProvider.DeleteUserByIdAsync(testUser.UserId));
        }

        [TestMethod]
        public void Mocked_TestFileReadWrite()
        {
            ProviderFactory.IsMocked = true;

            var dbProvider = ProviderFactory.CreateDatabaseProvider();

            Files testFile = new Files();
            testFile.UserId = "123456789";
            testFile.FileId = "987654321";
            testFile.Container = Guid.NewGuid().ToString();
            testFile.OriginalFileName = "Testdatei";
            testFile.OriginalFileType = "JPG";
            testFile.FileSizeInKB = 100;

            dbProvider.DeleteFileAsync(testFile);

            Assert.IsTrue(dbProvider.InsertFile(testFile));

            Assert.IsTrue(dbProvider.GetFilesByUserId(testFile.UserId) != null);

            Assert.IsTrue(dbProvider.GetFilesByContainerId(testFile.Container) != null);

            Assert.IsTrue(dbProvider.DeleteUserByIdAsync(testFile.FileId).Result);
        }

        [TestMethod]
        public async Task TestFileReadWrite()
        {
            ProviderFactory.IsMocked = false;

            var dbProvider = ProviderFactory.CreateDatabaseProvider();

            Users testUser = new Users();
            testUser.UserId = "111222333";
            testUser.ProviderUserName = "9999";
            testUser.Email = "9999@user.com";
            testUser.Container = Guid.NewGuid().ToString();

            await dbProvider.DeleteFilesAsync(dbProvider.GetFilesByUserId(testUser.UserId));
            await dbProvider.DeleteUserByProviderNameAsync(testUser.ProviderUserName);

            Assert.IsTrue(dbProvider.InsertUser(testUser));

            Files testFile = new Files();
            testFile.UserId = testUser.UserId;
            testFile.FileId = "987654321";
            testFile.Container = testUser.Container;
            testFile.OriginalFileName = "Testdatei";
            testFile.OriginalFileType = "JPG";
            testFile.FileSizeInKB = 100;

            await dbProvider.DeleteFileAsync(testFile);

            Assert.IsTrue(dbProvider.InsertFile(testFile));

            Assert.IsTrue(dbProvider.GetFilesByUserId(testFile.UserId) != null);

            Assert.IsTrue(dbProvider.GetFilesByContainerId(testFile.Container) != null);
        }
    }
}
