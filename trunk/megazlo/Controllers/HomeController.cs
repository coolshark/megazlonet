﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
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
			ViewBag.Meaasge = "Упс!";
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
			if (pst != null)
				return View("Post", pst);
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
		public ActionResult AddComment(Post post, int postid) {
			if (ModelState.IsValid) {

			} else
				return RedirectToAction("Error");
			Post pst = con.Posts.Find(postid);
			post.NewComment.PostId = postid;
			pst.Comment.Add(post.NewComment);
			//Comment cmn = post.NewComment;
			//cmn.PostId = post.Id;
			//con.Comments.Add(cmn);
			con.SaveChanges();
			return RedirectToAction("Post", new { id = pst.WebLink });
			//return RedirectToAction("Index");
		}


	}
}