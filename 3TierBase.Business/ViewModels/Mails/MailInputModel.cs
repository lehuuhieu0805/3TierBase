namespace _3TierBase.Business.ViewModels.Mails;

public class MailInputModel
{
    public required string ToEmail { get; set; }
    public required string Subject { get; set; }
    public required string BodyHtml { get; set; }
}