using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace XCaptcha
{
	public class RandomTextGenerator : IRandomTextGenerator
	{
		public RandomTextGenerator()
			: this(new XCaptcha.EncryptionProvider())
		{
		}

		public RandomTextGenerator(IEncryptionProvider encryptionProvider)
		{
			this.EncryptionProvider = encryptionProvider;
		}

		public string Create(int size, bool lowerCase)
		{
			StringBuilder builder = new StringBuilder();
			Random random = new Random();
			for (int i = 0; i < size; i++)
			{
				char ch = Convert.ToChar(Convert.ToInt32(Math.Floor((double)((26.0 * random.NextDouble()) + 65.0))));
				builder.Append(ch);
			}
			if (!lowerCase)
			{
				return builder.ToString();
			}
			return builder.ToString().ToLower();
		}

		public string CreateRandomEncrypedText(string secretKey, int size, bool lowercase = false)
		{
			string text = this.Create(size, lowercase);
			return this.EncryptionProvider.Encrypt(text, secretKey);
		}

		public IEncryptionProvider EncryptionProvider { get; protected set; }
	}
}

