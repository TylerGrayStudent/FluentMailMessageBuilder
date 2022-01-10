using System.IO;
using System.Net.Mail;
using FluentAssertions;
using Xunit;

namespace FluentMailMessageBuilderTests;

    public class UnitTests
    {
        private const string To = "to@email";
        private const string From = "from@email";
        private const string Subject = "subject";
        private const string Body = "body";
        public readonly string HtmlBody = "<p>html body</p>";
        private const string Cc = "cc@email";
        private const string Bcc = "bcc@email";

        [Fact]
        public void CreatesAnEmptyMailMessage()
        {
            var mailMessage = new FluentMailMessageBuilder.MailMessageBuilder().Build();
            var expected = new MailMessage();
            mailMessage.Should().BeEquivalentTo(expected);
        }
        
        [Fact]
        public void CreatesAMailMessageWithToAndSend()
        {
            var mailMessage = new FluentMailMessageBuilder.MailMessageBuilder().To(To).From(From)
                .Build();
            var expected = new MailMessage
            {
                To =  { new MailAddress(To) },
                From = new MailAddress(From)
            };
            mailMessage.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void CreatesAMailMessageWithToAndSendAndSubject()
        {

            var mailMessage = new FluentMailMessageBuilder.MailMessageBuilder().To(To).From(From).Subject(Subject)
                .Build();
            var expected = new MailMessage
            {
                To = {new MailAddress(To)},
                From = new MailAddress(From),
                Subject = Subject
            };
            mailMessage.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void CreatesAFullyInflatedMailMessageWithRegularBody()
        {
            var mailMessage = new FluentMailMessageBuilder.MailMessageBuilder().To(To).From(From).Subject(Subject)
                .Body(Body).Cc(Cc).Bcc(Bcc).Build();
            var expected = new MailMessage
            {
                To = {new MailAddress(To)},
                From = new MailAddress(From),
                Subject = Subject,
                CC = {new MailAddress(Cc)},
                Bcc = {new MailAddress(Bcc)},
                Body = Body,
            };
            mailMessage.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void CreatesAMailMessageWithHtmlBody()
        {
            var mailMessage = new FluentMailMessageBuilder.MailMessageBuilder().HtmlBody(HtmlBody).Build();
            var expected = new MailMessage
            {
                Body = HtmlBody,
                IsBodyHtml = true
            };
            mailMessage.Should().BeEquivalentTo(expected);
        }
        
        [Fact]
        public void CreatesAMailMessageWithMultipleRecipients()
        {
            var mailMessage = new FluentMailMessageBuilder.MailMessageBuilder().To(To, Cc, Bcc).Cc(To, Cc, Bcc)
                .Bcc(To, Cc, Bcc).Build();
            var expected = new MailMessage
            {
                To = {new MailAddress(To), new MailAddress(Cc), new MailAddress(Bcc)},
                CC = {new MailAddress(To), new MailAddress(Cc), new MailAddress(Bcc)},
                Bcc = {new MailAddress(To), new MailAddress(Cc), new MailAddress(Bcc)},
            };
            mailMessage.Should().BeEquivalentTo(expected);
        }

        
        //[Fact]
        public void CreatesAMailMessageWithAttachmentsFromPath()
        {
            var mailMessage = new FluentMailMessageBuilder.MailMessageBuilder().Attach("file.txt").Build();
            var expected = new MailMessage
            {
                Attachments = {new Attachment("file.txt")}
            };
            mailMessage.Should().BeEquivalentTo(expected);
        }

        // Currently Fails. need to figure out how to get the test working with the file correctly.
        //[Fact]
        public void CreatesAMailMessageWithAttachmentsFromFileBytes()
        {
            var fileStream1 = File.Open("file.txt", FileMode.Open);
            var fileBytes = Helpers.ReadFully(fileStream1);
            fileStream1.Close();
            var fileStream2 = File.Open("file.txt", FileMode.Open);
            var mailMessage = new FluentMailMessageBuilder.MailMessageBuilder().Attach(fileBytes, "file.txt").Build();
            var expected = new MailMessage
            {
                Attachments = {new Attachment(fileStream2, "file.txt")}
            };
            fileStream2.Close();
            mailMessage.Should().BeEquivalentTo(expected);
        }
    }
