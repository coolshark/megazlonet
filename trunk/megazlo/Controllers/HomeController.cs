using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using megazlo.Code;
using megazlo.Models;
using CaptchaMVC.Attribute;
using CaptchaMVC.HtmlHelpers;

namespace megazlo.Controllers {
	public class HomeController : Controller {
		const int pageSize = 10;
		ZloContext con = new ZloContext();

		public ActionResult Index() {
			return View();
		}

		[HttpPost]
		public JsonResult Index(string arg) {
			JsonResult rez = new JsonResult();
			rez.Data = RenderPartialViewToString("Index", null);
			return rez;
		}

		public ActionResult Error(string id) {
			ViewBag.Title = "Упс!";
			ViewBag.ErrType = "";
			ViewBag.Meaasge = "Упс, мы ее теряем!";
			ViewBag.ImagePath = "~/Content/images/404.png";
			if (id == "html5") {
				ViewBag.ErrType = "html5";
				ViewBag.Meaasge = "Устаревший браузер!";
				ViewBag.ImagePath = "~/Content/images/html5_1.png";
			}
			return View();
		}

		public ActionResult Post(string id) {
			Post pst = null;
			if (con.Posts.Where(p => p.WebLink == id).Count() > 0)
				pst = con.Posts.Where(p => p.WebLink == id).First();
			if (pst != null) {
				ViewBag.Title = pst.Title;
				return View("Post", pst);
			}
			return RedirectToAction("Error");
		}

		[HttpPost]
		public JsonResult Post(string id, string dop) {
			JsonResult rez = new JsonResult();
			Post pst = null;
			if (con.Posts.Where(p => p.WebLink == id).Count() > 0)
				pst = con.Posts.Where(p => p.WebLink == id).First();
			if (pst != null)
				rez.Data = RenderPartialViewToString("Post", pst);
			return rez;
		}

		public ActionResult Category(string id, int page = 0) {
			if (page < 0)
				return RedirectToAction("Error");
			ViewBag.Title = id;
			ViewBag.PageNum = page;
			ViewBag.PageSize = pageSize;
			ViewBag.PostCount = con.Categorys.Where(c => c.Title == id).First().Posts.Count;
			ICollection<Post> mod = con.Categorys.Where(c => c.Title == id).First().Posts.OrderByDescending(p => p.Id).Skip(page * pageSize).Take(pageSize).ToList();
			int pgCount = (int)Math.Ceiling((double)ViewBag.PostCount / pageSize);
			if (page < 0 || page > pgCount)
				return RedirectToAction("Error");
			return View("PostList", mod);
		}

		[HttpPost]
		public JsonResult Category(string id, string test, int page = 0) {
			JsonResult rez = new JsonResult();
			if (page < 0)
				return rez;
			ViewBag.Title = id;
			ViewBag.PageNum = page;
			ViewBag.PageSize = pageSize;
			ViewBag.PostCount = con.Categorys.Where(c => c.Title == id).First().Posts.Count;
			ICollection<Post> mod = con.Categorys.Where(c => c.Title == id).First().Posts.OrderByDescending(p => p.Id).Skip(page * pageSize).Take(pageSize).ToList();
			int pgCount = (int)Math.Ceiling((double)ViewBag.PostCount / pageSize);
			if (page < 0 || page > pgCount)
				return rez;
			rez.Data = RenderPartialViewToString("PostList", mod);
			return rez;
		}

		#region Comments
		[HttpPost]
		public JsonResult AddComment(Comment cmt) {
			JsonResult rez = new JsonResult();
			if (ModelState.IsValid && (User.Identity.IsAuthenticated || this.IsCaptchaVerify("Captcha is not valid"))) {
				try {
					con.Comments.Add(cmt);
					con.SaveChanges();
					rez.Data = RenderPartialViewToString("CommentRead", cmt);
				} catch (Exception e) {
					rez.Data = e.Message;
				}
			} else {
				foreach (var item in ModelState.Keys)
					for (int i = 0; i < ModelState[item].Errors.Count; i++)
						rez.Data += ModelState[item].Errors[i].ErrorMessage + "\r\n";
			}
			return rez;
		}

		public string RenderPartialViewToString(string viewName, object model) {
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

		#region Installation
		public ActionResult Install() {
#if !DEBUG
			int cnt = con.Users.Count();
			if (cnt > 0)
				return RedirectToAction("Index");
#endif
#if(DEBUG)
			User usr = new User() { IsAdmin = true, Login = "admin", Email = "paradoxfm@mail.ru", DateBorn = new DateTime(1984, 11, 11), Name = "Иван", Family = "Гуркин", LastName = "Александрович" };
#else
			User usr = new User() { IsAdmin = true, Id = "admin" };
#endif
			return View(usr);
		}

		[HttpPost]
		public ActionResult Install(User usr, HttpPostedFileBase ava) {
			bool imVald = usr.IsAdmin = true;
			if (ava != null) {
				imVald = ava.ContentLength < 204800;
				if (!imVald)
					ModelState.AddModelError("", "Слишком большой размер изображения.");
			}
			if (imVald && ModelState.IsValid) {
				usr.ConfirmPassWord = usr.PassWord = Hash.CreateHash(usr.PassWord);
				AvatarUploader upl = new AvatarUploader();
				Thread t = new Thread(upl.LoadAvatar);
				t.Start(new object[] { ava, usr.Login });
				con.Users.Add(usr);
				con.SaveChanges();
				return RedirectToAction("Index");
			}
			return View("Install", usr);
		}
		#endregion
	}
}
