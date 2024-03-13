namespace WebApplication4.Models
{
    public class NullUserAuth : UserAuth
    {
        public NullUserAuth()
        {
            Id = -1;
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            DateOfBirth = DateTime.MinValue; 
            LastLogin = DateTime.MinValue;
            FailedLoginAttempts = 0;
        }

        public string Password
        {
            get { return string.Empty; } 
            set { _password = string.Empty; }
        }
    }
}
