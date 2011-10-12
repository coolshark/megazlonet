namespace XCaptcha
{
    using System;

    public interface IEncryptionInfo
    {
        string InitialVector { get; }

        int PasswordIterations { get; }

        string Salt { get; }
    }
}

