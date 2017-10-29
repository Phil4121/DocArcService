using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocArcSharedLibrary.Interfaces
{
    public interface IFileProcessingProvider
    {
        Task<bool> ProzessImage(string ImageFileName, string Container);
    }
}
