using Hospital_Management.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Hospital_Management.Services
{
    public class EmailServices : IEmailservice
    {
        private readonly IConfiguration _config;
        public EmailServices(IConfiguration configuration)
        {
            _config = configuration;
        }
        public async Task<bool> SendEmail(string toEmail, string subject, string body)
        {
            var fromEmail = _config["EmailSettings:FromEmail"];
            var fromPassword = _config["EmailSettings:password"];

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromEmail, fromPassword),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            try
            {
                mailMessage.To.Add(toEmail);
                await smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
