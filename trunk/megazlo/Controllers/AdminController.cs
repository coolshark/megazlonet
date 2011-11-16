using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using megazlo.Code;
using megazlo.Models;

namespace megazlo.Controllers {

	[Authorize]
	public class AdminController : Controller {
		ZloContext con = new ZloContext();

		#region index
		public ActionResult Index() {
			return View();
		}

		[HttpPost]
		public JsonResult Index(string e) {
			JsonResult rez = new JsonResult();
			rez.Data = RenderPartialViewToString("Index", null);
			return rez;
		}
		#endregion

		#region Статьи
		public ActionResult Post(string id) {
			Post pst = new Post() { IsCommentable = true, IsShowInfo = true };
			if (id != null) {
				int ids = int.Parse(id);
				pst = con.Posts.Where(p => p.Id == ids).FirstOrDefault();
			}
			return View(pst);
		}

		[HttpPost]
		public JsonResult Post(string id, string dop) {
			JsonResult rez = new JsonResult();
			Post pst = new Post() { IsCommentable = true, IsShowInfo = true };
			if (id != null) {
				int ids = int.Parse(id);
				pst = con.Posts.Where(p => p.Id == ids).FirstOrDefault();
			}
			rez.Data = RenderPartialViewToString("Post", pst);
			return rez;
		}

		[HttpPost]
		public JsonResult PostSave(Post post) {
			JsonResult rez = new JsonResult();
			if (!ModelState.IsValid) {
				foreach (var item in ModelState.Keys)
					for (int i = 0; i < ModelState[item].Errors.Count; i++)
						rez.Data += ModelState[item].Errors[i].ErrorMessage + "<br />";
				return rez;
			}
			post.Text = Uploader.Parce(post.Text);
			post.WebLink = Uploader.ParceLink(post.Title);
			post.UserId = User.Identity.Name;
			if (post.Id == 0)
				con.Posts.Add(post);
			else
				con.Entry(post).State = System.Data.EntityState.Modified;
			con.SaveChanges();
			if (post.InCatMenu)
				MenuHelper.UpdateCache();
			rez.Data = "Статья успешно сохранена.;" + post.Id;
			return rez;
		}

		[HttpPost]
		public JsonResult PostDelete(int id) {
			JsonResult rez = new JsonResult();
			Post post = con.Posts.Where(p => p.Id == id).FirstOrDefault();
			con.Entry(post).State = System.Data.EntityState.Deleted;
			con.SaveChanges();
			if (post.InCatMenu)
				MenuHelper.UpdateCache();
			rez.Data = "Статья успешно удалена.;true";
			return rez;
		}
		#endregion

		#region Категории меню
		[HttpPost]
		public JsonResult CategoryForm(Category cat) {
			JsonResult rez = new JsonResult();
			rez.Data = RenderPartialViewToString("Category", cat);
			return rez;
		}

		[HttpPost]
		public JsonResult Category(Category cat) {
			JsonResult rez = new JsonResult();
			if (cat.Id == 0) {
				con.Categorys.Add(cat);
				rez.Data = RenderPartialViewToString("CategoryRow", cat);
			} else {
				con.Entry(cat).State = System.Data.EntityState.Modified;
				rez.Data = "Внесены изменения в категорию.";
			}
			con.SaveChanges();
			MenuHelper.UpdateCache();
			return rez;
		}

		public ActionResult CategoryList() {
			List<Category> mod = con.Categorys.OrderBy(c => c.Por).ToList();
			return View(mod);
		}

		[HttpPost]
		public JsonResult CategoryList(string e) {
			JsonResult rez = new JsonResult();
			List<Category> cats = con.Categorys.OrderBy(c => c.Por).ToList();
			rez.Data = RenderPartialViewToString("CategoryList", cats);
			return rez;
		}

		[HttpPost]
		public JsonResult CategoryDelete(int id) {
			JsonResult rez = new JsonResult();
			Category cat = new Category() { Id = id };
			con.Entry(cat).State = System.Data.EntityState.Deleted;
			con.SaveChanges();
			MenuHelper.UpdateCache();
			rez.Data = "Категория меню удалена.;true";
			return rez;
		}
		#endregion

		#region Теги
		public ActionResult TagList() {
			List<Tag> rez = con.Tags.ToList();
			return View(rez);
		}

		[HttpPost]
		public JsonResult TagList(string e) {
			JsonResult rez = new JsonResult();
			List<Tag> tags = con.Tags.ToList();
			rez.Data = RenderPartialViewToString("TagList", tags);
			return rez;
		}

		[HttpPost]
		public JsonResult TagForm(Tag tg) {
			JsonResult rez = new JsonResult();
			rez.Data = RenderPartialViewToString("Tag", tg);
			return rez;
		}

		[HttpPost]
		public JsonResult Tag(Tag tg) {
			JsonResult rez = new JsonResult();
			if (tg.Id == 0) {
				con.Tags.Add(tg);
				rez.Data = RenderPartialViewToString("TagRow", tg);
			} else {
				con.Entry(tg).State = System.Data.EntityState.Modified;
				rez.Data = "Внесены изменения в тег.";
			}
			con.SaveChanges();
			return rez;
		}

		public JsonResult LoadTags(string test) {
			JsonResult rez = new JsonResult();
			Tag[] tg = con.Tags.ToArray();
			string[] arr = new string[tg.Count()];
			for (int i = 0; i < arr.Length; i++)
				arr[i] = tg[i].Title;
			rez.Data = arr;
			return rez;
		}

		[HttpPost]
		public JsonResult TagDelete(int id) {
			JsonResult rez = new JsonResult();
			con.Entry(new Tag() { Id = id }).State = System.Data.EntityState.Deleted;
			con.SaveChanges();
			rez.Data = "Тег удален";
			return rez;
		}
		#endregion

		#region комент сру рендер
		[HttpPost]
		public JsonResult CommentDelete(int id) {
			JsonResult rez = new JsonResult();
			Comment cmn = new Comment() { Id = id };
			con.Entry(cmn).State = System.Data.EntityState.Deleted;
			con.SaveChanges();
			rez.Data = "Коментарий удален.;true";
			return rez;
		}

		public ActionResult ShortUrl(string cod) {
			// TODO: Short ru ССылки
			// TODO: Короткие ru ССылки
			Response.StatusCode = 301;
			Response.StatusDescription = "Moved Permanently";
			Response.AddHeader("Location", "/Home");
			return null;
		}

		protected string RenderPartialViewToString(string viewName, object model) {
			if (string.IsNullOrEmpty(viewName))
				return string.Empty;
			ViewData.Model = model;
			using (StringWriter sw = new StringWriter()) {
				ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
				ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
				viewResult.View.Render(viewContext, sw);
				return sw.GetStringBuilder().ToString();
			}
		}
		#endregion

	}
}
