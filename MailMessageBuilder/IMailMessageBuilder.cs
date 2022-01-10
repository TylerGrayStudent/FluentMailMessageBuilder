using System.IO;
using System.Net.Mail;

namespace MailMessageBuilder
{
    public interface IMailMessageBuilder
    {
        IMailMessageBuilder From(string from);
        IMailMessageBuilder To(params string[] to);
        IMailMessageBuilder Cc(params string[] cc);
        IMailMessageBuilder Bcc(params string[] bcc);
        IMailMessageBuilder Subject(string subject);
        IMailMessageBuilder Body(string body);
        IMailMessageBuilder HtmlBody(string htmlBody);
        IMailMessageBuilder Attach(string filePath);
        IMailMessageBuilder Attach(byte[] file, string fileName);
        IMailMessageBuilder Attach(Stream stream, string fileName);
        MailMessage Build();
    }
}