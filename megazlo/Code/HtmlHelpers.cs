using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace megazlo.Code {
	public static class HtmlHelpers {
		/// <summary>
		/// Создает информационный блок:
		/// 1) "pageTitle" - с названием страницы
		/// 2) "pageUrl" - с адресом страницы
		/// </summary>
		public static MvcHtmlString PageInfo(this HtmlHelper helper, Object attr = null) {
			RouteValueDictionary dic = attr == null ? new RouteValueDictionary() : HtmlHelper.AnonymousObjectToHtmlAttributes(attr);
			// Create title
			TagBuilder info = new TagBuilder("input");
			info.MergeAttribute("id", "pageInfo");
			info.MergeAttribute("type", "hidden");
			foreach (string key in dic.Keys)
				info.MergeAttribute(key, dic[key].ToString());
			return new MvcHtmlString(info.ToString(TagRenderMode.Normal));
		}
	}
}