namespace XCaptcha
{
    using System;
    using System.Runtime.InteropServices;

    public interface IRandomTextGenerator
    {
        string Create(int size, bool lowerCase);
        string CreateRandomEncrypedText(string secretKey, int size, bool lowercase = false);

        IEncryptionProvider EncryptionProvider { get; }
    }
}

