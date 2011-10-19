using System.Net;
using System.Net.Mail;

namespace megazlo.Code {
	public static class Mail {
		public static bool Send(string toEml, string pass) {
			MailMessage mail = new MailMessage();
			mail.From = new MailAddress(Sets.EmailAccount);

			mail.Subject = "Востановление пароля на " + Sets.SiteName;
			mail.Body = "Вы получили это письмо. потому что кто-то на сайте " + Sets.SiteName +
				" отправил запрос на востановление.\r\n\r\n"
				+ "Ваш новый пароль: " + pass + "\r\nВы можете его поменять в настройках аккаунта\r\n\r\n" +
				"С уважением администрация сайта.";
			mail.IsBodyHtml = false;

			SmtpClient client = new SmtpClient(Sets.SmtpServer);
			client.UseDefaultCredentials = false;
			client.EnableSsl = true;
			client.Port = 25;
			client.Credentials = new NetworkCredential(Sets.EmailAccount, Sets.EmailPassword);
			try {
				client.Send(mail);
				return true;
			} catch {
				return false;
			}
		}
	}
}