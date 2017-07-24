using DocArcService.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocArcService.AbstractClasses
{
    public abstract class FileProcessingProvider : IFileProcessingProvider
    {
        public abstract Task<bool> ProzessImage(string ImageFileName, string Container);
    }
}
