using System.Web;

namespace XCaptcha.Web.Mvc
{
	public static class RandomTextGeneratorExtensions
	{

		public static string CreateRandomUrlEncodedEncrypedText(this IRandomTextGenerator randomTextGenerator, string secretKey, int size, bool lowercase = false)
		{
			var text = randomTextGenerator.CreateRandomEncrypedText(secretKey, size, lowercase);
			return HttpUtility.UrlEncode(text);
		}

	}
}