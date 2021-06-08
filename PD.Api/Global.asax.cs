using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace PD.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            //var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            //json.SerializerSettings.PreserveReferencesHandling =
            //    Newtonsoft.Json.PreserveReferencesHandling.All;
            GlobalConfiguration.Configure(WebApiConfig.Register);

        }
        protected void Application_EndRequest()
        {
            Response.Headers.Remove("Access-Control-Allow-Origin");
        }
    }
}
