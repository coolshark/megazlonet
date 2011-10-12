namespace XCaptcha
{
    using System;

    public interface IEncryptionProvider
    {
        string Decrypt(string encryptedText, string secretKey);
        string Encrypt(string plainText, string secretKey);

        IEncryptionInfo EncryptionInfo { get; }
    }
}

