namespace SuperCroods.Skeleton.Security.Cipher
{
    public class RijndaelSymmetricCipherParameter : ICipherParameter
    {
        public CipherSize CipherSize { get; set; }
        public string Plaintext { get; set; }
    }
}
