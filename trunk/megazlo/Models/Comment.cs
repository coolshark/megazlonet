using System;
using System.ComponentModel.DataAnnotations;

namespace megazlo.Models {
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
}