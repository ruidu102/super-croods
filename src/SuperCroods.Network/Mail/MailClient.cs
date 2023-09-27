using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;

namespace SuperCroods.Network.Mail
{
    public class MailClient : ISmtpCommand
    {
        private SmtpInfo smtp;
        private MailInfo mail;
        private MailAttach attach = new NullMailAttach();        

        public MailClient(SmtpInfo smtp, MailInfo mail)
        {
            this.smtp = smtp;
            this.mail = mail;
        }

        public void Execute()
        {
            Send();
        }

        public MailClient Attachment(MailAttach arg)
        {
            attach = arg;
            return this;
        }

        private void Send()
        {
            SmtpClient client =
                new SmtpClient(smtp.Server, smtp.Port);
            MailMessage message = GetMailMessage();
            if (!attach.IsNull)
                message.Attachments.Add(GetMailAttachment());
            try
            {
                client.Send(message);
            }
            catch
            {
                Console.Write(new SmtpException("Fail to send email, please check settings").ToString());
            }
            finally
            {
                client.Dispose();
            }
        }

        private MailMessage GetMailMessage()
        {
            var result = new MailMessage();
            result.Subject = mail.Subject;
            result.From = new MailAddress(mail.From, mail.FromDisplay);
            foreach (var to in mail.To.Split(new [] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                result.To.Add(new MailAddress(to));
            result.Body = mail.Message;
            result.IsBodyHtml = mail.IsHtml;

            return result;
        }

        private Attachment GetMailAttachment()
        {
            byte[] bytes = File.ReadAllBytes(attach.Path);
            var result =
                new Attachment(new MemoryStream(bytes),
                    attach.Name,
                    MediaTypeNames.Application.Octet);
            ContentDisposition disposition = result.ContentDisposition;
            disposition.CreationDate = File.GetCreationTime(attach.Path);
            disposition.ModificationDate = File.GetLastWriteTime(attach.Path);
            disposition.ReadDate = File.GetLastAccessTime(attach.Path);

            return result;
        }

    }
}
