using System.Security.Cryptography;

namespace SuperCroods.Skeleton.Security.Cipher
{
    public class RNGCipherProvider : ICipherProvider
    {
        public byte[] GetCryptoBytes(byte[] arg)
        {
            RNGCryptoServiceProvider.Create().GetBytes(arg);
            return arg;
        }
    }
}
