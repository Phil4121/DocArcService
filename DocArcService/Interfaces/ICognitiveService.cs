using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocArcService.Interfaces
{
    interface ICognitiveService
    {
        Task<bool> ProzessImageAsync(string blobFileName, string container);
    }
}
