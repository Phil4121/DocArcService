﻿using DocArcSharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DocArcSharedLibrary.Interfaces
{
    public interface IBlobStorageUploadProvider
    {
        List<BlobUploadModel> Uploads { get; set; }

        string ContainerName { get; }

        Task ExecutePostProcessingAsync();
    }
}
