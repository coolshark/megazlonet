using System;
using System.Runtime.CompilerServices;

namespace XCaptcha
{
	public class CaptchaResult
	{
		public CaptchaResult(string solution, byte[] image)
		{
			this.Solution = solution;
			this.Image = image;
		}

		public string ContentType
		{
			get
			{
				return "image/gif";
			}
		}

		public byte[] Image { get; private set; }

		public string Solution { get; private set; }
	}
}

