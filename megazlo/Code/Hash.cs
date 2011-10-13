using System;
using System.Security.Cryptography;
using System.Text;

namespace megazlo.Code {
	public static class Hash {

		public static string CreateHash(string input) {
			byte[] data = MD5.Create().ComputeHash(Encoding.Default.GetBytes(input));
			StringBuilder sBuilder = new StringBuilder();
			for (int i = 0; i < data.Length; i++)
				sBuilder.Append(data[i].ToString("x2"));
			return sBuilder.ToString();
		}
	}
}