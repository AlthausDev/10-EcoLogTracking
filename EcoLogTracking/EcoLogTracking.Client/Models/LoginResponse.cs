namespace EcoLogTracking.Client.Models
{
    public class LoginResponse
    {
        public User User { get; set; }
        public string Token { get; set; }

        public LoginResponse()
        {
        }

        public LoginResponse(User user, string token)
        {
            User = user;
            Token = token;
        }
    }
}
