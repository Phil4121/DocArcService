using DocArcService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocArcService.Models;
using System.Threading.Tasks;

namespace DocArcService.MockedProvider
{
    public class MockedDatabaseProvider : IDatabaseProvider
    {
        public Users GetUserByProviderUserName(string ProviderUserName)
        {
            Users user = new Users();
            user.UserId = Guid.NewGuid().ToString();
            user.ProviderUserName = "111010103234";
            user.Email = "test@test.com";
            user.Container = "111-000-222-333";

            return user;
        }

        public bool DatabaseIsReachable()
        {
            return true;
        }

        #region Users

        public Users GetUserById(string UserId)
        {
            Users user = new Users();
            user.UserId = UserId;
            user.ProviderUserName = "111010103234";
            user.Email = "test@test.com";
            user.Container = "111-000-222-333";

            return user;
        }

        public bool InsertUser(Users User)
        {
            return User != null;
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