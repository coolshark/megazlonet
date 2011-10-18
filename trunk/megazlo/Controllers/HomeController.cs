using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using megazlo.Code;
using megazlo.Models;

namespace megazlo.Controllers {
	public class HomeController : Controller {
		const int pageSize = 10;
		ZloContext con = new ZloContext();

		public ActionResult Index() {
			ViewBag.Title = "megazlo.net";
			//foreach (var item in con.Posts)
			//  item.Text = megazlo.Code.PostXml.Parce(item.Text.Replace("/Content/Upload/img/", "/Upload/img/"));
			//con.SaveChanges();
			return View();
		}

		public ActionResult Error(string id) {
			ViewBag.Title = "Упс!";
			ViewBag.ErrType = "";
			ViewBag.Meaasge = "Упс, мы ее теряем!";
			ViewBag.ImagePath = "~/Content/styles/images/404.png";
			if (id == "html5") {
				ViewBag.ErrType = "html5";
				ViewBag.Meaasge = "Устаревший браузер!";
				ViewBag.ImagePath = "~/Content/styles/images/html5_1.png";
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
		public JsonResult AddComment(Comment cmt) {
			JsonResult rez = new JsonResult();
			if (ModelState.IsValid) {
				try {
					con.Comments.Add(cmt);
					con.SaveChanges();
					rez.Data = true;
				} catch (Exception e) {
					rez.Data = e.Message;
				}
			} else
				foreach (var item in ModelState.Keys)
					for (int i = 0; i < ModelState[item].Errors.Count; i++)
						rez.Data += ModelState[item].Errors[i].ErrorMessage + "\r\n";
			return rez;
		}

		public ActionResult Install() {
			int cnt = con.Users.Count();
			if (cnt > 0)
				return RedirectToAction("Index");
			ViewBag.Title = "Установка";
#if(DEBUG)
			User usr = new User() { IsAdmin = true, NickName = "admin", Email = "paradoxfm@mail.ru", DateBorn = new DateTime(1984, 11, 11), Name = "Иван", LastName = "Гуркин", Family = "Александрович" };
#else
			User usr = new User() { IsAdmin = true, NickName = "admin" };
#endif
			return View(usr);
		}

		[HttpPost]
		public ActionResult Install(User usr) {
			if (ModelState.IsValid) {
				usr.ConfirmPassWord = usr.PassWord = Hash.CreateHash(usr.PassWord);
				con.Users.Add(usr);
				con.SaveChanges();
				return RedirectToAction("Login", "Account");
			}
			return View("Install", usr);
		}
	}
}
