using MvcExtensions;
using MvcExtensions.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BlogModule {

    public class MvcApplication : WindsorMvcApplication {

        public MvcApplication() {
            Bootstrapper.BootstrapperTasks
                .Include<RegisterControllers>()
                .Include<RegisterModelMetadata>()
                .Include<RegisterAreas>();
        }



        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }
    }
}