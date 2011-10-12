using System.Web.Mvc;
using System.Web.Routing;

namespace XCaptcha.Web.Mvc
{
	public class RouteBuilder
	{
		private RouteBuilder()
		{

		}

		private static RouteBuilder _default;

		public static RouteBuilder Default
		{
			get { return _default ?? (_default = new RouteBuilder()); }
		}

		public void Map(RouteCollection routes, string controller, string imageAction = "CaptchaImage",
				string solutionAction = "EncryptedCaptchaSolution")
		{
			MapImageAction(routes, controller, imageAction);
			MapSolutionAction(routes, controller, solutionAction);
		}

		public void MapImageAction(RouteCollection routes, string controller, string action = "CaptchaImage")
		{

			routes.MapRoute(
					RouteNames.Image,
					string.Format("{0}/{1}", controller, action),
					new { controller, action }
			);
		}

		public void MapSolutionAction(RouteCollection routes, string controller, string action = "EncryptedCaptchaSolution")
		{
			routes.MapRoute(
				 RouteNames.EncryptedSolution,
				 string.Format("{0}/{1}", controller, action),
				 new { controller, action }
		 );
		}

	}
}