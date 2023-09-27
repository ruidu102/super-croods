namespace SuperCroods.Skeleton.Security.Cipher
{
    public interface ICipherProvider
    {
        byte[] GetCryptoBytes(byte[] arg);
    }
}
