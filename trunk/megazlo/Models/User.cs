using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace megazlo.Models {
	public class User {
		public User() {
			DateRegister = DateTime.Now;
		}
		[Key]
		[Required]
		[Display(Name = "Ник (требуется для авторизации)")]
		[MaxLength(100)]
		public string Id { get; set; }
		[Required]
		[Display(Name = "Имя")]
		[MaxLength(100)]
		public string Name { get; set; }
		[Display(Name = "Фамилия")]
		[MaxLength(100)]
		public string Family { get; set; }
		[Display(Name = "Отчество")]
		[MaxLength(100)]
		public string LastName { get; set; }
		[DataType(DataType.Date)]
		[Display(Name = "Дата рождения")]
		public DateTime DateBorn { get; set; }
		[Display(Name = "Дата регистрации")]
		public DateTime DateRegister { get; set; }
		[Required]
		[DataType(DataType.EmailAddress)]
		[Display(Name = "Адрес Email")]
		[MaxLength(200)]
		[RegularExpression(@"^([A-Za-z0-9]|[A-Za-z0-9](([a-zA-Z0-9,=\.!\-#|\$%\^&*\+/\?_`\{\}~]+)*)[a-zA-Z0-9,=!\-#|\$%\^&*\+/\?_`\{\}~])@(?:[0-9a-zA-Z-]+\.)+[a-zA-Z]{2,9}$", ErrorMessage = "Необходимо ввести email адрес")]
		public string Email { get; set; }
		[Display(Name = "Аватара")]
		[DataType(DataType.ImageUrl)]
		public string Avatar { get; set; }
		[Display(Name = "Администратор")]
		public bool IsAdmin { get; set; }
		[Required]
		[Display(Name = "Пароль")]
		[DataType(DataType.Password)]
		[StringLength(32, MinimumLength = 6)]
		public string PassWord { get; set; }
		[Required]
		[Display(Name = "Подтверждение пароля")]
		[DataType(DataType.Password)]
		[Compare("PassWord", ErrorMessage = "Пароль и подтверждение не совпадают!")]
		[NotMapped]
		public string ConfirmPassWord { get; set; }
		public virtual ICollection<Post> Post { get; set; }
	}
}