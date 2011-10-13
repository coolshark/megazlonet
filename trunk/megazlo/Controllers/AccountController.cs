using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using megazlo.Models;

namespace megazlo.Controllers {
	public class AccountController : Controller {
		public IFormsAuthenticationService FormsService { get; set; }
		public IMembershipService MembershipService { get; set; }

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

		#region LogOn
		public ActionResult LogOn() {
			ViewBag.Title = "Авторизация";
			return View();
		}

		[HttpPost]
		public ActionResult LogOn([Bind(Include = "NickName, PassWord")]User user, string returnUrl) {
			ViewBag.Title = "Авторизация";
			if (user != null && user.NickName != null && user.PassWord != null) {
				if (MembershipService.ValidateUser(user.NickName, user.PassWord)) {
					FormsService.SignIn(user.NickName, true);
					//if (!Roles.RoleExists("Admin"))
					//  Roles.CreateRole("Admin");
					//using (ZloContext con = new ZloContext()) {
					//  User us = con.Users.Where(u => u.NickName == user.NickName).First();
					//  Roles.AddUserToRole(us.NickName, "Admin");
					//}
					if (Url.IsLocalUrl(returnUrl))
						return Redirect(returnUrl);
					else
						return RedirectToAction("Index", "Home");
				}
				ModelState.AddModelError("", "Неверное имя пользователя или пароль.");
			}
			return View(user);
		}
		#endregion

		public ActionResult LogOff() {
			FormsService.SignOut();
			return RedirectToAction("Index", "Home");
		}
	}
}
