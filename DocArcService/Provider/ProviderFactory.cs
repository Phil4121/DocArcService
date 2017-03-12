using DocArcService.AbstractClasses;
using DocArcService.Interfaces;
using DocArcService.MockedProvider;
using DocArcService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace DocArcService.Provider
{
    public static class ProviderFactory
    {
        public static bool IsMocked { get; set; } = false;

        public static StorageUploadProvider CreateBlobStorageUploadProvider(string containerName)
        {
            return IsMocked
                ? new MockedBlobStorageUploadProvider(containerName) as StorageUploadProvider
                : new BlobStorageUploadProvider(containerName);
        }

        public static IDatabaseProvider CreateDatabaseProvider()
        {
            return IsMocked
                ? new MockedDatabaseProvider() as IDatabaseProvider
                : new AzureDatabaseProvider();
        }
    }
}