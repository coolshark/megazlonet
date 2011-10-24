using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using megazlo.Code;
using megazlo.Models;
using System.Configuration;
using System.Web.Configuration;

namespace megazlo.Controllers {

	[Authorize]
	public class AdminController : Controller {
		string view = "Post";
		ZloContext con = new ZloContext();

		public ActionResult Index() {
			ViewBag.Title = "Админка";
			return View();
		}

		public ActionResult AddNews() {
			ViewBag.Title = "Добавление статей";
			ViewBag.ButtonOk = "Создать";
			Post pst = new Post();
			pst.InitTags();
			return View(view, pst);
		}

		[HttpPost]
		public ActionResult AddNews(Post post) {
			if (!ModelState.IsValid)
				return View(view, post);
			ViewBag.Title = "Добавление статей";
			ViewBag.ButtonOk = "Создать";
			post.Text = Uploader.Parce(post.Text);
			post.WebLink = Uploader.ParceLink(post.Title);
			post.UserId = User.Identity.Name;
			con.Posts.Add(post);
			con.SaveChanges();
			if (post.InCatMenu)
				MenuHelper.UpdateCache();
			return RedirectToAction("Post", "Home", new { id = post.WebLink });
		}

		public ActionResult EditNews(int id) {
			Post pst = con.Posts.Where(p => p.Id == id).First();
			ViewBag.ButtonOk = "Изменить";
			ViewBag.Title = "Редактирование статьи: " + pst.Title;
			return View(view, pst);
		}

		[HttpPost]
		public ActionResult EditNews(Post post) {
			if (!ModelState.IsValid)
				return View(view, post);
			post.Text = Uploader.Parce(post.Text);
			post.WebLink = Uploader.ParceLink(post.Title);
			con.Entry(post).State = System.Data.EntityState.Modified;
			con.SaveChanges();
			ViewBag.ButtonOk = "Изменить";
			ViewBag.Title = "Редактирование статьи: " + post.Title;
			return RedirectToAction("Post", "Home", new { id = post.WebLink });
		}

		public ActionResult DeleteNews(int id) {
			Post post = con.Posts.Where(p => p.Id == id).FirstOrDefault();
			con.Entry(post).State = System.Data.EntityState.Deleted;
			con.SaveChanges();
			ViewBag.Title = "Удалено";
			if (post.InCatMenu)
				MenuHelper.UpdateCache();
			return RedirectToAction("Index");
		}

		public ActionResult AddCategory() {
			ViewBag.Title = "Добавить категорию";
			return View("Category");
		}

		[HttpPost]
		public ActionResult AddCategory(Category cat) {
			ViewBag.Title = "Добавить категорию";
			con.Categorys.Add(cat);
			con.SaveChanges();
			MenuHelper.UpdateCache();
			return RedirectToAction("CategoryList");
		}

		public ActionResult EditCategory(int id) {
			Category cat = con.Categorys.Where(p => p.Id == id).First();
			ViewBag.ButtonOk = "Изменить";
			ViewBag.Title = "Редактирование категории: " + cat.Title;
			return View("Category", cat);
		}

		public ActionResult CategoryList() {
			ViewBag.Title = "Категории";
			List<Category> mod = con.Categorys.OrderBy(c => c.Por).ToList();
			return View(mod);
		}

		public ActionResult DeleteCat(int id) {
			Category cat = new Category() { Id = id };
			con.Entry(cat).State = System.Data.EntityState.Deleted;//.Categorys.Remove(cat);
			con.SaveChanges();
			MenuHelper.UpdateCache();
			return RedirectToAction("CategoryList");
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

	}
}
