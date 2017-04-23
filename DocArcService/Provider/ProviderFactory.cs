﻿using DocArcService.AbstractClasses;
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

        public static StorageUploadProvider CreateBlobStorageUploadProvider(string containerName, string providerUserName)
        {
            return IsMocked
                ? new MockedBlobStorageUploadProvider(containerName, providerUserName) as StorageUploadProvider
                : new BlobStorageUploadProvider(containerName, providerUserName);
        }

        public static IDatabaseProvider CreateDatabaseProvider()
        {
            return IsMocked
                ? new MockedDatabaseProvider() as IDatabaseProvider
                : new AzureDatabaseProvider();
        }

        public static INoSqlDatabase CreateDocumentDBProvider()
        {
            return IsMocked
                ? new MockedDocumentDBProvider() as INoSqlDatabase
                : new DocumentDBProvider();
        }
    }
}