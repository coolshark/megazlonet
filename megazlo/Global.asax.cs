﻿using System.Web.Mvc;
using System.Web.Routing;
#if DEBUG
using megazlo.Models;
using System.Data.Entity;
using SquishIt.Framework;
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

			routes.MapRoute("AjaxPost", "Home/AjaxPost/{id}",
				new { controller = "Home", action = "AjaxPost", id = UrlParameter.Optional });

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

			Bundle.Css()
				.Add("~/Content/styles/main.css",
				"~/Content/styles/style.css",
				"~/Scripts/Slider/global.css",
				"~/Scripts/highlight/styles/vs.css",
				"~/Content/redmond/jquery-ui-1.8.16.custom.css")
				.ForceRelease()
				.AsNamed("main_cs", "~/Upload/StyleScript/Site.min_#.css");

			Bundle.JavaScript()
				.Add("~/Scripts/jquery.slidemenu.js",
				"~/Scripts/jquery-ui-1.8.16.custom.min.js",
				"~/Scripts/Ajax/ajaxnavigation.js",
				"~/Scripts/Ajax/Comments.js",
				//"~/Scripts/jquery.validate.min.js",
				//"~/Scripts/jquery.validate.unobtrusive.min.js",
				"~/Scripts/jquery.vtip.js",
				"~/Scripts/highlight/highlight.pack.js",
				"~/Scripts/Slider/slides.min.jquery.js")
				.ForceRelease()
				.AsNamed("main_js", "~/Upload/StyleScript/Site.min_#.js");
		}
	}
}