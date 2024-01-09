namespace _3TierBase.Business.Services.SendSms
{
    public interface ISmsService
    {
        public void SendASMSVerificationCode(string receiverPhoneNumber);
        public bool CheckAVerificationCode(string receiverPhoneNumber, string code);
    }
}
