using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SMSApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
          
        }

        void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "Enviar",
                "Usuario/Enviar/Texto/HoraEnvio",
                new
                {
                    controller = "Usuario",
                    action = "Enviar",
                    Texto = UrlParameter.Optional,
                    HoraEnvio = UrlParameter.Optional
                });
        }


    }
}
