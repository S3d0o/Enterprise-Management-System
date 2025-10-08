using Demo.DataAccess.Models.Shared;
using System.Data;
using System.Net;
using System.Net.Mail;

namespace Demo.BusinessLogic.Services.EmailSender
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587); // Enable SSL, TLS for Gmail
            client.EnableSsl = true; // for both SSL and TLS
            client.Credentials = new NetworkCredential("S3d0o.xn@gmail.com", "btqzsthathjrsvvi");
            using var message = new MailMessage(); // [using] => to dispose it after use
            message.From = new MailAddress("S3d0o.xn@gmail.com");
            message.To.Add(new MailAddress(email.To));
            message.Subject = email.Subject;
            message.Body = email.Body;
            message.IsBodyHtml = true; // to send html content

            client.Send(message);
        }
    }
}