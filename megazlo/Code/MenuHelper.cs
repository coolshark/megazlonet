using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using megazlo.Models;
using System;

namespace megazlo.Code {
	public static class MenuHelper {
		private static string tmplC = "<li><a href=\"/Home/Category/{0}\">{0}</a>{1}</li>";
		private static string tmplP = "<li><a href=\"/Home/Post/{0}\">{1}</a></li>";
		private static MvcHtmlString Cashe;

		private static DateTime TimeCashe = new DateTime(1900, 1, 1);
		private static TimeSpan PeriodCache = new TimeSpan(0, 55, 0);

		public static MvcHtmlString GetMenu(this HtmlHelper hlp) {
			if (DateTime.Now - TimeCashe > PeriodCache) {
				TimeCashe = DateTime.Now;
				Cashe = MvcHtmlString.Create(GetMenuCats() + GetMenuPosts());
			}
			return Cashe;
		}

		public static void UpdateCache() {
			TimeCashe = DateTime.Now;
			Cashe = MvcHtmlString.Create(GetMenuCats() + GetMenuPosts());
		}

		private static string GetMenuCats() {
			StringBuilder bld = new StringBuilder();
			using (ZloContext cn = new ZloContext()) {
				foreach (var cat in cn.Categorys.OrderBy(c => c.Por))
					bld.AppendLine(string.Format(tmplC, cat.Title, GetPostInCat(cat)));
			}
			return bld.ToString();
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

		private static string GetMenuPosts() {
			StringBuilder bld = new StringBuilder();
			using (ZloContext cn = new ZloContext()) {
				IQueryable<Post> pst = cn.Posts.Where(p => p.CategoryId == null);
				foreach (var ps in pst)
					bld.AppendLine(string.Format(tmplP, ps.WebLink, ps.Title));
			}
			return bld.ToString();
		}
	}
}