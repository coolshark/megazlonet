using System.Web.Mvc;
using System.Web.Routing;
#if DEBUG
using megazlo.Models;
using System.Data.Entity;
#endif

namespace megazlo {

	public class MvcApplication : System.Web.HttpApplication {
		public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute("Post", "Home/Post/{id}",
				new { controller = "Home", action = "Post", id = UrlParameter.Optional });

			routes.MapRoute("Pages", // Route name
				"{controller}/{action}/{id}/{page}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional, page = UrlParameter.Optional }); // Parameter defaults

			routes.MapRoute("Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional }); // Parameter defaults
		}

		protected void Application_Start() {
#if DEBUG
			Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ZloContext>());
#endif
			AreaRegistration.RegisterAllAreas();
			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}
	}
}