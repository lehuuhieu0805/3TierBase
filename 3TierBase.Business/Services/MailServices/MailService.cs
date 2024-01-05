using _3TierBase.Business.ViewModels.Mails;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace _3TierBase.Business.Services.MailServices
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(MailInputModel mailInputModel)
        {
            MailMessage message = new();
            SmtpClient smtp = new();
            message.From = new MailAddress(_configuration["MailSettings:Mail"], _configuration["MailSettings:DisplayName"]);
            message.To.Add(new MailAddress(mailInputModel.ToEmail));
            message.Subject = mailInputModel.Subject;
            message.IsBodyHtml = true;
            message.Body = mailInputModel.BodyHtml;
            smtp.Port = Convert.ToInt32(_configuration["MailSettings:Port"]);
            smtp.Host = _configuration["MailSettings:Host"];
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_configuration["MailSettings:Mail"], _configuration["MailSettings:Password"]);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }
    }
}