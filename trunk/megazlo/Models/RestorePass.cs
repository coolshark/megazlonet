using System.ComponentModel.DataAnnotations;
using R = megazlo.Res.Model.RestorePass;

namespace megazlo.Models {
	public class RestorePass {
		[Required]
		[Display(Name = "Name", ResourceType = typeof(R))]
		[StringLength(50, MinimumLength = 3)]
		public string Name { get; set; }
	}
}