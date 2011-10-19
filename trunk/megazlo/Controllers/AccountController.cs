using System;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Routing;
using megazlo.Code;
using megazlo.Models;

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

		public ActionResult Login() {
			ViewBag.Title = "Вход";
			return View();
		}

		[HttpPost]
		public ActionResult Login(LoginUser usr, string returnUrl) {
			ViewBag.Title = "Вход";
			if (ModelState.IsValid) {
				if (MembershipService.ValidateUser(usr.Login, usr.Password)) {
					FormsService.SignIn(usr.Login, true);
					return Url.IsLocalUrl(returnUrl) ? Redirect(returnUrl) : Redirect("/");
				}
				usr.Password = string.Empty;
				ModelState.AddModelError("", "Неверное имя пользователя или пароль.");
			}
			usr.Password = string.Empty;
			return View(usr);
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
	}
}
