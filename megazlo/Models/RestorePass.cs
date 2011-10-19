using System.ComponentModel.DataAnnotations;

namespace megazlo.Models {
	public class RestorePass {
		[Required]
		[Display(Name = "Введите логин для востановления")]
		[StringLength(50, MinimumLength = 3)]
		public string Name { get; set; }
	}
}