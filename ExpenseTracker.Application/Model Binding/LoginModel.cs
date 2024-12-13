namespace ExpenseTracker.Application.Model_Binding
{
    public class LoginModel
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // Parameterless constructor
        public LoginModel() { }
    }


}
