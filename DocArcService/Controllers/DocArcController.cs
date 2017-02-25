using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DocArcService.Controllers
{
    public class DocArcController : ApiController
    {
        [HttpGet]
        public string Ping()
        {
            return "PONG...";
        }
    }
}
