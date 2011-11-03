using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace megazlo.Code {
	public abstract class ControllerBase : Controller {
		protected override void Execute(RequestContext requestContext) {
			// Если браузер запрашивает метод с именем Ajax{Something}, значит он ожидает получить PartialView
			//  однако, если при этом не задан заголовок X-Requested-With, значит пользователь попытался отобразить ссылку в новом окне/вкладке
			//  и, следовательно, его нужно перенаправить на полную страницу.
			Boolean isAjaxRequest = requestContext.HttpContext.Request.QueryString["X-Requested-With"] != null;
			Boolean urlRequestPartialView = requestContext.HttpContext.Request.RawUrl.ToLower().Contains("ajax");
			if ((urlRequestPartialView) && (!isAjaxRequest)) {
				String newUrl = requestContext.HttpContext.Request.RawUrl.Replace("ajax", "");
				requestContext.HttpContext.Response.Redirect(newUrl);
			}
			base.Execute(requestContext);
		}
	}
}