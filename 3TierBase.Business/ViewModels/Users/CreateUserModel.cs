namespace _3TierBase.Business.ViewModels.Users
{
    public class CreateUserModel
    {
        public required string FullName { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Status { get; set; }
        public required DateTime CreatedAt { get; set; }
    }
}
