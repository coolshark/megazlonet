using System.ComponentModel.DataAnnotations;
using R = megazlo.Res.Model.RestorePass;

namespace megazlo.Models {
	public class RestorePass {
		[Required(ErrorMessageResourceName = "R_Name_E", ErrorMessageResourceType = typeof(R))]
		[Display(Name = "Name", ResourceType = typeof(R))]
		[StringLength(50, MinimumLength = 3, ErrorMessageResourceName = "SL_Name_E", ErrorMessageResourceType = typeof(R))]
		public string Name { get; set; }
	}
}