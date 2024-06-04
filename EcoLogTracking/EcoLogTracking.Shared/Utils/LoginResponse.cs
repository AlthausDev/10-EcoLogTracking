using EcoLogTracking.Shared.Models;

namespace EcoLogTracking.Shared.Utils
{
    public class LoginResponse
    {
        public User User { get; set; }
        public string Token { get; set; }
    }

}
