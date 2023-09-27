namespace SuperCroods.Network.SecureFTP
{
    public class SecureFTPInfo
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Path { get; set; }
        public SecureFTPCredential Credential { get; set; }
    }
}
