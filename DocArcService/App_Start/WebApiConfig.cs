using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Routing;

namespace DocArcService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API-Konfiguration und -Dienste

            // Web API-Routen
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                "PostBlobUpload",
                "blobs/upload",
                new {controller = "Blobs", action = "PostBlobUpload" },
                new {httpMethod = new HttpMethodConstraint("POST")}
            );

            config.Routes.MapHttpRoute(
                "GetBlobDownload",
                "blobs/{blobFileName}/download",
                new { controller = "Blobs", action = "GetBlobDownload" },
                new { httpMethod = new HttpMethodConstraint("GET") }
            );
        }
    }
}
