using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace XCaptcha
{
	public class EncryptionProvider : IEncryptionProvider
	{
		public EncryptionProvider()
			: this(new DefaultEncryptionInfo())
		{
		}

		public EncryptionProvider(IEncryptionInfo encryptionInfo)
		{
			this.EncryptionInfo = encryptionInfo;
		}

		public string Decrypt(string encryptedText, string secretKey)
		{
			return Decrypt(encryptedText, secretKey, this.EncryptionInfo.Salt, this.EncryptionInfo.PasswordIterations, this.EncryptionInfo.InitialVector, KeySize.Is128);
		}

		private static string Decrypt(string cipherText, string passPhrase, string saltValue, int passwordIterations, string initVector, KeySize keySize)
		{
			byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
			byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
			byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
			byte[] keyBytes = new Rfc2898DeriveBytes(passPhrase, saltValueBytes, passwordIterations).GetBytes((int)((int)keySize / 8));
			ICryptoTransform decryptor = new RijndaelManaged { Mode = CipherMode.CBC }.CreateDecryptor(keyBytes, initVectorBytes);
			MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
			CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
			byte[] plainTextBytes = new byte[cipherTextBytes.Length];
			int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
			memoryStream.Close();
			cryptoStream.Close();
			return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
		}

		public string Encrypt(string plainText, string secretKey)
		{
			return Encrypt(plainText, secretKey, this.EncryptionInfo.Salt, this.EncryptionInfo.PasswordIterations, this.EncryptionInfo.InitialVector, KeySize.Is128);
		}

		private static string Encrypt(string plainText, string passPhrase, string saltValue, int passwordIterations, string initVector, KeySize keySize)
		{
			byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
			byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
			byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
			byte[] keyBytes = new Rfc2898DeriveBytes(passPhrase, saltValueBytes, passwordIterations).GetBytes((int)((int)keySize / 8));
			ICryptoTransform encryptor = new RijndaelManaged { Mode = CipherMode.CBC }.CreateEncryptor(keyBytes, initVectorBytes);
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
			cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
			cryptoStream.FlushFinalBlock();
			byte[] cipherTextBytes = memoryStream.ToArray();
			memoryStream.Close();
			cryptoStream.Close();
			return Convert.ToBase64String(cipherTextBytes);
		}

		public IEncryptionInfo EncryptionInfo { get; protected set; }
	}
}

