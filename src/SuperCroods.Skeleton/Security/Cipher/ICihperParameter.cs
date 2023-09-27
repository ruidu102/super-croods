namespace SuperCroods.Skeleton.Security.Cipher
{
    public interface ICipherParameter
    {
        CipherSize CipherSize { get; set; }
        string Plaintext { get; set; }
    }
}
