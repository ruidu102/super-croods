namespace SuperCroods.Skeleton.Security.Cipher
{
    public class CipherSize
    {
        private CipherSize(int size)
        {
            this.Value = size;
        }

        public int Value { get; }

        public static CipherSize MAXIMUM = new CipherSize(256);

        public static CipherSize MEDIUM = new CipherSize(128);

        public static CipherSize MIMIMUM = new CipherSize(64);

    }
}
