using _3TierBase.Business.ViewModels.Mails;

namespace _3TierBase.Business.Services.MailServices
{
    public interface IMailService
    {
        public void SendEmail(MailInputModel mailInputModel);
    }
}