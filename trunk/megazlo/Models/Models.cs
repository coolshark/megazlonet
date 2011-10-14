using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace megazlo.Models {

	public class Category {
		[Key]
		public int Id { get; set; }
		[Required]
		[Display(Name = "Заголовок")]
		public string Title { get; set; }
		[Display(Name = "Порядок")]
		public int Por { get; set; }
		public virtual ICollection<Post> Posts { get; set; }
	}

	public class Post {
		public Post() {
			DatePost = DateTime.Now;
			WebLink = string.Empty;
		}
		[Key]
		public int Id { get; set; }
		[Required]
		[Display(Name = "Заголовок")]
		public string Title { get; set; }
		[Required]
		[Display(Name = "Текст")]
		[DataType(DataType.MultilineText)]
		[MaxLength]
		[AllowHtml]
		public string Text { get; set; }
		[NotMapped]
		public string TextPreview {
			get {
				if (string.IsNullOrEmpty(Text))
					return string.Empty;
				int from = Text.IndexOf("<p>");
				return Text.Substring(from, Text.IndexOf("</p>") - from + 4);
			}
		}
		[MaxLength(200)]
		public string WebLink { get; set; }
		[Display(Name = "Дата создания")]
		public DateTime DatePost { get; set; }
		[Display(Name = "Разрешить комментировать")]
		public bool IsCommentable { get; set; }
		public int UserId { get; set; }
		public int? CategoryId { get; set; }
		public virtual ICollection<Comment> Comment { get; set; }
		public virtual User User { get; set; }
		public virtual Category Category { get; set; }
		[Display(Name = "Категория")]
		[NotMapped]
		public SelectList Cat {
			get {
				using (ZloContext cn = new ZloContext())
					return new SelectList(cn.Categorys.ToList(), "Id", "Title");
			}
		}
		[Display(Name = "В меню категории")]
		public bool InCatMenu { get; set; }
		[Display(Name = "Отображать сведения")]
		public bool IsShowInfo { get; set; }
		[NotMapped]
		public Comment NewComment { get; set; }
	}

	public class User {
		public User() {
			DateRegister = DateTime.Now;
		}
		[Key]
		public int Id { get; set; }
		[Required]
		[Display(Name = "Имя")]
		[MaxLength(100)]
		public string FirstName { get; set; }
		[Display(Name = "Фамилия")]
		[MaxLength(100)]
		public string SecondName { get; set; }
		[Display(Name = "Отчество")]
		[MaxLength(100)]
		public string LastName { get; set; }
		[Required]
		[Display(Name = "Ник")]
		[MaxLength(100)]
		public string NickName { get; set; }
		[DataType(DataType.Date)]
		[Display(Name = "Дата рождения")]
		public DateTime DateBorn { get; set; }
		[Display(Name = "Дата регистрации")]
		public DateTime DateRegister { get; set; }
		[Required]
		[DataType(DataType.EmailAddress)]
		[Display(Name = "Адрес Email")]
		[MaxLength(200)]
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

	public class Comment {
		public Comment() {
			DatePost = DateTime.Now;
		}
		[Key]
		public int Id { get; set; }
		public int ParentId { get; set; }
		[Display(Name = "Дата")]
		[DataType(DataType.DateTime)]
		public DateTime DatePost { get; set; }
		[Required]
		[Display(Name = "Имя*")]
		[StringLength(50)]
		public string FirstName { get; set; }
		[Required]
		[Display(Name = "Email*")]
		[DataType(DataType.EmailAddress)]
		[StringLength(150, MinimumLength = 5)]
		public string Email { get; set; }
		[Required]
		[Display(Name = "Текст*")]
		[DataType(DataType.MultilineText)]
		[StringLength(1000, MinimumLength = 5, ErrorMessage = "Нефиг постить смайлики и поэмы.")]
		public string Text { get; set; }
		public int PostId { get; set; }
		public virtual Post Post { get; private set; }
	}

	public class ZloContext : DbContext {
		/// <summary>Категории(т.е. меню)</summary>
		public DbSet<Category> Categorys { get; set; }
		/// <summary>Список комментариев</summary>
		public DbSet<Comment> Comments { get; set; }
		/// <summary>Список сообщений</summary>
		public DbSet<Post> Posts { get; set; }
		/// <summary>Список пользователей</summary>
		public DbSet<User> Users { get; set; }
	}
}