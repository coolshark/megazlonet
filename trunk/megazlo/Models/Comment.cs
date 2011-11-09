using System;
using System.ComponentModel.DataAnnotations;
using R = megazlo.Res.Model.Comment;

namespace megazlo.Models {
	public class Comment {
		public Comment() {
			DatePost = DateTime.Now;
		}
		[Key]
		public int Id { get; set; }
		public int ParentId { get; set; }
		public bool IsAutor { get; set; }
		[Display(Name = "DatePost", ResourceType = typeof(R))]
		[DataType(DataType.DateTime)]
		public DateTime DatePost { get; set; }
		[Required(ErrorMessageResourceName = "R_FirstName_E", ErrorMessageResourceType = typeof(R))]
		[Display(Name = "FirstName", ResourceType = typeof(R))]
		[StringLength(50, MinimumLength = 3, ErrorMessageResourceName = "SL_FirstName_E", ErrorMessageResourceType = typeof(R))]
		public string FirstName { get; set; }
		[Required(ErrorMessageResourceName = "R_Email_E", ErrorMessageResourceType = typeof(R))]
		[Display(Name = "Email", ResourceType = typeof(R))]
		[DataType(DataType.EmailAddress)]
		[RegularExpression(@"^([A-Za-z0-9]|[A-Za-z0-9](([a-zA-Z0-9,=\.!\-#|\$%\^&*\+/\?_`\{\}~]+)*)[a-zA-Z0-9,=!\-#|\$%\^&*\+/\?_`\{\}~])@(?:[0-9a-zA-Z-]+\.)+[a-zA-Z]{2,9}$", ErrorMessageResourceName = "RE_Email_E", ErrorMessageResourceType = typeof(R))]
		public string Email { get; set; }
		[Required(ErrorMessageResourceName = "R_Text_E", ErrorMessageResourceType = typeof(R))]
		[Display(Name = "Text", ResourceType = typeof(R))]
		[DataType(DataType.MultilineText)]
		[StringLength(1000, MinimumLength = 5, ErrorMessageResourceName = "SL_Text_E", ErrorMessageResourceType = typeof(R))]
		public string Text { get; set; }
		public int PostId { get; set; }
		public virtual Post Post { get; private set; }
	}
}