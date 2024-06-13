using EcoLogTracking.Server.Models;

namespace EcoLogTracking.Server.Repository.Interfaces
{
    public interface IUserRepository
    {
        public User GetUserByUsernameAndPass(string user, string pass);

        public User GetUserByUsername(string user);
    }
}
