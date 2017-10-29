using DocArcSharedLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocArcSharedLibrary.Models;
using System.Threading.Tasks;

namespace DocArcSharedLibrary.MockedProvider
{
    public class MockedDatabaseProvider : IDatabaseProvider
    {
        public UserModel GetUserByProviderUserName(string ProviderUserName)
        {
            UserModel user = new UserModel();
            user.ProviderUserName = "111010103234";
            user.Email = "test@test.com";

            return user;
        }

        public bool DatabaseIsReachable()
        {
            return true;
        }

        #region Users

        public UserModel GetUserById(string UserId)
        {
            UserModel user = new UserModel();
            user.ProviderUserName = "111010103234";
            user.Email = "test@test.com";

            return user;
        }

        public UserModel AddUser(UserModel User)
        {
            Users mockedDBUser = new Users();
            mockedDBUser.UserId = Guid.NewGuid().ToString();
            mockedDBUser.Container = Guid.NewGuid().ToString();
            mockedDBUser.Email = User.Email;
            mockedDBUser.ProviderUserName = User.ProviderUserName;

            UserModel mockedUser = new UserModel(mockedDBUser.UserId, mockedDBUser.Container);
            mockedUser.Email = mockedDBUser.Email;
            mockedUser.ProviderUserName = mockedDBUser.ProviderUserName;

            return mockedUser;
        }

        public async Task<bool> DeleteUserByIdAsync(string UserId)
        {
            return await Task.FromResult(!string.IsNullOrEmpty(UserId));
        }

        public async Task<bool> DeleteUserByProviderNameAsync(string ProviderUserName)
        {
            return await Task.FromResult(!string.IsNullOrEmpty(ProviderUserName));
        }

        public string GetContainerId(string ProviderUserName)
        {
            return Guid.NewGuid().ToString();
        }

        #endregion

        #region Files


        public bool InsertFile(Files file, bool SaveChangesAsyncImed = true)
        {
            return file != null;
        }

        public async Task<bool> InsertFiles(List<Files> files)
        {
            return await Task.FromResult(files.Count > 0);
        }

        public async Task<bool> DeleteFileAsync(Files file, bool SaveChangesAsyncImed = true)
        {
            return await Task.FromResult(file != null);
        }

        public async Task<bool> DeleteFilesAsync(List<Files> files)
        {
            return await Task.FromResult(files.Count > 0);
        }

        public async Task<bool> DeleteAllFilesFromUserAsync(string UserId)
        {
            return await Task.FromResult(!string.IsNullOrEmpty(UserId));
        }

        public List<Files> GetFilesByUserId(string UserId)
        {
            Files file = new Files();
            file.Container = Guid.NewGuid().ToString();
            file.UserId = UserId;
            file.FileId = Guid.NewGuid().ToString();
            file.OriginalFileName = "Testfile";
            file.OriginalFileType = "JPG";
            file.FileSizeInKB = 10;

            var fileList = new List<Files>();
            fileList.Add(file);

            return fileList;
        }

        public List<Files> GetFilesByContainerId(string ContainerId)
        {
            Files file = new Files();
            file.Container = ContainerId;
            file.UserId = Guid.NewGuid().ToString();
            file.FileId = Guid.NewGuid().ToString();
            file.OriginalFileName = "Testfile2";
            file.OriginalFileType = "JPG";
            file.FileSizeInKB = 100;

            var fileList = new List<Files>();
            fileList.Add(file);

            return fileList;
        }

        #endregion
    }
}