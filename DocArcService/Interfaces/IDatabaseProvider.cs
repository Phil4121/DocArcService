using DocArcService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocArcService.Interfaces
{
    public interface IDatabaseProvider
    {
        bool DatabaseIsReachable();

        #region Users

        UserModel GetUserByProviderUserName(string ProviderUserName);

        UserModel GetUserById(string UserId);

        UserModel AddUser(UserModel User);

        Task<bool> DeleteUserByIdAsync(string UserId);

        Task<bool> DeleteUserByProviderNameAsync(string ProviderUserName);

        string GetContainerId(string ProviderUserName);

        #endregion

        #region Files

        bool InsertFile(Files file, bool SaveChangesAsyncImed = true);

        Task<bool> InsertFiles(List<Files> files);

        Task<bool> DeleteFileAsync(Files file, bool SaveChangesAsyncImed = true);

        Task<bool> DeleteFilesAsync(List<Files> files);

        Task<bool> DeleteAllFilesFromUserAsync(string UserId);

        List<Files> GetFilesByUserId(string UserId);

        List<Files> GetFilesByContainerId(string ContainerId);

        #endregion
    }
}
