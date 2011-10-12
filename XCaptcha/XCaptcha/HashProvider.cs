namespace XCaptcha
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;

    public abstract class HashProvider : IDisposable
    {
        protected HashProvider(System.Security.Cryptography.HMAC hmac)
        {
            this.HMAC = hmac;
        }

        public abstract string Create(string solution, string secretKey);
        public void Dispose()
        {
            this.HMAC.Clear();
        }

        public abstract bool Verify(string hashedSolution, string attempt, string secretKey);

        public System.Security.Cryptography.HMAC HMAC { get; set; }
    }
}

