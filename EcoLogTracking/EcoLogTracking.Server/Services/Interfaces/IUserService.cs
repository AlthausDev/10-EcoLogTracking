using EcoLogTracking.Server.Models;

namespace EcoLogTracking.Server.Services.Interfaces
{
    public interface IUserService
    {
        public User GetUserByUsernameAndPass(string user, string pass);

        public User GetUserByUsername(string user);
    }

}
