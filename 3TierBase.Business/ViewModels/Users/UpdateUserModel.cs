namespace _3TierBase.Business.ViewModels.Users
{
    public class UpdateUserModel
    {
        public Guid Id { get; set; }

        public string? FullName { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Status { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
