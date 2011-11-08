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
		[Required]
		[MaxLength(100)]
		[Display(Name = "Id", ResourceType = typeof(R))]
		public string Id { get; set; }
		[Required]
		[MaxLength(100)]
		[Display(Name = "Name", ResourceType = typeof(R))]
		public string Name { get; set; }
		[MaxLength(100)]
		[Display(Name = "Family", ResourceType = typeof(R))]
		public string Family { get; set; }
		[MaxLength(100)]
		[Display(Name = "LastName", ResourceType = typeof(R))]
		public string LastName { get; set; }
		[DataType(DataType.Date)]
		[Display(Name = "DateBorn", ResourceType = typeof(R))]
		public DateTime DateBorn { get; set; }
		[Display(Name = "DateRegister", ResourceType = typeof(R))]
		public DateTime DateRegister { get; set; }
		[Required]
		[DataType(DataType.EmailAddress)]
		[Display(Name = "Email", ResourceType = typeof(R))]
		[MaxLength(200)]
		[RegularExpression(@"^([A-Za-z0-9]|[A-Za-z0-9](([a-zA-Z0-9,=\.!\-#|\$%\^&*\+/\?_`\{\}~]+)*)[a-zA-Z0-9,=!\-#|\$%\^&*\+/\?_`\{\}~])@(?:[0-9a-zA-Z-]+\.)+[a-zA-Z]{2,9}$", ErrorMessage = "Необходимо ввести email адрес")]
		public string Email { get; set; }
		[NotMapped]
		[Display(Name = "HaveAvatar", ResourceType = typeof(R))]
		public bool HaveAvatar { get; set; }
		[Display(Name = "IsAdmin", ResourceType = typeof(R))]
		public bool IsAdmin { get; set; }
		[Required]
		[DataType(DataType.Password)]
		[StringLength(32, MinimumLength = 6)]
		[Display(Name = "PassWord", ResourceType = typeof(R))]
		public string PassWord { get; set; }
		[Required]
		[DataType(DataType.Password)]
		[Compare("PassWord", ErrorMessage = "Пароль и подтверждение не совпадают!")]
		[NotMapped]
		[Display(Name = "ConfirmPassWord", ResourceType = typeof(R))]
		public string ConfirmPassWord { get; set; }
		public virtual ICollection<Post> Post { get; set; }
	}
}