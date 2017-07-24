using DocArcService.AbstractClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace DocArcService.MockedProvider
{
    public class MockedCognitiveServiceProvider : FileProcessingProvider
    {
        public override async Task<bool> ProzessImage(string ImageFileName, string Container)
        {
            return await Task.FromResult(true);
        }
    }
}