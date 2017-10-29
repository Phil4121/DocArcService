using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DocArcService.Provider;
using DocArcSharedLibrary.Models;
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

            UserModel testUser = new UserModel();
            testUser.ProviderUserName = "7701";
            testUser.Email = "test@user.com";

            await dbProvider.DeleteUserByIdAsync(testUser.UserId);

            var dbUser = dbProvider.AddUser(testUser);

            Assert.IsTrue(dbUser.UserId != string.Empty);

            Assert.IsTrue(dbProvider.GetUserById(dbUser.UserId) != null);

            Assert.IsTrue(dbProvider.GetUserByProviderUserName(dbUser.ProviderUserName) != null);

            Assert.IsTrue(dbProvider.DeleteUserByIdAsync(dbUser.UserId).Result);
        }

        [TestMethod]
        public async Task TestUserReadWrite()
        {
            ProviderFactory.IsMocked = false;

            var dbProvider = ProviderFactory.CreateDatabaseProvider();

            UserModel testUser = new UserModel();
            testUser.ProviderUserName = "7701";
            testUser.Email = "testUserReadWrite@user.com";

            var dbUser = dbProvider.GetUserByProviderUserName(testUser.ProviderUserName);

            await dbProvider.DeleteAllFilesFromUserAsync(dbUser.UserId);
            await dbProvider.DeleteUserByProviderNameAsync(testUser.ProviderUserName);

            dbUser = dbProvider.AddUser(testUser);

            Assert.IsTrue(dbUser.UserId != string.Empty);

            Assert.IsTrue(dbProvider.GetUserById(dbUser.UserId) != null);

            Assert.IsTrue(dbProvider.GetUserByProviderUserName(dbUser.ProviderUserName) != null);

            Assert.IsTrue(dbProvider.GetContainerId(dbUser.ProviderUserName) == dbUser.Container);

            Assert.IsTrue(await dbProvider.DeleteUserByIdAsync(dbUser.UserId));
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

            UserModel testUser = new UserModel();
            testUser.ProviderUserName = "9999";
            testUser.Email = "9999@user.com";

            var dbUser = dbProvider.GetUserByProviderUserName(testUser.ProviderUserName);

            await dbProvider.DeleteAllFilesFromUserAsync(dbUser.UserId);
            await dbProvider.DeleteUserByProviderNameAsync(testUser.ProviderUserName);

            dbUser = dbProvider.AddUser(testUser);

            Assert.IsTrue(dbUser.UserId != string.Empty);

            Files testFile = new Files();
            testFile.UserId = dbUser.UserId;
            testFile.FileId = "987654321";
            testFile.Container = dbUser.Container;
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
