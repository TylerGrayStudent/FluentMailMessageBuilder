using System;
using System.IO;
using System.Net.Mail;

namespace FluentMailMessageBuilder
{
    public class MailMessageBuilder : IMailMessageBuilder
    {
        private readonly MailMessage _mailMessage;
        public MailMessageBuilder() => _mailMessage = new MailMessage();

        public IMailMessageBuilder From(string from)
        {
            _mailMessage.From = new MailAddress(from);
            return this;
        }

        public IMailMessageBuilder To(params string[] to)
        {
            foreach (var address in to)
            {
                _mailMessage.To.Add(address);
            }
            return this;
        }

        public IMailMessageBuilder Cc(params string[] cc)
        {
            foreach (var address in cc)
            {
                _mailMessage.CC.Add(address);
            }
            return this;
        }

        public IMailMessageBuilder Bcc(params string[] bcc)
        {
            foreach (var address in bcc)
            {
                _mailMessage.Bcc.Add(address);
            }
            return this;
        }

        public IMailMessageBuilder Subject(string subject)
        {
            _mailMessage.Subject = subject;
            return this;
        }

        public IMailMessageBuilder Body(string body)
        {
            _mailMessage.IsBodyHtml = false;
            _mailMessage.Body = body;
            return this;
        }

        public IMailMessageBuilder HtmlBody(string htmlBody)
        {
            _mailMessage.IsBodyHtml = true;
            _mailMessage.Body = htmlBody;
            return this;
        }

        public IMailMessageBuilder Attach(string filePath)
        {
            _mailMessage.Attachments.Add(new Attachment(filePath));
            return this;
        }

        public IMailMessageBuilder Attach(byte[] file, string fileName)
        {
            _mailMessage.Attachments.Add(new Attachment(new MemoryStream(file), fileName));
            return this;
        }

        public IMailMessageBuilder Attach(Stream stream, string fileName)
        {
            _mailMessage.Attachments.Add(new Attachment(stream, fileName));
            return this;
        }

        public MailMessage Build()
        {
            return _mailMessage;
        }
    }
}