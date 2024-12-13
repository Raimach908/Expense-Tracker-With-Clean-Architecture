
namespace ExpenseTracker.Application.Model_Binding
{
    public class RegisterModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNo { get; set; }

        // Parameterless constructor
        public RegisterModel() { }
    }

}
