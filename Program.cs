using System.Net;
using System.Net.Mail;

namespace Examples.SmtpExamples.Async
{
    class Program
    {
        static void Main(string[] args)
        {
            var email = SmtpService.CreateMail(
                "FromName",
                "FromEmail@gmail.com",
                "ToEmail@outlook.com",
                "Subject",
                "Body msg text");

            SmtpService.SendMail(
                "smtp.gmail.com",
                587, 
                "FromEmail@gmail.com",
                "app pass/key",
                email);
        }

        public class SmtpService
        {
            public static MailMessage CreateMail(string name, string from, string to, string subject, string body)
            {
                var emailFrom = new MailAddress(from, name);
                var emailTo = new MailAddress(to);
                var emailMessage = new MailMessage(emailFrom, emailTo);

                emailMessage.Subject = subject;
                emailMessage.Body = body;
                emailMessage.IsBodyHtml = true;

                return emailMessage;
            }

            public static void SendMail(string host, int smptPort, string emailFrom, string pass, MailMessage msg)
            {
                var smtpClient = new SmtpClient(host, smptPort);
                smtpClient.Credentials = new NetworkCredential(emailFrom, pass);
                smtpClient.EnableSsl = true;

                smtpClient.Send(msg);
            }
        }
    }
}