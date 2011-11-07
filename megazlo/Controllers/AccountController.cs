using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using megazlo.Code;
using megazlo.Models;
using System.IO;

namespace megazlo.Controllers {
	public class AccountController : Controller {
		public IFormsAuthenticationService FormsService { get; set; }
		public IMembershipService MembershipService { get; set; }
		ZloContext con = new ZloContext();

		protected override void Initialize(RequestContext requestContext) {
			if (FormsService == null)
				FormsService = new FormsAuthenticationService();
			if (MembershipService == null)
				MembershipService = new MZMembershipService();
			base.Initialize(requestContext);
		}

		public ActionResult Index() {
			return View();
		}

		#region Register
		public ActionResult Register() {
			return View();
		}

		[HttpPost]
		public ActionResult Register(User user) {
			return View();
		}
		#endregion

		#region EditProfile
		[Authorize]
		public ActionResult EditProfile() {
			return View();
		}

		[HttpPost]
		[Authorize]
		public ActionResult EditProfiles(User user) {
			return View();
		}
		#endregion

		[HttpPost]
		public JsonResult GetLoginForm() {
			JsonResult rez = new JsonResult();
			rez.Data = RenderPartialViewToString("Login", new LoginUser());
			return rez;
		}

		[HttpPost]
		public JsonResult LoginAjax(LoginUser usr) {
			JsonResult rez = new JsonResult();
			if (ModelState.IsValid) {
				User usrs = MembershipService.ValidateUser(usr.Login, usr.Password);
				if (usrs != null) {
					FormsService.SignIn(usr.Login, usr.IsRemember);
					rez.Data = true;
					return rez;
				}
			}
			rez.Data = "Неверное имя пользователя или пароль.";
			return rez;
		}

		[Authorize]
		public ActionResult LogOff() {
			FormsService.SignOut();
			return RedirectToAction("Index", "Home");
		}

		public ActionResult RestorePassword() {
			ViewBag.Title = "Востановление пароля";
			return View();
		}

		[HttpPost]
		public JsonResult RestorePassword(RestorePass rest) {
			JsonResult rez = new JsonResult();
			ViewBag.Title = "Востановление пароля";
			if (ModelState.IsValid) {
				IQueryable<User> usrs = con.Users.Where(u => u.Id == rest.Name);
				if (usrs.Count() > 0) {
					User usr = usrs.First();
					Random rnd = new Random();
					string newpas = string.Empty;
					for (int i = 0; i < 7; i++)
						newpas += (char)rnd.Next(33, 126);
					if (Mail.Send(usr.Email, newpas)) {
						usr.PassWord = Hash.CreateHash(newpas);
						con.Entry(usr).State = System.Data.EntityState.Modified;
						con.SaveChanges();
						rez.Data = "Пароль отправлен на почтовый адрес, указанный при регистрации.";
					} else
						rez.Data = "Возникла ошибка при востановлении.";
				} else {
					rez.Data = "Нет пользователя с таким логином.";
				}
			}
			return rez;
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
	}
}
