using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
}