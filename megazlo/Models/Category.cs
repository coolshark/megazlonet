using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using R = megazlo.Res.Model.Category;

namespace megazlo.Models {
	public class Category {
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessageResourceName = "R_Name_E", ErrorMessageResourceType = typeof(R))]
		[Display(Name = "Title", ResourceType = typeof(R))]
		public string Title { get; set; }
		[Display(Name = "Por", ResourceType = typeof(R))]
		public int Por { get; set; }
		public virtual ICollection<Post> Posts { get; set; }
	}
}