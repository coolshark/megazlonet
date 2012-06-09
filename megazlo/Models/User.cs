using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using R = megazlo.Res.Model.User;

namespace megazlo.Models {
	public class User {
		public User() {
			DateRegister = DateTime.Now;
		}
		[Key]
		public int Id { get; set; }		
		[Required(ErrorMessageResourceName = "R_Login_E", ErrorMessageResourceType = typeof(R))]
		[StringLength(100, MinimumLength = 2, ErrorMessageResourceName = "SL_Login_E", ErrorMessageResourceType = typeof(R))]
		[Display(Name = "Login", ResourceType = typeof(R))]
		public string Login { get; set; }		
		[Required(ErrorMessageResourceName = "R_Name_E", ErrorMessageResourceType = typeof(R))]
		[MaxLength(100, ErrorMessageResourceName = "ML_Name_E", ErrorMessageResourceType = typeof(R))]
		[Display(Name = "Name", ResourceType = typeof(R))]
		public string Name { get; set; }
		[MaxLength(100, ErrorMessageResourceName = "ML_Family_E", ErrorMessageResourceType = typeof(R))]
		[Display(Name = "Family", ResourceType = typeof(R))]
		public string Family { get; set; }
		[MaxLength(100, ErrorMessageResourceName = "ML_LastName_E", ErrorMessageResourceType = typeof(R))]
		[Display(Name = "LastName", ResourceType = typeof(R))]
		public string LastName { get; set; }
		[DataType(DataType.Date)]
		[Display(Name = "DateBorn", ResourceType = typeof(R))]
		public DateTime DateBorn { get; set; }
		[Display(Name = "DateRegister", ResourceType = typeof(R))]
		public DateTime DateRegister { get; set; }
		[Required(ErrorMessageResourceName = "R_Email_E", ErrorMessageResourceType = typeof(R))]
		[DataType(DataType.EmailAddress)]
		[Display(Name = "Email", ResourceType = typeof(R))]
		[MaxLength(200, ErrorMessageResourceName = "ML_Email_E", ErrorMessageResourceType = typeof(R))]
		[RegularExpression(@"^([A-Za-z0-9]|[A-Za-z0-9](([a-zA-Z0-9,=\.!\-#|\$%\^&*\+/\?_`\{\}~]+)*)[a-zA-Z0-9,=!\-#|\$%\^&*\+/\?_`\{\}~])@(?:[0-9a-zA-Z-]+\.)+[a-zA-Z]{2,9}$", ErrorMessageResourceName = "RE_Email_E", ErrorMessageResourceType = typeof(R))]
		public string Email { get; set; }
		[NotMapped]
		[Display(Name = "HaveAvatar", ResourceType = typeof(R))]
		public bool HaveAvatar { get; set; }
		[Display(Name = "IsAdmin", ResourceType = typeof(R))]
		public bool IsAdmin { get; set; }
		[Required(ErrorMessageResourceName = "R_PassWord_E", ErrorMessageResourceType = typeof(R))]
		[DataType(DataType.Password)]
		[StringLength(32, MinimumLength = 6, ErrorMessageResourceName = "SL_PassWord_E", ErrorMessageResourceType = typeof(R))]
		[Display(Name = "PassWord", ResourceType = typeof(R))]
		public string PassWord { get; set; }
		[Required(ErrorMessageResourceName = "R_ConfirmPassWord_E", ErrorMessageResourceType = typeof(R))]
		[DataType(DataType.Password)]
		[Compare("PassWord", ErrorMessageResourceName = "C_ConfirmPassWord_E", ErrorMessageResourceType = typeof(R))]
		[NotMapped]
		[Display(Name = "ConfirmPassWord", ResourceType = typeof(R))]
		public string ConfirmPassWord { get; set; }
		public virtual ICollection<Post> Post { get; set; }
	}
}