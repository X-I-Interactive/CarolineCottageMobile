using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using CarolineCottage.Domain.Infrastructure;

namespace CarolineCottageMobile
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutoMapperDomainConfiguration.Configure("CarolineCottage.Domain");
        }

        protected void Application_BeginRequest()
        {
            //if (!Context.Request.IsSecureConnection
            //            && !Context.Request.IsLocal // to avoid switching to https when local testing
            //            )
            //{
            //    // The 301 Moved Permanently redirect status response code is considered a best practice for upgrading users from HTTP to HTTPS (see Google recommendations)
            //    Response.Clear();
            //    Response.Status = "301 Moved Permanently";
            //    Response.AddHeader("Location", Context.Request.Url.ToString().Insert(4, "s"));
            //    Response.End();
            //    //  OTHERWISE use
            //    // Only insert an "s" to the "http:", and avoid replacing wrongly http: in the url parameters
            //    // Response.Redirect(Context.Request.Url.ToString().Insert(4, "s"));
            //}
        }
    }
}
