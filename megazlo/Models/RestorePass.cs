using System.ComponentModel.DataAnnotations;

namespace megazlo.Models {
	public class RestorePass {
		[Required]
		[RegularExpression(@"^([A-Za-z0-9]|[A-Za-z0-9](([a-zA-Z0-9,=\.!\-#|\$%\^&*\+/\?_`\{\}~]+)*)[a-zA-Z0-9,=!\-#|\$%\^&*\+/\?_`\{\}~])@(?:[0-9a-zA-Z-]+\.)+[a-zA-Z]{2,9}$", ErrorMessage = "Необходимо ввести email адрес")]
		public string Email { get; set; }
	}
}