namespace ExpenseTracker.Core.Entities
{
    public class UserEntity
    {
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNo { get; set; } = null!;
        public byte[] ProfilePicturePath { get; set; } = new byte[0];
    }
}
