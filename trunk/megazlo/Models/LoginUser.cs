using System.ComponentModel.DataAnnotations;
using R = megazlo.Res.Model.LoginUser;

namespace megazlo.Models {
	public class LoginUser {
		public LoginUser() {
			Login = Password = string.Empty;
			IsRemember = false;
		}
		[Required(ErrorMessageResourceName = "R_Login_E", ErrorMessageResourceType = typeof(R))]
		[Display(Name = "Login", ResourceType = typeof(R))]
		[StringLength(100, MinimumLength = 2, ErrorMessageResourceName = "SL_Login_E", ErrorMessageResourceType = typeof(R))]
		public string Login { get; set; }
		[Required(ErrorMessageResourceName = "R_Password_E", ErrorMessageResourceType = typeof(R))]
		[Display(Name = "Password", ResourceType = typeof(R))]
		[DataType(DataType.Password)]
		[StringLength(32, MinimumLength = 6, ErrorMessageResourceName = "SL_Password_E", ErrorMessageResourceType = typeof(R))]
		public string Password { get; set; }
		[Display(Name = "IsRemember", ResourceType = typeof(R))]
		public bool IsRemember { get; set; }
	}
}