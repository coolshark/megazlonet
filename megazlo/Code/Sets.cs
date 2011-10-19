using System.Configuration;
using System.Web.Configuration;

namespace megazlo.Code {
	public static class Sets {
		private static KeyValueConfigurationCollection vl;

		public static string EmailAccount { get { return vl["emailAccount"].Value.ToString(); } }
		public static string EmailPassword { get { return vl["emailPassword"].Value.ToString(); } }
		public static string SmtpServer { get { return vl["smtpServer"].Value.ToString(); } }
		public static string SiteName { get { return vl["siteName"].Value.ToString(); } }

		static Sets() {
			vl = WebConfigurationManager.OpenWebConfiguration("~/Web.config").AppSettings.Settings;
		}
	}
}