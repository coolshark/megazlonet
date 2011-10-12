using System;
using System.Runtime.CompilerServices;

namespace XCaptcha
{
	public class DefaultEncryptionInfo : IEncryptionInfo
	{
		public DefaultEncryptionInfo()
		{
			this.Salt = "A1445d4F6N@R8";
			this.InitialVector = "E3F3R3D4e6@6N7B8";
			this.PasswordIterations = 2;
		}

		public string InitialVector { get; private set; }

		public int PasswordIterations { get; private set; }

		public string Salt { get; private set; }
	}
}

