using System.Linq;
using System.Web.Mvc;

namespace XCaptcha.Web.Mvc {
	public class DecryptSolutionAttribute : ActionFilterAttribute {
		public string SecretKey {
			get;
			private set;
		}

		public DecryptSolutionAttribute(string secretKey) {
			SecretKey = secretKey;
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext) {
			if (filterContext.HttpContext.Request.QueryString.Count > 0) {
				var encrypedSolution = filterContext.HttpContext.Request.QueryString[0];
				var encryptionProvider = new EncryptionProvider();
				var solution = encryptionProvider.Decrypt(encrypedSolution, SecretKey);
				filterContext.ActionParameters[filterContext.ActionParameters.Keys.First()] = solution;
			}
		}
	}
}