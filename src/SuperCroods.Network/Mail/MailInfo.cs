namespace SuperCroods.Network.Mail
{
    public class MailInfo
    {
        public string Subject { get; set; }
        public string From { get; set; }
        public string FromDisplay { get; set; }
        public string To { get; set; }
        public string ToDisplay { get; set; }
        public string Message { get; set; }
        public bool IsHtml { get; set; }
    }
}
