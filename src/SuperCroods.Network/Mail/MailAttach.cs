namespace SuperCroods.Network.Mail
{
    public class MailAttach
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public int Size { get; set; }

        public virtual bool IsNull => false;
    }
}
