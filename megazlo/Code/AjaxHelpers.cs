using System;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;

namespace megazlo.Code {
	public static class AjaxHelpers {
		/// <summary>
		/// Метод создает ссылку, которая с помощью Ajax запроса загружает в контейнер с именем main
		/// требуемый PartialView
		/// </summary>
		public static MvcHtmlString ActionLinkTo(this AjaxHelper ajaxHelper, String linkText, String actionName, String controllerName = null, Object routeValues = null, String areaName = null, String loadMessage = null, Object htmlAttributes = null) {
			
			// Создаем маршрут
			RouteValueDictionary routeValueDictionary = routeValues == null ? new RouteValueDictionary() : (RouteValueDictionary)(routeValues);
			if (!String.IsNullOrEmpty(actionName) && !routeValueDictionary.ContainsKey("action"))
				routeValueDictionary.Add("action", actionName);
			if (!String.IsNullOrEmpty(controllerName) && !routeValueDictionary.ContainsKey("controller"))
				routeValueDictionary.Add("controller", controllerName);
			if (!routeValueDictionary.ContainsKey("area")) {
				if (!String.IsNullOrEmpty(areaName))
					routeValueDictionary.Add("area", areaName);
				else
					routeValueDictionary.Add("area", "");
			}
			// Создаем параметры Ajax            
			AjaxOptions ajaxOptions = new AjaxOptions() {
				UpdateTargetId = "main",
				InsertionMode = InsertionMode.Replace,
				HttpMethod = "POST",
				LoadingElementId = "loadLayout",
				OnBegin = "changeLoadMesage('" + loadMessage + "')",
				OnSuccess = "onPageLoaded()"
			};
			RouteValueDictionary htmlatr = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
			htmlatr.Add("onclick", "return ajaxClick(this);");
			// Возвращаем строку
			String ajaxActionName = "Ajax" + actionName;
			return ajaxHelper.ActionLink(linkText, ajaxActionName, null, routeValueDictionary, ajaxOptions, htmlatr);
		}
	}
}