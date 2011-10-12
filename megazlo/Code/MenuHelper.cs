using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using megazlo.Models;

namespace megazlo.Code {
	public static class MenuHelper {
		private static string tmplC = "<li><a href=\"/Home/Category/{0}\">{0}</a>{1}</li>";
		private static string tmplP = "<li><a href=\"/Home/Post/{0}\">{1}</a></li>";

		public static MvcHtmlString GetMenuCats(this HtmlHelper hlp) {
			StringBuilder bld = new StringBuilder();
			using (ZloContext cn = new ZloContext()) {
				foreach (var cat in cn.Categorys.OrderBy(c => c.Por))
					bld.AppendLine(string.Format(tmplC, cat.Title, GetPostInCat(cat)));
			}
			return MvcHtmlString.Create(bld.ToString());
		}

		private static string GetPostInCat(Category cat) {
			StringBuilder bld = new StringBuilder();
			using (ZloContext cn = new ZloContext()) {
				List<Post> psts = cn.Posts.Where(p => p.CategoryId == cat.Id).Where(p => p.InCatMenu).ToList();
				if (psts.Count > 0) {
					bld.AppendLine("<ul>");
					foreach (var ps in psts)
						bld.AppendLine(string.Format(tmplP, ps.WebLink, ps.Title));
					bld.AppendLine("</ul>");
				}
			}
			return bld.ToString();
		}

		public static MvcHtmlString GetMenuPosts(this HtmlHelper hlp) {
			StringBuilder bld = new StringBuilder();
			using (ZloContext cn = new ZloContext()) {
				IQueryable<Post> pst = cn.Posts.Where(p => p.CategoryId == null);
				foreach (var ps in pst)
					bld.AppendLine(string.Format(tmplP, ps.WebLink, ps.Title));
			}
			return MvcHtmlString.Create(bld.ToString());
		}
	}
}