using DocArcService.AbstractClasses;
using DocArcService.Interfaces;
using DocArcService.MockedProvider;
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

        public static StorageUploadProvider CreateBlobStorageUploadProvider()
        {
            return IsMocked
                ? new MockedBlobStorageUploadProvider() as StorageUploadProvider
                : new BlobStorageUploadProvider();
        }

    }
}