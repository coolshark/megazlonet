using System.ComponentModel.DataAnnotations;
using R = megazlo.Res.Model.LoginUser;

namespace megazlo.Models {
	public class LoginUser {
		public LoginUser() {
			Login = Password = string.Empty;
			IsRemember = false;
		}
		[Required]
		[Display(Name = "Login", ResourceType = typeof(R))]
		[MaxLength(100)]
		public string Login { get; set; }
		[Required]
		[Display(Name = "Password", ResourceType = typeof(R))]
		[DataType(DataType.Password)]
		[StringLength(32, MinimumLength = 6)]
		public string Password { get; set; }
		[Display(Name = "IsRemember", ResourceType = typeof(R))]
		public bool IsRemember { get; set; }
	}
}