using System.ComponentModel.DataAnnotations;

namespace megazlo.Models {
	public class LoginUser {
		public LoginUser() {
			Login = Password = string.Empty;
			IsRemember = false;
		}
		[Required]
		[Display(Name = "Ник")]
		[MaxLength(100)]
		public string Login { get; set; }
		[Required]
		[Display(Name = "Пароль")]
		[DataType(DataType.Password)]
		[StringLength(32, MinimumLength = 6)]
		public string Password { get; set; }
		[Display(Name = "Зпомнить")]
		public bool IsRemember { get; set; }
	}
}