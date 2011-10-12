using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace megazlo.Code {
	public static class PageHelper {
		public static MvcHtmlString PageNavigator(this HtmlHelper hlp, int ItemsCoun, int PageSize, int PageNum = 0) {
			StringBuilder sb = new StringBuilder();
			if (PageNum > 0)
				sb.AppendLine("<div class=\"nav-previous\">" + hlp.ActionLink("Следующие →", "Category", "Home", new { page = PageNum - 1 }, null) + "</div>");
			int pgCount = (int)Math.Ceiling((double)ItemsCoun / PageSize);
			if (PageNum < pgCount - 1)
				sb.AppendLine("<div class=\"nav-next\">" + hlp.ActionLink("← Предыдущие", "Category", "Home", new { page = PageNum + 1 }, null) + "</div>");
			return MvcHtmlString.Create(sb.ToString());
		}

		public static string GetClassPost(this HtmlHelper hlp, bool ishowinf) {
			return ishowinf ? "entry-content" : "";
		}
	}
}